using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000252 RID: 594
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector3 : Conditional
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x0002B6EC File Offset: 0x00029AEC
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002B723 File Offset: 0x00029B23
		public override void OnReset()
		{
			this.variable = Vector3.zero;
			this.compareTo = Vector3.zero;
		}

		// Token: 0x0400097F RID: 2431
		[Tooltip("The first variable to compare")]
		public SharedVector3 variable;

		// Token: 0x04000980 RID: 2432
		[Tooltip("The variable to compare to")]
		public SharedVector3 compareTo;
	}
}
