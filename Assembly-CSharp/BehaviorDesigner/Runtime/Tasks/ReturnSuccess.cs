using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F7 RID: 247
	[TaskDescription("The return success task will always return success except when the child task is running.")]
	[TaskIcon("{SkinColor}ReturnSuccessIcon.png")]
	public class ReturnSuccess : Decorator
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0001F9B0 File Offset: 0x0001DDB0
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001F9C9 File Offset: 0x0001DDC9
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001F9D2 File Offset: 0x0001DDD2
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == TaskStatus.Failure)
			{
				return TaskStatus.Success;
			}
			return status;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001F9DE File Offset: 0x0001DDDE
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400048E RID: 1166
		private TaskStatus executionStatus;
	}
}
