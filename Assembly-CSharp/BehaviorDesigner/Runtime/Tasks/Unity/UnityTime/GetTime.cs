using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000277 RID: 631
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the time in second since the start of the game.")]
	public class GetTime : Action
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x0002C42E File Offset: 0x0002A82E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.time;
			return TaskStatus.Success;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002C441 File Offset: 0x0002A841
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x040009D6 RID: 2518
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
