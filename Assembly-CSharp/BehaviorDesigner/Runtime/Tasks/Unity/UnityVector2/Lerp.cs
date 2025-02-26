using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A8 RID: 680
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Lerp the Vector2 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x0002DEC6 File Offset: 0x0002C2C6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Lerp(this.fromVector2.Value, this.toVector2.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002DEFC File Offset: 0x0002C2FC
		public override void OnReset()
		{
			this.fromVector2 = (this.toVector2 = (this.storeResult = Vector2.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04000A8B RID: 2699
		[Tooltip("The from value")]
		public SharedVector2 fromVector2;

		// Token: 0x04000A8C RID: 2700
		[Tooltip("The to value")]
		public SharedVector2 toVector2;

		// Token: 0x04000A8D RID: 2701
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04000A8E RID: 2702
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
