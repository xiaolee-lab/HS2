using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000183 RID: 387
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the pressed state of the specified key.")]
	public class GetKey : Action
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x00024AB3 File Offset: 0x00022EB3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetKey(this.key);
			return TaskStatus.Success;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00024ACC File Offset: 0x00022ECC
		public override void OnReset()
		{
			this.key = KeyCode.None;
			this.storeResult = false;
		}

		// Token: 0x040006A9 RID: 1705
		[Tooltip("The key to test.")]
		public KeyCode key;

		// Token: 0x040006AA RID: 1706
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
