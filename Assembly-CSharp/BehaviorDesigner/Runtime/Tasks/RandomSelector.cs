using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E0 RID: 224
	[TaskDescription("Similar to the selector task, the random selector task will return success as soon as a child task returns success.  The difference is that the random selector class will run its children in a random order. The selector task is deterministic in that it will always run the tasks from left to right within the tree. The random selector task shuffles the child tasks up and then begins execution in a random order. Other than that the random selector class is the same as the selector class. It will continue running tasks until a task completes successfully. If no child tasks return success then it will return failure.")]
	[TaskIcon("{SkinColor}RandomSelectorIcon.png")]
	public class RandomSelector : Composite
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001E408 File Offset: 0x0001C808
		public override void OnAwake()
		{
			if (this.useSeed)
			{
				UnityEngine.Random.InitState(this.seed);
			}
			this.childIndexList.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.childIndexList.Add(i);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001E45E File Offset: 0x0001C85E
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001E466 File Offset: 0x0001C866
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001E473 File Offset: 0x0001C873
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != TaskStatus.Success;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001E495 File Offset: 0x0001C895
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001E4BB File Offset: 0x0001C8BB
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = TaskStatus.Inactive;
			this.ShuffleChilden();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001E4D5 File Offset: 0x0001C8D5
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001E4E9 File Offset: 0x0001C8E9
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001E4FC File Offset: 0x0001C8FC
		private void ShuffleChilden()
		{
			for (int i = this.childIndexList.Count; i > 0; i--)
			{
				int index = UnityEngine.Random.Range(0, i);
				int num = this.childIndexList[index];
				this.childrenExecutionOrder.Push(num);
				this.childIndexList[index] = this.childIndexList[i - 1];
				this.childIndexList[i - 1] = num;
			}
		}

		// Token: 0x04000440 RID: 1088
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04000441 RID: 1089
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04000442 RID: 1090
		private List<int> childIndexList = new List<int>();

		// Token: 0x04000443 RID: 1091
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04000444 RID: 1092
		private TaskStatus executionStatus;
	}
}
