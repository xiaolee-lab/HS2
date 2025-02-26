using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000EF RID: 239
	[TaskDescription("The random probability task will return success when the random probability is above the succeed probability. It will otherwise return failure.")]
	public class RandomProbability : Conditional
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x0001F47F File Offset: 0x0001D87F
		public override void OnAwake()
		{
			if (this.useSeed.Value)
			{
				UnityEngine.Random.InitState(this.seed.Value);
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001F4A4 File Offset: 0x0001D8A4
		public override TaskStatus OnUpdate()
		{
			float value = UnityEngine.Random.value;
			if (value < this.successProbability.Value)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001F4CB File Offset: 0x0001D8CB
		public override void OnReset()
		{
			this.successProbability = 0.5f;
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x04000475 RID: 1141
		[Tooltip("The chance that the task will return success")]
		public SharedFloat successProbability = 0.5f;

		// Token: 0x04000476 RID: 1142
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public SharedInt seed;

		// Token: 0x04000477 RID: 1143
		[Tooltip("Do we want to use the seed?")]
		public SharedBool useSeed;
	}
}
