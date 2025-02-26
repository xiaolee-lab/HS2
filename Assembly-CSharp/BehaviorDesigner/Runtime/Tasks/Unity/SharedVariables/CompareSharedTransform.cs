using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200024F RID: 591
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransform : Conditional
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x0002B500 File Offset: 0x00029900
		public override TaskStatus OnUpdate()
		{
			if (this.variable.Value == null && this.compareTo.Value != null)
			{
				return TaskStatus.Failure;
			}
			if (this.variable.Value == null && this.compareTo.Value == null)
			{
				return TaskStatus.Success;
			}
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002B590 File Offset: 0x00029990
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04000979 RID: 2425
		[Tooltip("The first variable to compare")]
		public SharedTransform variable;

		// Token: 0x0400097A RID: 2426
		[Tooltip("The variable to compare to")]
		public SharedTransform compareTo;
	}
}
