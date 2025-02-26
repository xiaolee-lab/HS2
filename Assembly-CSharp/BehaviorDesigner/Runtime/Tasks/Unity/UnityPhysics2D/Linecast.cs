using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics2D
{
	// Token: 0x020001EE RID: 494
	[TaskCategory("Unity/Physics2D")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	public class Linecast : Action
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0002831F File Offset: 0x0002671F
		public override TaskStatus OnUpdate()
		{
			return (!Physics2D.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00028358 File Offset: 0x00026758
		public override void OnReset()
		{
			this.startPosition = Vector2.zero;
			this.endPosition = Vector2.zero;
			this.layerMask = -1;
		}

		// Token: 0x04000828 RID: 2088
		[Tooltip("The starting position of the linecast.")]
		public SharedVector2 startPosition;

		// Token: 0x04000829 RID: 2089
		[Tooltip("The ending position of the linecast.")]
		public SharedVector2 endPosition;

		// Token: 0x0400082A RID: 2090
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}
