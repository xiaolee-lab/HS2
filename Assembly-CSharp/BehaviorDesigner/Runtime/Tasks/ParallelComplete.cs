using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DD RID: 221
	[TaskDescription("Similar to the parallel selector task, except the parallel complete task will return the child status as soon as the child returns success or failure.The child tasks are executed simultaneously.")]
	[TaskIcon("{SkinColor}ParallelCompleteIcon.png")]
	public class ParallelComplete : Composite
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0001E06A File Offset: 0x0001C46A
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001E082 File Offset: 0x0001C482
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = TaskStatus.Running;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001E09B File Offset: 0x0001C49B
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001E09E File Offset: 0x0001C49E
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001E0A6 File Offset: 0x0001C4A6
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001E0BB File Offset: 0x0001C4BB
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001E0C8 File Offset: 0x0001C4C8
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001E100 File Offset: 0x0001C500
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.currentChildIndex == 0)
			{
				return TaskStatus.Success;
			}
			for (int i = 0; i < this.currentChildIndex; i++)
			{
				if (this.executionStatus[i] == TaskStatus.Success || this.executionStatus[i] == TaskStatus.Failure)
				{
					return this.executionStatus[i];
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001E158 File Offset: 0x0001C558
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04000439 RID: 1081
		private int currentChildIndex;

		// Token: 0x0400043A RID: 1082
		private TaskStatus[] executionStatus;
	}
}
