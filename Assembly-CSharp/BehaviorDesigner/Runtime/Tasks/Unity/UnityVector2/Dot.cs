using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A1 RID: 673
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the dot product of two Vector2 values.")]
	public class Dot : Action
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002DC82 File Offset: 0x0002C082
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002DCAC File Offset: 0x0002C0AC
		public override void OnReset()
		{
			this.leftHandSide = (this.rightHandSide = Vector2.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04000A7D RID: 2685
		[Tooltip("The left hand side of the dot product")]
		public SharedVector2 leftHandSide;

		// Token: 0x04000A7E RID: 2686
		[Tooltip("The right hand side of the dot product")]
		public SharedVector2 rightHandSide;

		// Token: 0x04000A7F RID: 2687
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
