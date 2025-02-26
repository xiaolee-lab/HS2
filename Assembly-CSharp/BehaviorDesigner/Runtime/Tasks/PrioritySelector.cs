using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DF RID: 223
	[TaskDescription("Similar to the selector task, the priority selector task will return success as soon as a child task returns success. Instead of running the tasks sequentially from left to right within the tree, the priority selector will ask the task what its priority is to determine the order. The higher priority tasks have a higher chance at being run first.")]
	[TaskIcon("{SkinColor}PrioritySelectorIcon.png")]
	public class PrioritySelector : Composite
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0001E2D4 File Offset: 0x0001C6D4
		public override void OnStart()
		{
			this.childrenExecutionOrder.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				float priority = this.children[i].GetPriority();
				int index = this.childrenExecutionOrder.Count;
				for (int j = 0; j < this.childrenExecutionOrder.Count; j++)
				{
					if (this.children[this.childrenExecutionOrder[j]].GetPriority() < priority)
					{
						index = j;
						break;
					}
				}
				this.childrenExecutionOrder.Insert(index, i);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001E378 File Offset: 0x0001C778
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder[this.currentChildIndex];
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001E38B File Offset: 0x0001C78B
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != TaskStatus.Success;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001E3B2 File Offset: 0x0001C7B2
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001E3C9 File Offset: 0x0001C7C9
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001E3D9 File Offset: 0x0001C7D9
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x0400043D RID: 1085
		private int currentChildIndex;

		// Token: 0x0400043E RID: 1086
		private TaskStatus executionStatus;

		// Token: 0x0400043F RID: 1087
		private List<int> childrenExecutionOrder = new List<int>();
	}
}
