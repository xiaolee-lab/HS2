using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B8 RID: 696
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002E516 File Offset: 0x0002C916
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.up;
			return TaskStatus.Success;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002E529 File Offset: 0x0002C929
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000AB7 RID: 2743
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
