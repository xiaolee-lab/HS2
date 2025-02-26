using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000260 RID: 608
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList variable to the specified object. Returns Success.")]
	public class SetSharedTransformList : Action
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002BAEA File Offset: 0x00029EEA
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002BB03 File Offset: 0x00029F03
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x0400099C RID: 2460
		[Tooltip("The value to set the SharedTransformList to.")]
		public SharedTransformList targetValue;

		// Token: 0x0400099D RID: 2461
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList targetVariable;
	}
}
