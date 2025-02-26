using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D1 RID: 209
	[TaskDescription("Perform the actual interruption. This will immediately stop the specified tasks from running and will return success or failure depending on the value of interrupt success.")]
	[TaskIcon("{SkinColor}PerformInterruptionIcon.png")]
	public class PerformInterruption : Action
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0001D3D8 File Offset: 0x0001B7D8
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.interruptTasks.Length; i++)
			{
				this.interruptTasks[i].DoInterrupt((!this.interruptSuccess.Value) ? TaskStatus.Failure : TaskStatus.Success);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001D423 File Offset: 0x0001B823
		public override void OnReset()
		{
			this.interruptTasks = null;
			this.interruptSuccess = false;
		}

		// Token: 0x04000402 RID: 1026
		[Tooltip("The list of tasks to interrupt. Can be any number of tasks")]
		public Interrupt[] interruptTasks;

		// Token: 0x04000403 RID: 1027
		[Tooltip("When we interrupt the task should we return a task status of success?")]
		public SharedBool interruptSuccess;
	}
}
