using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000278 RID: 632
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the scale at which time is passing.")]
	public class GetTimeScale : Action
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x0002C45B File Offset: 0x0002A85B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.timeScale;
			return TaskStatus.Success;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002C46E File Offset: 0x0002A86E
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x040009D7 RID: 2519
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
