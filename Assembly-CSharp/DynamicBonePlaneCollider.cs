using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
[AddComponentMenu("Dynamic Bone/Dynamic Bone Plane Collider")]
public class DynamicBonePlaneCollider : DynamicBoneColliderBase
{
	// Token: 0x06000DD7 RID: 3543 RVA: 0x00041FDC File Offset: 0x000403DC
	private void OnValidate()
	{
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x00041FE0 File Offset: 0x000403E0
	public override void Collide(ref Vector3 particlePosition, float particleRadius)
	{
		Vector3 vector = Vector3.up;
		DynamicBoneColliderBase.Direction direction = this.m_Direction;
		if (direction != DynamicBoneColliderBase.Direction.X)
		{
			if (direction != DynamicBoneColliderBase.Direction.Y)
			{
				if (direction == DynamicBoneColliderBase.Direction.Z)
				{
					vector = base.transform.forward;
				}
			}
			else
			{
				vector = base.transform.up;
			}
		}
		else
		{
			vector = base.transform.right;
		}
		Vector3 inPoint = base.transform.TransformPoint(this.m_Center);
		Plane plane = new Plane(vector, inPoint);
		float distanceToPoint = plane.GetDistanceToPoint(particlePosition);
		if (this.m_Bound == DynamicBoneColliderBase.Bound.Outside)
		{
			if (distanceToPoint < 0f)
			{
				particlePosition -= vector * distanceToPoint;
			}
		}
		else if (distanceToPoint > 0f)
		{
			particlePosition -= vector * distanceToPoint;
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x000420CC File Offset: 0x000404CC
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
		Vector3 b = Vector3.up;
		DynamicBoneColliderBase.Direction direction = this.m_Direction;
		if (direction != DynamicBoneColliderBase.Direction.X)
		{
			if (direction != DynamicBoneColliderBase.Direction.Y)
			{
				if (direction == DynamicBoneColliderBase.Direction.Z)
				{
					b = base.transform.forward;
				}
			}
			else
			{
				b = base.transform.up;
			}
		}
		else
		{
			b = base.transform.right;
		}
		Vector3 vector = base.transform.TransformPoint(this.m_Center);
		Gizmos.DrawLine(vector, vector + b);
	}
}
