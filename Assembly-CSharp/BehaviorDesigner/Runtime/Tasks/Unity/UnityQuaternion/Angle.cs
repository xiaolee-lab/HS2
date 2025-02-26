using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001FA RID: 506
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the angle in degrees between two rotations.")]
	public class Angle : Action
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x000287C1 File Offset: 0x00026BC1
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Angle(this.firstRotation.Value, this.secondRotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000287EC File Offset: 0x00026BEC
		public override void OnReset()
		{
			this.firstRotation = (this.secondRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x04000846 RID: 2118
		[Tooltip("The first rotation")]
		public SharedQuaternion firstRotation;

		// Token: 0x04000847 RID: 2119
		[Tooltip("The second rotation")]
		public SharedQuaternion secondRotation;

		// Token: 0x04000848 RID: 2120
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
