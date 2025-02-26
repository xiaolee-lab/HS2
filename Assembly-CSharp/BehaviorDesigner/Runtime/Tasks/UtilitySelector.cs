using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E5 RID: 229
	[TaskDescription("The utility selector task evaluates the child tasks using Utility Theory AI. The child task can override the GetUtility method and return the utility value at that particular time. The task with the highest utility value will be selected and the existing running task will be aborted. The utility selector task reevaluates its children every tick.")]
	[TaskIcon("{SkinColor}UtilitySelectorIcon.png")]
	public class UtilitySelector : Composite
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0001E97C File Offset: 0x0001CD7C
		public override void OnStart()
		{
			this.highestUtility = float.MinValue;
			this.availableChildren.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				float utility = this.children[i].GetUtility();
				if (utility > this.highestUtility)
				{
					this.highestUtility = utility;
					this.currentChildIndex = i;
				}
				this.availableChildren.Add(i);
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001E9F3 File Offset: 0x0001CDF3
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001E9FB File Offset: 0x0001CDFB
		public override void OnChildStarted(int childIndex)
		{
			this.executionStatus = TaskStatus.Running;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001EA04 File Offset: 0x0001CE04
		public override bool CanExecute()
		{
			return this.executionStatus != TaskStatus.Success && this.executionStatus != TaskStatus.Running && !this.reevaluating && this.availableChildren.Count > 0;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001EA3C File Offset: 0x0001CE3C
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus != TaskStatus.Inactive && childStatus != TaskStatus.Running)
			{
				this.executionStatus = childStatus;
				if (this.executionStatus == TaskStatus.Failure)
				{
					this.availableChildren.Remove(childIndex);
					this.highestUtility = float.MinValue;
					for (int i = 0; i < this.availableChildren.Count; i++)
					{
						float utility = this.children[this.availableChildren[i]].GetUtility();
						if (utility > this.highestUtility)
						{
							this.highestUtility = utility;
							this.currentChildIndex = this.availableChildren[i];
						}
					}
				}
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001EADF File Offset: 0x0001CEDF
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001EAEF File Offset: 0x0001CEEF
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001EAFF File Offset: 0x0001CEFF
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001EB07 File Offset: 0x0001CF07
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001EB0A File Offset: 0x0001CF0A
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001EB0D File Offset: 0x0001CF0D
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == TaskStatus.Inactive)
			{
				return false;
			}
			this.reevaluating = true;
			return true;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001EB24 File Offset: 0x0001CF24
		public override void OnReevaluationEnded(TaskStatus status)
		{
			this.reevaluating = false;
			int num = this.currentChildIndex;
			this.highestUtility = float.MinValue;
			for (int i = 0; i < this.availableChildren.Count; i++)
			{
				float utility = this.children[this.availableChildren[i]].GetUtility();
				if (utility > this.highestUtility)
				{
					this.highestUtility = utility;
					this.currentChildIndex = this.availableChildren[i];
				}
			}
			if (num != this.currentChildIndex)
			{
				BehaviorManager.instance.Interrupt(base.Owner, this.children[num], this);
				this.executionStatus = TaskStatus.Inactive;
			}
		}

		// Token: 0x04000452 RID: 1106
		private int currentChildIndex;

		// Token: 0x04000453 RID: 1107
		private float highestUtility;

		// Token: 0x04000454 RID: 1108
		private TaskStatus executionStatus;

		// Token: 0x04000455 RID: 1109
		private bool reevaluating;

		// Token: 0x04000456 RID: 1110
		private List<int> availableChildren = new List<int>();
	}
}
