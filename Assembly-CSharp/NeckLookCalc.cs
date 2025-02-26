using System;
using UnityEngine;

// Token: 0x020010E6 RID: 4326
public class NeckLookCalc : MonoBehaviour
{
	// Token: 0x06008FAA RID: 36778 RVA: 0x003BCAB0 File Offset: 0x003BAEB0
	private void Awake()
	{
		if (!this.initEnd)
		{
			this.Init();
		}
	}

	// Token: 0x06008FAB RID: 36779 RVA: 0x003BCAC3 File Offset: 0x003BAEC3
	private void Start()
	{
		if (!this.initEnd)
		{
			this.Init();
		}
	}

	// Token: 0x06008FAC RID: 36780 RVA: 0x003BCAD8 File Offset: 0x003BAED8
	public void Init()
	{
		if (this.rootNode == null)
		{
			this.rootNode = base.transform;
		}
		Quaternion rotation = this.neckObj.neckTransform.parent.rotation;
		Quaternion lhs = Quaternion.Inverse(rotation);
		this.neckObj.referenceLookDir = lhs * this.rootNode.rotation * this.headLookVector.normalized;
		this.neckObj.referenceUpDir = lhs * this.rootNode.rotation * this.headUpVector.normalized;
		this.neckObj.angleH = 0f;
		this.neckObj.angleV = 0f;
		this.neckObj.dirUp = this.neckObj.referenceUpDir;
		this.neckObj.origRotation = default(Quaternion);
		this.neckObj.origRotation = this.neckObj.neckTransform.localRotation;
		this.angleHRate = 0f;
		this.lookType = NECK_LOOK_TYPE.NO_LOOK;
		this.initEnd = true;
	}

