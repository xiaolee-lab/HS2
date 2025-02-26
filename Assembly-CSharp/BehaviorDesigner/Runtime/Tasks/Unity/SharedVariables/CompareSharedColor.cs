using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000245 RID: 581
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedColor : Conditional
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x0002AFA0 File Offset: 0x000293A0
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002AFD7 File Offset: 0x000293D7
		public override void OnReset()
		{
			this.variable = Color.black;
			this.compareTo = Color.black;
		}

		// Token: 0x04000965 RID: 2405
		[Tooltip("The first variable to compare")]
		public SharedColor variable;

		// Token: 0x04000966 RID: 2406
		[Tooltip("The variable to compare to")]
		public SharedColor compareTo;
	}
}
