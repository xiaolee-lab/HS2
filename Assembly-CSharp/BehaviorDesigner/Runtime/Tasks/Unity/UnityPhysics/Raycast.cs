using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics
{
	// Token: 0x020001EB RID: 491
	[TaskCategory("Unity/Physics")]
	[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
	public class Raycast : Action
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00027E14 File Offset: 0x00026214
		public override TaskStatus OnUpdate()
		{
			Vector3 vector = this.direction.Value;
			Vector3 origin;
			if (this.originGameObject.Value != null)
			{
				origin = this.originGameObject.Value.transform.position;
				if (this.space == Space.Self)
				{
					vector = this.originGameObject.Value.transform.TransformDirection(this.direction.Value);
				}
			}
			else
			{
				origin = this.originPosition.Value;
			}
			RaycastHit raycastHit;
			if (Physics.Raycast(origin, vector, out raycastHit, (this.distance.Value != -1f) ? this.distance.Value : float.PositiveInfinity, this.layerMask))
			{
				this.storeHitObject.Value = raycastHit.collider.gameObject;
				this.storeHitPoint.Value = raycastHit.point;
				this.storeHitNormal.Value = raycastHit.normal;
				this.storeHitDistance.Value = raycastHit.distance;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00027F2C File Offset: 0x0002632C
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector3.zero;
			this.direction = Vector3.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x04000808 RID: 2056
		[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x04000809 RID: 2057
		[Tooltip("Starts the ray at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x0400080A RID: 2058
		[Tooltip("The direction of the ray")]
		public SharedVector3 direction;

		// Token: 0x0400080B RID: 2059
		[Tooltip("The length of the ray. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x0400080C RID: 2060
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x0400080D RID: 2061
		[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = Space.Self;

		// Token: 0x0400080E RID: 2062
		[SharedRequired]
		[Tooltip("Stores the hit object of the raycast")]
		public SharedGameObject storeHitObject;

		// Token: 0x0400080F RID: 2063
		[SharedRequired]
		[Tooltip("Stores the hit point of the raycast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x04000810 RID: 2064
		[SharedRequired]
		[Tooltip("Stores the hit normal of the raycast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x04000811 RID: 2065
		[SharedRequired]
		[Tooltip("Stores the hit distance of the raycast")]
		public SharedFloat storeHitDistance;
	}
}
