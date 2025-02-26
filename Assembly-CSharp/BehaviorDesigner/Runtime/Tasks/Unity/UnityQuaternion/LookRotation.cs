using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x02000202 RID: 514
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion of a forward vector.")]
	public class LookRotation : Action
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x00028A9F File Offset: 0x00026E9F
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.LookRotation(this.forwardVector.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00028ABD File Offset: 0x00026EBD
		public override void OnReset()
		{
			this.forwardVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x0400085B RID: 2139
		[Tooltip("The forward vector")]
		public SharedVector3 forwardVector;

		// Token: 0x0400085C RID: 2140
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x0400085D RID: 2141
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
