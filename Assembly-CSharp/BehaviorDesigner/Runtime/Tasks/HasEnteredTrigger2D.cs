using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000EA RID: 234
	[TaskDescription("Returns success when an object enters the 2D trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredTrigger2D : Conditional
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0001F147 File Offset: 0x0001D547
		public override TaskStatus OnUpdate()
		{
			return (!this.enteredTrigger) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001F15B File Offset: 0x0001D55B
		public override void OnEnd()
		{
			this.enteredTrigger = false;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001F164 File Offset: 0x0001D564
		public override void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(other.gameObject.tag))
			{
				this.otherGameObject.Value = other.gameObject;
				this.enteredTrigger = true;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001F1BE File Offset: 0x0001D5BE
		public override void OnReset()
		{
			this.tag = string.Empty;
			this.otherGameObject = null;
		}

		// Token: 0x04000466 RID: 1126
		[Tooltip("The tag of the GameObject to check for a trigger against")]
		public SharedString tag = string.Empty;

		// Token: 0x04000467 RID: 1127
		[Tooltip("The object that entered the trigger")]
		public SharedGameObject otherGameObject;

		// Token: 0x04000468 RID: 1128
		private bool enteredTrigger;
	}
}
