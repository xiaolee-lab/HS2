using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B3 RID: 691
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the dot product of two Vector3 values.")]
	public class Dot : Action
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002E3A6 File Offset: 0x0002C7A6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002E3D0 File Offset: 0x0002C7D0
		public override void OnReset()
		{
			this.leftHandSide = (this.rightHandSide = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04000AAE RID: 2734
		[Tooltip("The left hand side of the dot product")]
		public SharedVector3 leftHandSide;

		// Token: 0x04000AAF RID: 2735
		[Tooltip("The right hand side of the dot product")]
		public SharedVector3 rightHandSide;

		// Token: 0x04000AB0 RID: 2736
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
