using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F5 RID: 245
	[TaskDescription("The repeater task will repeat execution of its child task until the child task has been run a specified number of times. It has the option of continuing to execute the child task even if the child task returns a failure.")]
	[TaskIcon("{SkinColor}RepeaterIcon.png")]
	public class Repeater : Decorator
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x0001F8C0 File Offset: 0x0001DCC0
		public override bool CanExecute()
		{
			return (this.repeatForever.Value || this.executionCount < this.count.Value) && (!this.endOnFailure.Value || (this.endOnFailure.Value && this.executionStatus != TaskStatus.Failure));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F928 File Offset: 0x0001DD28
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionCount++;
			this.executionStatus = childStatus;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001F93F File Offset: 0x0001DD3F
		public override void OnEnd()
		{
			this.executionCount = 0;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001F94F File Offset: 0x0001DD4F
		public override void OnReset()
		{
			this.count = 0;
			this.endOnFailure = true;
		}

		// Token: 0x04000488 RID: 1160
		[Tooltip("The number of times to repeat the execution of its child task")]
		public SharedInt count = 1;

		// Token: 0x04000489 RID: 1161
		[Tooltip("Allows the repeater to repeat forever")]
		public SharedBool repeatForever;

		// Token: 0x0400048A RID: 1162
		[Tooltip("Should the task return if the child task returns a failure")]
		public SharedBool endOnFailure;

		// Token: 0x0400048B RID: 1163
		private int executionCount;

		// Token: 0x0400048C RID: 1164
		private TaskStatus executionStatus;
	}
}
