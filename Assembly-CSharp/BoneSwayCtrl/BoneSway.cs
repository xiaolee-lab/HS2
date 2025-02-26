using System;
using System.Collections.Generic;
using System.Linq;
using DeepCopy;
using IllusionUtility.SetUtility;
using UnityEngine;

namespace BoneSwayCtrl
{
	// Token: 0x0200114B RID: 4427
	public class BoneSway
	{
		// Token: 0x0600923D RID: 37437 RVA: 0x003CA67C File Offset: 0x003C8A7C
		public BoneSway()
		{
			for (int i = 0; i < this.m_nObjCalcNum; i++)
			{
				this.m_listObjCalc.Add(new GameObject());
				this.m_listObjCalc[i].name = "HSecneCalc" + i.ToString();
				this.m_listObjCalc[i].hideFlags = HideFlags.HideInHierarchy;
			}
		}

		// Token: 0x0600923E RID: 37438 RVA: 0x003CA744 File Offset: 0x003C8B44
		protected override void Finalize()
		{
			try
			{
				foreach (GameObject obj in this.m_listObjCalc)
				{
					UnityEngine.Object.Destroy(obj);
				}
				this.m_listObjCalc.Clear();
				this.Release();
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600923F RID: 37439 RVA: 0x003CA7CC File Offset: 0x003C8BCC
		public bool Init(List<List<Transform>> _llTrans, CSwayParam _Param, bool _bLR)
		{
			bool flag = true;
			this.m_bLR = _bLR;
			this.m_nNumBone = _llTrans.Count;
			for (int i = 0; i < this.m_nNumBone; i++)
			{
				this.m_listBone.Add(new CBoneData());
				this.m_listSpringCtrl.Add(new SpringCtrl());
				this.m_listPos.Add(default(Vector3));
				this.m_listRot.Add(default(Vector3));
				this.m_listOldWorldPos.Add(default(Vector3));
				if (_llTrans[i][0] != null)
				{
					flag &= this.setFrameInfo(this.m_listBone[i].Bone, _llTrans[i][0]);
				}
				if (_llTrans[i][1] != null)
				{
					flag &= this.setFrameInfo(this.m_listBone[i].Reference, _llTrans[i][1]);
				}
				this.m_listBone[i].nNumLocater = _llTrans[i].Count - 2;
				for (int j = 2; j < this.m_listBone[i].nNumLocater + 2; j++)
				{
					this.m_listBone[i].listLocater.Add(new CFrameInfo());
					if (!(_llTrans[i][j] == null))
					{
						flag &= this.setFrameInfo(this.m_listBone[i].listLocater[j], _llTrans[i][j]);
					}
				}
				_Param.listDetail.Add(new CSwayParamDetail());
				flag &= this.m_listSpringCtrl[i].Init(_Param.listDetail[i].fMass, _Param.listDetail[i].fTension, _Param.listDetail[i].fShear, _Param.listDetail[i].fAttenuation, _Param.listDetail[i].fGravity, _Param.listDetail[i].fDrag, 2U, 1U);
				if (flag)
				{
					this.m_listPos[i] = this.m_listSpringCtrl[i].listPoint[0].vPos;
					this.m_listSpringCtrl[i].listPoint[0].bLock = true;
					this.setParamAll(_Param);
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06009240 RID: 37440 RVA: 0x003CAA75 File Offset: 0x003C8E75
		public bool Release()
		{
			this.MemberInit();
			return true;
		}

		// Token: 0x06009241 RID: 37441 RVA: 0x003CAA80 File Offset: 0x003C8E80
		public bool CatchProc(Transform _transRef, float _fmx, float _fmy, int _nBoneIdx)
		{
			bool flag = true;
			FakeTransform fakeTransform = new FakeTransform();
			flag &= this.CalcBlendMatrixT(fakeTransform, this.m_listBone[_nBoneIdx], true, this.m_Param.listDetail[_nBoneIdx]);
			Vector3 vector = new Vector3(_fmx, _fmy, 0f);
			vector = _transRef.TransformPoint(vector);
			this.m_listObjCalc[0].transform.Identity();
			this.m_listObjCalc[0].transform.position = fakeTransform.Pos;
			this.m_listObjCalc[0].transform.rotation = fakeTransform.Rot;
			this.m_listObjCalc[0].transform.localScale = fakeTransform.Scale;
			Vector3 b = this.m_listObjCalc[0].transform.InverseTransformVector(_transRef.position);
			vector = this.m_listObjCalc[0].transform.InverseTransformVector(vector);
			vector -= b;
			this.m_listSpringCtrl[_nBoneIdx].listPoint[0].vPos += vector;
			return flag & this.MoveLimit(_nBoneIdx);
		}

		// Token: 0x06009242 RID: 37442 RVA: 0x003CABB4 File Offset: 0x003C8FB4
		public bool AutoProc(float _ftime, bool _bCatch, CBoneBlend _Blend, int _nBoneIdx)
		{
			bool flag = true;
			FakeTransform fakeTransform = new FakeTransform();
			flag &= this.CalcBlendMatrixT(fakeTransform, this.m_listBone[_nBoneIdx], true, this.m_Param.listDetail[_nBoneIdx]);
			this.m_listObjCalc[0].transform.position = fakeTransform.Pos;
			this.m_listObjCalc[0].transform.rotation = fakeTransform.Rot;
			flag &= this.CalcForce(this.m_listObjCalc[0].transform, _nBoneIdx);
			if (_bCatch && (int)this.getBoneCatch() == _nBoneIdx)
			{
				this.m_listSpringCtrl[_nBoneIdx].ResetAllForce();
			}
			else
			{
				flag &= this.m_listSpringCtrl[_nBoneIdx].UpdateEulerMethod(this.m_listObjCalc[0].transform, _ftime);
				flag &= this.MoveLimit(_nBoneIdx);
			}
			flag &= this.CalcRot(_nBoneIdx);
			BoneSway.ResultMatrixFunc[] array = new BoneSway.ResultMatrixFunc[]
			{
				new BoneSway.ResultMatrixFunc(this.ResultMatrixProgram),
				new BoneSway.ResultMatrixFunc(this.ResultMatrixLocater),
				new BoneSway.ResultMatrixFunc(this.ResultMatrixBone)
			};
			if (_Blend.bBlend)
			{
				byte nCalcKind = _Blend.pNowParam.listDetail[_nBoneIdx].Calc.nCalcKind;
				byte nCalcKind2 = _Blend.pNextParam.listDetail[_nBoneIdx].Calc.nCalcKind;
				FakeTransform fakeTransform2 = new FakeTransform();
				FakeTransform fakeTransform3 = new FakeTransform();
				array[(int)nCalcKind](fakeTransform2, this.m_listBone[_nBoneIdx], _nBoneIdx);
				array[(int)nCalcKind2](fakeTransform3, this.m_listBone[_nBoneIdx], _nBoneIdx);
				this.m_listBone[_nBoneIdx].transResult.Pos = Vector3.Lerp(fakeTransform2.Pos, fakeTransform3.Pos, _Blend.fLerp);
				this.m_listBone[_nBoneIdx].transResult.Rot = Quaternion.Lerp(fakeTransform2.Rot, fakeTransform3.Rot, _Blend.fLerp);
				this.m_listBone[_nBoneIdx].transResult.Scale = Vector3.Lerp(fakeTransform2.Scale, fakeTransform3.Scale, _Blend.fLerp);
			}
			else
			{
				FakeTransform fakeTransform4 = new FakeTransform();
				array[(int)this.m_listBone[_nBoneIdx].nCalcKind](fakeTransform4, this.m_listBone[_nBoneIdx], _nBoneIdx);
				this.m_listBone[_nBoneIdx].transResult = (FakeTransform)fakeTransform4.DeepCopy();
			}
			this.m_listSpringCtrl[_nBoneIdx].setForce = Vector3.zero;
			return true;
		}

		// Token: 0x06009243 RID: 37443 RVA: 0x003CAE6C File Offset: 0x003C926C
		private bool CalcBlendMatrixT(FakeTransform _ftransBlend, CBoneData _Bone, bool _bWorld, CSwayParamDetail _Detail)
		{
			CFrameInfo cframeInfo = _Bone.listLocater[(int)_Bone.anLocaterTIdx[0]];
			CFrameInfo cframeInfo2 = _Bone.listLocater[(int)_Bone.anLocaterTIdx[1]];
			FakeTransform fakeTransform = new FakeTransform();
			FakeTransform fakeTransform2 = new FakeTransform();
			FakeTransform fakeTransform3 = new FakeTransform();
			fakeTransform.Pos = cframeInfo.transFrame.localPosition;
			fakeTransform.Rot = cframeInfo.transFrame.localRotation;
			fakeTransform.Scale = cframeInfo.transFrame.localScale;
			fakeTransform2.Pos = cframeInfo2.transFrame.localPosition;
			fakeTransform2.Rot = cframeInfo2.transFrame.localRotation;
			fakeTransform2.Scale = cframeInfo2.transFrame.localScale;
			if (this.m_bLR)
			{
				fakeTransform3.Pos.Set(-_Detail.vAddT.x, _Detail.vAddT.y, _Detail.vAddT.z);
			}
			else
			{
				fakeTransform3.Pos.Set(_Detail.vAddT.x, _Detail.vAddT.y, _Detail.vAddT.z);
			}
			fakeTransform.Pos += fakeTransform3.Pos;
			fakeTransform2.Pos += fakeTransform3.Pos;
			if (_bWorld)
			{
				fakeTransform.Pos = cframeInfo.transParent.TransformPoint(fakeTransform.Pos);
				fakeTransform.Rot = cframeInfo.transFrame.rotation;
				fakeTransform2.Pos = cframeInfo2.transParent.TransformPoint(fakeTransform2.Pos);
				fakeTransform2.Rot = cframeInfo2.transFrame.rotation;
			}
			if (_Bone.anLocaterTIdx[0] == _Bone.anLocaterTIdx[1])
			{
				_ftransBlend = (FakeTransform)fakeTransform.DeepCopy();
				return true;
			}
			_ftransBlend.Pos = Vector3.Lerp(fakeTransform.Pos, fakeTransform2.Pos, _Bone.fLerp);
			_ftransBlend.Rot = Quaternion.Slerp(fakeTransform.Rot, fakeTransform2.Rot, _Bone.fLerp);
			_ftransBlend.Scale = Vector3.Lerp(fakeTransform.Scale, fakeTransform2.Scale, _Bone.fLerp);
			return true;
		}

		// Token: 0x06009244 RID: 37444 RVA: 0x003CB090 File Offset: 0x003C9490
		private bool CalcBlendMatrixR(FakeTransform _ftransBlend, CBoneData _Bone, bool _bWorld, CSwayParamDetail _Detail, bool _bAddRot, bool _bRot)
		{
			CFrameInfo cframeInfo = _Bone.listLocater[(int)_Bone.anLocaterRIdx[0]];
			CFrameInfo cframeInfo2 = _Bone.listLocater[(int)_Bone.anLocaterRIdx[1]];
			CFrameInfo reference = _Bone.Reference;
			FakeTransform fakeTransform = new FakeTransform();
			FakeTransform fakeTransform2 = new FakeTransform();
			FakeTransform fakeTransform3 = new FakeTransform();
			FakeTransform fakeTransform4 = new FakeTransform();
			fakeTransform.Pos = cframeInfo.transFrame.localPosition;
			fakeTransform.Rot = cframeInfo.transFrame.localRotation;
			fakeTransform.Scale = cframeInfo.transFrame.localScale;
			fakeTransform2.Pos = cframeInfo2.transFrame.localPosition;
			fakeTransform2.Rot = cframeInfo2.transFrame.localRotation;
			fakeTransform2.Scale = cframeInfo2.transFrame.localScale;
			if (this.m_bLR)
			{
				fakeTransform3.Pos.Set(-_Detail.vAddT.x, _Detail.vAddT.y, _Detail.vAddT.z);
			}
			else
			{
				fakeTransform3.Pos.Set(_Detail.vAddT.x, _Detail.vAddT.y, _Detail.vAddT.z);
			}
			fakeTransform.Pos += fakeTransform3.Pos;
			fakeTransform2.Pos += fakeTransform3.Pos;
			if (_bWorld)
			{
				fakeTransform.Pos = cframeInfo.transParent.TransformPoint(fakeTransform.Pos);
				fakeTransform.Rot = cframeInfo.transFrame.rotation;
				fakeTransform2.Pos = cframeInfo2.transParent.TransformPoint(fakeTransform2.Pos);
				fakeTransform2.Rot = cframeInfo2.transFrame.rotation;
			}
			if (_Bone.anLocaterRIdx[0] == _Bone.anLocaterRIdx[1])
			{
				if (!_bAddRot)
				{
					fakeTransform4.Rot = Quaternion.identity;
				}
				else if (this.m_bLR)
				{
					fakeTransform4.Rot = Quaternion.Euler(_Detail.vAddR.x, -_Detail.vAddR.y, _Detail.vAddR.z);
				}
				else
				{
					fakeTransform4.Rot = Quaternion.Euler(_Detail.vAddR.x, _Detail.vAddR.y, _Detail.vAddR.z);
				}
				fakeTransform4.Rot *= fakeTransform.Rot;
				fakeTransform4.Pos = fakeTransform.Pos;
				fakeTransform4.Scale = fakeTransform.Scale;
				if (!_bRot || reference.transFrame == null)
				{
					_ftransBlend = (FakeTransform)fakeTransform4.DeepCopy();
				}
				else
				{
					this.CalcAutoRotation(_ftransBlend, fakeTransform4, reference.transFrame, _Detail);
				}
				return true;
			}
			_ftransBlend.Pos = Vector3.Lerp(fakeTransform.Pos, fakeTransform2.Pos, _Bone.fLerp);
			_ftransBlend.Rot = Quaternion.Slerp(fakeTransform.Rot, fakeTransform2.Rot, _Bone.fLerp);
			_ftransBlend.Scale = Vector3.Lerp(fakeTransform.Scale, fakeTransform2.Scale, _Bone.fLerp);
			if (!_bAddRot)
			{
				fakeTransform4.Rot = Quaternion.identity;
			}
			else if (this.m_bLR)
			{
				fakeTransform4.Rot = Quaternion.Euler(_Detail.vAddR.x, -_Detail.vAddR.y, _Detail.vAddR.z);
			}
			else
			{
				fakeTransform4.Rot = Quaternion.Euler(_Detail.vAddR.x, _Detail.vAddR.y, _Detail.vAddR.z);
			}
			_ftransBlend.Rot *= fakeTransform4.Rot;
			if (_bRot && reference.transFrame != null)
			{
				this.CalcAutoRotation(_ftransBlend, _ftransBlend, reference.transFrame, _Detail);
			}
			return true;
		}

		// Token: 0x06009245 RID: 37445 RVA: 0x003CB480 File Offset: 0x003C9880
		private bool CalcAutoRotation(FakeTransform _ftransBlend, FakeTransform _ftransBase, Transform _transRef, CSwayParamDetail _Detail)
		{
			Quaternion rhs = Quaternion.identity;
			float num = 1f;
			float num2 = 1f;
			float num3 = 0f;
			float num4 = Vector3.Dot(Vector3.up, _transRef.up);
			float num5 = Vector3.Dot(Vector3.up, _transRef.forward);
			float num6 = Vector3.Dot(Vector3.up, _transRef.right);
			num4 = Mathf.Abs(num4);
			if (num5 > 0f)
			{
				num = -1f;
			}
			num5 = Mathf.Abs(num5);
			if (num6 > 0f)
			{
				num2 = -1f;
			}
			num6 = Mathf.Abs(num6);
			float num7 = Mathf.InverseLerp(1f, 0f, num4);
			float num8 = Mathf.InverseLerp(0f, 1f, num6);
			float num9 = num7 * num8;
			float num10 = _Detail.fAutoRot;
			if (num2 < 0f)
			{
				num10 *= -1f;
			}
			float num11 = Mathf.Lerp(0f, num10, num9);
			if (!_Detail.bAutoRotUp || num > 0f)
			{
				num7 = Mathf.InverseLerp(1f, 0f, num4);
				float num12 = Mathf.InverseLerp(0f, 1f, num5);
				num9 = num7 * num12;
				num10 = _Detail.fAutoRot;
				if (!((num <= 0f) ? (!this.m_bLR) : this.m_bLR))
				{
					num10 *= -1f;
				}
				num3 = Mathf.Lerp(0f, num10, num9);
			}
			num9 = num3 + num11;
			rhs = Quaternion.AngleAxis(MathfEx.ToDegree(num9), Vector3.up);
			_ftransBlend.Rot = _ftransBase.Rot * rhs;
			_ftransBlend.Pos = _ftransBase.Pos;
			_ftransBlend.Scale = _ftransBase.Scale;
			return true;
		}

		// Token: 0x06009246 RID: 37446 RVA: 0x003CB66C File Offset: 0x003C9A6C
		private bool CalcForce(Transform _transFrame, int _nIdx)
		{
			CSwayParamDetail cswayParamDetail = this.m_Param.listDetail[_nIdx];
			Vector3 vector = this.m_listOldWorldPos[_nIdx] - _transFrame.position;
			this.m_listOldWorldPos[_nIdx] = _transFrame.position;
			float sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude > Mathf.Pow(cswayParamDetail.fForceLimit, 2f))
			{
				vector = vector.normalized * cswayParamDetail.fForceLimit;
			}
			vector *= cswayParamDetail.fForceScale;
			this.m_listSpringCtrl[_nIdx].setAutoForce = _transFrame.InverseTransformDirection(vector);
			return true;
		}

		// Token: 0x06009247 RID: 37447 RVA: 0x003CB70C File Offset: 0x003C9B0C
		private bool ResultMatrixProgram(FakeTransform _ftransResult, CBoneData _Bone, int _nIdx)
		{
			BoneSway.ResultTransformFunc[] array = new BoneSway.ResultTransformFunc[]
			{
				new BoneSway.ResultTransformFunc(this.TransformRotateAndTrans),
				new BoneSway.ResultTransformFunc(this.TransformRotate),
				new BoneSway.ResultTransformFunc(this.TransformTrans)
			};
			bool flag = true;
			bool bAutoRotProc = this.m_Param.listDetail[_nIdx].bAutoRotProc;
			FakeTransform fakeTransform = new FakeTransform();
			FakeTransform fakeTransform2 = new FakeTransform();
			flag &= this.CalcBlendMatrixR(fakeTransform, _Bone, false, this.m_Param.listDetail[_nIdx], true, bAutoRotProc);
			flag &= this.CalcBlendMatrixT(fakeTransform2, _Bone, false, this.m_Param.listDetail[_nIdx]);
			return flag & array[(int)_Bone.nTransformKind](_ftransResult, _Bone, _nIdx, fakeTransform2, fakeTransform);
		}

		// Token: 0x06009248 RID: 37448 RVA: 0x003CB7C8 File Offset: 0x003C9BC8
		private bool ResultMatrixLocater(FakeTransform _ftransResult, CBoneData _Bone, int _nIdx)
		{
			bool flag = true;
			bool bAutoRotProc = this.m_Param.listDetail[_nIdx].bAutoRotProc;
			FakeTransform fakeTransform = new FakeTransform();
			FakeTransform fakeTransform2 = new FakeTransform();
			flag &= this.CalcBlendMatrixR(fakeTransform, _Bone, false, this.m_Param.listDetail[_nIdx], true, bAutoRotProc);
			flag &= this.CalcBlendMatrixT(fakeTransform2, _Bone, false, this.m_Param.listDetail[_nIdx]);
			_ftransResult.Pos = fakeTransform2.Pos;
			_ftransResult.Rot = fakeTransform.Rot;
			_ftransResult.Scale = fakeTransform.Scale;
			return true;
		}

		// Token: 0x06009249 RID: 37449 RVA: 0x003CB85C File Offset: 0x003C9C5C
		private bool ResultMatrixBone(FakeTransform _ftransResult, CBoneData _Bone, int _nIdx)
		{
			_ftransResult.Pos = _Bone.Bone.transFrame.localPosition;
			_ftransResult.Rot = _Bone.Bone.transFrame.localRotation;
			_ftransResult.Scale = _Bone.Bone.transFrame.localScale;
			return true;
		}

		// Token: 0x0600924A RID: 37450 RVA: 0x003CB8AC File Offset: 0x003C9CAC
		private bool TransformRotateAndTrans(FakeTransform _ftransResult, CBoneData _Bone, int _nIdx, FakeTransform _ftransBaseT, FakeTransform _ftransBaseR)
		{
			FakeTransform fakeTransform = new FakeTransform();
			CSwayParamDetail cswayParamDetail = this.m_Param.listDetail[_nIdx];
			Quaternion lhs = Quaternion.AngleAxis(this.m_listRot[_nIdx].x * _Bone.fScaleR, Vector3.right);
			Quaternion rhs = Quaternion.AngleAxis(this.m_listRot[_nIdx].y * _Bone.fScaleR, Vector3.up);
			fakeTransform.Rot = lhs * rhs;
			Vector3 b = new Vector3(_Bone.fScaleT, _Bone.fScaleT, _Bone.fScaleT);
			if (this.m_listSpringCtrl[_nIdx].listPoint[0].vPos.y >= 0f)
			{
				b.y = _Bone.fScaleYT;
			}
			fakeTransform.Pos = Vector3.Scale(this.m_listSpringCtrl[_nIdx].listPoint[0].vPos, b);
			fakeTransform.Pos += _ftransBaseT.Pos;
			float z;
			float num;
			float y;
			if (fakeTransform.Pos.z > 0f)
			{
				float t = Mathf.InverseLerp(0f, cswayParamDetail.vLimitMaxT.z, fakeTransform.Pos.z);
				z = Mathf.Lerp(1f, cswayParamDetail.fCrushZMax, t) * cswayParamDetail.vAddS.z;
				num = Mathf.Lerp(1f, cswayParamDetail.fCrushXYMin, t);
				y = num * cswayParamDetail.vAddS.y;
				num *= cswayParamDetail.vAddS.x;
			}
			else
			{
				float t = Mathf.InverseLerp(0f, cswayParamDetail.vLimitMinT.z, fakeTransform.Pos.z);
				z = Mathf.Lerp(1f, cswayParamDetail.fCrushZMin, t) * cswayParamDetail.vAddS.z;
				num = Mathf.Lerp(1f, cswayParamDetail.fCrushXYMax, t);
				y = num * cswayParamDetail.vAddS.y;
				num *= cswayParamDetail.vAddS.x;
			}
			_ftransResult.Rot = fakeTransform.Rot * _ftransBaseR.Rot;
			_ftransResult.Pos = fakeTransform.Pos;
			_ftransResult.Scale = Vector3.Scale(new Vector3(num, y, z), _ftransBaseR.Scale);
			return true;
		}

		// Token: 0x0600924B RID: 37451 RVA: 0x003CBB08 File Offset: 0x003C9F08
		private bool TransformRotate(FakeTransform _ftransResult, CBoneData _Bone, int _nIdx, FakeTransform _ftransBaseT, FakeTransform _ftransBaseR)
		{
			FakeTransform fakeTransform = new FakeTransform();
			CSwayParamDetail cswayParamDetail = this.m_Param.listDetail[_nIdx];
			Quaternion lhs = Quaternion.AngleAxis(this.m_listRot[_nIdx].x * _Bone.fScaleR, Vector3.right);
			Quaternion rhs = Quaternion.AngleAxis(this.m_listRot[_nIdx].y * _Bone.fScaleR, Vector3.up);
			fakeTransform.Rot = lhs * rhs;
			_ftransResult.Rot = fakeTransform.Rot * _ftransBaseR.Rot;
			_ftransResult.Pos = _ftransBaseR.Pos;
			_ftransResult.Scale = Vector3.Scale(cswayParamDetail.vAddS, _ftransBaseR.Scale);
			return true;
		}

		// Token: 0x0600924C RID: 37452 RVA: 0x003CBBC8 File Offset: 0x003C9FC8
		private bool TransformTrans(FakeTransform _ftransResult, CBoneData _Bone, int _nIdx, FakeTransform _ftransBaseT, FakeTransform _ftransBaseR)
		{
			FakeTransform fakeTransform = new FakeTransform();
			CSwayParamDetail cswayParamDetail = this.m_Param.listDetail[_nIdx];
			Vector3 b = new Vector3(_Bone.fScaleT, _Bone.fScaleT, _Bone.fScaleT);
			if (this.m_listSpringCtrl[_nIdx].listPoint[0].vPos.y >= 0f)
			{
				b.y = _Bone.fScaleYT;
			}
			fakeTransform.Pos = Vector3.Scale(this.m_listSpringCtrl[_nIdx].listPoint[0].vPos, b);
			fakeTransform.Pos += _ftransBaseT.Pos;
			float z;
			float num;
			float y;
			if (fakeTransform.Pos.z > 0f)
			{
				float t = Mathf.InverseLerp(0f, cswayParamDetail.vLimitMaxT.z, fakeTransform.Pos.z);
				z = Mathf.Lerp(1f, cswayParamDetail.fCrushZMax, t) * cswayParamDetail.vAddS.z;
				num = Mathf.Lerp(1f, cswayParamDetail.fCrushXYMin, t);
				y = num * cswayParamDetail.vAddS.y;
				num *= cswayParamDetail.vAddS.x;
			}
			else
			{
				float t = Mathf.InverseLerp(0f, cswayParamDetail.vLimitMinT.z, fakeTransform.Pos.z);
				z = Mathf.Lerp(1f, cswayParamDetail.fCrushZMin, t) * cswayParamDetail.vAddS.z;
				num = Mathf.Lerp(1f, cswayParamDetail.fCrushXYMax, t);
				y = num * cswayParamDetail.vAddS.y;
				num *= cswayParamDetail.vAddS.x;
			}
			_ftransResult.Rot = _ftransBaseT.Rot;
			_ftransResult.Pos = fakeTransform.Pos;
			_ftransResult.Scale = Vector3.Scale(new Vector3(num, y, z), _ftransBaseT.Scale);
			return true;
		}

		// Token: 0x0600924D RID: 37453 RVA: 0x003CBDB8 File Offset: 0x003CA1B8
		private bool MoveLimit(int _nIdx)
		{
			CSwayParamDetail cswayParamDetail = this.m_Param.listDetail[_nIdx];
			SpringCtrl.CSpringPoint cspringPoint = this.m_listSpringCtrl[_nIdx].listPoint[0];
			cspringPoint.vPos.x = Mathf.Clamp(cspringPoint.vPos.x, cswayParamDetail.vLimitMinT.x, cswayParamDetail.vLimitMaxT.x);
			cspringPoint.vPos.y = Mathf.Clamp(cspringPoint.vPos.y, cswayParamDetail.vLimitMinT.y, cswayParamDetail.vLimitMaxT.y);
			cspringPoint.vPos.z = Mathf.Clamp(cspringPoint.vPos.z, cswayParamDetail.vLimitMinT.z, cswayParamDetail.vLimitMaxT.z);
			return true;
		}

		// Token: 0x0600924E RID: 37454 RVA: 0x003CBE84 File Offset: 0x003CA284
		private bool CalcRot(int _nIdx)
		{
			CSwayParamDetail cswayParamDetail = this.m_Param.listDetail[_nIdx];
			SpringCtrl.CSpringPoint cspringPoint = this.m_listSpringCtrl[_nIdx].listPoint[0];
			this.m_listRot[_nIdx] = Vector3.zero;
			int num = (cspringPoint.vPos.y < 0f) ? 1 : 0;
			int num2 = (cspringPoint.vPos.x < 0f) ? 1 : 0;
			float num3 = 0f;
			float num4 = 0f;
			float[] array = new float[]
			{
				cswayParamDetail.vLimitMaxT.y,
				cswayParamDetail.vLimitMinT.y
			};
			float[] array2 = new float[]
			{
				cswayParamDetail.vLimitMaxT.x,
				cswayParamDetail.vLimitMinT.x
			};
			float[] array3 = new float[]
			{
				cswayParamDetail.vLimitMaxR.y,
				cswayParamDetail.vLimitMinR.y
			};
			float[] array4 = new float[]
			{
				cswayParamDetail.vLimitMaxR.x,
				cswayParamDetail.vLimitMinR.x
			};
			if (array[num] == 1f)
			{
				num3 = -cspringPoint.vPos.y / array[num];
			}
			if (array2[num2] == 1f)
			{
				num4 = cspringPoint.vPos.x / array2[num2];
			}
			this.m_listRot[_nIdx].Set(array4[num] * num3, array3[num2] * num4, 0f);
			return true;
		}

		// Token: 0x0600924F RID: 37455 RVA: 0x003CC010 File Offset: 0x003CA410
		private void MemberInit()
		{
			this.m_bLR = false;
			this.m_nNumBone = 0;
			this.m_listBone.Clear();
			this.m_listPos.Clear();
			this.m_listRot.Clear();
			this.m_listOldWorldPos.Clear();
			this.m_listSpringCtrl.Clear();
			foreach (GameObject obj in this.m_listObjCalc)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.m_listObjCalc.Clear();
		}

		// Token: 0x06009250 RID: 37456 RVA: 0x003CC0BC File Offset: 0x003CA4BC
		public bool setFrameInfo(CFrameInfo _Info, Transform _transFrame)
		{
			if (null == _transFrame)
			{
				return false;
			}
			_Info.transFrame = _transFrame;
			_Info.transParent = _transFrame.parent;
			return true;
		}

		// Token: 0x06009251 RID: 37457 RVA: 0x003CC0E0 File Offset: 0x003CA4E0
		public void setParamAll(CSwayParam _Param)
		{
			this.m_Param = (CSwayParam)_Param.DeepCopy();
			this.m_Param.bCalc = _Param.bCalc;
			this.m_Param.bEntry = _Param.bEntry;
			this.m_Param.fBlendTime = _Param.fBlendTime;
			this.m_Param.fMoveRate = _Param.fMoveRate;
			this.m_Param.nCatch = _Param.nCatch;
			this.m_Param.nPtn = _Param.nPtn;
			this.m_Param.strName = _Param.strName;
			this.m_Param.listDetail.Clear();
			for (int j = 0; j < _Param.listDetail.Count; j++)
			{
				this.m_Param.listDetail.Add(new CSwayParamDetail());
				this.m_Param.listDetail[j].bAutoRotProc = _Param.listDetail[j].bAutoRotProc;
				this.m_Param.listDetail[j].bAutoRotUp = _Param.listDetail[j].bAutoRotUp;
				this.m_Param.listDetail[j].fAttenuation = _Param.listDetail[j].fAttenuation;
				this.m_Param.listDetail[j].fAutoRot = _Param.listDetail[j].fAutoRot;
				this.m_Param.listDetail[j].fCrushXYMax = _Param.listDetail[j].fCrushXYMax;
				this.m_Param.listDetail[j].fCrushXYMin = _Param.listDetail[j].fCrushXYMin;
				this.m_Param.listDetail[j].fCrushZMax = _Param.listDetail[j].fCrushZMax;
				this.m_Param.listDetail[j].fCrushZMin = _Param.listDetail[j].fCrushZMin;
				this.m_Param.listDetail[j].fDrag = _Param.listDetail[j].fDrag;
				this.m_Param.listDetail[j].fForceLimit = _Param.listDetail[j].fForceLimit;
				this.m_Param.listDetail[j].fForceScale = _Param.listDetail[j].fForceScale;
				this.m_Param.listDetail[j].fGravity = _Param.listDetail[j].fGravity;
				this.m_Param.listDetail[j].fInertiaScale = _Param.listDetail[j].fInertiaScale;
				this.m_Param.listDetail[j].fMass = _Param.listDetail[j].fMass;
				this.m_Param.listDetail[j].fShear = _Param.listDetail[j].fShear;
				this.m_Param.listDetail[j].fTension = _Param.listDetail[j].fTension;
				this.m_Param.listDetail[j].vAddR = _Param.listDetail[j].vAddR;
				this.m_Param.listDetail[j].vAddS = _Param.listDetail[j].vAddS;
				this.m_Param.listDetail[j].vAddT = _Param.listDetail[j].vAddT;
				this.m_Param.listDetail[j].vLimitMaxR = _Param.listDetail[j].vLimitMaxR;
				this.m_Param.listDetail[j].vLimitMaxT = _Param.listDetail[j].vLimitMaxT;
				this.m_Param.listDetail[j].vLimitMinR = _Param.listDetail[j].vLimitMinR;
				this.m_Param.listDetail[j].vLimitMinT = _Param.listDetail[j].vLimitMinT;
				this.m_Param.listDetail[j].Calc.fScaleR = _Param.listDetail[j].Calc.fScaleR;
				this.m_Param.listDetail[j].Calc.fScaleT = _Param.listDetail[j].Calc.fScaleT;
				this.m_Param.listDetail[j].Calc.fScaleYT = _Param.listDetail[j].Calc.fScaleYT;
				this.m_Param.listDetail[j].Calc.nCalcKind = _Param.listDetail[j].Calc.nCalcKind;
				this.m_Param.listDetail[j].Calc.nLocaterRIdx = _Param.listDetail[j].Calc.nLocaterRIdx;
				this.m_Param.listDetail[j].Calc.nLocaterTIdx = _Param.listDetail[j].Calc.nLocaterTIdx;
				this.m_Param.listDetail[j].Calc.nTransformKind = _Param.listDetail[j].Calc.nTransformKind;
			}
			this.setParamMoveRate(_Param.fMoveRate);
			this.setBoneCalc(_Param.bCalc);
			this.setBoneCatch(_Param.nCatch);
			foreach (var <>__AnonType in _Param.listDetail.Select((CSwayParamDetail v, int i) => new
			{
				v,
				i
			}))
			{
				this.setParamLimitMaxT(<>__AnonType.v.vLimitMaxT, <>__AnonType.i);
				this.setParamLimitMinT(<>__AnonType.v.vLimitMinT, <>__AnonType.i);
				this.setParamLimitMaxR(<>__AnonType.v.vLimitMaxR, <>__AnonType.i);
				this.setParamLimitMinR(<>__AnonType.v.vLimitMinR, <>__AnonType.i);
				this.setParamAddR(<>__AnonType.v.vAddR, <>__AnonType.i);
				this.setParamAddT(<>__AnonType.v.vAddT, <>__AnonType.i);
				this.setParamAddS(<>__AnonType.v.vAddS, <>__AnonType.i);
				this.setParamForceScale(<>__AnonType.v.fForceScale, <>__AnonType.i);
				this.setParamForceLimit(<>__AnonType.v.fForceLimit, <>__AnonType.i);
				this.setParamCalcKind(<>__AnonType.v.Calc.nCalcKind, <>__AnonType.i);
				this.setParamTransformKind(<>__AnonType.v.Calc.nTransformKind, <>__AnonType.i);
				this.setParamScaleT(<>__AnonType.v.Calc.fScaleT, <>__AnonType.i);
				this.setParamScaleYT(<>__AnonType.v.Calc.fScaleYT, <>__AnonType.i);
				this.setParamScaleR(<>__AnonType.v.Calc.fScaleR, <>__AnonType.i);
				this.setCrushZScale(<>__AnonType.v.fCrushZMax, <>__AnonType.v.fCrushZMin, <>__AnonType.i);
				this.setCrushXYScale(<>__AnonType.v.fCrushXYMax, <>__AnonType.v.fCrushXYMin, <>__AnonType.i);
				this.setAutoRotProc(<>__AnonType.v.bAutoRotProc, <>__AnonType.i);
				this.setAutoRot(<>__AnonType.v.fAutoRot, <>__AnonType.i);
				this.setAutoRotUp(<>__AnonType.v.bAutoRotUp, <>__AnonType.i);
				this.m_listSpringCtrl[<>__AnonType.i].SetParam(<>__AnonType.v.fMass, <>__AnonType.v.fTension, <>__AnonType.v.fShear, <>__AnonType.v.fAttenuation);
			}
		}

		// Token: 0x06009252 RID: 37458 RVA: 0x003CC94C File Offset: 0x003CAD4C
		public void getParamAll(ref CSwayParam _Param)
		{
			_Param = (CSwayParam)this.m_Param.DeepCopy();
		}

		// Token: 0x06009253 RID: 37459 RVA: 0x003CC960 File Offset: 0x003CAD60
		public void setParamPtn(int _nPtn)
		{
			this.m_Param.nPtn = _nPtn;
		}

		// Token: 0x06009254 RID: 37460 RVA: 0x003CC96E File Offset: 0x003CAD6E
		public int getParamPtn()
		{
			return this.m_Param.nPtn;
		}

		// Token: 0x06009255 RID: 37461 RVA: 0x003CC97B File Offset: 0x003CAD7B
		public void setParamName(string _strName)
		{
			this.m_Param.strName = _strName;
		}

		// Token: 0x06009256 RID: 37462 RVA: 0x003CC989 File Offset: 0x003CAD89
		public string getParamName()
		{
			return this.m_Param.strName;
		}

		// Token: 0x06009257 RID: 37463 RVA: 0x003CC996 File Offset: 0x003CAD96
		public void setParamBlendTime(float _fBlendTime)
		{
			this.m_Param.fBlendTime = _fBlendTime;
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x003CC9A4 File Offset: 0x003CADA4
		public float getParamBlendTime()
		{
			return this.m_Param.fBlendTime;
		}

		// Token: 0x06009259 RID: 37465 RVA: 0x003CC9B1 File Offset: 0x003CADB1
		public void setParamMoveRate(float _fMoveRate)
		{
			this.m_Param.fMoveRate = _fMoveRate;
		}

		// Token: 0x0600925A RID: 37466 RVA: 0x003CC9BF File Offset: 0x003CADBF
		public float getParamMoveRate()
		{
			return this.m_Param.fMoveRate;
		}

		// Token: 0x0600925B RID: 37467 RVA: 0x003CC9CC File Offset: 0x003CADCC
		public void setParamLimitMaxT(Vector3 _vLimitT, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vLimitMaxT = _vLimitT;
		}

		// Token: 0x0600925C RID: 37468 RVA: 0x003CC9E5 File Offset: 0x003CADE5
		public void setParamLimitMinT(Vector3 _vLimitT, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vLimitMinT = _vLimitT;
		}

		// Token: 0x0600925D RID: 37469 RVA: 0x003CC9FE File Offset: 0x003CADFE
		public Vector3 getParamLimitMaxT(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vLimitMaxT;
		}

		// Token: 0x0600925E RID: 37470 RVA: 0x003CCA16 File Offset: 0x003CAE16
		public Vector3 getParamLimitMinT(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vLimitMinT;
		}

		// Token: 0x0600925F RID: 37471 RVA: 0x003CCA2E File Offset: 0x003CAE2E
		public void setParamLimitMaxR(Vector3 _vLimitR, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vLimitMaxR = _vLimitR;
		}

		// Token: 0x06009260 RID: 37472 RVA: 0x003CCA47 File Offset: 0x003CAE47
		public void setParamLimitMinR(Vector3 _vLimitR, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vLimitMinR = _vLimitR;
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x003CCA60 File Offset: 0x003CAE60
		public Vector3 getParamLimitMaxR(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vLimitMaxR;
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x003CCA78 File Offset: 0x003CAE78
		public Vector3 getParamLimitMinR(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vLimitMinR;
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x003CCA90 File Offset: 0x003CAE90
		public void setParamForceScale(float _fForceScale, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fForceScale = _fForceScale;
		}

		// Token: 0x06009264 RID: 37476 RVA: 0x003CCAA9 File Offset: 0x003CAEA9
		public float getParamForceScale(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fForceScale;
		}

		// Token: 0x06009265 RID: 37477 RVA: 0x003CCAC1 File Offset: 0x003CAEC1
		public void setParamForceLimit(float _fForceLimit, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fForceLimit = _fForceLimit;
		}

		// Token: 0x06009266 RID: 37478 RVA: 0x003CCADA File Offset: 0x003CAEDA
		public float getParamForceLimit(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fForceLimit;
		}

		// Token: 0x06009267 RID: 37479 RVA: 0x003CCAF2 File Offset: 0x003CAEF2
		public void setParamInertiaScale(float _fInertiaScale, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fInertiaScale = _fInertiaScale;
		}

		// Token: 0x06009268 RID: 37480 RVA: 0x003CCB0B File Offset: 0x003CAF0B
		public float getParamInertiaScale(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fInertiaScale;
		}

		// Token: 0x06009269 RID: 37481 RVA: 0x003CCB23 File Offset: 0x003CAF23
		public void setParamCalcKind(byte _nCalcKind, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.nCalcKind = _nCalcKind;
			this.m_listBone[_nIdx].nCalcKind = _nCalcKind;
		}

		// Token: 0x0600926A RID: 37482 RVA: 0x003CCB53 File Offset: 0x003CAF53
		public byte getParamCalcKind(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.nCalcKind;
		}

		// Token: 0x0600926B RID: 37483 RVA: 0x003CCB70 File Offset: 0x003CAF70
		public void setParamLocaterTIdx(byte _nLocaterIdx, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.nLocaterTIdx = _nLocaterIdx;
		}

		// Token: 0x0600926C RID: 37484 RVA: 0x003CCB8E File Offset: 0x003CAF8E
		public byte getParamLocaterTIdx(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.nLocaterTIdx;
		}

		// Token: 0x0600926D RID: 37485 RVA: 0x003CCBAB File Offset: 0x003CAFAB
		public void setParamLocaterRIdx(byte _nLocaterIdx, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.nLocaterRIdx = _nLocaterIdx;
		}

		// Token: 0x0600926E RID: 37486 RVA: 0x003CCBC9 File Offset: 0x003CAFC9
		public byte getParamLocaterRIdx(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.nLocaterRIdx;
		}

		// Token: 0x0600926F RID: 37487 RVA: 0x003CCBE6 File Offset: 0x003CAFE6
		public void setParamTransformKind(byte _nTransformKind, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.nTransformKind = _nTransformKind;
			this.m_listBone[_nIdx].nTransformKind = _nTransformKind;
		}

		// Token: 0x06009270 RID: 37488 RVA: 0x003CCC16 File Offset: 0x003CB016
		public byte getParamTransformKind(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.nTransformKind;
		}

		// Token: 0x06009271 RID: 37489 RVA: 0x003CCC33 File Offset: 0x003CB033
		public void setParamScaleT(float _fScaleT, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.fScaleT = _fScaleT;
			this.m_listBone[_nIdx].fScaleT = _fScaleT;
		}

		// Token: 0x06009272 RID: 37490 RVA: 0x003CCC63 File Offset: 0x003CB063
		public float getParamScaleT(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.fScaleT;
		}

		// Token: 0x06009273 RID: 37491 RVA: 0x003CCC80 File Offset: 0x003CB080
		public void setParamScaleYT(float _fScaleYT, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.fScaleYT = _fScaleYT;
			this.m_listBone[_nIdx].fScaleYT = _fScaleYT;
		}

		// Token: 0x06009274 RID: 37492 RVA: 0x003CCCB0 File Offset: 0x003CB0B0
		public float getParamScaleYT(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.fScaleYT;
		}

		// Token: 0x06009275 RID: 37493 RVA: 0x003CCCCD File Offset: 0x003CB0CD
		public void setParamScaleR(float _fScaleR, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].Calc.fScaleR = _fScaleR;
			this.m_listBone[_nIdx].fScaleR = _fScaleR;
		}

		// Token: 0x06009276 RID: 37494 RVA: 0x003CCCFD File Offset: 0x003CB0FD
		public float getParamScaleR(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].Calc.fScaleR;
		}

		// Token: 0x06009277 RID: 37495 RVA: 0x003CCD1A File Offset: 0x003CB11A
		public void setParamGravity(float _fGravity, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fGravity = _fGravity;
			this.m_listSpringCtrl[_nIdx].setGravity = _fGravity;
		}

		// Token: 0x06009278 RID: 37496 RVA: 0x003CCD45 File Offset: 0x003CB145
		public float getParamGravity(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fGravity;
		}

		// Token: 0x06009279 RID: 37497 RVA: 0x003CCD5D File Offset: 0x003CB15D
		public void setParamDrag(float _fDrag, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fDrag = _fDrag;
			this.m_listSpringCtrl[_nIdx].setDrag = _fDrag;
		}

		// Token: 0x0600927A RID: 37498 RVA: 0x003CCD88 File Offset: 0x003CB188
		public float getParamDrag(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fDrag;
		}

		// Token: 0x0600927B RID: 37499 RVA: 0x003CCDA0 File Offset: 0x003CB1A0
		public void setParamTension(float _fTension, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fTension = _fTension;
			this.m_listSpringCtrl[_nIdx].setTension = _fTension;
		}

		// Token: 0x0600927C RID: 37500 RVA: 0x003CCDCB File Offset: 0x003CB1CB
		public float getParamTension(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fTension;
		}

		// Token: 0x0600927D RID: 37501 RVA: 0x003CCDE3 File Offset: 0x003CB1E3
		public void setParamShear(float _fShear, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fShear = _fShear;
			this.m_listSpringCtrl[_nIdx].setShear = _fShear;
		}

		// Token: 0x0600927E RID: 37502 RVA: 0x003CCE0E File Offset: 0x003CB20E
		public float getParamShear(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fShear;
		}

		// Token: 0x0600927F RID: 37503 RVA: 0x003CCE26 File Offset: 0x003CB226
		public void setParamAttenuation(float _fAttenuation, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fAttenuation = _fAttenuation;
			this.m_listSpringCtrl[_nIdx].setAttenuation = _fAttenuation;
		}

		// Token: 0x06009280 RID: 37504 RVA: 0x003CCE51 File Offset: 0x003CB251
		public float getParamAttenuation(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fAttenuation;
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x003CCE69 File Offset: 0x003CB269
		public void setParamMass(float _fMass, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fMass = _fMass;
			this.m_listSpringCtrl[_nIdx].setMass = _fMass;
		}

		// Token: 0x06009282 RID: 37506 RVA: 0x003CCE94 File Offset: 0x003CB294
		public float getParamMass(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fMass;
		}

		// Token: 0x06009283 RID: 37507 RVA: 0x003CCEAC File Offset: 0x003CB2AC
		public void setBoneCalc(bool _bCalc)
		{
			this.m_Param.bCalc = _bCalc;
		}

		// Token: 0x06009284 RID: 37508 RVA: 0x003CCEBA File Offset: 0x003CB2BA
		public bool getBoneCalc()
		{
			return this.m_Param.bCalc;
		}

		// Token: 0x06009285 RID: 37509 RVA: 0x003CCEC7 File Offset: 0x003CB2C7
		public void setCrushZScale(float _fScaleMax, float _fScaleMin, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fCrushZMax = _fScaleMax;
			this.m_Param.listDetail[_nIdx].fCrushZMin = _fScaleMin;
		}

		// Token: 0x06009286 RID: 37510 RVA: 0x003CCEF7 File Offset: 0x003CB2F7
		public void setCrushZScaleMax(float _fScale, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fCrushZMax = _fScale;
		}

		// Token: 0x06009287 RID: 37511 RVA: 0x003CCF10 File Offset: 0x003CB310
		public void setCrushZScaleMin(float _fScale, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fCrushZMin = _fScale;
		}

		// Token: 0x06009288 RID: 37512 RVA: 0x003CCF29 File Offset: 0x003CB329
		public float getCrushZScaleMax(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fCrushZMax;
		}

		// Token: 0x06009289 RID: 37513 RVA: 0x003CCF41 File Offset: 0x003CB341
		public float getCrushZScaleMin(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fCrushZMin;
		}

		// Token: 0x0600928A RID: 37514 RVA: 0x003CCF59 File Offset: 0x003CB359
		public void setCrushXYScale(float _fScaleMax, float _fScaleMin, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fCrushXYMax = _fScaleMax;
			this.m_Param.listDetail[_nIdx].fCrushXYMin = _fScaleMin;
		}

		// Token: 0x0600928B RID: 37515 RVA: 0x003CCF89 File Offset: 0x003CB389
		public void setCrushXYScaleMax(float _fScale, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fCrushXYMax = _fScale;
		}

		// Token: 0x0600928C RID: 37516 RVA: 0x003CCFA2 File Offset: 0x003CB3A2
		public void setCrushXYScaleMin(float _fScale, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fCrushXYMin = _fScale;
		}

		// Token: 0x0600928D RID: 37517 RVA: 0x003CCFBB File Offset: 0x003CB3BB
		public float getCrushXYScaleMax(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fCrushXYMax;
		}

		// Token: 0x0600928E RID: 37518 RVA: 0x003CCFD3 File Offset: 0x003CB3D3
		public float getCrushXYScaleMin(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fCrushXYMin;
		}

		// Token: 0x0600928F RID: 37519 RVA: 0x003CCFEB File Offset: 0x003CB3EB
		public void setAutoRotProc(bool _bAuto, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].bAutoRotProc = _bAuto;
		}

		// Token: 0x06009290 RID: 37520 RVA: 0x003CD004 File Offset: 0x003CB404
		public bool getAutoRotProc(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].bAutoRotProc;
		}

		// Token: 0x06009291 RID: 37521 RVA: 0x003CD01C File Offset: 0x003CB41C
		public void setAutoRot(float _fRot, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].fAutoRot = _fRot;
		}

		// Token: 0x06009292 RID: 37522 RVA: 0x003CD035 File Offset: 0x003CB435
		public float getAutoRot(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].fAutoRot;
		}

		// Token: 0x06009293 RID: 37523 RVA: 0x003CD04D File Offset: 0x003CB44D
		public void setAutoRotUp(bool _bUp, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].bAutoRotUp = _bUp;
		}

		// Token: 0x06009294 RID: 37524 RVA: 0x003CD066 File Offset: 0x003CB466
		public bool getAutoRotUp(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].bAutoRotUp;
		}

		// Token: 0x06009295 RID: 37525 RVA: 0x003CD07E File Offset: 0x003CB47E
		public void setBoneCatch(byte _nCatch)
		{
			this.m_Param.nCatch = _nCatch;
		}

		// Token: 0x06009296 RID: 37526 RVA: 0x003CD08C File Offset: 0x003CB48C
		public byte getBoneCatch()
		{
			return this.m_Param.nCatch;
		}

		// Token: 0x06009297 RID: 37527 RVA: 0x003CD09C File Offset: 0x003CB49C
		public void initLocaterHist(int _nIdx)
		{
			if (this.m_listBone[_nIdx].listLocater[0].transFrame == null)
			{
				return;
			}
			this.m_listOldWorldPos[_nIdx] = this.m_listBone[_nIdx].listLocater[0].transFrame.position;
		}

		// Token: 0x06009298 RID: 37528 RVA: 0x003CD0FE File Offset: 0x003CB4FE
		public void initLocalPos(int _nIdx)
		{
			this.m_listPos[_nIdx] = Vector3.zero;
		}

		// Token: 0x06009299 RID: 37529 RVA: 0x003CD111 File Offset: 0x003CB511
		public void initForce(int _nIdx)
		{
			this.m_listSpringCtrl[_nIdx].InitForce();
		}

		// Token: 0x0600929A RID: 37530 RVA: 0x003CD124 File Offset: 0x003CB524
		public void setParamAddR(Vector3 _vAddR, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vAddR = _vAddR;
		}

		// Token: 0x0600929B RID: 37531 RVA: 0x003CD13D File Offset: 0x003CB53D
		public Vector3 getParamAddR(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vAddR;
		}

		// Token: 0x0600929C RID: 37532 RVA: 0x003CD155 File Offset: 0x003CB555
		public void setParamAddT(Vector3 _vAddT, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vAddT = _vAddT;
		}

		// Token: 0x0600929D RID: 37533 RVA: 0x003CD16E File Offset: 0x003CB56E
		public Vector3 getParamAddT(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vAddT;
		}

		// Token: 0x0600929E RID: 37534 RVA: 0x003CD186 File Offset: 0x003CB586
		public void setParamAddS(Vector3 _vAddS, int _nIdx)
		{
			this.m_Param.listDetail[_nIdx].vAddS = _vAddS;
		}

		// Token: 0x0600929F RID: 37535 RVA: 0x003CD19F File Offset: 0x003CB59F
		public Vector3 getParamAddS(int _nIdx)
		{
			return this.m_Param.listDetail[_nIdx].vAddS;
		}

		// Token: 0x0400768C RID: 30348
		private bool m_bLR;

		// Token: 0x0400768D RID: 30349
		private int m_nNumBone;

		// Token: 0x0400768E RID: 30350
		private List<CBoneData> m_listBone = new List<CBoneData>();

		// Token: 0x0400768F RID: 30351
		private List<Vector3> m_listPos = new List<Vector3>();

		// Token: 0x04007690 RID: 30352
		private List<Vector3> m_listRot = new List<Vector3>();

		// Token: 0x04007691 RID: 30353
		private List<Vector3> m_listOldWorldPos = new List<Vector3>();

		// Token: 0x04007692 RID: 30354
		private CSwayParam m_Param = new CSwayParam();

		// Token: 0x04007693 RID: 30355
		private List<SpringCtrl> m_listSpringCtrl = new List<SpringCtrl>();

		// Token: 0x04007694 RID: 30356
		private List<GameObject> m_listObjCalc = new List<GameObject>();

		// Token: 0x04007695 RID: 30357
		private readonly int m_nObjCalcNum = 5;

		// Token: 0x0200114C RID: 4428
		// (Invoke) Token: 0x060092A2 RID: 37538
		private delegate bool ResultMatrixFunc(FakeTransform _ftransResult, CBoneData _Bone, int _nBoneIdx);

		// Token: 0x0200114D RID: 4429
		// (Invoke) Token: 0x060092A6 RID: 37542
		private delegate bool ResultTransformFunc(FakeTransform _ftransResult, CBoneData _Bone, int _nBoneIdx, FakeTransform _ftransT, FakeTransform _ftransR);
	}
}
