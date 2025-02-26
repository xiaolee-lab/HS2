using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000254 RID: 596
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedBool variable to the specified object. Returns Success.")]
	public class SetSharedBool : Action
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0002B7B1 File Offset: 0x00029BB1
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002B7CA File Offset: 0x00029BCA
		public override void OnReset()
		{
			this.targetValue = false;
			this.targetVariable = false;
		}

		// Token: 0x04000983 RID: 2435
		[Tooltip("The value to set the SharedBool to")]
		public SharedBool targetValue;

		// Token: 0x04000984 RID: 2436
		[RequiredField]
		[Tooltip("The SharedBool to set")]
		public SharedBool targetVariable;
	}
}
