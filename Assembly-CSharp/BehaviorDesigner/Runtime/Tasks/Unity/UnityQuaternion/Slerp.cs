using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x02000204 RID: 516
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Spherically lerp between two quaternions.")]
	public class Slerp : Action
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x00028B63 File Offset: 0x00026F63
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Slerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00028B98 File Offset: 0x00026F98
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x04000862 RID: 2146
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04000863 RID: 2147
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04000864 RID: 2148
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x04000865 RID: 2149
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
