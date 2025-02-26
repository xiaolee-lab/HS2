using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F6 RID: 246
	[TaskDescription("The return failure task will always return failure except when the child task is running.")]
	[TaskIcon("{SkinColor}ReturnFailureIcon.png")]
	public class ReturnFailure : Decorator
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x0001F971 File Offset: 0x0001DD71
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001F98A File Offset: 0x0001DD8A
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001F993 File Offset: 0x0001DD93
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == TaskStatus.Success)
			{
				return TaskStatus.Failure;
			}
			return status;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001F99F File Offset: 0x0001DD9F
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400048D RID: 1165
		private TaskStatus executionStatus;
	}
}
