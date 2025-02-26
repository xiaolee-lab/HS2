using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F8 RID: 248
	[TaskDescription("The task guard task is similar to a semaphore in multithreaded programming. The task guard task is there to ensure a limited resource is not being overused. \n\nFor example, you may place a task guard above a task that plays an animation. Elsewhere within your behavior tree you may also have another task that plays a different animation but uses the same bones for that animation. Because of this you don't want that animation to play twice at the same time. Placing a task guard will let you specify how many times a particular task can be accessed at the same time.\n\nIn the previous animation task example you would specify an access count of 1. With this setup the animation task can be only controlled by one task at a time. If the first task is playing the animation and a second task wants to control the animation as well, it will either have to wait or skip over the task completely.")]
	[TaskIcon("{SkinColor}TaskGuardIcon.png")]
	public class TaskGuard : Decorator
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0001F9EF File Offset: 0x0001DDEF
		public override bool CanExecute()
		{
			return this.executingTasks < this.maxTaskAccessCount.Value && !this.executing;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001FA14 File Offset: 0x0001DE14
		public override void OnChildStarted()
		{
			this.executingTasks++;
			this.executing = true;
			for (int i = 0; i < this.linkedTaskGuards.Length; i++)
			{
				this.linkedTaskGuards[i].taskExecuting(true);
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001FA5D File Offset: 0x0001DE5D
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return (this.executing || !this.waitUntilTaskAvailable.Value) ? status : TaskStatus.Running;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001FA81 File Offset: 0x0001DE81
		public void taskExecuting(bool increase)
		{
			this.executingTasks += ((!increase) ? -1 : 1);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001FAA0 File Offset: 0x0001DEA0
		public override void OnEnd()
		{
			if (this.executing)
			{
				this.executingTasks--;
				for (int i = 0; i < this.linkedTaskGuards.Length; i++)
				{
					this.linkedTaskGuards[i].taskExecuting(false);
				}
				this.executing = false;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001FAF4 File Offset: 0x0001DEF4
		public override void OnReset()
		{
			this.maxTaskAccessCount = null;
			this.linkedTaskGuards = null;
			this.waitUntilTaskAvailable = true;
		}

		// Token: 0x0400048F RID: 1167
		[Tooltip("The number of times the child tasks can be accessed by parallel tasks at once")]
		public SharedInt maxTaskAccessCount;

		// Token: 0x04000490 RID: 1168
		[Tooltip("The linked tasks that also guard a task. If the task guard is not linked against any other tasks it doesn't have much purpose. Marked as LinkedTask to ensure all tasks linked are linked to the same set of tasks")]
		[LinkedTask]
		public TaskGuard[] linkedTaskGuards;

		// Token: 0x04000491 RID: 1169
		[Tooltip("If true the task will wait until the child task is available. If false then any unavailable child tasks will be skipped over")]
		public SharedBool waitUntilTaskAvailable;

		// Token: 0x04000492 RID: 1170
		private int executingTasks;

		// Token: 0x04000493 RID: 1171
		private bool executing;
	}
}
