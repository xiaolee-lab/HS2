using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B6 RID: 694
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002E492 File Offset: 0x0002C892
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.right;
			return TaskStatus.Success;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002E4A5 File Offset: 0x0002C8A5
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000AB4 RID: 2740
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
