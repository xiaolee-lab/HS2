using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200024E RID: 590
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedString : Conditional
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x0002B4AD File Offset: 0x000298AD
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002B4D6 File Offset: 0x000298D6
		public override void OnReset()
		{
			this.variable = string.Empty;
			this.compareTo = string.Empty;
		}

		// Token: 0x04000977 RID: 2423
		[Tooltip("The first variable to compare")]
		public SharedString variable;

		// Token: 0x04000978 RID: 2424
		[Tooltip("The variable to compare to")]
		public SharedString compareTo;
	}
}
