using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x02000203 RID: 515
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion after a rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00028AE7 File Offset: 0x00026EE7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.RotateTowards(this.fromQuaternion.Value, this.toQuaternion.Value, this.maxDeltaDegrees.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00028B1C File Offset: 0x00026F1C
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.maxDeltaDegrees = 0f;
		}

		// Token: 0x0400085E RID: 2142
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x0400085F RID: 2143
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04000860 RID: 2144
		[Tooltip("The maximum degrees delta")]
		public SharedFloat maxDeltaDegrees;

		// Token: 0x04000861 RID: 2145
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
