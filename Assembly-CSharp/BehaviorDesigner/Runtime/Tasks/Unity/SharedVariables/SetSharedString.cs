using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200025E RID: 606
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedString variable to the specified object. Returns Success.")]
	public class SetSharedString : Action
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002BA55 File Offset: 0x00029E55
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002BA6E File Offset: 0x00029E6E
		public override void OnReset()
		{
			this.targetValue = string.Empty;
			this.targetVariable = string.Empty;
		}

		// Token: 0x04000998 RID: 2456
		[Tooltip("The value to set the SharedString to")]
		public SharedString targetValue;

		// Token: 0x04000999 RID: 2457
		[RequiredField]
		[Tooltip("The SharedString to set")]
		public SharedString targetVariable;
	}
}
