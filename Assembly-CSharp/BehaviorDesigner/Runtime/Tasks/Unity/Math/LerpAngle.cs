using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B3 RID: 435
	[TaskCategory("Unity/Math")]
	[TaskDescription("Lerp the angle by an amount.")]
	public class LerpAngle : Action
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x0002607C File Offset: 0x0002447C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.LerpAngle(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000260B0 File Offset: 0x000244B0
		public override void OnReset()
		{
			this.fromValue = (this.toValue = (this.lerpAmount = (this.storeResult = 0f)));
		}

		// Token: 0x04000746 RID: 1862
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x04000747 RID: 1863
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x04000748 RID: 1864
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04000749 RID: 1865
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
