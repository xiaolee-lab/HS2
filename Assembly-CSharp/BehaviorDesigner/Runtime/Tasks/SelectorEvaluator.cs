using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E3 RID: 227
	[TaskDescription("The selector evaluator is a selector task which reevaluates its children every tick. It will run the lowest priority child which returns a task status of running. This is done each tick. If a higher priority child is running and the next frame a lower priority child wants to run it will interrupt the higher priority child. The selector evaluator will return success as soon as the first child returns success otherwise it will keep trying higher priority children. This task mimics the conditional abort functionality except the child tasks don't always have to be conditional tasks.")]
	[TaskIcon("{SkinColor}SelectorEvaluatorIcon.png")]
	public class SelectorEvaluator : Composite
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0001E774 File Offset: 0x0001CB74
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001E77C File Offset: 0x0001CB7C
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus = TaskStatus.Running;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001E794 File Offset: 0x0001CB94
		public override bool CanExecute()
		{
			if (this.executionStatus == TaskStatus.Success || this.executionStatus == TaskStatus.Running)
			{
				return false;
			}
			if (this.storedCurrentChildIndex != -1)
			{
				return this.currentChildIndex < this.storedCurrentChildIndex - 1;
			}
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001E7EB File Offset: 0x0001CBEB
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus == TaskStatus.Inactive && this.children[childIndex].Disabled)
			{
				this.executionStatus = TaskStatus.Failure;
			}
			if (childStatus != TaskStatus.Inactive && childStatus != TaskStatus.Running)
			{
				this.executionStatus = childStatus;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001E824 File Offset: 0x0001CC24
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001E834 File Offset: 0x0001CC34
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001E844 File Offset: 0x0001CC44
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001E84C File Offset: 0x0001CC4C
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001E84F File Offset: 0x0001CC4F
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001E852 File Offset: 0x0001CC52
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == TaskStatus.Inactive)
			{
				return false;
			}
			this.storedCurrentChildIndex = this.currentChildIndex;
			this.storedExecutionStatus = this.executionStatus;
			this.currentChildIndex = 0;
			this.executionStatus = TaskStatus.Inactive;
			return true;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001E888 File Offset: 0x0001CC88
		public override void OnReevaluationEnded(TaskStatus status)
		{
			if (this.executionStatus != TaskStatus.Failure && this.executionStatus != TaskStatus.Inactive)
			{
				BehaviorManager.instance.Interrupt(base.Owner, this.children[this.storedCurrentChildIndex - 1], this);
			}
			else
			{
				this.currentChildIndex = this.storedCurrentChildIndex;
				this.executionStatus = this.storedExecutionStatus;
			}
			this.storedCurrentChildIndex = -1;
			this.storedExecutionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400044C RID: 1100
		private int currentChildIndex;

		// Token: 0x0400044D RID: 1101
		private TaskStatus executionStatus;

		// Token: 0x0400044E RID: 1102
		private int storedCurrentChildIndex = -1;

		// Token: 0x0400044F RID: 1103
		private TaskStatus storedExecutionStatus;
	}
}
