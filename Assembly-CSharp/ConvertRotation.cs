using System;
using UnityEngine;

// Token: 0x02000828 RID: 2088
public class ConvertRotation
{
	// Token: 0x06003522 RID: 13602 RVA: 0x001395C1 File Offset: 0x001379C1
	public static void CheckNaN(ref float val, float correct = 0f)
	{
		if (float.IsNaN(val))
		{
			val = correct;
		}
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x001395D4 File Offset: 0x001379D4
	public static Quaternion ConvertDegreeToQuaternion(ConvertRotation.RotationOrder order, float x, float y, float z)
	{
		Quaternion quaternion = Quaternion.AngleAxis(x, Vector3.right);
		Quaternion quaternion2 = Quaternion.AngleAxis(y, Vector3.up);
		Quaternion quaternion3 = Quaternion.AngleAxis(z, Vector3.forward);
		switch (order)
		{
		case ConvertRotation.RotationOrder.xyz:
			return quaternion * quaternion2 * quaternion3;
		case ConvertRotation.RotationOrder.xzy:
			return quaternion * quaternion3 * quaternion2;
		case ConvertRotation.RotationOrder.yxz:
			return quaternion2 * quaternion * quaternion3;
		case ConvertRotation.RotationOrder.yzx:
			return quaternion2 * quaternion3 * quaternion;
		case ConvertRotation.RotationOrder.zxy:
			return quaternion3 * quaternion * quaternion2;
		case ConvertRotation.RotationOrder.zyx:
			return quaternion3 * quaternion2 * quaternion;
		default:
			return Quaternion.identity;
		}
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x00139681 File Offset: 0x00137A81
	public static Quaternion ConvertRadianToQuaternion(ConvertRotation.RotationOrder order, float x, float y, float z)
	{
		return ConvertRotation.ConvertDegreeToQuaternion(order, x * 57.29578f, y * 57.29578f, z * 57.29578f);
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x001396A0 File Offset: 0x00137AA0
	public static Vector3 ConvertDegreeFromQuaternion(ConvertRotation.RotationOrder order, Quaternion q)
	{
		Vector3 vector = ConvertRotation.ConvertRadianFromQuaternion(order, q);
		return new Vector3(vector.x * 57.29578f, vector.y * 57.29578f, vector.z * 57.29578f);
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x001396E4 File Offset: 0x00137AE4
	public static Vector3 ConvertRadianFromQuaternion(ConvertRotation.RotationOrder order, Quaternion q)
	{
		Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
		return ConvertRotation.ConvertRadianFromMatrix(order, m);
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x0013970C File Offset: 0x00137B0C
	private static float[] threeaxisrot(float r11, float r12, float r21, float r31, float r32)
	{
		float[] array = new float[3];
		if (Mathf.Abs(r21) > 0.9999f)
		{
			array[0] = Mathf.Atan2(r31, r32);
			array[1] = Mathf.Asin(Mathf.Clamp(r21, -1f, 1f));
			array[2] = Mathf.Atan2(r11, r12);
		}
		else
		{
			array[0] = Mathf.Atan2(r31, r32);
			array[1] = Mathf.Asin(Mathf.Clamp(r21, -1f, 1f));
			array[2] = Mathf.Atan2(r11, r12);
		}
		return array;
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x00139790 File Offset: 0x00137B90
	public static Vector3 ConvertDegreeFromQuaternionEx(ConvertRotation.RotationOrder order, Quaternion q)
	{
		Vector3 vector = ConvertRotation.ConvertRadianFromQuaternionEx(order, q);
		return new Vector3(vector.x * 57.29578f, vector.y * 57.29578f, vector.z * 57.29578f);
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x001397D4 File Offset: 0x00137BD4
	public static Vector3 ConvertRadianFromQuaternionEx(ConvertRotation.RotationOrder order, Quaternion q)
	{
		switch (order)
		{
		case ConvertRotation.RotationOrder.xyz:
		{
			float[] array = ConvertRotation.threeaxisrot(-2f * (q.y * q.z - q.w * q.x), q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z, 2f * (q.x * q.z + q.w * q.y), -2f * (q.x * q.y - q.w * q.z), q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z);
			return new Vector3(array[2], array[1], array[0]);
		}
		case ConvertRotation.RotationOrder.xzy:
		{
			float[] array2 = ConvertRotation.threeaxisrot(2f * (q.y * q.z + q.w * q.x), q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z, -2f * (q.x * q.y - q.w * q.z), 2f * (q.x * q.z + q.w * q.y), q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z);
			return new Vector3(array2[2], array2[0], array2[1]);
		}
		case ConvertRotation.RotationOrder.yxz:
		{
			float[] array3 = ConvertRotation.threeaxisrot(2f * (q.x * q.z + q.w * q.y), q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z, -2f * (q.y * q.z - q.w * q.x), 2f * (q.x * q.y + q.w * q.z), q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z);
			return new Vector3(array3[1], array3[2], array3[0]);
		}
		case ConvertRotation.RotationOrder.yzx:
		{
			float[] array4 = ConvertRotation.threeaxisrot(-2f * (q.x * q.z - q.w * q.y), q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z, 2f * (q.x * q.y + q.w * q.z), -2f * (q.y * q.z - q.w * q.x), q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z);
			return new Vector3(array4[0], array4[2], array4[1]);
		}
		case ConvertRotation.RotationOrder.zxy:
		{
			float[] array5 = ConvertRotation.threeaxisrot(-2f * (q.x * q.y - q.w * q.z), q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z, 2f * (q.y * q.z + q.w * q.x), -2f * (q.x * q.z - q.w * q.y), q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z);
			return new Vector3(array5[1], array5[0], array5[2]);
		}
		case ConvertRotation.RotationOrder.zyx:
		{
			float[] array6 = ConvertRotation.threeaxisrot(2f * (q.x * q.y + q.w * q.z), q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z, -2f * (q.x * q.z - q.w * q.y), 2f * (q.y * q.z + q.w * q.x), q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z);
			return new Vector3(array6[0], array6[1], array6[2]);
		}
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x00139E24 File Offset: 0x00138224
	public static Vector3 ConvertDegreeFromMatrix(ConvertRotation.RotationOrder order, Matrix4x4 m)
	{
		Vector3 vector = ConvertRotation.ConvertRadianFromMatrix(order, m);
		return new Vector3(vector.x * 57.29578f, vector.y * 57.29578f, vector.z * 57.29578f);
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x00139E68 File Offset: 0x00138268
	public static Vector3 ConvertRadianFromMatrix(ConvertRotation.RotationOrder order, Matrix4x4 m)
	{
		switch (order)
		{
		case ConvertRotation.RotationOrder.xyz:
		{
			float num = m.m02;
			float y = Mathf.Asin(Mathf.Clamp(num, -1f, 1f));
			float z;
			float x;
			if (num > 0.9999f)
			{
				z = 0f;
				x = Mathf.Atan2(m.m21, m.m11);
				ConvertRotation.CheckNaN(ref x, 0f);
			}
			else
			{
				x = Mathf.Atan2(-m.m12, m.m22);
				ConvertRotation.CheckNaN(ref x, 0f);
				z = Mathf.Atan2(-m.m01, m.m00);
				ConvertRotation.CheckNaN(ref z, 0f);
			}
			return new Vector3(x, y, z);
		}
		case ConvertRotation.RotationOrder.xzy:
		{
			float num = -m.m01;
			float z = Mathf.Asin(Mathf.Clamp(num, -1f, 1f));
			float y;
			float x;
			if (num > 0.9999f)
			{
				y = 0f;
				x = Mathf.Atan2(-m.m12, m.m22);
				ConvertRotation.CheckNaN(ref x, 0f);
			}
			else
			{
				x = Mathf.Atan2(m.m21, m.m11);
				ConvertRotation.CheckNaN(ref x, 0f);
				y = Mathf.Atan2(m.m02, m.m00);
				ConvertRotation.CheckNaN(ref y, 0f);
			}
			return new Vector3(x, y, z);
		}
		case ConvertRotation.RotationOrder.yxz:
		{
			float num = -m.m12;
			float x = Mathf.Asin(Mathf.Clamp(num, -1f, 1f));
			float y;
			float z;
			if (num > 0.9999f)
			{
				z = 0f;
				y = Mathf.Atan2(-m.m20, m.m00);
				ConvertRotation.CheckNaN(ref y, 0f);
			}
			else
			{
				y = Mathf.Atan2(m.m02, m.m22);
				ConvertRotation.CheckNaN(ref y, 0f);
				z = Mathf.Atan2(m.m10, m.m11);
				ConvertRotation.CheckNaN(ref z, 0f);
			}
			return new Vector3(x, y, z);
		}
		case ConvertRotation.RotationOrder.yzx:
		{
			float num = m.m10;
			float z = Mathf.Asin(Mathf.Clamp(num, -1f, 1f));
			float y;
			float x;
			if (num > 0.9999f)
			{
				x = 0f;
				y = Mathf.Atan2(m.m02, m.m22);
				ConvertRotation.CheckNaN(ref y, 0f);
			}
			else
			{
				x = Mathf.Atan2(-m.m12, m.m11);
				ConvertRotation.CheckNaN(ref x, 0f);
				y = Mathf.Atan2(-m.m20, m.m00);
				ConvertRotation.CheckNaN(ref y, 0f);
			}
			return new Vector3(x, y, z);
		}
		case ConvertRotation.RotationOrder.zxy:
		{
			float num = m.m21;
			float x = Mathf.Asin(Mathf.Clamp(num, -1f, 1f));
			float y;
			float z;
			if (num > 0.9999f)
			{
				y = 0f;
				z = Mathf.Atan2(m.m10, m.m00);
				ConvertRotation.CheckNaN(ref z, 0f);
			}
			else
			{
				y = Mathf.Atan2(-m.m20, m.m22);
				ConvertRotation.CheckNaN(ref y, 0f);
				z = Mathf.Atan2(-m.m01, m.m11);
				ConvertRotation.CheckNaN(ref z, 0f);
			}
			return new Vector3(x, y, z);
		}
		case ConvertRotation.RotationOrder.zyx:
		{
			float num = -m.m20;
			float y = Mathf.Asin(Mathf.Clamp(num, -1f, 1f));
			float z;
			float x;
			if (num > 0.9999f)
			{
				x = 0f;
				z = Mathf.Atan2(-m.m01, m.m11);
				ConvertRotation.CheckNaN(ref z, 0f);
			}
			else
			{
				x = Mathf.Atan2(m.m21, m.m22);
				ConvertRotation.CheckNaN(ref x, 0f);
				z = Mathf.Atan2(m.m10, m.m00);
				ConvertRotation.CheckNaN(ref z, 0f);
			}
			return new Vector3(x, y, z);
		}
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x02000829 RID: 2089
	public enum RotationOrder
	{
		// Token: 0x040035C0 RID: 13760
		xyz,
		// Token: 0x040035C1 RID: 13761
		xzy,
		// Token: 0x040035C2 RID: 13762
		yxz,
		// Token: 0x040035C3 RID: 13763
		yzx,
		// Token: 0x040035C4 RID: 13764
		zxy,
		// Token: 0x040035C5 RID: 13765
		zyx
	}
}
