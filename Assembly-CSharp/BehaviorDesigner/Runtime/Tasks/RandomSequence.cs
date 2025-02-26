using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E1 RID: 225
	[TaskDescription("Similar to the sequence task, the random sequence task will return success as soon as every child task returns success.  The difference is that the random sequence class will run its children in a random order. The sequence task is deterministic in that it will always run the tasks from left to right within the tree. The random sequence task shuffles the child tasks up and then begins execution in a random order. Other than that the random sequence class is the same as the sequence class. It will stop running tasks as soon as a single task ends in failure. On a task failure it will stop executing all of the child tasks and return failure. If no child returns failure then it will return success.")]
	[TaskIcon("{SkinColor}RandomSequenceIcon.png")]
	public class RandomSequence : Composite
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001E590 File Offset: 0x0001C990
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

		// Token: 0x0600050A RID: 1290 RVA: 0x0001E5E6 File Offset: 0x0001C9E6
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001E5EE File Offset: 0x0001C9EE
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001E5FB File Offset: 0x0001C9FB
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != TaskStatus.Failure;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001E61D File Offset: 0x0001CA1D
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001E643 File Offset: 0x0001CA43
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = TaskStatus.Inactive;
			this.ShuffleChilden();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001E65D File Offset: 0x0001CA5D
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001E671 File Offset: 0x0001CA71
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001E684 File Offset: 0x0001CA84
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

		// Token: 0x04000445 RID: 1093
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04000446 RID: 1094
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04000447 RID: 1095
		private List<int> childIndexList = new List<int>();

		// Token: 0x04000448 RID: 1096
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04000449 RID: 1097
		private TaskStatus executionStatus;
	}
}
