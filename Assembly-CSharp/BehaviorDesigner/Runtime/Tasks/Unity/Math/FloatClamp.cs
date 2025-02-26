using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A5 RID: 421
	[TaskCategory("Unity/Math")]
	[TaskDescription("Clamps the float between two values.")]
	public class FloatClamp : Action
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x000258F5 File Offset: 0x00023CF5
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Clamp(this.floatVariable.Value, this.minValue.Value, this.maxValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002592C File Offset: 0x00023D2C
		public override void OnReset()
		{
			this.floatVariable = (this.minValue = (this.maxValue = 0f));
		}

		// Token: 0x0400070D RID: 1805
		[Tooltip("The float to clamp")]
		public SharedFloat floatVariable;

		// Token: 0x0400070E RID: 1806
		[Tooltip("The maximum value of the float")]
		public SharedFloat minValue;

		// Token: 0x0400070F RID: 1807
		[Tooltip("The maximum value of the float")]
		public SharedFloat maxValue;
	}
}
