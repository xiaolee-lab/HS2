using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B2 RID: 690
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Returns the distance between two Vector3s.")]
	public class Distance : Action
	{
		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002E33E File Offset: 0x0002C73E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Distance(this.firstVector3.Value, this.secondVector3.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002E368 File Offset: 0x0002C768
		public override void OnReset()
		{
			this.firstVector3 = (this.secondVector3 = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04000AAB RID: 2731
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04000AAC RID: 2732
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04000AAD RID: 2733
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
