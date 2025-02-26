using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics2D
{
	// Token: 0x020001ED RID: 493
	[TaskCategory("Unity/Physics2D")]
	[TaskDescription("Casts a circle against all colliders in the scene. Returns success if a collider was hit.")]
	public class Circlecast : Action
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00028168 File Offset: 0x00026568
		public override TaskStatus OnUpdate()
		{
			Vector2 vector = this.direction.Value;
			Vector2 origin;
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
			RaycastHit2D raycastHit2D = Physics2D.CircleCast(origin, this.radius.Value, vector, (this.distance.Value != -1f) ? this.distance.Value : float.PositiveInfinity, this.layerMask);
			if (raycastHit2D.collider != null)
			{
				this.storeHitObject.Value = raycastHit2D.collider.gameObject;
				this.storeHitPoint.Value = raycastHit2D.point;
				this.storeHitNormal.Value = raycastHit2D.normal;
				this.storeHitDistance.Value = raycastHit2D.distance;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000282A4 File Offset: 0x000266A4
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector2.zero;
			this.direction = Vector2.zero;
			this.radius = 0f;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x0400081D RID: 2077
		[Tooltip("Starts the circlecast at the GameObject's position. If null the originPosition will be used.")]
		public SharedGameObject originGameObject;

		// Token: 0x0400081E RID: 2078
		[Tooltip("Starts the circlecast at the position. Only used if originGameObject is null.")]
		public SharedVector2 originPosition;

		// Token: 0x0400081F RID: 2079
		[Tooltip("The radius of the circlecast")]
		public SharedFloat radius;

		// Token: 0x04000820 RID: 2080
		[Tooltip("The direction of the circlecast")]
		public SharedVector2 direction;

		// Token: 0x04000821 RID: 2081
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public SharedFloat distance = -1f;

		// Token: 0x04000822 RID: 2082
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;

		// Token: 0x04000823 RID: 2083
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified.")]
		public Space space = Space.Self;

		// Token: 0x04000824 RID: 2084
		[SharedRequired]
		[Tooltip("Stores the hit object of the circlecast.")]
		public SharedGameObject storeHitObject;

		// Token: 0x04000825 RID: 2085
		[SharedRequired]
		[Tooltip("Stores the hit point of the circlecast.")]
		public SharedVector2 storeHitPoint;

		// Token: 0x04000826 RID: 2086
		[SharedRequired]
		[Tooltip("Stores the hit normal of the circlecast.")]
		public SharedVector2 storeHitNormal;

		// Token: 0x04000827 RID: 2087
		[SharedRequired]
		[Tooltip("Stores the hit distance of the circlecast.")]
		public SharedFloat storeHitDistance;
	}
}
