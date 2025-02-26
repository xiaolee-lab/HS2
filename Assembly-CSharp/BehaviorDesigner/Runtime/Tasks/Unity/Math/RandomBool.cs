using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B4 RID: 436
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a random bool value")]
	public class RandomBool : Action
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x000260F0 File Offset: 0x000244F0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = (UnityEngine.Random.value < 0.5f);
			return TaskStatus.Success;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002610A File Offset: 0x0002450A
		public override void OnReset()
		{
			this.storeResult.Value = false;
		}

		// Token: 0x0400074A RID: 1866
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}
