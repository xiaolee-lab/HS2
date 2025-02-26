using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E2 RID: 226
	[TaskDescription("The selector task is similar to an \"or\" operation. It will return success as soon as one of its child tasks return success. If a child task returns failure then it will sequentially run the next task. If no child task returns success then it will return failure.")]
	[TaskIcon("{SkinColor}SelectorIcon.png")]
	public class Selector : Composite
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0001E6FF File Offset: 0x0001CAFF
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001E707 File Offset: 0x0001CB07
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != TaskStatus.Success;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001E72E File Offset: 0x0001CB2E
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001E745 File Offset: 0x0001CB45
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001E755 File Offset: 0x0001CB55
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x0400044A RID: 1098
		private int currentChildIndex;

		// Token: 0x0400044B RID: 1099
		private TaskStatus executionStatus;
	}
}
