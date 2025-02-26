using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002BD RID: 701
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Multiply the Vector3 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002E73B File Offset: 0x0002CB3B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value * this.multiplyBy.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002E764 File Offset: 0x0002CB64
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x04000AC6 RID: 2758
		[Tooltip("The Vector3 to multiply of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000AC7 RID: 2759
		[Tooltip("The value to multiply the Vector3 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04000AC8 RID: 2760
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
