using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000276 RID: 630
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the real time in seconds since the game started.")]
	public class GetRealtimeSinceStartup : Action
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x0002C401 File Offset: 0x0002A801
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.realtimeSinceStartup;
			return TaskStatus.Success;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002C414 File Offset: 0x0002A814
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x040009D5 RID: 2517
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
