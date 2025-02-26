using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F9 RID: 249
	[TaskDescription("The until failure task will keep executing its child task until the child task returns failure.")]
	[TaskIcon("{SkinColor}UntilFailureIcon.png")]
	public class UntilFailure : Decorator
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0001FB18 File Offset: 0x0001DF18
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Success || this.executionStatus == TaskStatus.Inactive;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001FB32 File Offset: 0x0001DF32
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001FB3B File Offset: 0x0001DF3B
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000494 RID: 1172
		private TaskStatus executionStatus;
	}
}
