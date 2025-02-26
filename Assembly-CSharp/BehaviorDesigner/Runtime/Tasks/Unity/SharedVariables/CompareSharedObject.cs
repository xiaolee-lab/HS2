using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200024A RID: 586
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObject : Conditional
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x0002B24C File Offset: 0x0002964C
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

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002B2DC File Offset: 0x000296DC
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x0400096F RID: 2415
		[Tooltip("The first variable to compare")]
		public SharedObject variable;

		// Token: 0x04000970 RID: 2416
		[Tooltip("The variable to compare to")]
		public SharedObject compareTo;
	}
}
