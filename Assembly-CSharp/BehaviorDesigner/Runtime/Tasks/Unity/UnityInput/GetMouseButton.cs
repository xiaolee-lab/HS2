using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000184 RID: 388
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the state of the specified mouse button.")]
	public class GetMouseButton : Action
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x00024AE9 File Offset: 0x00022EE9
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetMouseButton(this.buttonIndex.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00024B07 File Offset: 0x00022F07
		public override void OnReset()
		{
			this.buttonIndex = 0;
			this.storeResult = false;
		}

		// Token: 0x040006AB RID: 1707
		[Tooltip("The index of the button")]
		public SharedInt buttonIndex;

		// Token: 0x040006AC RID: 1708
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
