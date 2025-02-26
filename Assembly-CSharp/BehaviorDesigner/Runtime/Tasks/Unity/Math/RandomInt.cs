using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B6 RID: 438
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a random int value")]
	public class RandomInt : Action
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x000261D4 File Offset: 0x000245D4
		public override TaskStatus OnUpdate()
		{
			if (this.inclusive)
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value + 1);
			}
			else
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00026240 File Offset: 0x00024640
		public override void OnReset()
		{
			this.min.Value = 0;
			this.max.Value = 0;
			this.inclusive = false;
			this.storeResult.Value = 0;
		}

		// Token: 0x0400074F RID: 1871
		[Tooltip("The minimum amount")]
		public SharedInt min;

		// Token: 0x04000750 RID: 1872
		[Tooltip("The maximum amount")]
		public SharedInt max;

		// Token: 0x04000751 RID: 1873
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x04000752 RID: 1874
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}
