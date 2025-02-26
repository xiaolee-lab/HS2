using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000253 RID: 595
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector4 : Conditional
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x0002B750 File Offset: 0x00029B50
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002B787 File Offset: 0x00029B87
		public override void OnReset()
		{
			this.variable = Vector4.zero;
			this.compareTo = Vector4.zero;
		}

		// Token: 0x04000981 RID: 2433
		[Tooltip("The first variable to compare")]
		public SharedVector4 variable;

		// Token: 0x04000982 RID: 2434
		[Tooltip("The variable to compare to")]
		public SharedVector4 compareTo;
	}
}
