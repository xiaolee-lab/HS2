using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001FC RID: 508
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the dot product between two rotations.")]
	public class Dot : Action
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0002888D File Offset: 0x00026C8D
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Dot(this.leftRotation.Value, this.rightRotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000288B8 File Offset: 0x00026CB8
		public override void OnReset()
		{
			this.leftRotation = (this.rightRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x0400084C RID: 2124
		[Tooltip("The first rotation")]
		public SharedQuaternion leftRotation;

		// Token: 0x0400084D RID: 2125
		[Tooltip("The second rotation")]
		public SharedQuaternion rightRotation;

		// Token: 0x0400084E RID: 2126
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
