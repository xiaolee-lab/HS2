using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E4 RID: 228
	[TaskDescription("The sequence task is similar to an \"and\" operation. It will return failure as soon as one of its child tasks return failure. If a child task returns success then it will sequentially run the next task. If all child tasks return success then it will return success.")]
	[TaskIcon("{SkinColor}SequenceIcon.png")]
	public class Sequence : Composite
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0001E903 File Offset: 0x0001CD03
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001E90B File Offset: 0x0001CD0B
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != TaskStatus.Failure;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001E932 File Offset: 0x0001CD32
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001E949 File Offset: 0x0001CD49
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001E959 File Offset: 0x0001CD59
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x04000450 RID: 1104
		private int currentChildIndex;

		// Token: 0x04000451 RID: 1105
		private TaskStatus executionStatus;
	}
}
