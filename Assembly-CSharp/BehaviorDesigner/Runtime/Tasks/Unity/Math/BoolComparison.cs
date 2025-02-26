using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A0 RID: 416
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a comparison between two bools.")]
	public class BoolComparison : Conditional
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0002572B File Offset: 0x00023B2B
		public override TaskStatus OnUpdate()
		{
			return (this.bool1.Value != this.bool2.Value) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002574F File Offset: 0x00023B4F
		public override void OnReset()
		{
			this.bool1.Value = false;
			this.bool2.Value = false;
		}

		// Token: 0x04000700 RID: 1792
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x04000701 RID: 1793
		[Tooltip("The second bool")]
		public SharedBool bool2;
	}
}
