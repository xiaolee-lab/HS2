using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200024C RID: 588
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedQuaternion : Conditional
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0002B3D4 File Offset: 0x000297D4
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002B40B File Offset: 0x0002980B
		public override void OnReset()
		{
			this.variable = Quaternion.identity;
			this.compareTo = Quaternion.identity;
		}

		// Token: 0x04000973 RID: 2419
		[Tooltip("The first variable to compare")]
		public SharedQuaternion variable;

		// Token: 0x04000974 RID: 2420
		[Tooltip("The variable to compare to")]
		public SharedQuaternion compareTo;
	}
}
