using System;
using System.IO;
using UnityEngine;

// Token: 0x020010E0 RID: 4320
public class EyeLookCalc : MonoBehaviour
{
	// Token: 0x17001F11 RID: 7953
	// (get) Token: 0x06008F8E RID: 36750 RVA: 0x003BB405 File Offset: 0x003B9805
	// (set) Token: 0x06008F8F RID: 36751 RVA: 0x003BB40D File Offset: 0x003B980D
	public Vector3 TargetPos
	{
		get
		{
			return this.targetPos;
		}
		set
		{
			this.targetPos = value;
		}
	}

	// Token: 0x06008F90 RID: 36752 RVA: 0x003BB416 File Offset: 0x003B9816
	private void Awake()
	{
		if (!this.initEnd)
		{
			this.Init();
		}
	}

	// Token: 0x06008F91 RID: 36753 RVA: 0x003BB429 File Offset: 0x003B9829
	private void Start()
	{
		if (!this.initEnd)
		{
			this.Init();
		}
	}

	// Token: 0x06008F92 RID: 36754 RVA: 0x003BB43C File Offset: 0x003B983C
	private void OnDrawGizmos()
	{
		if (!this.isDebugDraw)
		{
			return;
		}
		if (this.rootNode)
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.8f);
			if (this.eyeTypeStates.Length > this.ptnDraw)
			{
				EyeTypeState eyeTypeState = this.eyeTypeStates[this.ptnDraw];
				Gizmos.color = new Color(0f, 1f, 1f, 0.8f);
				Vector3 vector = this.rootNode.TransformDirection(Quaternion.Euler(0f, eyeTypeState.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + this.rootNode.position;
				Gizmos.DrawLine(this.rootNode.position, vector);
				Vector3 vector2 = this.rootNode.TransformDirection(Quaternion.Euler(0f, -eyeTypeState.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + this.rootNode.position;
				Gizmos.DrawLine(this.rootNode.position, vector2);
				Vector3 vector3 = this.rootNode.TransformDirection(Quaternion.Euler(eyeTypeState.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.rootNode.position;
				Gizmos.DrawLine(this.rootNode.position, vector3);
				Vector3 vector4 = this.rootNode.TransformDirection(Quaternion.Euler(-eyeTypeState.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.rootNode.position;
				Gizmos.DrawLine(this.rootNode.position, vector4);
				Gizmos.DrawLine(vector, vector4);
				Gizmos.DrawLine(vector4, vector2);
				Gizmos.DrawLine(vector2, vector3);
				Gizmos.DrawLine(vector3, vector);
				Gizmos.color = new Color(1f, 0f, 1f, 0.8f);
				for (int i = 0; i < this.eyeObjs.Length; i++)
				{
					vector = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(0f, eyeTypeState.minBendingAngle * (float)((this.eyeObjs[i].eyeLR != EYE_LR.EYE_R) ? -1 : 1), 0f) * Vector3.forward * this.drawLineLength) + this.eyeObjs[i].eyeTransform.position;
					Gizmos.DrawLine(this.eyeObjs[i].eyeTransform.position, vector);
					vector2 = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(0f, eyeTypeState.maxBendingAngle * (float)((this.eyeObjs[i].eyeLR != EYE_LR.EYE_R) ? -1 : 1), 0f) * Vector3.forward * this.drawLineLength) + this.eyeObjs[i].eyeTransform.position;
					Gizmos.DrawLine(this.eyeObjs[i].eyeTransform.position, vector2);
					vector3 = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(eyeTypeState.upBendingAngle, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.eyeObjs[i].eyeTransform.position;
					Gizmos.DrawLine(this.eyeObjs[i].eyeTransform.position, vector3);
					vector4 = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(eyeTypeState.downBendingAngle, 0f, 0f) * Vector3.forward * this.drawLineLength) + this.eyeObjs[i].eyeTransform.position;
					Gizmos.DrawLine(this.eyeObjs[i].eyeTransform.position, vector4);
					Gizmos.DrawLine(vector, vector4);
					Gizmos.DrawLine(vector4, vector2);
					Gizmos.DrawLine(vector2, vector3);
					Gizmos.DrawLine(vector3, vector);
				}
			}
			Gizmos.color = new Color(1f, 1f, 0f, 0.8f);
			for (int j = 0; j < this.eyeObjs.Length; j++)
			{
				Gizmos.DrawLine(this.eyeObjs[j].eyeTransform.position, this.eyeObjs[j].eyeTransform.position + this.eyeObjs[j].eyeTransform.forward * this.drawLineLength);
			}
		}
		Gizmos.color = Color.white;
	}

	// Token: 0x06008F93 RID: 36755 RVA: 0x003BB924 File Offset: 0x003B9D24
	public void Init()
	{
		if (this.rootNode == null)
		{
			this.rootNode = base.transform;
		}
		foreach (EyeObject eyeObject in this.eyeObjs)
		{
			if (!(eyeObject.eyeTransform == null))
			{
				Quaternion rotation = eyeObject.eyeTransform.parent.rotation;
				Quaternion lhs = Quaternion.Inverse(rotation);
				eyeObject.referenceLookDir = lhs * this.rootNode.rotation * this.headLookVector.normalized;
				eyeObject.referenceUpDir = lhs * this.rootNode.rotation * this.headUpVector.normalized;
				eyeObject.angleH = 0f;
				eyeObject.angleV = 0f;
				eyeObject.dirUp = eyeObject.referenceUpDir;
				eyeObject.origRotation = default(Quaternion);
				eyeObject.origRotation = eyeObject.eyeTransform.localRotation;
				this.angleHRate = new float[2];
			}
		}
		this.initEnd = true;
	}

	// Token: 0x06008F94 RID: 36756 RVA: 0x003BBA44 File Offset: 0x003B9E44
	public void EyeUpdateCalc(Vector3 target, int ptnNo)
	{
		if (!this.initEnd)
		{
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		this.nowPtnNo = ptnNo;
		if (!EyeLookCalc.isEnabled)
		{
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		if (Time.deltaTime == 0f)
		{
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		EyeTypeState eyeTypeState = this.eyeTypeStates[ptnNo];
		EYE_LOOK_TYPE eye_LOOK_TYPE = this.eyeTypeStates[ptnNo].lookType;
		if (eye_LOOK_TYPE == EYE_LOOK_TYPE.NO_LOOK)
		{
			this.eyeObjs[0].eyeTransform.localRotation = this.fixAngle[0];
			this.eyeObjs[1].eyeTransform.localRotation = this.fixAngle[1];
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		Vector3 position = this.rootNode.InverseTransformPoint(target);
		float magnitude = position.magnitude;
		if (magnitude < this.eyeTypeStates[ptnNo].nearDis)
		{
			position = position.normalized * this.eyeTypeStates[ptnNo].nearDis;
			target = this.rootNode.TransformPoint(position);
		}
		Vector3 vector = new Vector3(position.x, 0f, position.z);
		float num = Vector3.Dot(vector, Vector3.forward);
		float num2 = Vector3.Angle(vector, Vector3.forward);
		vector = new Vector3(0f, position.y, position.z);
		float num3 = Vector3.Dot(vector, Vector3.forward);
		float num4 = Vector3.Angle(vector, Vector3.forward);
		if (num < 0f || num3 < 0f || num2 > this.eyeTypeStates[ptnNo].hAngleLimit || num4 > this.eyeTypeStates[ptnNo].vAngleLimit)
		{
			eye_LOOK_TYPE = EYE_LOOK_TYPE.FORWARD;
		}
		if (eye_LOOK_TYPE == EYE_LOOK_TYPE.FORWARD)
		{
			target = this.rootNode.position + this.rootNode.forward * this.eyeTypeStates[ptnNo].forntTagDis;
		}
		if (eye_LOOK_TYPE == EYE_LOOK_TYPE.CONTROL || this.eyeTypeStates[ptnNo].lookType == EYE_LOOK_TYPE.CONTROL)
		{
			if (this.targetObj != null)
			{
				if (!this.targetObj.activeSelf)
				{
					this.targetObj.SetActive(true);
				}
				target = Vector3.MoveTowards(this.rootNode.transform.position, this.targetObj.transform.position, this.eyeTypeStates[ptnNo].forntTagDis);
				this.targetObj.transform.position = Vector3.MoveTowards(this.rootNode.transform.position, target, 0.5f);
			}
		}
		else if (this.targetObj != null)
		{
			this.targetObj.transform.position = Vector3.MoveTowards(this.rootNode.transform.position, target, 0.5f);
			if (this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
		}
		float num5 = -1f;
		foreach (EyeObject eyeObject in this.eyeObjs)
		{
			eyeObject.eyeTransform.localRotation = eyeObject.origRotation;
			Quaternion rotation = eyeObject.eyeTransform.parent.rotation;
			Quaternion rotation2 = Quaternion.Inverse(rotation);
			Vector3 normalized = (target - eyeObject.eyeTransform.position).normalized;
			Vector3 vector2 = rotation2 * normalized;
			float num6 = EyeLookCalc.AngleAroundAxis(eyeObject.referenceLookDir, vector2, eyeObject.referenceUpDir);
			Vector3 axis = Vector3.Cross(eyeObject.referenceUpDir, vector2);
			Vector3 dirA = vector2 - Vector3.Project(vector2, eyeObject.referenceUpDir);
			float num7 = EyeLookCalc.AngleAroundAxis(dirA, vector2, axis);
			float f = Mathf.Max(0f, Mathf.Abs(num6) - eyeTypeState.thresholdAngleDifference) * Mathf.Sign(num6);
			float f2 = Mathf.Max(0f, Mathf.Abs(num7) - eyeTypeState.thresholdAngleDifference) * Mathf.Sign(num7);
			num6 = Mathf.Max(Mathf.Abs(f) * Mathf.Abs(eyeTypeState.bendingMultiplier), Mathf.Abs(num6) - eyeTypeState.maxAngleDifference) * Mathf.Sign(num6) * Mathf.Sign(eyeTypeState.bendingMultiplier);
			num7 = Mathf.Max(Mathf.Abs(f2) * Mathf.Abs(eyeTypeState.bendingMultiplier), Mathf.Abs(num7) - eyeTypeState.maxAngleDifference) * Mathf.Sign(num7) * Mathf.Sign(eyeTypeState.bendingMultiplier);
			float max = eyeTypeState.maxBendingAngle;
			float min = eyeTypeState.minBendingAngle;
			if (eyeObject.eyeLR == EYE_LR.EYE_R)
			{
				max = -eyeTypeState.minBendingAngle;
				min = -eyeTypeState.maxBendingAngle;
			}
			num6 = Mathf.Clamp(num6, min, max);
			num7 = Mathf.Clamp(num7, eyeTypeState.upBendingAngle, eyeTypeState.downBendingAngle);
			Vector3 axis2 = Vector3.Cross(eyeObject.referenceUpDir, eyeObject.referenceLookDir);
			if (eye_LOOK_TYPE == EYE_LOOK_TYPE.AWAY)
			{
				if (num5 == -1f)
				{
					float num8 = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.eyeTypeStates[this.nowPtnNo].maxBendingAngle, -this.eyeTypeStates[this.nowPtnNo].minBendingAngle, eyeObject.angleH));
					float num9 = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.eyeTypeStates[this.nowPtnNo].maxBendingAngle, -this.eyeTypeStates[this.nowPtnNo].minBendingAngle, num6));
					float num10 = num8 - num9;
					if (Mathf.Abs(num10) < this.sorasiRate)
					{
						if (num10 < 0f)
						{
							if (num9 < -this.sorasiRate)
							{
								num8 = num9 + this.sorasiRate;
							}
							else
							{
								num8 = num9 - this.sorasiRate;
							}
						}
						else if (num10 > 0f)
						{
							if (num9 > this.sorasiRate)
							{
								num8 = num9 - this.sorasiRate;
							}
							else
							{
								num8 = num9 + this.sorasiRate;
							}
						}
						else
						{
							num8 = num9 + this.sorasiRate;
						}
						num5 = Mathf.InverseLerp(-1f, 1f, num8);
						num6 = Mathf.Lerp(-this.eyeTypeStates[this.nowPtnNo].maxBendingAngle, -this.eyeTypeStates[this.nowPtnNo].minBendingAngle, num5);
					}
					else
					{
						num5 = Mathf.InverseLerp(-1f, 1f, num8);
						num6 = eyeObject.angleH;
					}
				}
				else
				{
					num6 = Mathf.Lerp(-this.eyeTypeStates[this.nowPtnNo].maxBendingAngle, -this.eyeTypeStates[this.nowPtnNo].minBendingAngle, num5);
				}
				num7 = -num7;
			}
			eyeObject.angleH = Mathf.Lerp(eyeObject.angleH, num6, Time.deltaTime * eyeTypeState.leapSpeed);
			eyeObject.angleV = Mathf.Lerp(eyeObject.angleV, num7, Time.deltaTime * eyeTypeState.leapSpeed);
			vector2 = Quaternion.AngleAxis(eyeObject.angleH, eyeObject.referenceUpDir) * Quaternion.AngleAxis(eyeObject.angleV, axis2) * eyeObject.referenceLookDir;
			Vector3 referenceUpDir = eyeObject.referenceUpDir;
			Vector3.OrthoNormalize(ref vector2, ref referenceUpDir);
			Vector3 forward = vector2;
			eyeObject.dirUp = Vector3.Slerp(eyeObject.dirUp, referenceUpDir, Time.deltaTime * 5f);
			Vector3.OrthoNormalize(ref forward, ref eyeObject.dirUp);
			Quaternion lhs = rotation * Quaternion.LookRotation(forward, eyeObject.dirUp) * Quaternion.Inverse(rotation * Quaternion.LookRotation(eyeObject.referenceLookDir, eyeObject.referenceUpDir));
			eyeObject.eyeTransform.rotation = lhs * eyeObject.eyeTransform.rotation;
		}
		this.targetPos = target;
		this.fixAngle[0] = this.eyeObjs[0].eyeTransform.localRotation;
		this.fixAngle[1] = this.eyeObjs[1].eyeTransform.localRotation;
		this.AngleHRateCalc();
		this.angleVRate = this.AngleVRateCalc();
	}

	// Token: 0x06008F95 RID: 36757 RVA: 0x003BC300 File Offset: 0x003BA700
	public static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
	{
		dirA -= Vector3.Project(dirA, axis);
		dirB -= Vector3.Project(dirB, axis);
		float num = Vector3.Angle(dirA, dirB);
		return num * (float)((Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) >= 0f) ? 1 : -1);
	}

	// Token: 0x06008F96 RID: 36758 RVA: 0x003BC354 File Offset: 0x003BA754
	public void setEnable(bool setFlag)
	{
		EyeLookCalc.isEnabled = setFlag;
	}

	// Token: 0x06008F97 RID: 36759 RVA: 0x003BC35C File Offset: 0x003BA75C
	private void AngleHRateCalc()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.eyeObjs[i] != null)
			{
				if (this.eyeObjs[i].eyeLR == EYE_LR.EYE_R)
				{
					this.angleHRate[i] = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.eyeTypeStates[this.nowPtnNo].maxBendingAngle, -this.eyeTypeStates[this.nowPtnNo].minBendingAngle, this.eyeObjs[i].angleH));
				}
				else
				{
					this.angleHRate[i] = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(this.eyeTypeStates[this.nowPtnNo].minBendingAngle, this.eyeTypeStates[this.nowPtnNo].maxBendingAngle, this.eyeObjs[i].angleH));
				}
			}
		}
	}

	// Token: 0x06008F98 RID: 36760 RVA: 0x003BC43C File Offset: 0x003BA83C
	private float AngleVRateCalc()
	{
		if (this.eyeObjs[0] == null)
		{
			return 0f;
		}
		if (this.eyeTypeStates[this.nowPtnNo].downBendingAngle <= this.eyeTypeStates[this.nowPtnNo].upBendingAngle)
		{
			if (0f <= this.eyeObjs[0].angleV)
			{
				return -Mathf.InverseLerp(0f, this.eyeTypeStates[this.nowPtnNo].upBendingAngle, this.eyeObjs[0].angleV);
			}
			return Mathf.InverseLerp(0f, this.eyeTypeStates[this.nowPtnNo].downBendingAngle, this.eyeObjs[0].angleV);
		}
		else
		{
			if (0f <= this.eyeObjs[0].angleV)
			{
				return -Mathf.InverseLerp(0f, this.eyeTypeStates[this.nowPtnNo].downBendingAngle, this.eyeObjs[0].angleV);
			}
			return Mathf.InverseLerp(0f, this.eyeTypeStates[this.nowPtnNo].upBendingAngle, this.eyeObjs[0].angleV);
		}
	}

	// Token: 0x06008F99 RID: 36761 RVA: 0x003BC55C File Offset: 0x003BA95C
	public float GetAngleHRate(EYE_LR eyeLR)
	{
		if (eyeLR == EYE_LR.EYE_L)
		{
			return this.angleHRate[0];
		}
		return this.angleHRate[1];
	}

	// Token: 0x06008F9A RID: 36762 RVA: 0x003BC575 File Offset: 0x003BA975
	public float GetAngleVRate()
	{
		return this.angleVRate;
	}

	// Token: 0x06008F9B RID: 36763 RVA: 0x003BC580 File Offset: 0x003BA980
	public void SaveAngle(BinaryWriter writer)
	{
		this.fixAngle[0] = this.eyeObjs[0].eyeTransform.localRotation;
		this.fixAngle[1] = this.eyeObjs[1].eyeTransform.localRotation;
		writer.Write(this.fixAngle[0].x);
		writer.Write(this.fixAngle[0].y);
		writer.Write(this.fixAngle[0].z);
		writer.Write(this.fixAngle[0].w);
		writer.Write(this.fixAngle[1].x);
		writer.Write(this.fixAngle[1].y);
		writer.Write(this.fixAngle[1].z);
		writer.Write(this.fixAngle[1].w);
	}

	// Token: 0x06008F9C RID: 36764 RVA: 0x003BC68C File Offset: 0x003BAA8C
	public void LoadAngle(BinaryReader reader)
	{
		this.fixAngle[0] = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		this.fixAngle[1] = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		this.eyeObjs[0].eyeTransform.localRotation = this.fixAngle[0];
		this.eyeObjs[1].eyeTransform.localRotation = this.fixAngle[1];
	}

	// Token: 0x0400741A RID: 29722
	public static bool isEnabled = true;

	// Token: 0x0400741B RID: 29723
	public Transform rootNode;

	// Token: 0x0400741C RID: 29724
	public EyeObject[] eyeObjs;

	// Token: 0x0400741D RID: 29725
	public Vector3 headLookVector = Vector3.forward;

	// Token: 0x0400741E RID: 29726
	public Vector3 headUpVector = Vector3.up;

	// Token: 0x0400741F RID: 29727
	public EyeTypeState[] eyeTypeStates;

	// Token: 0x04007420 RID: 29728
	public float[] angleHRate;

	// Token: 0x04007421 RID: 29729
	public float angleVRate;

	// Token: 0x04007422 RID: 29730
	public float sorasiRate = 1f;

	// Token: 0x04007423 RID: 29731
	public bool isDebugDraw = true;

	// Token: 0x04007424 RID: 29732
	public int ptnDraw;

	// Token: 0x04007425 RID: 29733
	public float drawLineLength = 1f;

	// Token: 0x04007426 RID: 29734
	private int nowPtnNo;

	// Token: 0x04007427 RID: 29735
	private bool initEnd;

	// Token: 0x04007428 RID: 29736
	public GameObject targetObj;

	// Token: 0x04007429 RID: 29737
	private Vector3 targetPos = Vector3.zero;

	// Token: 0x0400742A RID: 29738
	public Quaternion[] fixAngle = new Quaternion[2];
}
