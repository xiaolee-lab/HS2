using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001FF RID: 511
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion identity.")]
	public class Identity : Action
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x000289A6 File Offset: 0x00026DA6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.identity;
			return TaskStatus.Success;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000289B9 File Offset: 0x00026DB9
		public override void OnReset()
		{
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04000854 RID: 2132
		[Tooltip("The identity")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
