using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000EE RID: 238
	[TaskDescription("Returns success when an object exits the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedTrigger2D : Conditional
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x0001F3D7 File Offset: 0x0001D7D7
		public override TaskStatus OnUpdate()
		{
			return (!this.exitedTrigger) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001F3EB File Offset: 0x0001D7EB
		public override void OnEnd()
		{
			this.exitedTrigger = false;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001F3F4 File Offset: 0x0001D7F4
		public override void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.exitedTrigger = true;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001F44E File Offset: 0x0001D84E
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.otherGameObject = null;
		}

		// Token: 0x04000472 RID: 1138
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = string.Empty;

		// Token: 0x04000473 RID: 1139
		[Tooltip("The object that exited the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000474 RID: 1140
		private bool exitedTrigger;
	}
}
