using System;
using UnityEngine;

// Token: 0x02000A93 RID: 2707
[Serializable]
public class H_LookAtDan_Info
{
	// Token: 0x06004FC7 RID: 20423 RVA: 0x001EB674 File Offset: 0x001E9A74
	public H_LookAtDan_Info()
	{
		this.trfLookAt = null;
		this.trfTarget = null;
		this.trfUpAxis = null;
	}

	// Token: 0x17000ED9 RID: 3801
	// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x001EB6EB File Offset: 0x001E9AEB
	// (set) Token: 0x06004FC9 RID: 20425 RVA: 0x001EB6F3 File Offset: 0x001E9AF3
	public Transform trfLookAt { get; private set; }

	// Token: 0x17000EDA RID: 3802
	// (get) Token: 0x06004FCA RID: 20426 RVA: 0x001EB6FC File Offset: 0x001E9AFC
	// (set) Token: 0x06004FCB RID: 20427 RVA: 0x001EB704 File Offset: 0x001E9B04
	public Transform trfTarget { get; private set; }

	// Token: 0x17000EDB RID: 3803
	// (get) Token: 0x06004FCC RID: 20428 RVA: 0x001EB70D File Offset: 0x001E9B0D
	// (set) Token: 0x06004FCD RID: 20429 RVA: 0x001EB715 File Offset: 0x001E9B15
	public Transform trfUpAxis { get; private set; }

	// Token: 0x06004FCE RID: 20430 RVA: 0x001EB71E File Offset: 0x001E9B1E
	public void SetLookAtTransform(Transform trf)
	{
		this.trfLookAt = trf;
	}

	// Token: 0x06004FCF RID: 20431 RVA: 0x001EB727 File Offset: 0x001E9B27
	public void SetTargetTransform(Transform trf)
	{
		this.trfTarget = trf;
	}

	// Token: 0x06004FD0 RID: 20432 RVA: 0x001EB730 File Offset: 0x001E9B30
	public void SetUpAxisTransform(Transform trf)
	{
		this.trfUpAxis = trf;
	}

	// Token: 0x06004FD1 RID: 20433 RVA: 0x001EB739 File Offset: 0x001E9B39
	public void SetOldRotation(Quaternion q)
	{
		this.oldRotation = q;
	}

