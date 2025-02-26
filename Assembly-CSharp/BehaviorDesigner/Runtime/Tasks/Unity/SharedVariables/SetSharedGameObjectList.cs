using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000258 RID: 600
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedGameObjectList variable to the specified object. Returns Success.")]
	public class SetSharedGameObjectList : Action
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x0002B8ED File Offset: 0x00029CED
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002B906 File Offset: 0x00029D06
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x0400098C RID: 2444
		[Tooltip("The value to set the SharedGameObjectList to.")]
		public SharedGameObjectList targetValue;

		// Token: 0x0400098D RID: 2445
		[RequiredField]
		[Tooltip("The SharedGameObjectList to set")]
		public SharedGameObjectList targetVariable;
	}
}
