using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001FB RID: 507
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the rotation which rotates the specified degrees around the specified axis.")]
	public class AngleAxis : Action
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x0002882A File Offset: 0x00026C2A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.AngleAxis(this.degrees.Value, this.axis.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00028853 File Offset: 0x00026C53
		public override void OnReset()
		{
			this.degrees = 0f;
			this.axis = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04000849 RID: 2121
		[Tooltip("The number of degrees")]
		public SharedFloat degrees;

		// Token: 0x0400084A RID: 2122
		[Tooltip("The axis direction")]
		public SharedVector3 axis;

		// Token: 0x0400084B RID: 2123
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
