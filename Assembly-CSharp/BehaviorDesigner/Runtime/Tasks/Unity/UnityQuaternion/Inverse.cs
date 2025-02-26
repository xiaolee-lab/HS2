using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x02000200 RID: 512
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the inverse of the specified quaternion.")]
	public class Inverse : Action
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x000289D3 File Offset: 0x00026DD3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Inverse(this.targetQuaternion.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000289F4 File Offset: 0x00026DF4
		public override void OnReset()
		{
			this.targetQuaternion = (this.storeResult = Quaternion.identity);
		}

		// Token: 0x04000855 RID: 2133
		[Tooltip("The target quaternion")]
		public SharedQuaternion targetQuaternion;

		// Token: 0x04000856 RID: 2134
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
