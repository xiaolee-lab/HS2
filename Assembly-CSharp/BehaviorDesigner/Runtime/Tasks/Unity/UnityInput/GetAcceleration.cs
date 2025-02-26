using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200017F RID: 383
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the acceleration value.")]
	public class GetAcceleration : Action
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x00024939 File Offset: 0x00022D39
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.acceleration;
			return TaskStatus.Success;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0002494C File Offset: 0x00022D4C
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x040006A0 RID: 1696
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector3 storeResult;
	}
}
