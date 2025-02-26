using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000257 RID: 599
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedGameObject variable to the specified object. Returns Success.")]
	public class SetSharedGameObject : Action
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0002B874 File Offset: 0x00029C74
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((!(this.targetValue.Value != null) && !this.valueCanBeNull.Value) ? this.gameObject : this.targetValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002B8C9 File Offset: 0x00029CC9
		public override void OnReset()
		{
			this.valueCanBeNull = false;
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x04000989 RID: 2441
		[Tooltip("The value to set the SharedGameObject to. If null the variable will be set to the current GameObject")]
		public SharedGameObject targetValue;

		// Token: 0x0400098A RID: 2442
		[RequiredField]
		[Tooltip("The SharedGameObject to set")]
		public SharedGameObject targetVariable;

		// Token: 0x0400098B RID: 2443
		[Tooltip("Can the target value be null?")]
		public SharedBool valueCanBeNull;
	}
}
