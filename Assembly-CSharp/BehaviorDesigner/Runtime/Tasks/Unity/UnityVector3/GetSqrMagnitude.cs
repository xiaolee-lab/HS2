using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B7 RID: 695
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the square magnitude of the Vector3.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002E4C0 File Offset: 0x0002C8C0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.sqrMagnitude;
			return TaskStatus.Success;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002E4EC File Offset: 0x0002C8EC
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04000AB5 RID: 2741
		[Tooltip("The Vector3 to get the square magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000AB6 RID: 2742
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
