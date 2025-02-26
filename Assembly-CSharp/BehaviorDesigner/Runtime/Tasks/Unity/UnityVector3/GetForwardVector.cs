using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B4 RID: 692
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the forward vector value.")]
	public class GetForwardVector : Action
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x0002E40E File Offset: 0x0002C80E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.forward;
			return TaskStatus.Success;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002E421 File Offset: 0x0002C821
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000AB1 RID: 2737
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
