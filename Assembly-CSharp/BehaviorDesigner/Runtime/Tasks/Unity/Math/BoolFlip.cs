using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A1 RID: 417
	[TaskCategory("Unity/Math")]
	[TaskDescription("Flips the value of the bool.")]
	public class BoolFlip : Action
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x00025771 File Offset: 0x00023B71
		public override TaskStatus OnUpdate()
		{
			this.boolVariable.Value = !this.boolVariable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002578D File Offset: 0x00023B8D
		public override void OnReset()
		{
			this.boolVariable.Value = false;
		}

		// Token: 0x04000702 RID: 1794
		[Tooltip("The bool to flip the value of")]
		public SharedBool boolVariable;
	}
}
