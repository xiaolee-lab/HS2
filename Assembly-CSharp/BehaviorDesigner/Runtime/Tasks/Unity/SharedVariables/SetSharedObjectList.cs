using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200025B RID: 603
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedObjectList variable to the specified object. Returns Success.")]
	public class SetSharedObjectList : Action
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002B98A File Offset: 0x00029D8A
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002B9A3 File Offset: 0x00029DA3
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04000992 RID: 2450
		[Tooltip("The value to set the SharedObjectList to.")]
		public SharedObjectList targetValue;

		// Token: 0x04000993 RID: 2451
		[RequiredField]
		[Tooltip("The SharedObjectList to set")]
		public SharedObjectList targetVariable;
	}
}
