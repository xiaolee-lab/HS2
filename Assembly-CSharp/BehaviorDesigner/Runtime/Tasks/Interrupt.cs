using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F3 RID: 243
	[TaskDescription("The interrupt task will stop all child tasks from running if it is interrupted. The interruption can be triggered by the perform interruption task. The interrupt task will keep running its child until this interruption is called. If no interruption happens and the child task completed its execution the interrupt task will return the value assigned by the child task.")]
	[TaskIcon("{SkinColor}InterruptIcon.png")]
	public class Interrupt : Decorator
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0001F80F File Offset: 0x0001DC0F
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001F828 File Offset: 0x0001DC28
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001F831 File Offset: 0x0001DC31
		public void DoInterrupt(TaskStatus status)
		{
			this.interruptStatus = status;
			BehaviorManager.instance.Interrupt(base.Owner, this);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001F84B File Offset: 0x0001DC4B
		public override TaskStatus OverrideStatus()
		{
			return this.interruptStatus;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001F853 File Offset: 0x0001DC53
		public override void OnEnd()
		{
			this.interruptStatus = TaskStatus.Failure;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000485 RID: 1157
		private TaskStatus interruptStatus = TaskStatus.Failure;

		// Token: 0x04000486 RID: 1158
		private TaskStatus executionStatus;
	}
}
