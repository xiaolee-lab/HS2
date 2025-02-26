using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x02000201 RID: 513
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Lerps between two quaternions.")]
	public class Lerp : Action
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00028A22 File Offset: 0x00026E22
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Lerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00028A58 File Offset: 0x00026E58
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x04000857 RID: 2135
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04000858 RID: 2136
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04000859 RID: 2137
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x0400085A RID: 2138
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
