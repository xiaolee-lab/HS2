using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B8 RID: 440
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a float value")]
	public class SetFloat : Action
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x000262B0 File Offset: 0x000246B0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.floatValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000262C9 File Offset: 0x000246C9
		public override void OnReset()
		{
			this.floatValue.Value = 0f;
			this.storeResult.Value = 0f;
		}

		// Token: 0x04000755 RID: 1877
		[Tooltip("The float value to set")]
		public SharedFloat floatValue;

		// Token: 0x04000756 RID: 1878
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
