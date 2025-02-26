using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000244 RID: 580
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedBool : Conditional
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x0002AF44 File Offset: 0x00029344
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002AF7B File Offset: 0x0002937B
		public override void OnReset()
		{
			this.variable = false;
			this.compareTo = false;
		}

		// Token: 0x04000963 RID: 2403
		[Tooltip("The first variable to compare")]
		public SharedBool variable;

		// Token: 0x04000964 RID: 2404
		[Tooltip("The variable to compare to")]
		public SharedBool compareTo;
	}
}
