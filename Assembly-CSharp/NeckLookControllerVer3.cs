using System;
using UnityEngine;

// Token: 0x02000BAE RID: 2990
public class NeckLookControllerVer3 : MonoBehaviour
{
	// Token: 0x060059C2 RID: 22978 RVA: 0x0026655B File Offset: 0x0026495B
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060059C3 RID: 22979 RVA: 0x00266563 File Offset: 0x00264963
	private void Start()
	{
		if (!this.target && Camera.main)
		{
			this.target = Camera.main.transform;
		}
		this.Init();
	}

	// Token: 0x060059C4 RID: 22980 RVA: 0x0026659C File Offset: 0x0026499C
	private void LateUpdate()
	{
		this.UpdateCall(this.ptnNo);
		if (this.target != null)
		{
			Vector3 position = this.transformAim.position;
			Vector3 position2 = this.target.position;
			for (int i = 0; i < 2; i++)
			{
				position2[i] = Mathf.Lerp(position[i], position2[i], this.rate);
			}
			this.NeckUpdateCalc(position2, this.ptnNo, false);
		}
		else
		{
			this.NeckUpdateCalc(Vector3.zero, this.ptnNo, true);
		}
	}

	// Token: 0x060059C5 RID: 22981 RVA: 0x00266638 File Offset: 0x00264A38
	private void OnDrawGizmos()
	{
		Transform transform = (!this.boneCalcAngle) ? ((!this.m_boneCalcAngle) ? null : this.m_boneCalcAngle) : this.boneCalcAngle;
		if (!transform || this.neckTypeStates.Length <= this.ptnDraw || this.ptnDraw < 0)
		{
			Gizmos.color = Color.white;
			return;
		}
		NeckTypeStateVer3 neckTypeStateVer = this.neckTypeStates[this.ptnDraw];
		Vector3 position = transform.position;
		Gizmos.color = new Color(0f, 1f, 1f, 0.75f);
		Vector3 vector = transform.TransformDirection(Quaternion.Euler(0f, neckTypeStateVer.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + position;
		Vector3 vector2 = transform.TransformDirection(Quaternion.Euler(0f, -neckTypeStateVer.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + position;
		Vector3 vector3 = transform.TransformDirection(Quaternion.Euler(neckTypeStateVer.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
		Vector3 vector4 = transform.TransformDirection(Quaternion.Euler(-neckTypeStateVer.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
		Gizmos.DrawLine(position, vector);
		Gizmos.DrawLine(position, vector2);
		Gizmos.DrawLine(position, vector3);
		Gizmos.DrawLine(position, vector4);
		Gizmos.DrawLine(vector, vector4);
		Gizmos.DrawLine(vector4, vector2);
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.DrawLine(vector3, vector);
		if (neckTypeStateVer.limitBreakCorrectionValue != 0f)
		{
			Gizmos.color = new Color(1f, 1f, 0f, 0.75f);
			vector = transform.TransformDirection(Quaternion.Euler(0f, neckTypeStateVer.hAngleLimit + neckTypeStateVer.limitBreakCorrectionValue, 0f) * Vector3.forward * this.drawLineLength) + position;
			vector2 = transform.TransformDirection(Quaternion.Euler(0f, -neckTypeStateVer.hAngleLimit - neckTypeStateVer.limitBreakCorrectionValue, 0f) * Vector3.forward * this.drawLineLength) + position;
			vector3 = transform.TransformDirection(Quaternion.Euler(neckTypeStateVer.vAngleLimit + neckTypeStateVer.limitBreakCorrectionValue, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
			vector4 = transform.TransformDirection(Quaternion.Euler(-neckTypeStateVer.vAngleLimit - neckTypeStateVer.limitBreakCorrectionValue, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
			Gizmos.DrawLine(position, vector);
			Gizmos.DrawLine(position, vector2);
			Gizmos.DrawLine(position, vector3);
			Gizmos.DrawLine(position, vector4);
			Gizmos.DrawLine(vector, vector4);
			Gizmos.DrawLine(vector4, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector);
		}
		Gizmos.color = new Color(1f, 0f, 1f, 0.75f);
		float num = 0f;
		for (int i = 0; i < neckTypeStateVer.aParam.Length; i++)
		{
			num += neckTypeStateVer.aParam[i].rightBendingAngle;
		}
		vector = transform.TransformDirection(Quaternion.Euler(0f, num, 0f) * Vector3.forward * this.drawLineLength) + position;
		vector2 = transform.TransformDirection(Quaternion.Euler(0f, num - neckTypeStateVer.limitAway, 0f) * Vector3.forward * this.drawLineLength) + position;
		Gizmos.DrawLine(position, vector);
		Gizmos.DrawLine(position, vector2);
		Gizmos.DrawLine(vector, vector2);
		num = 0f;
		for (int j = 0; j < neckTypeStateVer.aParam.Length; j++)
		{
			num += neckTypeStateVer.aParam[j].leftBendingAngle;
		}
		vector = transform.TransformDirection(Quaternion.Euler(0f, num, 0f) * Vector3.forward * this.drawLineLength) + position;
		vector2 = transform.TransformDirection(Quaternion.Euler(0f, num + neckTypeStateVer.limitAway, 0f) * Vector3.forward * this.drawLineLength) + position;
		Gizmos.DrawLine(position, vector);
		Gizmos.DrawLine(position, vector2);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.color = Color.white;
	}

	// Token: 0x060059C6 RID: 22982 RVA: 0x00266B14 File Offset: 0x00264F14
	private void CreateObj(Transform _origin, ref Transform _newObj, ref Quaternion _worldRotation, ref Quaternion _localRotation, string _name = "")
	{
		if (_newObj != null && _newObj.gameObject != null)
		{
			UnityEngine.Object.Destroy(_newObj.gameObject);
		}
		_newObj = null;
		if (_name.IsNullOrEmpty())
		{
			_name = _origin.name + "(Ref)";
		}
		GameObject gameObject = new GameObject(_name);
		_newObj = gameObject.transform;
		_newObj.parent = _origin.parent;
		_newObj.rotation = Quaternion.identity;
		_newObj.localScale = _origin.localScale;
		_newObj.position = _origin.position;
		_worldRotation = _origin.rotation * Quaternion.Inverse(this.rootNode.rotation);
		_localRotation = _origin.localRotation;
	}

	// Token: 0x060059C7 RID: 22983 RVA: 0x00266BE0 File Offset: 0x00264FE0
	public void Init()
	{
		if (this.initEnd)
		{
			return;
		}
		if (!this.rootNode)
		{
			this.rootNode = base.transform;
		}
		Quaternion rotation = default(Quaternion);
		if (this.rootNode)
		{
			rotation = this.rootNode.rotation;
			this.rootNode.rotation = Quaternion.identity;
		}
		this.DestroyReference();
		if (this.m_transformAim != null)
		{
			this.CreateObj(this.m_transformAim, ref this.transformAim, ref this.transformAimWorldRotation, ref this.transformAimLocalRotation, this.m_transformAim.name + "(AimRef)");
		}
		if (this.m_boneCalcAngle != null)
		{
			this.CreateObj(this.m_boneCalcAngle, ref this.boneCalcAngle, ref this.boneCalcAngleWorldRotation, ref this.boneCalcAngleLocalRotation, this.m_boneCalcAngle.name + "(CalcRef)");
		}
		foreach (NeckObjectVer3 neckObjectVer in this.aBones)
		{
			if (neckObjectVer.m_referenceCalc == null)
			{
				neckObjectVer.m_referenceCalc = base.transform;
			}
			if (neckObjectVer.m_referenceCalc != null)
			{
				this.CreateObj(neckObjectVer.m_referenceCalc, ref neckObjectVer.referenceCalc, ref neckObjectVer.referenceCalcWorldRotation, ref neckObjectVer.referenceCalcLocalRotation, neckObjectVer.m_referenceCalc.name + "(軸Ref)");
			}
			neckObjectVer.neckBoneWorldRotation = neckObjectVer.neckBone.rotation * Quaternion.Inverse(this.rootNode.rotation);
			neckObjectVer.neckBoneLocalRotation = neckObjectVer.neckBone.localRotation;
			neckObjectVer.angleH = (neckObjectVer.angleV = (neckObjectVer.angleHRate = (neckObjectVer.angleVRate = 0f)));
		}
		if (this.rootNode)
		{
			this.rootNode.rotation = rotation;
		}
		this.initEnd = true;
	}

	// Token: 0x060059C8 RID: 22984 RVA: 0x00266DE0 File Offset: 0x002651E0
	private void UpdateCall(int _ptnNo)
	{
		if (this.neckTypeStates.Length <= _ptnNo || _ptnNo < 0)
		{
			_ptnNo = 0;
		}
		if (this.neckTypeStates.Length == 0)
		{
			return;
		}
		bool flag = this.lookType != this.neckTypeStates[_ptnNo].lookType;
		if (flag)
		{
			this.lookType = this.neckTypeStates[_ptnNo].lookType;
			this.changeTypeTimer = 0f;
			for (int i = 0; i < this.aBones.Length; i++)
			{
				this.aBones[i].fixAngleBackup = this.aBones[i].fixAngle;
			}
		}
		if (this.lookType == NECK_LOOK_TYPE_VER3.TARGET)
		{
			for (int j = 0; j < this.aBones.Length; j++)
			{
				this.aBones[j].backupLocalRotationByTarget = this.aBones[j].neckBone.localRotation;
				this.aBones[j].neckBone.localRotation = this.aBones[j].fixAngle;
			}
		}
	}

	// Token: 0x060059C9 RID: 22985 RVA: 0x00266EE8 File Offset: 0x002652E8
	private void NeckUpdateCalc(Vector3 _target, int _ptnNo, bool _isUseBackUpPos = false)
	{
		if (!this.initEnd)
		{
			return;
		}
		this.nowPtnNo = _ptnNo;
		if (!this.isEnabled || this.nowPtnNo < 0 || this.neckTypeStates.Length <= this.nowPtnNo || this.neckTypeStates.Length == 0)
		{
			return;
		}
		NeckTypeStateVer3 neckTypeStateVer = this.neckTypeStates[this.nowPtnNo];
		if (!_isUseBackUpPos)
		{
			this.backupPos = _target;
		}
		if (neckTypeStateVer.aParam.Length != this.aBones.Length)
		{
			return;
		}
		this.changeTypeTimer = Mathf.Clamp(this.changeTypeTimer + Time.deltaTime, 0f, this.changeTypeLeapTime);
		float num = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
		if (this.changeTypeLerpCurve != null)
		{
			num = this.changeTypeLerpCurve.Evaluate(num);
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.ANIMATION)
		{
			for (int i = 0; i < this.aBones.Length; i++)
			{
				this.aBones[i].fixAngle = this.aBones[i].neckBone.localRotation;
				this.aBones[i].neckBone.localRotation = Quaternion.Slerp(this.aBones[i].fixAngleBackup, this.aBones[i].fixAngle, num);
				Transform controlBone = this.aBones[i].controlBone;
				if (controlBone != null)
				{
					controlBone.localRotation = this.aBones[i].fixAngle;
					if (controlBone.gameObject.activeSelf)
					{
						controlBone.gameObject.SetActive(false);
					}
				}
			}
			return;
		}
		bool flag = false;
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.CONTROL)
		{
			for (int j = 0; j < this.aBones.Length; j++)
			{
				if (flag = (this.aBones[j].controlBone == null))
				{
					break;
				}
			}
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.CONTROL && !flag)
		{
			for (int k = 0; k < this.aBones.Length; k++)
			{
				if (k != 0 && this.aBones[k].controlBone.gameObject.activeSelf == this.aBones[k].controlBone.gameObject.activeSelf)
				{
					this.aBones[k].controlBone.gameObject.SetActive(!this.aBones[k].controlBone.gameObject.activeSelf);
				}
				this.aBones[k].fixAngle = this.aBones[k].controlBone.localRotation;
				num = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
				this.aBones[k].neckBone.localRotation = Quaternion.Lerp(this.aBones[k].fixAngleBackup, this.aBones[k].fixAngle, num);
			}
			return;
		}
		for (int l = 0; l < this.aBones.Length; l++)
		{
			if (this.aBones[l].controlBone != null && this.aBones[l].controlBone.gameObject.activeSelf)
			{
				this.aBones[l].controlBone.gameObject.SetActive(false);
			}
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.FORWARD || flag)
		{
			for (int m = 0; m < this.aBones.Length; m++)
			{
				this.aBones[m].fixAngle = this.aBones[m].neckBoneLocalRotation;
				Quaternion b = Quaternion.Slerp(this.aBones[m].neckBone.localRotation, this.aBones[m].fixAngle, this.calcLerp);
				this.aBones[m].neckBone.localRotation = Quaternion.Slerp(this.aBones[m].fixAngleBackup, b, num);
				if (this.aBones[m].controlBone != null)
				{
					this.aBones[m].controlBone.localRotation = this.aBones[m].fixAngle;
				}
			}
			return;
		}
		if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.FIX)
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
		Vector3 dirB = _target - this.boneCalcAngle.position;
		Vector3 forward = this.boneCalcAngle.forward;
		float f = this.AngleAroundAxis(forward, dirB, this.boneCalcAngle.up);
		float f2 = this.AngleAroundAxis(forward, dirB, this.boneCalcAngle.right);
		bool flag2 = false;
		float num2 = (!neckTypeStateVer.isLimitBreakBackup) ? this.neckTypeStates[_ptnNo].limitBreakCorrectionValue : 0f;
		if (Mathf.Abs(f) > this.neckTypeStates[_ptnNo].hAngleLimit + num2 || Mathf.Abs(f2) > this.neckTypeStates[_ptnNo].vAngleLimit + num2)
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
				_target = this.backupPos;
			}
			this.nowAngle = this.GetAngleToTarget(_target, this.aBones[this.aBones.Length - 1]);
			if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.TARGET)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.transformAim.position, this.boneCalcAngle.rotation, this.transformAim.lossyScale);
				Vector3 vector = matrix4x.inverse.MultiplyPoint3x4(_target);
				Vector3 vector2 = matrix4x.inverse.MultiplyPoint3x4(this.boneCalcAngle.position);
				if (vector.z < 0f && vector.y < 0f)
				{
					if (vector2.x < 0f)
					{
						if (vector2.x < vector.x && vector.x < 0f)
						{
							_target = this.transformAim.position;
						}
					}
					else if (vector.x < vector2.x && 0f < vector.x)
					{
						_target = this.transformAim.position;
					}
				}
				if ((this.transformAim.position - _target).magnitude == 0f)
				{
					for (int num3 = 0; num3 < this.aBones.Length; num3++)
					{
						this.CalcNeckBone(this.aBones[num3]);
						this.aBones[num3].neckBone.localRotation = Quaternion.Slerp(this.aBones[num3].fixAngleBackup, this.aBones[num3].fixAngle, num);
					}
					return;
				}
			}
			else if (neckTypeStateVer.lookType == NECK_LOOK_TYPE_VER3.AWAY)
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
						num5 += neckTypeStateVer.aParam[num7].rightBendingAngle;
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
							this.nowAngle.y = this.nowAngle.y + neckTypeStateVer.aParam[num8].leftBendingAngle;
						}
					}
				}
				else if (num4 < this.nowAngle.y)
				{
					for (int num9 = 0; num9 < this.aBones.Length; num9++)
					{
						num5 += neckTypeStateVer.aParam[num9].leftBendingAngle;
					}
					if (num5 + neckTypeStateVer.limitAway <= this.nowAngle.y || 0f < this.nowAngle.y)
					{
						this.nowAngle.y = num5;
					}
					else
					{
						this.nowAngle.y = 0f;
						for (int num10 = 0; num10 < this.aBones.Length; num10++)
						{
							this.nowAngle.y = this.nowAngle.y + neckTypeStateVer.aParam[num10].rightBendingAngle;
						}
					}
				}
				this.nowAngle.x = -this.nowAngle.x;
			}
		}
		Vector2 vector3 = this.nowAngle;
		int num11 = this.aBones.Length - 1;
		while (0 <= num11)
		{
			this.RotateToAngle(this.aBones[num11], neckTypeStateVer, num11, ref vector3);
			num11--;
		}
		for (int num12 = 0; num12 < this.aBones.Length; num12++)
		{
			Quaternion b3 = Quaternion.Slerp(this.aBones[num12].backupLocalRotationByTarget, this.aBones[num12].fixAngle, this.calcLerp);
			this.aBones[num12].neckBone.localRotation = Quaternion.Slerp(this.aBones[num12].fixAngleBackup, b3, num);
			if (this.aBones[num12].controlBone != null)
			{
				this.aBones[num12].controlBone.localRotation = this.aBones[num12].fixAngle;
			}
		}
	}

	// Token: 0x060059CA RID: 22986 RVA: 0x0026794C File Offset: 0x00265D4C
	private float AngleAroundAxis(Vector3 _dirA, Vector3 _dirB, Vector3 _axis)
	{
		_dirA -= Vector3.Project(_dirA, _axis);
		_dirB -= Vector3.Project(_dirB, _axis);
		float num = Vector3.Angle(_dirA, _dirB);
		return num * (float)((Vector3.Dot(_axis, Vector3.Cross(_dirA, _dirB)) >= 0f) ? 1 : -1);
	}

	// Token: 0x060059CB RID: 22987 RVA: 0x002679A0 File Offset: 0x00265DA0
	private Vector2 GetAngleToTarget(Vector3 _targetPosition, NeckObjectVer3 _bone)
	{
		Quaternion quaternion = Quaternion.FromToRotation(this.transformAim.rotation * Vector3.forward, _targetPosition - this.transformAim.position);
		Quaternion rotation = quaternion * (_bone.neckBone.rotation * Quaternion.Inverse(_bone.neckBoneWorldRotation));
		float num = this.AngleAroundAxis(this.boneCalcAngle.forward, rotation * Vector3.forward, this.boneCalcAngle.up);
		quaternion = Quaternion.AngleAxis(num, this.boneCalcAngle.up) * this.boneCalcAngle.rotation;
		Vector3 axis = Vector3.Cross(this.boneCalcAngle.up, quaternion * Vector3.forward);
		float x = this.AngleAroundAxis(quaternion * Vector3.forward, rotation * Vector3.forward, axis);
		return new Vector2(x, num);
	}

	// Token: 0x060059CC RID: 22988 RVA: 0x00267A88 File Offset: 0x00265E88
	private void CalcNeckBone(NeckObjectVer3 _bone)
	{
		Quaternion lhs = Quaternion.AngleAxis(_bone.angleH, _bone.referenceCalc.up) * Quaternion.AngleAxis(_bone.angleV, _bone.referenceCalc.right) * _bone.referenceCalc.rotation;
		_bone.neckBone.rotation = lhs * _bone.neckBoneWorldRotation;
		_bone.fixAngle = _bone.neckBone.localRotation;
	}

	// Token: 0x060059CD RID: 22989 RVA: 0x00267B00 File Offset: 0x00265F00
	private void RotateToAngle(NeckObjectVer3 _bone, NeckTypeStateVer3 _param, int _boneNum, ref Vector2 _angle)
	{
		float num = Mathf.Clamp(_angle.y, _param.aParam[_boneNum].leftBendingAngle, _param.aParam[_boneNum].rightBendingAngle);
		float num2 = Mathf.Clamp(_angle.x, _param.aParam[_boneNum].upBendingAngle, _param.aParam[_boneNum].downBendingAngle);
		_angle -= new Vector2(num2, num);
		float t = Mathf.Clamp01(Time.deltaTime * _param.leapSpeed);
		_bone.angleH = Mathf.Lerp(_bone.angleH, num, t);
		_bone.angleV = Mathf.Lerp(_bone.angleV, num2, t);
		this.CalcNeckBone(_bone);
	}

	// Token: 0x060059CE RID: 22990 RVA: 0x00267BB4 File Offset: 0x00265FB4
	public void SetEnable(bool _setFlag)
	{
		this.isEnabled = _setFlag;
	}

	// Token: 0x060059CF RID: 22991 RVA: 0x00267BC0 File Offset: 0x00265FC0
	private void DestroyReference()
	{
		if (this.transformAim != null)
		{
			UnityEngine.Object.Destroy(this.transformAim.gameObject);
			this.transformAim = null;
		}
		if (this.boneCalcAngle != null)
		{
			UnityEngine.Object.Destroy(this.boneCalcAngle.gameObject);
			this.boneCalcAngle = null;
		}
		foreach (NeckObjectVer3 neckObjectVer in this.aBones)
		{
			if (neckObjectVer.referenceCalc != null)
			{
				UnityEngine.Object.Destroy(neckObjectVer.referenceCalc.gameObject);
				neckObjectVer.referenceCalc = null;
			}
		}
	}

	// Token: 0x060059D0 RID: 22992 RVA: 0x00267C64 File Offset: 0x00266064
	private void OnDestroy()
	{
		this.DestroyReference();
	}

	// Token: 0x04005207 RID: 20999
	public int ptnNo;

	// Token: 0x04005208 RID: 21000
	public Transform target;

	// Token: 0x04005209 RID: 21001
	public float rate = 1f;

	// Token: 0x0400520A RID: 21002
	public Transform rootNode;

	// Token: 0x0400520B RID: 21003
	public bool isEnabled = true;

	// Token: 0x0400520C RID: 21004
	public Transform m_transformAim;

	// Token: 0x0400520D RID: 21005
	public Transform m_boneCalcAngle;

	// Token: 0x0400520E RID: 21006
	private Transform transformAim;

	// Token: 0x0400520F RID: 21007
	private Transform boneCalcAngle;

	// Token: 0x04005210 RID: 21008
	private Quaternion transformAimWorldRotation;

	// Token: 0x04005211 RID: 21009
	private Quaternion transformAimLocalRotation;

	// Token: 0x04005212 RID: 21010
	private Quaternion boneCalcAngleWorldRotation;

	// Token: 0x04005213 RID: 21011
	private Quaternion boneCalcAngleLocalRotation;

	// Token: 0x04005214 RID: 21012
	public NeckObjectVer3[] aBones;

	// Token: 0x04005215 RID: 21013
	public NeckTypeStateVer3[] neckTypeStates;

	// Token: 0x04005216 RID: 21014
	[Tooltip("表示するパターン番号")]
	public int ptnDraw;

	// Token: 0x04005217 RID: 21015
	public float drawLineLength = 10f;

	// Token: 0x04005218 RID: 21016
	public float changeTypeLeapTime = 1f;

	// Token: 0x04005219 RID: 21017
	public AnimationCurve changeTypeLerpCurve = new AnimationCurve();

	// Token: 0x0400521A RID: 21018
	[Tooltip("無条件でこれだけ回る")]
	public Vector2 nowAngle;

	// Token: 0x0400521B RID: 21019
	[Range(0f, 1f)]
	public float calcLerp = 1f;

	// Token: 0x0400521C RID: 21020
	private int nowPtnNo;

	// Token: 0x0400521D RID: 21021
	private bool initEnd;

	// Token: 0x0400521E RID: 21022
	private Vector3 backupPos = Vector3.zero;

	// Token: 0x0400521F RID: 21023
	private float changeTypeTimer;

	// Token: 0x04005220 RID: 21024
	private NECK_LOOK_TYPE_VER3 lookType = NECK_LOOK_TYPE_VER3.NONE;
}
