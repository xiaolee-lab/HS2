using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000275 RID: 629
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the time in seconds it took to complete the last frame.")]
	public class GetDeltaTime : Action
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x0002C3D4 File Offset: 0x0002A7D4
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.deltaTime;
			return TaskStatus.Success;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002C3E7 File Offset: 0x0002A7E7
		public override void OnReset()
		{
			this.storeResult.Value = 0f;
		}

		// Token: 0x040009D4 RID: 2516
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
