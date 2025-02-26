using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B5 RID: 437
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a random float value")]
	public class RandomFloat : Action
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00026120 File Offset: 0x00024520
		public override TaskStatus OnUpdate()
		{
			if (this.inclusive)
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value);
			}
			else
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value - 1E-05f);
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00026190 File Offset: 0x00024590
		public override void OnReset()
		{
			this.min.Value = 0f;
			this.max.Value = 0f;
			this.inclusive = false;
			this.storeResult.Value = 0f;
		}

		// Token: 0x0400074B RID: 1867
		[Tooltip("The minimum amount")]
		public SharedFloat min;

		// Token: 0x0400074C RID: 1868
		[Tooltip("The maximum amount")]
		public SharedFloat max;

		// Token: 0x0400074D RID: 1869
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x0400074E RID: 1870
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
