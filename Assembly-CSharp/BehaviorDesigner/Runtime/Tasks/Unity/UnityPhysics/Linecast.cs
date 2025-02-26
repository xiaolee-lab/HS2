using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics
{
	// Token: 0x020001EA RID: 490
	[TaskCategory("Unity/Physics")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	public class Linecast : Action
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x00027D85 File Offset: 0x00026185
		public override TaskStatus OnUpdate()
		{
			return (!Physics.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00027DB9 File Offset: 0x000261B9
		public override void OnReset()
		{
			this.startPosition = Vector3.zero;
			this.endPosition = Vector3.zero;
			this.layerMask = -1;
		}

		// Token: 0x04000805 RID: 2053
		[Tooltip("The starting position of the linecast")]
		public SharedVector3 startPosition;

		// Token: 0x04000806 RID: 2054
		[Tooltip("The ending position of the linecast")]
		public SharedVector3 endPosition;

		// Token: 0x04000807 RID: 2055
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}
