using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000259 RID: 601
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedInt variable to the specified object. Returns Success.")]
	public class SetSharedInt : Action
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002B91E File Offset: 0x00029D1E
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002B937 File Offset: 0x00029D37
		public override void OnReset()
		{
			this.targetValue = 0;
			this.targetVariable = 0;
		}

		// Token: 0x0400098E RID: 2446
		[Tooltip("The value to set the SharedInt to")]
		public SharedInt targetValue;

		// Token: 0x0400098F RID: 2447
		[RequiredField]
		[Tooltip("The SharedInt to set")]
		public SharedInt targetVariable;
	}
}
