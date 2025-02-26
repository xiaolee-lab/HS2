using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002BE RID: 702
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Normalize the Vector3.")]
	public class Normalize : Action
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0002E7A2 File Offset: 0x0002CBA2
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Normalize(this.vector3Variable.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002E7C0 File Offset: 0x0002CBC0
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
		}

		// Token: 0x04000AC9 RID: 2761
		[Tooltip("The Vector3 to normalize")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000ACA RID: 2762
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
