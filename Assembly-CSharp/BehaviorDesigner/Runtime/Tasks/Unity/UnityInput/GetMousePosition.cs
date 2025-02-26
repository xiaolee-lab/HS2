using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000185 RID: 389
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the mouse position.")]
	public class GetMousePosition : Action
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x00024B29 File Offset: 0x00022F29
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.mousePosition;
			return TaskStatus.Success;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00024B3C File Offset: 0x00022F3C
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x040006AD RID: 1709
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector3 storeResult;
	}
}
