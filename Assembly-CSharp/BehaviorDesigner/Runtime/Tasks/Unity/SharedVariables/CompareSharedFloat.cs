using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000246 RID: 582
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedFloat : Conditional
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x0002B004 File Offset: 0x00029404
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002B03B File Offset: 0x0002943B
		public override void OnReset()
		{
			this.variable = 0f;
			this.compareTo = 0f;
		}

		// Token: 0x04000967 RID: 2407
		[Tooltip("The first variable to compare")]
		public SharedFloat variable;

		// Token: 0x04000968 RID: 2408
		[Tooltip("The variable to compare to")]
		public SharedFloat compareTo;
	}
}
