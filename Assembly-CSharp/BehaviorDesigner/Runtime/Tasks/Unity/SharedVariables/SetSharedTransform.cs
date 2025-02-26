using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200025F RID: 607
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedTransform variable to the specified object. Returns Success.")]
	public class SetSharedTransform : Action
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002BA98 File Offset: 0x00029E98
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((!(this.targetValue.Value != null)) ? this.transform : this.targetValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002BAD2 File Offset: 0x00029ED2
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x0400099A RID: 2458
		[Tooltip("The value to set the SharedTransform to. If null the variable will be set to the current Transform")]
		public SharedTransform targetValue;

		// Token: 0x0400099B RID: 2459
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedTransform targetVariable;
	}
}
