using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000250 RID: 592
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransformList : Conditional
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x0002B5A8 File Offset: 0x000299A8
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

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002B670 File Offset: 0x00029A70
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x0400097B RID: 2427
		[Tooltip("The first variable to compare")]
		public SharedTransformList variable;

		// Token: 0x0400097C RID: 2428
		[Tooltip("The variable to compare to")]
		public SharedTransformList compareTo;
	}
}