	// Token: 0x06004FD2 RID: 20434 RVA: 0x001EB744 File Offset: 0x001E9B44
	public void ManualCalc()
	{
		if (null == this.trfTarget || null == this.trfLookAt)
		{
			return;
		}
		Vector3 upVector = this.GetUpVector();
		Vector3 vector = Vector3.Normalize(this.trfTarget.position - this.trfLookAt.position);
		Vector3 vector2 = Vector3.Normalize(Vector3.Cross(upVector, vector));
		Vector3 vector3 = Vector3.Cross(vector, vector2);
		if (this.targetAxisType == H_LookAtDan_Info.AxisType.RevX || this.targetAxisType == H_LookAtDan_Info.AxisType.RevY || this.targetAxisType == H_LookAtDan_Info.AxisType.RevZ)
		{
			vector = -vector;
			vector2 = -vector2;
		}
		Vector3 xvec = Vector3.zero;
		Vector3 yvec = Vector3.zero;
		Vector3 zvec = Vector3.zero;
		switch (this.targetAxisType)
		{
		case H_LookAtDan_Info.AxisType.X:
		case H_LookAtDan_Info.AxisType.RevX:
			xvec = vector;
			if (this.sourceAxisType == H_LookAtDan_Info.AxisType.Y)
			{
				yvec = vector3;
				zvec = -vector2;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.RevY)
			{
				yvec = -vector3;
				zvec = vector2;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.Z)
			{
				yvec = vector2;
				zvec = vector3;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.RevZ)
			{
				yvec = -vector2;
				zvec = -vector3;
			}
			break;
		case H_LookAtDan_Info.AxisType.Y:
		case H_LookAtDan_Info.AxisType.RevY:
			yvec = vector;
			if (this.sourceAxisType == H_LookAtDan_Info.AxisType.X)
			{
				xvec = vector3;
				zvec = vector2;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.RevX)
			{
				xvec = -vector3;
				zvec = -vector2;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.Z)
			{
				xvec = -vector2;
				zvec = vector3;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.RevZ)
			{
				xvec = vector2;
				zvec = -vector3;
			}
			break;
		case H_LookAtDan_Info.AxisType.Z:
		case H_LookAtDan_Info.AxisType.RevZ:
			zvec = vector;
			if (this.sourceAxisType == H_LookAtDan_Info.AxisType.X)
			{
				xvec = vector3;
				yvec = -vector2;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.RevX)
			{
				xvec = -vector3;
				yvec = vector2;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.Y)
			{
				xvec = vector2;
				yvec = vector3;
			}
			else if (this.sourceAxisType == H_LookAtDan_Info.AxisType.RevY)
			{
				xvec = -vector2;
				yvec = -vector3;
			}
			break;
		}
		if (this.limitAxisType == H_LookAtDan_Info.AxisType.None)
		{
			Quaternion rotation = default(Quaternion);
			if (this.LookAtQuat(xvec, yvec, zvec, ref rotation))
			{
				this.trfLookAt.rotation = rotation;
			}
			else
			{
				this.trfLookAt.rotation = this.oldRotation;
			}
			this.oldRotation = this.trfLookAt.rotation;
		}
		else
		{
			Quaternion rotation2 = default(Quaternion);
			if (this.LookAtQuat(xvec, yvec, zvec, ref rotation2))
			{
				this.trfLookAt.rotation = rotation2;
			}
			else
			{
				this.trfLookAt.rotation = this.oldRotation;
			}
			ConvertRotation.RotationOrder order = (ConvertRotation.RotationOrder)this.rotOrder;
			Quaternion localRotation = this.trfLookAt.localRotation;
			Vector3 vector4 = ConvertRotation.ConvertDegreeFromQuaternion(order, localRotation);
			Quaternion q = Quaternion.Slerp(localRotation, Quaternion.identity, 0.5f);
			Vector3 vector5 = ConvertRotation.ConvertDegreeFromQuaternion(order, q);
			if (this.limitAxisType == H_LookAtDan_Info.AxisType.X)
			{
				if ((vector4.x < 0f && vector5.x > 0f) || (vector4.x > 0f && vector5.x < 0f))
				{
					vector4.x *= -1f;
				}
				vector4.x = Mathf.Clamp(vector4.x, this.limitMin, this.limitMax);
			}
			else if (this.limitAxisType == H_LookAtDan_Info.AxisType.Y)
			{
				if ((vector4.y < 0f && vector5.y > 0f) || (vector4.y > 0f && vector5.y < 0f))
				{
					vector4.y *= -1f;
				}
				vector4.y = Mathf.Clamp(vector4.y, this.limitMin, this.limitMax);
			}
			else if (this.limitAxisType == H_LookAtDan_Info.AxisType.Z)
			{
				if ((vector4.z < 0f && vector5.z > 0f) || (vector4.z > 0f && vector5.z < 0f))
				{
					vector4.z *= -1f;
				}
				vector4.z = Mathf.Clamp(vector4.z, this.limitMin, this.limitMax);
			}
			this.trfLookAt.localRotation = ConvertRotation.ConvertDegreeToQuaternion(order, vector4.x, vector4.y, vector4.z);
			this.oldRotation = this.trfLookAt.rotation;
		}
	}

