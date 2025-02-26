using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DC RID: 220
	[TaskDescription("Similar to the sequence task, the parallel task will run each child task until a child task returns failure. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. Like the sequence class, the parallel task will return success once all of its children tasks have return success. If one tasks returns failure the parallel task will end all of the child tasks and return failure.")]
	[TaskIcon("{SkinColor}ParallelIcon.png")]
	public class Parallel : Composite
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0001DF3A File Offset: 0x0001C33A
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001DF52 File Offset: 0x0001C352
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = TaskStatus.Running;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001DF6B File Offset: 0x0001C36B
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001DF6E File Offset: 0x0001C36E
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001DF76 File Offset: 0x0001C376
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001DF8B File Offset: 0x0001C38B
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001DF98 File Offset: 0x0001C398
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == TaskStatus.Running)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == TaskStatus.Failure)
				{
					return TaskStatus.Failure;
				}
			}
			return (!flag) ? TaskStatus.Running : TaskStatus.Success;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001DFF4 File Offset: 0x0001C3F4
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001E02C File Offset: 0x0001C42C
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04000437 RID: 1079
		private int currentChildIndex;

		// Token: 0x04000438 RID: 1080
		private TaskStatus[] executionStatus;
	}
}
