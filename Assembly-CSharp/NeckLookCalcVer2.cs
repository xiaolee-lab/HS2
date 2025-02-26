using System;
using UnityEngine;

// Token: 0x020010EB RID: 4331
public class NeckLookCalcVer2 : MonoBehaviour
{
	// Token: 0x06008FBB RID: 36795 RVA: 0x003BD83D File Offset: 0x003BBC3D
	private void Awake()
	{
		if (!this.initEnd)
		{
			this.Init();
		}
	}

	// Token: 0x06008FBC RID: 36796 RVA: 0x003BD850 File Offset: 0x003BBC50
	private void Start()
	{
		if (!this.initEnd)
		{
			this.Init();
		}
	}

	// Token: 0x06008FBD RID: 36797 RVA: 0x003BD864 File Offset: 0x003BBC64
	private void OnDrawGizmos()
	{
		if (this.boneCalcAngle)
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
			if (this.neckTypeStates.Length > this.ptnDraw)
			{
				NeckTypeStateVer2 neckTypeStateVer = this.neckTypeStates[this.ptnDraw];
				Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
				Vector3 vector = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, neckTypeStateVer.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector);
				Vector3 vector2 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, -neckTypeStateVer.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector2);
				Vector3 vector3 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(neckTypeStateVer.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector3);
				Vector3 vector4 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(-neckTypeStateVer.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector4);
				Gizmos.DrawLine(vector, vector4);
				Gizmos.DrawLine(vector4, vector2);
				Gizmos.DrawLine(vector2, vector3);
				Gizmos.DrawLine(vector3, vector);
				if (neckTypeStateVer.limitBreakCorrectionValue != 0f)
				{
					Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
					vector = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, neckTypeStateVer.hAngleLimit + neckTypeStateVer.limitBreakCorrectionValue, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
					Gizmos.DrawLine(this.boneCalcAngle.position, vector);
					vector2 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, -neckTypeStateVer.hAngleLimit - neckTypeStateVer.limitBreakCorrectionValue, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
					Gizmos.DrawLine(this.boneCalcAngle.position, vector2);
					vector3 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(neckTypeStateVer.vAngleLimit + neckTypeStateVer.limitBreakCorrectionValue, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
					Gizmos.DrawLine(this.boneCalcAngle.position, vector3);
					vector4 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(-neckTypeStateVer.vAngleLimit - neckTypeStateVer.limitBreakCorrectionValue, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
					Gizmos.DrawLine(this.boneCalcAngle.position, vector4);
					Gizmos.DrawLine(vector, vector4);
					Gizmos.DrawLine(vector4, vector2);
					Gizmos.DrawLine(vector2, vector3);
					Gizmos.DrawLine(vector3, vector);
				}
				Gizmos.color = new Color(1f, 0f, 1f, 0.8f);
				float num = 0f;
				for (int i = 0; i < neckTypeStateVer.aParam.Length; i++)
				{
					num += neckTypeStateVer.aParam[i].maxBendingAngle;
				}
				vector = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, num, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector);
				vector2 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, num - neckTypeStateVer.limitAway, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector2);
				Gizmos.DrawLine(vector, vector2);
				num = 0f;
				for (int j = 0; j < neckTypeStateVer.aParam.Length; j++)
				{
					num += neckTypeStateVer.aParam[j].minBendingAngle;
				}
				vector = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, num, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector);
				vector2 = this.boneCalcAngle.TransformDirection(Quaternion.Euler(0f, num + neckTypeStateVer.limitAway, 0f) * Vector3.forward * this.drawLineLength) + this.boneCalcAngle.position;
				Gizmos.DrawLine(this.boneCalcAngle.position, vector2);
				Gizmos.DrawLine(vector, vector2);
			}
		}
		Gizmos.color = Color.white;
	}

	// Token: 0x06008FBE RID: 36798 RVA: 0x003BDE24 File Offset: 0x003BC224
	public void Init()
	{
		foreach (NeckObjectVer2 neckObjectVer in this.aBones)
		{
			if (neckObjectVer.referenceCalc == null)
			{
				neckObjectVer.referenceCalc = base.transform;
			}
			neckObjectVer.angleH = 0f;
			neckObjectVer.angleV = 0f;
			neckObjectVer.angleHRate = 0f;
			neckObjectVer.angleVRate = 0f;
		}
		this.lookType = NECK_LOOK_TYPE_VER2.ANIMATION;
		this.initEnd = true;
	}

	// Token: 0x06008FBF RID: 36799 RVA: 0x003BDEA8 File Offset: 0x003BC2A8
	public void UpdateCall(int ptnNo)
	{
		if (ptnNo >= this.neckTypeStates.Length)
		{
			ptnNo = 0;
		}
		if (this.lookType != this.neckTypeStates[ptnNo].lookType)
		{
			this.lookType = this.neckTypeStates[ptnNo].lookType;
			this.changeTypeTimer = 0f;
			for (int i = 0; i < this.aBones.Length; i++)
			{
				this.aBones[i].fixAngleBackup = this.aBones[i].fixAngle;
			}
			if (this.lookType == NECK_LOOK_TYPE_VER2.FORWARD)
			{
				for (int j = 0; j < this.aBones.Length; j++)
				{
					this.aBones[j].angleH = 0f;
					this.aBones[j].angleV = 0f;
				}
			}
		}
		if (this.lookType == NECK_LOOK_TYPE_VER2.TARGET)
		{
			for (int k = 0; k < this.aBones.Length; k++)
			{
				this.aBones[k].backupLocalRotaionByTarget = this.aBones[k].neckBone.localRotation;
				this.aBones[k].neckBone.localRotation = this.aBones[k].fixAngle;
			}
		}
	}

	// Token: 0x06008FC0 RID: 36800 RVA: 0x003BDFDC File Offset: 0x003BC3DC
	public void NeckUpdateCalc(Vector3 target, int ptnNo, bool _isUseBackUpPos = false)
	{
		if (!this.initEnd)
		{
			return;
		}
		this.nowPtnNo = ptnNo;
		if (!this.isEnabled)
		{
			return;
		}
		if (!this.skipCalc && Time.deltaTime == 0f)
		{
			return;
		}
		NeckTypeStateVer2 neckTypeStateVer = this.neckTypeStates[this.nowPtnNo];
		if (!_isUseBackUpPos)
		{
			this.backupPos = target;
		}
		if (neckTypeStateVer.aParam.Length != this.aBones.Length)
		{
			return;
		}
		if (this.skipCalc)
		{
			this.changeTypeTimer = this.changeTypeLeapTime;
		}
		this.changeTypeTimer = Mathf.Clamp(this.changeTypeTimer + Time.deltaTime, 0f, this.changeTypeLeapTime);
		float num = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
		if (this.changeTypeLerpCurve != null)
		{
			num = this.changeTypeLerpCurve.Evaluate(num);
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.ANIMATION)
		{
			for (int i = 0; i < this.aBones.Length; i++)
			{
				this.aBones[i].fixAngle = this.aBones[i].neckBone.localRotation;
				this.aBones[i].neckBone.localRotation = Quaternion.Slerp(this.aBones[i].fixAngleBackup, this.aBones[i].fixAngle, num);
				if (this.aBones[i].controlBone != null)
				{
					this.aBones[i].controlBone.localRotation = this.aBones[i].fixAngle;
					if (this.aBones[i].controlBone.gameObject.activeSelf)
					{
						this.aBones[i].controlBone.gameObject.SetActive(false);
					}
				}
			}
			return;
		}
		bool flag = false;
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.CONTROL)
		{
			for (int j = 0; j < this.aBones.Length; j++)
			{
				if (!(this.aBones[j].controlBone != null))
				{
					flag = true;
					break;
				}
			}
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.CONTROL && !flag)
		{
			for (int k = 0; k < this.aBones.Length; k++)
			{
				if (k != 0)
				{
					this.aBones[k].controlBone.gameObject.SetActive(!this.aBones[0].controlBone.gameObject.activeSelf);
				}
				this.aBones[k].fixAngle = this.aBones[k].controlBone.localRotation;
				num = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
				this.aBones[k].neckBone.localRotation = Quaternion.Lerp(this.aBones[k].fixAngleBackup, this.aBones[k].fixAngle, num);
			}
			return;
		}
		for (int l = 0; l < this.aBones.Length; l++)
		{
			if (!(this.aBones[l].controlBone == null))
			{
				if (this.aBones[l].controlBone.gameObject.activeSelf)
				{
					this.aBones[l].controlBone.gameObject.SetActive(false);
				}
			}
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.FORWARD || flag)
		{
			for (int m = 0; m < this.aBones.Length; m++)
			{
				this.aBones[m].fixAngle = Quaternion.identity;
				Quaternion b = Quaternion.Slerp(this.aBones[m].neckBone.localRotation, this.aBones[m].fixAngle, this.calcLerp);
				this.aBones[m].neckBone.localRotation = Quaternion.Slerp(this.aBones[m].fixAngleBackup, b, num);
				if (this.aBones[m].controlBone != null)
				{
					this.aBones[m].controlBone.localRotation = this.aBones[m].fixAngle;
				}
			}
			return;
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.FIX)
		{
			for (int n = 0; n < this.aBones.Length; n++)
			{
				Quaternion b2 = Quaternion.Slerp(this.aBones[n].neckBone.localRotation, this.aBones[n].fixAngle, this.calcLerp);
				this.aBones[n].neckBone.localRotation = Quaternion.Slerp(this.aBones[n].fixAngleBackup, b2, num);
				if (this.aBones[n].controlBone != null)
				{
					this.aBones[n].controlBone.localRotation = this.aBones[n].fixAngle;
				}
			}
			return;
		}
		Vector3 dirB = target - this.boneCalcAngle.position;
		float f = this.AngleAroundAxis(this.boneCalcAngle.forward, dirB, this.boneCalcAngle.up);
		float f2 = this.AngleAroundAxis(this.boneCalcAngle.forward, dirB, this.boneCalcAngle.right);
		bool flag2 = false;
		float num2 = (!neckTypeStateVer.isLimitBreakBackup) ? this.neckTypeStates[ptnNo].limitBreakCorrectionValue : 0f;
		if (Mathf.Abs(f) > this.neckTypeStates[ptnNo].hAngleLimit + num2 || Mathf.Abs(f2) > this.neckTypeStates[ptnNo].vAngleLimit + num2)
		{
			flag2 = true;
		}
		neckTypeStateVer.isLimitBreakBackup = flag2;
		if (flag2)
		{
			this.nowAngle = Vector2.zero;
		}
		else
		{
			if (_isUseBackUpPos)
			{
				target = this.backupPos;
			}
			this.nowAngle = this.GetAngleToTarget(target, this.aBones[this.aBones.Length - 1], 1f);
			if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.TARGET)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.transformAim.position, this.boneCalcAngle.rotation, this.transformAim.lossyScale);
				Vector3 vector = matrix4x.inverse.MultiplyPoint3x4(target);
				Vector3 vector2 = matrix4x.inverse.MultiplyPoint3x4(this.boneCalcAngle.position);
				if (vector.z < 0f && vector.y < 0f)
				{
					if (vector2.x < 0f)
					{
						if (vector2.x < vector.x && vector.x < 0f)
						{
							target = this.transformAim.position;
						}
					}
					else if (vector2.x > vector.x && vector.x > 0f)
					{
						target = this.transformAim.position;
					}
				}
				if ((this.transformAim.position - target).magnitude == 0f)
				{
					for (int num3 = 0; num3 < this.aBones.Length; num3++)
					{
						this.CalcNeckBone(this.aBones[num3]);
						this.aBones[num3].neckBone.localRotation = Quaternion.Slerp(this.aBones[num3].fixAngleBackup, this.aBones[num3].fixAngle, num);
					}
					return;
				}
			}
			else if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER2.AWAY)
			{
				float num4 = 0f;
				float num5 = 0f;
				for (int num6 = 0; num6 < this.aBones.Length; num6++)
				{
					num4 += this.aBones[num6].angleH;
				}
				if (this.nowAngle.y <= num4)
				{
					for (int num7 = 0; num7 < this.aBones.Length; num7++)
					{
						num5 += neckTypeStateVer.aParam[num7].maxBendingAngle;
					}
					if (this.nowAngle.y <= num5 - neckTypeStateVer.limitAway || this.nowAngle.y < 0f)
					{
						this.nowAngle.y = num5;
					}
					else
					{
						this.nowAngle.y = 0f;
						for (int num8 = 0; num8 < this.aBones.Length; num8++)
						{
							this.nowAngle.y = this.nowAngle.y + neckTypeStateVer.aParam[num8].minBendingAngle;
						}
					}
				}
				else if (this.nowAngle.y > num4)
				{
					for (int num9 = 0; num9 < this.aBones.Length; num9++)
					{
						num5 += neckTypeStateVer.aParam[num9].minBendingAngle;
					}
					if (this.nowAngle.y >= num5 + neckTypeStateVer.limitAway || this.nowAngle.y > 0f)
					{
						this.nowAngle.y = num5;
					}
					else
					{
						this.nowAngle.y = 0f;
						for (int num10 = 0; num10 < this.aBones.Length; num10++)
						{
							this.nowAngle.y = this.nowAngle.y + neckTypeStateVer.aParam[num10].maxBendingAngle;
						}
					}
				}
				this.nowAngle.x = -this.nowAngle.x;
			}
		}
		Vector2 vector3 = this.nowAngle;
		for (int num11 = this.aBones.Length - 1; num11 > -1; num11--)
		{
			this.RotateToAngle(this.aBones[num11], neckTypeStateVer, num11, ref vector3);
		}
		for (int num12 = 0; num12 < this.aBones.Length; num12++)
		{
			Quaternion b3 = Quaternion.Slerp(this.aBones[num12].backupLocalRotaionByTarget, this.aBones[num12].fixAngle, this.calcLerp);
			this.aBones[num12].neckBone.localRotation = Quaternion.Slerp(this.aBones[num12].fixAngleBackup, b3, num);
			if (this.aBones[num12].controlBone != null)
			{
				this.aBones[num12].controlBone.localRotation = this.aBones[num12].fixAngle;
			}
		}
	}

	// Token: 0x06008FC1 RID: 36801 RVA: 0x003BEA3C File Offset: 0x003BCE3C
	private Vector2 GetAngleToTarget(Vector3 _targetPosition, NeckObjectVer2 _bone, float _weight)
	{
		Quaternion quaternion = Quaternion.FromToRotation(this.transformAim.rotation * Vector3.forward, _targetPosition - this.transformAim.position);
		Quaternion rotation = quaternion * _bone.neckBone.rotation;
		float num = this.AngleAroundAxis(this.boneCalcAngle.forward, rotation * Vector3.forward, this.boneCalcAngle.up);
		quaternion = Quaternion.AngleAxis(num, this.boneCalcAngle.up) * this.boneCalcAngle.rotation;
		Vector3 axis = Vector3.Cross(this.boneCalcAngle.up, quaternion * Vector3.forward);
		float x = this.AngleAroundAxis(quaternion * Vector3.forward, rotation * Vector3.forward, axis);
		return new Vector2(x, num);
	}

	// Token: 0x06008FC2 RID: 36802 RVA: 0x003BEB14 File Offset: 0x003BCF14
	private void RotateToAngle(NeckObjectVer2 _bone, NeckTypeStateVer2 _param, int _boneNum, ref Vector2 _Angle)
	{
		float num = Mathf.Clamp(_Angle.y, _param.aParam[_boneNum].minBendingAngle, _param.aParam[_boneNum].maxBendingAngle);
		float num2 = Mathf.Clamp(_Angle.x, _param.aParam[_boneNum].upBendingAngle, _param.aParam[_boneNum].downBendingAngle);
		_Angle -= new Vector2(num2, num);
		float t = Mathf.Clamp01(Time.deltaTime * _param.leapSpeed);
		if (this.skipCalc)
		{
			t = 1f;
		}
		_bone.angleH = Mathf.Lerp(_bone.angleH, num, t);
		_bone.angleV = Mathf.Lerp(_bone.angleV, num2, t);
		this.CalcNeckBone(_bone);
	}

	// Token: 0x06008FC3 RID: 36803 RVA: 0x003BEBDC File Offset: 0x003BCFDC
	private void CalcNeckBone(NeckObjectVer2 _bone)
	{
		Quaternion rotation = Quaternion.AngleAxis(_bone.angleH, _bone.referenceCalc.up) * Quaternion.AngleAxis(_bone.angleV, _bone.referenceCalc.right) * _bone.referenceCalc.rotation;
		_bone.neckBone.rotation = rotation;
		_bone.fixAngle = _bone.neckBone.localRotation;
	}

	// Token: 0x06008FC4 RID: 36804 RVA: 0x003BEC48 File Offset: 0x003BD048
	private float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
	{
		dirA -= Vector3.Project(dirA, axis);
		dirB -= Vector3.Project(dirB, axis);
		float num = Vector3.Angle(dirA, dirB);
		return num * (float)((Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) >= 0f) ? 1 : -1);
	}

	// Token: 0x06008FC5 RID: 36805 RVA: 0x003BEC9C File Offset: 0x003BD09C
	public void setEnable(bool setFlag)
	{
		this.isEnabled = setFlag;
	}

	// Token: 0x0400748C RID: 29836
	public bool isEnabled = true;

	// Token: 0x0400748D RID: 29837
	public Transform transformAim;

	// Token: 0x0400748E RID: 29838
	public Transform boneCalcAngle;

	// Token: 0x0400748F RID: 29839
	public NeckObjectVer2[] aBones;

	// Token: 0x04007490 RID: 29840
	public NeckTypeStateVer2[] neckTypeStates;

	// Token: 0x04007491 RID: 29841
	[Tooltip("表示するパターン番号")]
	public int ptnDraw;

	// Token: 0x04007492 RID: 29842
	public float drawLineLength = 1f;

	// Token: 0x04007493 RID: 29843
	public float changeTypeLeapTime = 1f;

	// Token: 0x04007494 RID: 29844
	public AnimationCurve changeTypeLerpCurve = new AnimationCurve();

	// Token: 0x04007495 RID: 29845
	[Tooltip("無条件でこれだけ回る")]
	public Vector2 nowAngle;

	// Token: 0x04007496 RID: 29846
	[Range(0f, 1f)]
	public float calcLerp = 1f;

	// Token: 0x04007497 RID: 29847
	public bool skipCalc;

	// Token: 0x04007498 RID: 29848
	private int nowPtnNo;

	// Token: 0x04007499 RID: 29849
	private bool initEnd;

	// Token: 0x0400749A RID: 29850
	private Vector3 backupPos = Vector3.zero;

	// Token: 0x0400749B RID: 29851
	private float changeTypeTimer;

	// Token: 0x0400749C RID: 29852
	private NECK_LOOK_TYPE_VER2 lookType;
}
