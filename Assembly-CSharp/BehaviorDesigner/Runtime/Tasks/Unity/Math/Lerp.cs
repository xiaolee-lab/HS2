using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B2 RID: 434
	[TaskCategory("Unity/Math")]
	[TaskDescription("Lerp the float by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x00026006 File Offset: 0x00024406
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.Lerp(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002603C File Offset: 0x0002443C
		public override void OnReset()
		{
			this.fromValue = (this.toValue = (this.lerpAmount = (this.storeResult = 0f)));
		}

		// Token: 0x04000742 RID: 1858
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x04000743 RID: 1859
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x04000744 RID: 1860
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04000745 RID: 1861
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
