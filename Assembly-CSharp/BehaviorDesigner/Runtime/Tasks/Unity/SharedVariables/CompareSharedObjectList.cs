using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200024B RID: 587
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObjectList : Conditional
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0002B2F4 File Offset: 0x000296F4
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
			if (this.variable.Value.Count != this.compareTo.Value.Count)
			{
				return TaskStatus.Failure;
			}
			for (int i = 0; i < this.variable.Value.Count; i++)
			{
				if (this.variable.Value[i] != this.compareTo.Value[i])
				{
					return TaskStatus.Failure;
				}
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002B3BC File Offset: 0x000297BC
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x04000971 RID: 2417
		[Tooltip("The first variable to compare")]
		public SharedObjectList variable;

		// Token: 0x04000972 RID: 2418
		[Tooltip("The variable to compare to")]
		public SharedObjectList compareTo;
	}
}
