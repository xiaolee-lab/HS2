using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000249 RID: 585
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedInt : Conditional
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0002B1F0 File Offset: 0x000295F0
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002B227 File Offset: 0x00029627
		public override void OnReset()
		{
			this.variable = 0;
			this.compareTo = 0;
		}

		// Token: 0x0400096D RID: 2413
		[Tooltip("The first variable to compare")]
		public SharedInt variable;

		// Token: 0x0400096E RID: 2414
		[Tooltip("The variable to compare to")]
		public SharedInt compareTo;
	}
}
