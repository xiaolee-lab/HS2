using System;
using System.IO;
using UnityEngine;

// Token: 0x02000BA1 RID: 2977
public class EyeLookControllerVer2 : MonoBehaviour
{
	// Token: 0x17001077 RID: 4215
	// (get) Token: 0x060058D0 RID: 22736 RVA: 0x002615BD File Offset: 0x0025F9BD
	// (set) Token: 0x060058D1 RID: 22737 RVA: 0x002615C5 File Offset: 0x0025F9C5
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

	// Token: 0x060058D2 RID: 22738 RVA: 0x002615CE File Offset: 0x0025F9CE
	private void Awake()
	{
	}

	// Token: 0x060058D3 RID: 22739 RVA: 0x002615D0 File Offset: 0x0025F9D0
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060058D4 RID: 22740 RVA: 0x002615D8 File Offset: 0x0025F9D8
	private void Start()
	{
		if (!this.target && Camera.main)
		{
			this.target = Camera.main.transform;
		}
		this.Init();
	}

	// Token: 0x060058D5 RID: 22741 RVA: 0x00261610 File Offset: 0x0025FA10
	private void OnDrawGizmos()
	{
		Transform transform = this.calcRootNode ?? this.objRootNode;
		if (!this.isDebugDraw || !transform)
		{
			Gizmos.color = Color.white;
			return;
		}
		if (0 < this.eyeTypeStates.Length)
		{
			if (this.ptnDraw < 0)
			{
				this.ptnDraw = 0;
			}
			else if (this.eyeTypeStates.Length <= this.ptnDraw)
			{
				this.ptnDraw = this.eyeTypeStates.Length - 1;
			}
			Gizmos.color = new Color(1f, 1f, 1f, 0.8f);
			EyeTypeState_Ver2 eyeTypeState_Ver = this.eyeTypeStates[this.ptnDraw];
			Gizmos.color = new Color(0f, 1f, 1f, 0.8f);
			Vector3 position = transform.position;
			Vector3 vector = transform.TransformDirection(Quaternion.Euler(0f, eyeTypeState_Ver.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + position;
			Vector3 vector2 = transform.TransformDirection(Quaternion.Euler(0f, -eyeTypeState_Ver.hAngleLimit, 0f) * Vector3.forward * this.drawLineLength) + position;
			Vector3 vector3 = transform.TransformDirection(Quaternion.Euler(eyeTypeState_Ver.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
			Vector3 vector4 = transform.TransformDirection(Quaternion.Euler(-eyeTypeState_Ver.vAngleLimit, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
			Gizmos.DrawLine(position, vector);
			Gizmos.DrawLine(position, vector2);
			Gizmos.DrawLine(position, vector3);
			Gizmos.DrawLine(position, vector4);
			Gizmos.DrawLine(vector, vector4);
			Gizmos.DrawLine(vector4, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector);
			Gizmos.color = new Color(1f, 0f, 1f, 0.8f);
			for (int i = 0; i < this.eyeObjs.Length; i++)
			{
				position = this.eyeObjs[i].eyeTransform.position;
				vector = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(0f, eyeTypeState_Ver.inBendingAngle * (float)this.eyeObjs[i].eyeLR, 0f) * Vector3.forward * this.drawLineLength) + position;
				vector2 = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(0f, eyeTypeState_Ver.outBendingAngle * (float)this.eyeObjs[i].eyeLR, 0f) * Vector3.forward * this.drawLineLength) + position;
				vector3 = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(eyeTypeState_Ver.upBendingAngle, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
				vector4 = this.eyeObjs[i].eyeTransform.TransformDirection(Quaternion.Euler(eyeTypeState_Ver.downBendingAngle, 0f, 0f) * Vector3.forward * this.drawLineLength) + position;
				Gizmos.DrawLine(position, vector);
				Gizmos.DrawLine(position, vector2);
				Gizmos.DrawLine(position, vector3);
				Gizmos.DrawLine(position, vector4);
				Gizmos.DrawLine(vector, vector4);
				Gizmos.DrawLine(vector4, vector2);
				Gizmos.DrawLine(vector2, vector3);
				Gizmos.DrawLine(vector3, vector);
			}
		}
		Gizmos.color = new Color(1f, 1f, 0f, 0.8f);
		for (int j = 0; j < this.eyeObjs.Length; j++)
		{
			Vector3 position2 = this.eyeObjs[j].eyeTransform.position;
			Gizmos.DrawLine(position2, position2 + this.eyeObjs[j].eyeTransform.forward * this.drawLineLength);
		}
		Gizmos.color = Color.white;
	}

	// Token: 0x060058D6 RID: 22742 RVA: 0x00261A61 File Offset: 0x0025FE61
	private void LateUpdate()
	{
		if (!this.target)
		{
			return;
		}
		this.EyeUpdateCalc(this.target.position, this.ptnNo);
	}

	// Token: 0x060058D7 RID: 22743 RVA: 0x00261A8C File Offset: 0x0025FE8C
	private void Init()
	{
		if (this.initEnd)
		{
			return;
		}
		if (this.calcRootNode == null)
		{
			this.calcRootNode = base.transform;
		}
		if (this.objRootNode == null && this.calcRootNode != base.transform && this.calcRootNode != base.transform.parent)
		{
			this.objRootNode = (base.transform.parent ?? base.transform);
		}
		Quaternion rotation = Quaternion.identity;
		if (this.objRootNode)
		{
			rotation = this.objRootNode.rotation;
			this.objRootNode.rotation = Quaternion.identity;
		}
		this.originRootNode = (this.calcRootNode ?? this.objRootNode);
		if (this.calcRootNode != null)
		{
			this.calcRootNodeRef = new GameObject(this.calcRootNode.name + "(EyeRef)")
			{
				transform = 
				{
					parent = this.calcRootNode.parent,
					localPosition = this.calcRootNode.localPosition,
					rotation = Quaternion.identity
				}
			}.transform;
		}
		foreach (EyeObject_Ver2 eyeObject_Ver in this.eyeObjs)
		{
			Quaternion rotation2 = eyeObject_Ver.eyeTransform.parent.rotation;
			eyeObject_Ver.parentFirstWorldRotation = rotation2;
			eyeObject_Ver.parentFirstLocalRotation = eyeObject_Ver.eyeTransform.parent.localRotation;
			Quaternion lhs = Quaternion.Inverse(rotation2);
			Transform transform = this.calcRootNodeRef ?? this.calcRootNode;
			eyeObject_Ver.referenceLookDir = lhs * transform.rotation * this.headLookVector.normalized;
			eyeObject_Ver.referenceUpDir = lhs * transform.rotation * this.headUpVector.normalized;
			eyeObject_Ver.angleH = 0f;
			eyeObject_Ver.angleV = 0f;
			eyeObject_Ver.dirUp = eyeObject_Ver.referenceUpDir;
			eyeObject_Ver.origRotation = default(Quaternion);
			eyeObject_Ver.origRotation = eyeObject_Ver.eyeTransform.localRotation;
			this.angleHRate = new float[2];
		}
		if (this.objRootNode)
		{
			this.objRootNode.rotation = rotation;
		}
		this.initEnd = true;
	}

	// Token: 0x060058D8 RID: 22744 RVA: 0x00261D10 File Offset: 0x00260110
	private void EyeUpdateCalc(Vector3 _target, int _ptnNo)
	{
		if (!this.initEnd)
		{
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		this.nowPtnNo = _ptnNo;
		if (!EyeLookControllerVer2.isEnabled || Time.deltaTime == 0f)
		{
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		EyeTypeState_Ver2 eyeTypeState_Ver = this.eyeTypeStates[_ptnNo];
		EYE_LOOK_TYPE_VER2 eye_LOOK_TYPE_VER = this.eyeTypeStates[_ptnNo].lookType;
		Transform transform = this.calcRootNodeRef;
		if (eye_LOOK_TYPE_VER == EYE_LOOK_TYPE_VER2.NO_LOOK)
		{
			this.eyeObjs[0].eyeTransform.localRotation = this.fixAngle[0];
			this.eyeObjs[1].eyeTransform.localRotation = this.fixAngle[1];
			if (this.targetObj != null && this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
			return;
		}
		Vector3 position = transform.InverseTransformPoint(_target);
		float magnitude = position.magnitude;
		if (magnitude < this.eyeTypeStates[_ptnNo].nearDis)
		{
			position = position.normalized * this.eyeTypeStates[_ptnNo].nearDis;
			_target = transform.TransformPoint(position);
		}
		Vector3 vector = new Vector3(position.x, 0f, position.z);
		float num = Vector3.Dot(vector, Vector3.forward);
		float num2 = Vector3.Angle(vector, Vector3.forward);
		vector = new Vector3(0f, position.y, position.z);
		float num3 = Vector3.Dot(vector, Vector3.forward);
		float num4 = Vector3.Angle(vector, Vector3.forward);
		if (num < 0f || num3 < 0f || this.eyeTypeStates[_ptnNo].hAngleLimit < num2 || this.eyeTypeStates[_ptnNo].vAngleLimit < num4)
		{
			eye_LOOK_TYPE_VER = EYE_LOOK_TYPE_VER2.FORWARD;
		}
		if (eye_LOOK_TYPE_VER == EYE_LOOK_TYPE_VER2.FORWARD)
		{
			_target = transform.position + this.originRootNode.forward * this.eyeTypeStates[_ptnNo].frontTagDis;
		}
		if (eye_LOOK_TYPE_VER == EYE_LOOK_TYPE_VER2.CONTROL || this.eyeTypeStates[_ptnNo].lookType == EYE_LOOK_TYPE_VER2.CONTROL)
		{
			if (this.targetObj != null)
			{
				if (!this.targetObj.activeSelf)
				{
					this.targetObj.SetActive(true);
				}
				_target = Vector3.MoveTowards(transform.transform.position, this.targetObj.transform.position, this.eyeTypeStates[_ptnNo].frontTagDis);
				this.targetObj.transform.position = Vector3.MoveTowards(transform.transform.position, _target, 0.5f);
			}
		}
		else if (this.targetObj != null)
		{
			this.targetObj.transform.position = Vector3.MoveTowards(transform.transform.position, _target, 0.5f);
			if (this.targetObj.activeSelf)
			{
				this.targetObj.SetActive(false);
			}
		}
		float num5 = -1f;
		foreach (EyeObject_Ver2 eyeObject_Ver in this.eyeObjs)
		{
			eyeObject_Ver.eyeTransform.localRotation = eyeObject_Ver.origRotation;
			Quaternion rotation = eyeObject_Ver.eyeTransform.parent.rotation;
			Quaternion rotation2 = Quaternion.Inverse(rotation);
			Vector3 normalized = (_target - eyeObject_Ver.eyeTransform.position).normalized;
			Vector3 vector2 = rotation2 * normalized;
			float num6 = this.AngleAroundAxis(eyeObject_Ver.referenceLookDir, vector2, eyeObject_Ver.referenceUpDir);
			Vector3 axis = Vector3.Cross(eyeObject_Ver.referenceUpDir, vector2);
			Vector3 dirA = vector2 - Vector3.Project(vector2, eyeObject_Ver.referenceUpDir);
			float num7 = this.AngleAroundAxis(dirA, vector2, axis);
			float f = Mathf.Max(0f, Mathf.Abs(num6) - eyeTypeState_Ver.thresholdAngleDifference) * Mathf.Sign(num6);
			float f2 = Mathf.Max(0f, Mathf.Abs(num7) - eyeTypeState_Ver.thresholdAngleDifference) * Mathf.Sign(num7);
			num6 = Mathf.Max(Mathf.Abs(f) * Mathf.Abs(eyeTypeState_Ver.bendingMultiplier), Mathf.Abs(num6) - eyeTypeState_Ver.maxAngleDifference) * Mathf.Sign(num6) * Mathf.Sign(eyeTypeState_Ver.bendingMultiplier);
			num7 = Mathf.Max(Mathf.Abs(f2) * Mathf.Abs(eyeTypeState_Ver.bendingMultiplier), Mathf.Abs(num7) - eyeTypeState_Ver.maxAngleDifference) * Mathf.Sign(num7) * Mathf.Sign(eyeTypeState_Ver.bendingMultiplier);
			float max = (eyeObject_Ver.eyeLR != EYE_LR_VER2.EYE_L) ? (-eyeTypeState_Ver.inBendingAngle) : eyeTypeState_Ver.outBendingAngle;
			float min = (eyeObject_Ver.eyeLR != EYE_LR_VER2.EYE_L) ? (-eyeTypeState_Ver.outBendingAngle) : eyeTypeState_Ver.inBendingAngle;
			num6 = Mathf.Clamp(num6, min, max);
			num7 = Mathf.Clamp(num7, eyeTypeState_Ver.upBendingAngle, eyeTypeState_Ver.downBendingAngle);
			Vector3 axis2 = Vector3.Cross(eyeObject_Ver.referenceUpDir, eyeObject_Ver.referenceLookDir);
			if (eye_LOOK_TYPE_VER == EYE_LOOK_TYPE_VER2.AWAY)
			{
				if (num5 == -1f)
				{
					float num8 = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.eyeTypeStates[this.nowPtnNo].outBendingAngle, -this.eyeTypeStates[this.nowPtnNo].inBendingAngle, eyeObject_Ver.angleH));
					float num9 = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.eyeTypeStates[this.nowPtnNo].outBendingAngle, -this.eyeTypeStates[this.nowPtnNo].inBendingAngle, num6));
					float num10 = num8 - num9;
					if (Mathf.Abs(num10) < this.awayRate)
					{
						if (num10 < 0f)
						{
							if (num9 < -this.awayRate)
							{
								num8 = num9 + this.awayRate;
							}
							else
							{
								num8 = num9 - this.awayRate;
							}
						}
						else if (0f < num10)
						{
							if (this.awayRate < num9)
							{
								num8 = num9 - this.awayRate;
							}
							else
							{
								num8 = num9 + this.awayRate;
							}
						}
						else
						{
							num8 = num9 + this.awayRate;
						}
						num5 = Mathf.InverseLerp(-1f, 1f, num8);
						num6 = Mathf.Lerp(-this.eyeTypeStates[this.nowPtnNo].outBendingAngle, -this.eyeTypeStates[this.nowPtnNo].inBendingAngle, num5);
					}
					else
					{
						num5 = Mathf.InverseLerp(-1f, 1f, num8);
						num6 = eyeObject_Ver.angleH;
					}
				}
				else
				{
					num6 = Mathf.Lerp(-this.eyeTypeStates[this.nowPtnNo].outBendingAngle, -this.eyeTypeStates[this.nowPtnNo].inBendingAngle, num5);
				}
				num7 = -num7;
			}
			eyeObject_Ver.angleH = Mathf.Lerp(eyeObject_Ver.angleH, num6, Time.deltaTime * eyeTypeState_Ver.leapSpeed);
			eyeObject_Ver.angleV = Mathf.Lerp(eyeObject_Ver.angleV, num7, Time.deltaTime * eyeTypeState_Ver.leapSpeed);
			vector2 = Quaternion.AngleAxis(eyeObject_Ver.angleH, eyeObject_Ver.referenceUpDir) * Quaternion.AngleAxis(eyeObject_Ver.angleV, axis2) * eyeObject_Ver.referenceLookDir;
			Vector3 referenceUpDir = eyeObject_Ver.referenceUpDir;
			Vector3.OrthoNormalize(ref vector2, ref referenceUpDir);
			Vector3 forward = vector2;
			eyeObject_Ver.dirUp = Vector3.Slerp(eyeObject_Ver.dirUp, referenceUpDir, Time.deltaTime);
			Vector3.OrthoNormalize(ref forward, ref eyeObject_Ver.dirUp);
			Quaternion lhs = rotation * Quaternion.LookRotation(forward, eyeObject_Ver.dirUp) * Quaternion.Inverse(rotation * Quaternion.LookRotation(eyeObject_Ver.referenceLookDir, eyeObject_Ver.referenceUpDir));
			eyeObject_Ver.eyeTransform.rotation = lhs * eyeObject_Ver.eyeTransform.rotation;
		}
		this.targetPos = _target;
		this.fixAngle[0] = this.eyeObjs[0].eyeTransform.localRotation;
		this.fixAngle[1] = this.eyeObjs[1].eyeTransform.localRotation;
		this.AngleHRateCalc();
		this.angleVRate = this.AngleVRateCalc();
	}

	// Token: 0x060058D9 RID: 22745 RVA: 0x00262594 File Offset: 0x00260994
	public float AngleAroundAxis(Vector3 _dirA, Vector3 _dirB, Vector3 _axis)
	{
		_dirA -= Vector3.Project(_dirA, _axis);
		_dirB -= Vector3.Project(_dirB, _axis);
		float num = Vector3.Angle(_dirA, _dirB);
		return num * (float)((Vector3.Dot(_axis, Vector3.Cross(_dirA, _dirB)) >= 0f) ? 1 : -1);
	}

	// Token: 0x060058DA RID: 22746 RVA: 0x002625E8 File Offset: 0x002609E8
	public void SetEnable(bool _setFlag)
	{
		EyeLookControllerVer2.isEnabled = _setFlag;
	}

	// Token: 0x060058DB RID: 22747 RVA: 0x002625F0 File Offset: 0x002609F0
	private void AngleHRateCalc()
	{
		for (int i = 0; i < 2; i++)
		{
			if (this.eyeObjs[i] != null)
			{
				if (this.eyeObjs[i].eyeLR == EYE_LR_VER2.EYE_R)
				{
					this.angleHRate[i] = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(-this.eyeTypeStates[this.nowPtnNo].outBendingAngle, -this.eyeTypeStates[this.nowPtnNo].inBendingAngle, this.eyeObjs[i].angleH));
				}
				else
				{
					this.angleHRate[i] = Mathf.Lerp(-1f, 1f, Mathf.InverseLerp(this.eyeTypeStates[this.nowPtnNo].inBendingAngle, this.eyeTypeStates[this.nowPtnNo].outBendingAngle, this.eyeObjs[i].angleH));
				}
			}
		}
	}

	// Token: 0x060058DC RID: 22748 RVA: 0x002626D8 File Offset: 0x00260AD8
	private float AngleVRateCalc()
	{
		if (this.eyeObjs[0] == null)
		{
			return 0f;
		}
		EyeTypeState_Ver2 eyeTypeState_Ver = this.eyeTypeStates[this.nowPtnNo];
		if (eyeTypeState_Ver.downBendingAngle <= eyeTypeState_Ver.upBendingAngle)
		{
			if (0f <= this.eyeObjs[0].angleV)
			{
				return -Mathf.InverseLerp(0f, eyeTypeState_Ver.upBendingAngle, this.eyeObjs[0].angleV);
			}
			return Mathf.InverseLerp(0f, eyeTypeState_Ver.downBendingAngle, this.eyeObjs[0].angleV);
		}
		else
		{
			if (0f <= this.eyeObjs[0].angleV)
			{
				return -Mathf.InverseLerp(0f, eyeTypeState_Ver.downBendingAngle, this.eyeObjs[0].angleV);
			}
			return Mathf.InverseLerp(0f, eyeTypeState_Ver.upBendingAngle, this.eyeObjs[0].angleV);
		}
	}

	// Token: 0x060058DD RID: 22749 RVA: 0x002627BE File Offset: 0x00260BBE
	public float GetAngleHRate(EYE_LR_VER2 _eyeLR)
	{
		return this.angleHRate[(_eyeLR != EYE_LR_VER2.EYE_L) ? 1 : 0];
	}

	// Token: 0x060058DE RID: 22750 RVA: 0x002627D5 File Offset: 0x00260BD5
	public float GetAngleVRate()
	{
		return this.angleVRate;
	}

	// Token: 0x060058DF RID: 22751 RVA: 0x002627E0 File Offset: 0x00260BE0
	public void SaveAngle(BinaryWriter _writer)
	{
		int num = 0;
		while (num < 2 && num < this.eyeObjs.Length)
		{
			this.fixAngle[num] = this.eyeObjs[num].eyeTransform.localRotation;
			_writer.Write(this.fixAngle[num].x);
			_writer.Write(this.fixAngle[num].y);
			_writer.Write(this.fixAngle[num].z);
			_writer.Write(this.fixAngle[num].w);
			num++;
		}
	}

	// Token: 0x060058E0 RID: 22752 RVA: 0x0026288C File Offset: 0x00260C8C
	public void LoadAngle(BinaryReader _reader)
	{
		int num = 0;
		while (num < 2 && num < this.eyeObjs.Length)
		{
			this.fixAngle[num] = new Quaternion(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
			this.eyeObjs[num].eyeTransform.localRotation = this.fixAngle[num];
			num++;
		}
	}

	// Token: 0x04005164 RID: 20836
	public int ptnNo;

	// Token: 0x04005165 RID: 20837
	public Transform target;

	// Token: 0x04005166 RID: 20838
	public static bool isEnabled = true;

	// Token: 0x04005167 RID: 20839
	public Transform calcRootNode;

	// Token: 0x04005168 RID: 20840
	private Transform calcRootNodeRef;

	// Token: 0x04005169 RID: 20841
	public Transform objRootNode;

	// Token: 0x0400516A RID: 20842
	public EyeObject_Ver2[] eyeObjs;

	// Token: 0x0400516B RID: 20843
	public Vector3 headLookVector = Vector3.forward;

	// Token: 0x0400516C RID: 20844
	public Vector3 headUpVector = Vector3.up;

	// Token: 0x0400516D RID: 20845
	public EyeTypeState_Ver2[] eyeTypeStates;

	// Token: 0x0400516E RID: 20846
	public float[] angleHRate;

	// Token: 0x0400516F RID: 20847
	public float angleVRate;

	// Token: 0x04005170 RID: 20848
	public float awayRate = 1f;

	// Token: 0x04005171 RID: 20849
	public bool isDebugDraw = true;

	// Token: 0x04005172 RID: 20850
	public int ptnDraw;

	// Token: 0x04005173 RID: 20851
	public float drawLineLength = 1f;

	// Token: 0x04005174 RID: 20852
	private int nowPtnNo;

	// Token: 0x04005175 RID: 20853
	private bool initEnd;

	// Token: 0x04005176 RID: 20854
	public GameObject targetObj;

	// Token: 0x04005177 RID: 20855
	private Transform originRootNode;

	// Token: 0x04005178 RID: 20856
	private Vector3 targetPos = Vector3.zero;

	// Token: 0x04005179 RID: 20857
	public Quaternion[] fixAngle = new Quaternion[2];
}
