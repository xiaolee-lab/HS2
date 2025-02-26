using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000248 RID: 584
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObjectList : Conditional
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x0002B110 File Offset: 0x00029510
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

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002B1D8 File Offset: 0x000295D8
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x0400096B RID: 2411
		[Tooltip("The first variable to compare")]
		public SharedGameObjectList variable;

		// Token: 0x0400096C RID: 2412
		[Tooltip("The variable to compare to")]
		public SharedGameObjectList compareTo;
	}
}