	// Token: 0x06004FD3 RID: 20435 RVA: 0x001EBC20 File Offset: 0x001EA020
	private Vector3 GetUpVector()
	{
		Vector3 result = Vector3.up;
		if (this.trfUpAxis)
		{
			H_LookAtDan_Info.AxisType axisType = this.upAxisType;
			if (axisType != H_LookAtDan_Info.AxisType.X)
			{
				if (axisType != H_LookAtDan_Info.AxisType.Y)
				{
					if (axisType == H_LookAtDan_Info.AxisType.Z)
					{
						result = this.trfUpAxis.forward;
					}
				}
				else
				{
					result = this.trfUpAxis.up;
				}
			}
			else
			{
				result = this.trfUpAxis.right;
			}
		}
		return result;
	}

	// Token: 0x06004FD4 RID: 20436 RVA: 0x001EBC98 File Offset: 0x001EA098
	private bool LookAtQuat(Vector3 xvec, Vector3 yvec, Vector3 zvec, ref Quaternion q)
	{
		float num = 1f + xvec.x + yvec.y + zvec.z;
		if (num == 0f)
		{
			GlobalMethod.DebugLog("LookAt 計算不可 値0", 1);
			return false;
		}
		float num2 = Mathf.Sqrt(num) / 2f;
		if (float.IsNaN(num2))
		{
			GlobalMethod.DebugLog("LookAt 計算不可 NaN", 1);
			return false;
		}
		float num3 = 4f * num2;
		if (num3 == 0f)
		{
			GlobalMethod.DebugLog("LookAt 計算不可 w=0", 1);
			return false;
		}
		float x = (yvec.z - zvec.y) / num3;
		float y = (zvec.x - xvec.z) / num3;
		float z = (xvec.y - yvec.x) / num3;
		q = new Quaternion(x, y, z, num2);
		return true;
	}

	// Token: 0x040048BF RID: 18623
	public string lookAtName = string.Empty;

	// Token: 0x040048C1 RID: 18625
	public string targetName = string.Empty;

	// Token: 0x040048C3 RID: 18627
	public H_LookAtDan_Info.AxisType targetAxisType = H_LookAtDan_Info.AxisType.Z;

	// Token: 0x040048C4 RID: 18628
	public string upAxisName = string.Empty;

	// Token: 0x040048C6 RID: 18630
	public H_LookAtDan_Info.AxisType upAxisType = H_LookAtDan_Info.AxisType.Y;

	// Token: 0x040048C7 RID: 18631
	public H_LookAtDan_Info.AxisType sourceAxisType = H_LookAtDan_Info.AxisType.Y;

	// Token: 0x040048C8 RID: 18632
	public H_LookAtDan_Info.AxisType limitAxisType = H_LookAtDan_Info.AxisType.None;

	// Token: 0x040048C9 RID: 18633
	public H_LookAtDan_Info.RotationOrder rotOrder = H_LookAtDan_Info.RotationOrder.ZXY;

	// Token: 0x040048CA RID: 18634
	[Range(-180f, 180f)]
	public float limitMin;

	// Token: 0x040048CB RID: 18635
	[Range(-180f, 180f)]
	public float limitMax;

	// Token: 0x040048CC RID: 18636
	private Quaternion oldRotation = Quaternion.identity;

	// Token: 0x02000A94 RID: 2708
	public enum AxisType
	{
		// Token: 0x040048CE RID: 18638
		X,
		// Token: 0x040048CF RID: 18639
		Y,
		// Token: 0x040048D0 RID: 18640
		Z,
		// Token: 0x040048D1 RID: 18641
		RevX,
		// Token: 0x040048D2 RID: 18642
		RevY,
		// Token: 0x040048D3 RID: 18643
		RevZ,
		// Token: 0x040048D4 RID: 18644
		None
	}

	// Token: 0x02000A95 RID: 2709
	public enum RotationOrder
	{
		// Token: 0x040048D6 RID: 18646
		XYZ,
		// Token: 0x040048D7 RID: 18647
		XZY,
		// Token: 0x040048D8 RID: 18648
		YXZ,
		// Token: 0x040048D9 RID: 18649
		YZX,
		// Token: 0x040048DA RID: 18650
		ZXY,
		// Token: 0x040048DB RID: 18651
		ZYX
	}
}
