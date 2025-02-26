using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000FA RID: 250
	[TaskDescription("The until success task will keep executing its child task until the child task returns success.")]
	[TaskIcon("{SkinColor}UntilSuccessIcon.png")]
	public class UntilSuccess : Decorator
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001FB4C File Offset: 0x0001DF4C
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Failure || this.executionStatus == TaskStatus.Inactive;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001FB66 File Offset: 0x0001DF66
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001FB6F File Offset: 0x0001DF6F
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000495 RID: 1173
		private TaskStatus executionStatus;
	}
}
