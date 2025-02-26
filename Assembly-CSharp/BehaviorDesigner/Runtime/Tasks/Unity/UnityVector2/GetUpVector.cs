using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A5 RID: 677
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002DDC6 File Offset: 0x0002C1C6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.up;
			return TaskStatus.Success;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002DDD9 File Offset: 0x0002C1D9
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04000A85 RID: 2693
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
