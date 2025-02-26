using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics
{
	// Token: 0x020001EC RID: 492
	[TaskCategory("Unity/Physics")]
	[TaskDescription("Casts a sphere against all colliders in the scene. Returns success if a collider was hit.")]
	public class SphereCast : Action
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x00027FB0 File Offset: 0x000263B0
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
			if (Physics.SphereCast(origin, this.radius.Value, vector, out raycastHit, (this.distance.Value != -1f) ? this.distance.Value : float.PositiveInfinity, this.layerMask))
			{
				this.storeHitObject.Value = raycastHit.collider.gameObject;
				this.storeHitPoint.Value = raycastHit.point;
				this.storeHitNormal.Value = raycastHit.normal;
				this.storeHitDistance.Value = raycastHit.distance;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000280D4 File Offset: 0x000264D4
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector3.zero;
			this.radius = 0f;
			this.direction = Vector3.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x04000812 RID: 2066
		[Tooltip("Starts the spherecast at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x04000813 RID: 2067
		[Tooltip("Starts the sherecast at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x04000814 RID: 2068
		[Tooltip("The radius of the spherecast")]
		public SharedFloat radius;

		// Token: 0x04000815 RID: 2069
		[Tooltip("The direction of the spherecast")]
		public SharedVector3 direction;

		// Token: 0x04000816 RID: 2070
		[Tooltip("The length of the spherecast. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x04000817 RID: 2071
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x04000818 RID: 2072
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = Space.Self;

		// Token: 0x04000819 RID: 2073
		[SharedRequired]
		[Tooltip("Stores the hit object of the spherecast")]
		public SharedGameObject storeHitObject;

		// Token: 0x0400081A RID: 2074
		[SharedRequired]
		[Tooltip("Stores the hit point of the spherecast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x0400081B RID: 2075
		[SharedRequired]
		[Tooltip("Stores the hit normal of the spherecast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x0400081C RID: 2076
		[SharedRequired]
		[Tooltip("Stores the hit distance of the spherecast")]
		public SharedFloat storeHitDistance;
	}
}
