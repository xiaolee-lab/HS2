using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B7 RID: 439
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a bool value")]
	public class SetBool : Action
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x00026275 File Offset: 0x00024675
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.boolValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002628E File Offset: 0x0002468E
		public override void OnReset()
		{
			this.boolValue.Value = false;
			this.storeResult.Value = false;
		}

		// Token: 0x04000753 RID: 1875
		[Tooltip("The bool value to set")]
		public SharedBool boolValue;

		// Token: 0x04000754 RID: 1876
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}
