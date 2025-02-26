using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000ED RID: 237
	[TaskDescription("Returns success when an object exits the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedTrigger : Conditional
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x0001F32F File Offset: 0x0001D72F
		public override TaskStatus OnUpdate()
		{
			return (!this.exitedTrigger) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001F343 File Offset: 0x0001D743
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001F34C File Offset: 0x0001D74C
		public override void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001F3A6 File Offset: 0x0001D7A6
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.otherGameObject = null;
		}

		// Token: 0x0400046F RID: 1135
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = string.Empty;

		// Token: 0x04000470 RID: 1136
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000471 RID: 1137
		private bool exitedTrigger;
	}
}
