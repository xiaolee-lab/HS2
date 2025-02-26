using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B5 RID: 693
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the magnitude of the Vector3.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x0002E43C File Offset: 0x0002C83C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.magnitude;
			return TaskStatus.Success;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002E468 File Offset: 0x0002C868
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04000AB2 RID: 2738
		[Tooltip("The Vector3 to get the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000AB3 RID: 2739
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
