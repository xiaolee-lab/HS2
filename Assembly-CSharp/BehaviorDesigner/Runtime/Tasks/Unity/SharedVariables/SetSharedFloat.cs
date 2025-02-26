using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000256 RID: 598
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedFloat variable to the specified object. Returns Success.")]
	public class SetSharedFloat : Action
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0002B82F File Offset: 0x00029C2F
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002B848 File Offset: 0x00029C48
		public override void OnReset()
		{
			this.targetValue = 0f;
			this.targetVariable = 0f;
		}

		// Token: 0x04000987 RID: 2439
		[Tooltip("The value to set the SharedFloat to")]
		public SharedFloat targetValue;

		// Token: 0x04000988 RID: 2440
		[RequiredField]
		[Tooltip("The SharedFloat to set")]
		public SharedFloat targetVariable;
	}
}
