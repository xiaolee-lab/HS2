using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001FD RID: 509
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion of a euler vector.")]
	public class Euler : Action
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x000288F6 File Offset: 0x00026CF6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Euler(this.eulerVector.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00028914 File Offset: 0x00026D14
		public override void OnReset()
		{
			this.eulerVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x0400084F RID: 2127
		[Tooltip("The euler vector")]
		public SharedVector3 eulerVector;

		// Token: 0x04000850 RID: 2128
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
