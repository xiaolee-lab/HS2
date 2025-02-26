using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DB RID: 219
	[TaskDescription("Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
	[TaskIcon("{SkinColor}WaitIcon.png")]
	public class Wait : Action
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001DE48 File Offset: 0x0001C248
		public override void OnStart()
		{
			this.startTime = Time.time;
			if (this.randomWait.Value)
			{
				this.waitDuration = UnityEngine.Random.Range(this.randomWaitMin.Value, this.randomWaitMax.Value);
			}
			else
			{
				this.waitDuration = this.waitTime.Value;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001DEA7 File Offset: 0x0001C2A7
		public override TaskStatus OnUpdate()
		{
			if (this.startTime + this.waitDuration < Time.time)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001DEC3 File Offset: 0x0001C2C3
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this.pauseTime = Time.time;
			}
			else
			{
				this.startTime += Time.time - this.pauseTime;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001DEF4 File Offset: 0x0001C2F4
		public override void OnReset()
		{
			this.waitTime = 1f;
			this.randomWait = false;
			this.randomWaitMin = 1f;
			this.randomWaitMax = 1f;
		}

		// Token: 0x04000430 RID: 1072
		[Tooltip("The amount of time to wait")]
		public SharedFloat waitTime = 1f;

		// Token: 0x04000431 RID: 1073
		[Tooltip("Should the wait be randomized?")]
		public SharedBool randomWait = false;

		// Token: 0x04000432 RID: 1074
		[Tooltip("The minimum wait time if random wait is enabled")]
		public SharedFloat randomWaitMin = 1f;

		// Token: 0x04000433 RID: 1075
		[Tooltip("The maximum wait time if random wait is enabled")]
		public SharedFloat randomWaitMax = 1f;

		// Token: 0x04000434 RID: 1076
		private float waitDuration;

		// Token: 0x04000435 RID: 1077
		private float startTime;

		// Token: 0x04000436 RID: 1078
		private float pauseTime;
	}
}
