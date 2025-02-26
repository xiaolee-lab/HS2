using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F4 RID: 244
	[TaskDescription("The inverter task will invert the return value of the child task after it has finished executing. If the child returns success, the inverter task will return failure. If the child returns failure, the inverter task will return success.")]
	[TaskIcon("{SkinColor}InverterIcon.png")]
	public class Inverter : Decorator
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x0001F86B File Offset: 0x0001DC6B
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001F884 File Offset: 0x0001DC84
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001F88D File Offset: 0x0001DC8D
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == TaskStatus.Success)
			{
				return TaskStatus.Failure;
			}
			if (status == TaskStatus.Failure)
			{
				return TaskStatus.Success;
			}
			return status;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001F8A2 File Offset: 0x0001DCA2
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000487 RID: 1159
		private TaskStatus executionStatus;
	}
}
