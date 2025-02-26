using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A0 RID: 672
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Returns the distance between two Vector2s.")]
	public class Distance : Action
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002DC1A File Offset: 0x0002C01A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Distance(this.firstVector2.Value, this.secondVector2.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002DC44 File Offset: 0x0002C044
		public override void OnReset()
		{
			this.firstVector2 = (this.secondVector2 = Vector2.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04000A7A RID: 2682
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x04000A7B RID: 2683
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x04000A7C RID: 2684
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
