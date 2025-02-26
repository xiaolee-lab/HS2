using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002BB RID: 699
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Lerp the Vector3 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002E63B File Offset: 0x0002CA3B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Lerp(this.fromVector3.Value, this.toVector3.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002E670 File Offset: 0x0002CA70
		public override void OnReset()
		{
			this.fromVector3 = (this.toVector3 = (this.storeResult = Vector3.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04000ABE RID: 2750
		[Tooltip("The from value")]
		public SharedVector3 fromVector3;

		// Token: 0x04000ABF RID: 2751
		[Tooltip("The to value")]
		public SharedVector3 toVector3;

		// Token: 0x04000AC0 RID: 2752
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04000AC1 RID: 2753
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
