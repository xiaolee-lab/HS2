using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B9 RID: 441
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets an int value")]
	public class SetInt : Action
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x000262F3 File Offset: 0x000246F3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.intValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002630C File Offset: 0x0002470C
		public override void OnReset()
		{
			this.intValue.Value = 0;
			this.storeResult.Value = 0;
		}

		// Token: 0x04000757 RID: 1879
		[Tooltip("The int value to set")]
		public SharedInt intValue;

		// Token: 0x04000758 RID: 1880
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}
