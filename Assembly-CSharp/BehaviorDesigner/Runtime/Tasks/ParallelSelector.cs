using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DE RID: 222
	[TaskDescription("Similar to the selector task, the parallel selector task will return success as soon as a child task returns success. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. If one tasks returns success the parallel selector task will end all of the child tasks and return success. If every child task returns failure then the parallel selector task will return failure.")]
	[TaskIcon("{SkinColor}ParallelSelectorIcon.png")]
	public class ParallelSelector : Composite
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x0001E196 File Offset: 0x0001C596
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001E1AE File Offset: 0x0001C5AE
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = TaskStatus.Running;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001E1C7 File Offset: 0x0001C5C7
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001E1CA File Offset: 0x0001C5CA
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001E1D2 File Offset: 0x0001C5D2
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001E1E7 File Offset: 0x0001C5E7
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001E1F4 File Offset: 0x0001C5F4
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001E22C File Offset: 0x0001C62C
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == TaskStatus.Running)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == TaskStatus.Success)
				{
					return TaskStatus.Success;
				}
			}
			return (!flag) ? TaskStatus.Running : TaskStatus.Failure;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001E288 File Offset: 0x0001C688
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x0400043B RID: 1083
		private int currentChildIndex;

		// Token: 0x0400043C RID: 1084
		private TaskStatus[] executionStatus;
	}
}
