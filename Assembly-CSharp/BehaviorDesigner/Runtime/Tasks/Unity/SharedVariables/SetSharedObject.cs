using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200025A RID: 602
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedObject variable to the specified object. Returns Success.")]
	public class SetSharedObject : Action
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002B959 File Offset: 0x00029D59
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002B972 File Offset: 0x00029D72
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04000990 RID: 2448
		[Tooltip("The value to set the SharedObject to")]
		public SharedObject targetValue;

		// Token: 0x04000991 RID: 2449
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedObject targetVariable;
	}
}
