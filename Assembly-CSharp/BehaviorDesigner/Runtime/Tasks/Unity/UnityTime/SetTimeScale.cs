using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000279 RID: 633
	[TaskCategory("Unity/Time")]
	[TaskDescription("Sets the scale at which time is passing.")]
	public class SetTimeScale : Action
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x0002C488 File Offset: 0x0002A888
		public override TaskStatus OnUpdate()
		{
			Time.timeScale = this.timeScale.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002C49B File Offset: 0x0002A89B
		public override void OnReset()
		{
			this.timeScale.Value = 0f;
		}

		// Token: 0x040009D8 RID: 2520
		[Tooltip("The timescale")]
		public SharedFloat timeScale;
	}
}
