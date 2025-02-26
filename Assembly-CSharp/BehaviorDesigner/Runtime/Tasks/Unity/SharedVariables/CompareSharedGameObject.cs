using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000247 RID: 583
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObject : Conditional
	{
		// Token: 0x06000A6C RID: 2668 RVA: 0x0002B068 File Offset: 0x00029468
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

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002B0F8 File Offset: 0x000294F8
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04000969 RID: 2409
		[Tooltip("The first variable to compare")]
		public SharedGameObject variable;

		// Token: 0x0400096A RID: 2410
		[Tooltip("The variable to compare to")]
		public SharedGameObject compareTo;
	}
}
