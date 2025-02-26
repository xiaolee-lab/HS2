using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E9 RID: 233
	[TaskDescription("Returns success when an object enters the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredTrigger : Conditional
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0001F09F File Offset: 0x0001D49F
		public override TaskStatus OnUpdate()
		{
			return (!this.enteredTrigger) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001F0B3 File Offset: 0x0001D4B3
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001F0BC File Offset: 0x0001D4BC
		public override void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001F116 File Offset: 0x0001D516
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.otherGameObject = null;
		}

		// Token: 0x04000463 RID: 1123
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = string.Empty;

		// Token: 0x04000464 RID: 1124
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000465 RID: 1125
		private bool enteredTrigger;
	}
}
