using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B0 RID: 432
	[TaskCategory("Unity/Math")]
	[TaskDescription("Is the float a positive value?")]
	public class IsFloatPositive : Conditional
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00025F9E File Offset: 0x0002439E
		public override TaskStatus OnUpdate()
		{
			return (this.floatVariable.Value <= 0f) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00025FBC File Offset: 0x000243BC
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x04000740 RID: 1856
		[Tooltip("The float to check if positive")]
		public SharedFloat floatVariable;
	}
}