	// Token: 0x06008FAD RID: 36781 RVA: 0x003BCBF8 File Offset: 0x003BAFF8
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
			if (this.lookType == NECK_LOOK_TYPE.TARGET)
			{
				this.fixAngleBackup = this.neckObj.neckTransform.localRotation;
			}
			else
			{
				this.fixAngleBackup = this.fixAngle;
			}
		}
	}

	// Token: 0x06008FAE RID: 36782 RVA: 0x003BCC80 File Offset: 0x003BB080
	public void SetFixAngle(Quaternion angle)
	{
		this.fixAngle = angle;
		this.fixAngleBackup = angle;
		if (this.neckObj != null)
		{
			this.neckObj.neckTransform.localRotation = angle;
		}
	}

	// Token: 0x06008FAF RID: 36783 RVA: 0x003BCCBC File Offset: 0x003BB0BC
	public void NeckUpdateCalc(Vector3 target, int ptnNo)
	{
		this.backupPos = target;
		if (!this.initEnd)
		{
			return;
		}
		this.nowPtnNo = ptnNo;
		if (!NeckLookCalc.isEnabled)
		{
			return;
		}
		if (Time.deltaTime == 0f)
		{
			return;
		}
		NeckTypeState neckTypeState = this.neckTypeStates[ptnNo];
		this.changeTypeTimer += Time.deltaTime;
		if (this.lookType == NECK_LOOK_TYPE.NO_LOOK)
		{
			this.fixAngle = this.neckObj.neckTransform.localRotation;
			float t = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
			this.neckObj.neckTransform.localRotation = Quaternion.Lerp(this.fixAngleBackup, this.fixAngle, t);
			if (this.controlObj != null)
			{
				this.controlObj.localRotation = this.fixAngle;
				if (this.controlObj.gameObject.activeSelf)
				{
					this.controlObj.gameObject.SetActive(false);
				}
			}
			return;
		}
		if (this.controlObj == null && this.lookType == NECK_LOOK_TYPE.CONTROL)
		{
			this.lookType = NECK_LOOK_TYPE.FIX;
		}
		if (this.controlObj != null)
		{
			if (this.lookType == NECK_LOOK_TYPE.CONTROL)
			{
				if (!this.controlObj.gameObject.activeSelf)
				{
					this.controlObj.gameObject.SetActive(true);
				}
				this.controlObj.gameObject.SetActive(true);
				this.fixAngle = this.controlObj.localRotation;
				float t2 = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
				this.neckObj.neckTransform.localRotation = Quaternion.Lerp(this.fixAngleBackup, this.fixAngle, t2);
				return;
			}
			if (this.controlObj.gameObject.activeSelf)
			{
				this.controlObj.gameObject.SetActive(false);
			}
		}
		if (this.lookType == NECK_LOOK_TYPE.FIX)
		{
			float t3 = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
			this.neckObj.neckTransform.localRotation = Quaternion.Lerp(this.fixAngleBackup, this.fixAngle, t3);
			if (this.controlObj != null)
			{
				this.controlObj.localRotation = this.fixAngle;
			}
			return;
		}
		Vector3 b = target - this.rootNode.position;
		float num = Vector3.Distance(target, this.rootNode.position);
		if (num < this.neckTypeStates[ptnNo].nearDis)
		{
			b = b.normalized * this.neckTypeStates[ptnNo].nearDis;
			target = this.rootNode.position + b;
		}
		float num2 = Vector3.Angle(new Vector3(b.x, this.rootNode.forward.y, b.z), this.rootNode.forward);
		float num3 = Vector3.Angle(new Vector3(this.rootNode.forward.x, b.y, b.z), this.rootNode.forward);
		bool flag = false;
		if (num2 > this.neckTypeStates[ptnNo].hAngleLimit || num3 > this.neckTypeStates[ptnNo].vAngleLimit)
		{
			flag = true;
		}
		if (flag || this.lookType == NECK_LOOK_TYPE.FORWARD)
		{
			target = this.rootNode.position + this.rootNode.forward * this.neckTypeStates[ptnNo].forntTagDis;
		}
		this.neckObj.neckTransform.localRotation = this.neckObj.origRotation;
		Quaternion rotation = this.neckObj.neckTransform.parent.rotation;
		Quaternion rotation2 = Quaternion.Inverse(rotation);
		Vector3 normalized = (target - this.neckObj.neckTransform.position).normalized;
		Vector3 vector = rotation2 * normalized;
		float num4 = NeckLookCalc.AngleAroundAxis(this.neckObj.referenceLookDir, vector, this.neckObj.referenceUpDir);
		Vector3 axis = Vector3.Cross(this.neckObj.referenceUpDir, vector);
		Vector3 dirA = vector - Vector3.Project(vector, this.neckObj.referenceUpDir);
		float num5 = NeckLookCalc.AngleAroundAxis(dirA, vector, axis);
		float f = Mathf.Max(0f, Mathf.Abs(num4) - neckTypeState.thresholdAngleDifference) * Mathf.Sign(num4);
		float f2 = Mathf.Max(0f, Mathf.Abs(num5) - neckTypeState.thresholdAngleDifference) * Mathf.Sign(num5);
		num4 = Mathf.Max(Mathf.Abs(f) * Mathf.Abs(neckTypeState.bendingMultiplier), Mathf.Abs(num4) - neckTypeState.maxAngleDifference) * Mathf.Sign(num4) * Mathf.Sign(neckTypeState.bendingMultiplier);
		num5 = Mathf.Max(Mathf.Abs(f2) * Mathf.Abs(neckTypeState.bendingMultiplier), Mathf.Abs(num5) - neckTypeState.maxAngleDifference) * Mathf.Sign(num5) * Mathf.Sign(neckTypeState.bendingMultiplier);
		float maxBendingAngle = neckTypeState.maxBendingAngle;
		float minBendingAngle = neckTypeState.minBendingAngle;
		num4 = Mathf.Clamp(num4, minBendingAngle, maxBendingAngle);
		num5 = Mathf.Clamp(num5, neckTypeState.upBendingAngle, neckTypeState.downBendingAngle);
		Vector3 axis2 = Vector3.Cross(this.neckObj.referenceUpDir, this.neckObj.referenceLookDir);
		if (this.lookType == NECK_LOOK_TYPE.AWAY)
		{
			float num6 = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.neckTypeStates[this.nowPtnNo].maxBendingAngle, -this.neckTypeStates[this.nowPtnNo].minBendingAngle, this.neckObj.angleH));
			float num7 = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.neckTypeStates[this.nowPtnNo].maxBendingAngle, -this.neckTypeStates[this.nowPtnNo].minBendingAngle, num4));
			float num8 = num6 - num7;
			if (Mathf.Abs(num8) < this.sorasiRate)
			{
				if (num8 < 0f)
				{
					if (num7 < -this.sorasiRate)
					{
						num6 = num7 + this.sorasiRate;
					}
					else
					{
						num6 = num7 - this.sorasiRate;
					}
				}
				else if (num8 > 0f)
				{
					if (num7 > this.sorasiRate)
					{
						num6 = num7 - this.sorasiRate;
					}
					else
					{
						num6 = num7 + this.sorasiRate;
					}
				}
				else
				{
					num6 = num7 + this.sorasiRate;
				}
				num4 = Mathf.Lerp(-this.neckTypeStates[this.nowPtnNo].maxBendingAngle, -this.neckTypeStates[this.nowPtnNo].minBendingAngle, Mathf.InverseLerp(-1f, 1f, num6));
			}
			else
			{
				num4 = this.neckObj.angleH;
			}
			num5 = -num5;
		}
		this.neckObj.angleH = Mathf.Lerp(this.neckObj.angleH, num4, Time.deltaTime * neckTypeState.leapSpeed);
		this.neckObj.angleV = Mathf.Lerp(this.neckObj.angleV, num5, Time.deltaTime * neckTypeState.leapSpeed);
		vector = Quaternion.AngleAxis(this.neckObj.angleH, this.neckObj.referenceUpDir) * Quaternion.AngleAxis(this.neckObj.angleV, axis2) * this.neckObj.referenceLookDir;
		Vector3 referenceUpDir = this.neckObj.referenceUpDir;
		Vector3.OrthoNormalize(ref vector, ref referenceUpDir);
		Vector3 forward = vector;
		this.neckObj.dirUp = Vector3.Slerp(this.neckObj.dirUp, referenceUpDir, Time.deltaTime * 5f);
		Vector3.OrthoNormalize(ref forward, ref this.neckObj.dirUp);
		Quaternion lhs = rotation * Quaternion.LookRotation(forward, this.neckObj.dirUp) * Quaternion.Inverse(rotation * Quaternion.LookRotation(this.neckObj.referenceLookDir, this.neckObj.referenceUpDir));
		this.neckObj.neckTransform.rotation = lhs * this.neckObj.neckTransform.rotation;
		this.fixAngle = this.neckObj.neckTransform.localRotation;
		float t4 = Mathf.InverseLerp(0f, this.changeTypeLeapTime, this.changeTypeTimer);
		this.neckObj.neckTransform.localRotation = Quaternion.Lerp(this.fixAngleBackup, this.fixAngle, t4);
		if (this.controlObj != null)
		{
			this.controlObj.localRotation = this.fixAngle;
		}
		this.backupPos = target;
		this.AngleHRateCalc();
		this.angleVRate = this.AngleVRateCalc();
	}

	// Token: 0x06008FB0 RID: 36784 RVA: 0x003BD588 File Offset: 0x003BB988
	public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
	{
		dirA -= Vector3.Project(dirA, axis);
		dirB -= Vector3.Project(dirB, axis);
		float num = Vector3.Angle(dirA, dirB);
		return num * (float)((Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) >= 0f) ? 1 : -1);
	}

	// Token: 0x06008FB1 RID: 36785 RVA: 0x003BD5DC File Offset: 0x003BB9DC
	public void setEnable(bool setFlag)
	{
		NeckLookCalc.isEnabled = setFlag;
	}

	// Token: 0x06008FB2 RID: 36786 RVA: 0x003BD5E4 File Offset: 0x003BB9E4
	private void AngleHRateCalc()
	{
		if (this.neckObj != null)
		{
			this.angleHRate = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.neckTypeStates[this.nowPtnNo].maxBendingAngle, -this.neckTypeStates[this.nowPtnNo].minBendingAngle, this.neckObj.angleH));
		}
	}

	// Token: 0x06008FB3 RID: 36787 RVA: 0x003BD648 File Offset: 0x003BBA48
	private float AngleVRateCalc()
	{
		if (this.neckObj == null)
		{
			return 0f;
		}
		if (this.neckTypeStates[this.nowPtnNo].downBendingAngle <= this.neckTypeStates[this.nowPtnNo].upBendingAngle)
		{
			if (0f <= this.neckObj.angleV)
			{
				return -Mathf.InverseLerp(0f, this.neckTypeStates[this.nowPtnNo].upBendingAngle, this.neckObj.angleV);
			}
			return Mathf.InverseLerp(0f, this.neckTypeStates[this.nowPtnNo].downBendingAngle, this.neckObj.angleV);
		}
		else
		{
			if (0f <= this.neckObj.angleV)
			{
				return -Mathf.InverseLerp(0f, this.neckTypeStates[this.nowPtnNo].downBendingAngle, this.neckObj.angleV);
			}
			return Mathf.InverseLerp(0f, this.neckTypeStates[this.nowPtnNo].upBendingAngle, this.neckObj.angleV);
		}
	}

	// Token: 0x06008FB4 RID: 36788 RVA: 0x003BD75A File Offset: 0x003BBB5A
	public float GetAngleHRate()
	{
		return this.angleHRate;
	}

	// Token: 0x06008FB5 RID: 36789 RVA: 0x003BD762 File Offset: 0x003BBB62
	public float GetAngleVRate()
	{
		return this.angleVRate;
	}

	// Token: 0x0400745A RID: 29786
	public static bool isEnabled = true;

	// Token: 0x0400745B RID: 29787
	public Transform rootNode;

	// Token: 0x0400745C RID: 29788
	public NeckObject neckObj;

	// Token: 0x0400745D RID: 29789
	public Transform controlObj;

	// Token: 0x0400745E RID: 29790
	public Vector3 headLookVector = Vector3.forward;

	// Token: 0x0400745F RID: 29791
	public Vector3 headUpVector = Vector3.up;

	// Token: 0x04007460 RID: 29792
	public NeckTypeState[] neckTypeStates;

	// Token: 0x04007461 RID: 29793
	public float angleHRate;

	// Token: 0x04007462 RID: 29794
	public float angleVRate;

	// Token: 0x04007463 RID: 29795
	private int nowPtnNo;

	// Token: 0x04007464 RID: 29796
	private bool initEnd;

	// Token: 0x04007465 RID: 29797
	public float sorasiRate = 1f;

	// Token: 0x04007466 RID: 29798
	public Quaternion fixAngle = Quaternion.identity;

	// Token: 0x04007467 RID: 29799
	private Quaternion fixAngleBackup = Quaternion.identity;

	// Token: 0x04007468 RID: 29800
	private float changeTypeTimer;

	// Token: 0x04007469 RID: 29801
	public float changeTypeLeapTime = 1f;

	// Token: 0x0400746A RID: 29802
	private NECK_LOOK_TYPE lookType;

	// Token: 0x0400746B RID: 29803
	public Vector3 backupPos = Vector3.zero;
}
