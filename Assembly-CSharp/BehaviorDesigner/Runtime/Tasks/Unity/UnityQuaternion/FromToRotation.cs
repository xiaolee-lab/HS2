using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001FE RID: 510
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores a rotation which rotates from the first direction to the second.")]
	public class FromToRotation : Action
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x0002893E File Offset: 0x00026D3E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.FromToRotation(this.fromDirection.Value, this.toDirection.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00028968 File Offset: 0x00026D68
		public override void OnReset()
		{
			this.fromDirection = (this.toDirection = Vector3.zero);
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04000851 RID: 2129
		[Tooltip("The from rotation")]
		public SharedVector3 fromDirection;

		// Token: 0x04000852 RID: 2130
		[Tooltip("The to rotation")]
		public SharedVector3 toDirection;

		// Token: 0x04000853 RID: 2131
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
