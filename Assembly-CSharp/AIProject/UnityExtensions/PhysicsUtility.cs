using System;
using System.Diagnostics;
using UnityEngine;
using UnityEx.Misc;

namespace AIProject.UnityExtensions
{
	// Token: 0x0200096C RID: 2412
	public static class PhysicsUtility
	{
		// Token: 0x060042F1 RID: 17137 RVA: 0x001A59A4 File Offset: 0x001A3DA4
		public static bool CheckPointInPolygon(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector3 lhs = b - a;
			Vector3 rhs = p - b;
			Vector3 lhs2 = c - b;
			Vector3 rhs2 = p - c;
			Vector3 lhs3 = a - c;
			Vector3 rhs3 = p - a;
			Vector3 lhs4 = Vector3.Cross(lhs, rhs);
			Vector3 rhs4 = Vector3.Cross(lhs2, rhs2);
			Vector3 rhs5 = Vector3.Cross(lhs3, rhs3);
			float num = Vector3.Dot(lhs4, rhs4);
			float num2 = Vector3.Dot(lhs4, rhs5);
			return num > 0f && num2 > 0f;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x001A5A30 File Offset: 0x001A3E30
		public static bool CheckFOV(LayerMask layer, Vector2 angle, Transform transform, Collider target, float viewDistance)
		{
			if (layer.Contains(target.gameObject.layer))
			{
				return false;
			}
			Vector3 point = Vector3.zero;
			float num = 0f;
			if (!(target is SphereCollider) && !(target is CapsuleCollider))
			{
				return false;
			}
			if (target is SphereCollider)
			{
				point = (target as SphereCollider).center;
				num = (target as SphereCollider).radius;
			}
			else if (target is CapsuleCollider)
			{
				point = (target as CapsuleCollider).center;
				num = (target as CapsuleCollider).radius;
			}
			return PhysicsUtility.CheckInsideFOV(angle, transform, target.transform.position + target.transform.rotation * point, viewDistance + num);
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x001A5AF0 File Offset: 0x001A3EF0
		public static bool CheckFOVPoint(LayerMask layer, Vector2 angle, Transform transform, Transform target, float viewDistance)
		{
			return !layer.Contains(target.gameObject.layer) && PhysicsUtility.CheckInsideFOV(angle, transform, target, viewDistance);
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x001A5B14 File Offset: 0x001A3F14
		public static bool CheckInsideFOV(Vector2 angle, Transform transform, Transform target, float viewDistance)
		{
			return PhysicsUtility.CheckInsideFOV(angle, transform, target.position, viewDistance);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x001A5B24 File Offset: 0x001A3F24
		public static bool CheckInsideFOV(Vector2 angle, Transform transform, Vector3 targetPosition, float viewDistance)
		{
			float num = Vector3.Distance(transform.position, targetPosition);
			if (num > viewDistance)
			{
				return false;
			}
			Vector2 vector = angle / 2f;
			Vector3 vector2 = targetPosition - transform.position;
			Vector3 to = Vector3.Normalize(new Vector3(vector2.x, 0f, vector2.y));
			float num2 = Vector3.Angle(transform.forward, to);
			if (num2 > 180f)
			{
				num2 = Mathf.Abs(360f - num2);
			}
			return num2 <= vector.x;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x001A5BB8 File Offset: 0x001A3FB8
		[Conditional("UNITY_EDITOR")]
		public static void DrawWireFOV(float hAngle, float vAngle, Vector3 position, Quaternion rotation, float viewDistance)
		{
		}
	}
}
