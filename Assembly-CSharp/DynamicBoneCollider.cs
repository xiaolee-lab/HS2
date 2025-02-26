using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
[AddComponentMenu("Dynamic Bone/Dynamic Bone Collider")]
public class DynamicBoneCollider : DynamicBoneColliderBase
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x0003ED23 File Offset: 0x0003D123
	private void OnValidate()
	{
		this.m_Radius = Mathf.Max(this.m_Radius, 0f);
		this.m_Height = Mathf.Max(this.m_Height, 0f);
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0003ED54 File Offset: 0x0003D154
	public override void Collide(ref Vector3 particlePosition, float particleRadius)
	{
		float num = this.m_Radius * Mathf.Abs(base.transform.lossyScale.x);
		float num2 = this.m_Height * 0.5f - this.m_Radius;
		if (num2 <= 0f)
		{
			if (this.m_Bound == DynamicBoneColliderBase.Bound.Outside)
			{
				DynamicBoneCollider.OutsideSphere(ref particlePosition, particleRadius, base.transform.TransformPoint(this.m_Center), num);
			}
			else
			{
				DynamicBoneCollider.InsideSphere(ref particlePosition, particleRadius, base.transform.TransformPoint(this.m_Center), num);
			}
		}
		else
		{
			Vector3 center = this.m_Center;
			Vector3 center2 = this.m_Center;
			DynamicBoneColliderBase.Direction direction = this.m_Direction;
			if (direction != DynamicBoneColliderBase.Direction.X)
			{
				if (direction != DynamicBoneColliderBase.Direction.Y)
				{
					if (direction == DynamicBoneColliderBase.Direction.Z)
					{
						center.z -= num2;
						center2.z += num2;
					}
				}
				else
				{
					center.y -= num2;
					center2.y += num2;
				}
			}
			else
			{
				center.x -= num2;
				center2.x += num2;
			}
			if (this.m_Bound == DynamicBoneColliderBase.Bound.Outside)
			{
				DynamicBoneCollider.OutsideCapsule(ref particlePosition, particleRadius, base.transform.TransformPoint(center), base.transform.TransformPoint(center2), num);
			}
			else
			{
				DynamicBoneCollider.InsideCapsule(ref particlePosition, particleRadius, base.transform.TransformPoint(center), base.transform.TransformPoint(center2), num);
			}
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0003EED8 File Offset: 0x0003D2D8
	private static void OutsideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
	{
		float num = sphereRadius + particleRadius;
		float num2 = num * num;
		Vector3 a = particlePosition - sphereCenter;
		float sqrMagnitude = a.sqrMagnitude;
		if (sqrMagnitude > 0f && sqrMagnitude < num2)
		{
			float num3 = Mathf.Sqrt(sqrMagnitude);
			particlePosition = sphereCenter + a * (num / num3);
		}
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0003EF34 File Offset: 0x0003D334
	private static void InsideSphere(ref Vector3 particlePosition, float particleRadius, Vector3 sphereCenter, float sphereRadius)
	{
		float num = sphereRadius - particleRadius;
		float num2 = num * num;
		Vector3 a = particlePosition - sphereCenter;
		float sqrMagnitude = a.sqrMagnitude;
		if (sqrMagnitude > num2)
		{
			float num3 = Mathf.Sqrt(sqrMagnitude);
			particlePosition = sphereCenter + a * (num / num3);
		}
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0003EF84 File Offset: 0x0003D384
	private static void OutsideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
	{
		float num = capsuleRadius + particleRadius;
		float num2 = num * num;
		Vector3 vector = capsuleP1 - capsuleP0;
		Vector3 vector2 = particlePosition - capsuleP0;
		float num3 = Vector3.Dot(vector2, vector);
		if (num3 <= 0f)
		{
			float sqrMagnitude = vector2.sqrMagnitude;
			if (sqrMagnitude > 0f && sqrMagnitude < num2)
			{
				float num4 = Mathf.Sqrt(sqrMagnitude);
				particlePosition = capsuleP0 + vector2 * (num / num4);
			}
		}
		else
		{
			float sqrMagnitude2 = vector.sqrMagnitude;
			if (num3 >= sqrMagnitude2)
			{
				vector2 = particlePosition - capsuleP1;
				float sqrMagnitude3 = vector2.sqrMagnitude;
				if (sqrMagnitude3 > 0f && sqrMagnitude3 < num2)
				{
					float num5 = Mathf.Sqrt(sqrMagnitude3);
					particlePosition = capsuleP1 + vector2 * (num / num5);
				}
			}
			else if (sqrMagnitude2 > 0f)
			{
				num3 /= sqrMagnitude2;
				vector2 -= vector * num3;
				float sqrMagnitude4 = vector2.sqrMagnitude;
				if (sqrMagnitude4 > 0f && sqrMagnitude4 < num2)
				{
					float num6 = Mathf.Sqrt(sqrMagnitude4);
					particlePosition += vector2 * ((num - num6) / num6);
				}
			}
		}
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0003F0CC File Offset: 0x0003D4CC
	private static void InsideCapsule(ref Vector3 particlePosition, float particleRadius, Vector3 capsuleP0, Vector3 capsuleP1, float capsuleRadius)
	{
		float num = capsuleRadius - particleRadius;
		float num2 = num * num;
		Vector3 vector = capsuleP1 - capsuleP0;
		Vector3 vector2 = particlePosition - capsuleP0;
		float num3 = Vector3.Dot(vector2, vector);
		if (num3 <= 0f)
		{
			float sqrMagnitude = vector2.sqrMagnitude;
			if (sqrMagnitude > num2)
			{
				float num4 = Mathf.Sqrt(sqrMagnitude);
				particlePosition = capsuleP0 + vector2 * (num / num4);
			}
		}
		else
		{
			float sqrMagnitude2 = vector.sqrMagnitude;
			if (num3 >= sqrMagnitude2)
			{
				vector2 = particlePosition - capsuleP1;
				float sqrMagnitude3 = vector2.sqrMagnitude;
				if (sqrMagnitude3 > num2)
				{
					float num5 = Mathf.Sqrt(sqrMagnitude3);
					particlePosition = capsuleP1 + vector2 * (num / num5);
				}
			}
			else if (sqrMagnitude2 > 0f)
			{
				num3 /= sqrMagnitude2;
				vector2 -= vector * num3;
				float sqrMagnitude4 = vector2.sqrMagnitude;
				if (sqrMagnitude4 > num2)
				{
					float num6 = Mathf.Sqrt(sqrMagnitude4);
					particlePosition += vector2 * ((num - num6) / num6);
				}
			}
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0003F1F0 File Offset: 0x0003D5F0
	private void OnDrawGizmosSelected()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.m_Bound == DynamicBoneColliderBase.Bound.Outside)
		{
			Gizmos.color = Color.yellow;
		}
		else
		{
			Gizmos.color = Color.magenta;
		}
		float radius = this.m_Radius * Mathf.Abs(base.transform.lossyScale.x);
		float num = this.m_Height * 0.5f - this.m_Radius;
		if (num <= 0f)
		{
			Gizmos.DrawWireSphere(base.transform.TransformPoint(this.m_Center), radius);
		}
		else
		{
			Vector3 center = this.m_Center;
			Vector3 center2 = this.m_Center;
			DynamicBoneColliderBase.Direction direction = this.m_Direction;
			if (direction != DynamicBoneColliderBase.Direction.X)
			{
				if (direction != DynamicBoneColliderBase.Direction.Y)
				{
					if (direction == DynamicBoneColliderBase.Direction.Z)
					{
						center.z -= num;
						center2.z += num;
					}
				}
				else
				{
					center.y -= num;
					center2.y += num;
				}
			}
			else
			{
				center.x -= num;
				center2.x += num;
			}
			Gizmos.DrawWireSphere(base.transform.TransformPoint(center), radius);
			Gizmos.DrawWireSphere(base.transform.TransformPoint(center2), radius);
		}
	}

	// Token: 0x04000D37 RID: 3383
	public float m_Radius = 0.5f;

	// Token: 0x04000D38 RID: 3384
	public float m_Height;
}
