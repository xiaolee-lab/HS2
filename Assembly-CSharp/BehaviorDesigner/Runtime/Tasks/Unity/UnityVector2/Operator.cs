using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002AC RID: 684
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Performs a math operation on two Vector2s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002E08C File Offset: 0x0002C48C
		public override TaskStatus OnUpdate()
		{
			Operator.Operation operation = this.operation;
			if (operation != Operator.Operation.Add)
			{
				if (operation != Operator.Operation.Subtract)
				{
					if (operation == Operator.Operation.Scale)
					{
						this.storeResult.Value = Vector2.Scale(this.firstVector2.Value, this.secondVector2.Value);
					}
				}
				else
				{
					this.storeResult.Value = this.firstVector2.Value - this.secondVector2.Value;
				}
			}
			else
			{
				this.storeResult.Value = this.firstVector2.Value + this.secondVector2.Value;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002E13C File Offset: 0x0002C53C
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector2 = (this.secondVector2 = (this.storeResult = Vector2.zero));
		}

		// Token: 0x04000A98 RID: 2712
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04000A99 RID: 2713
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x04000A9A RID: 2714
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x04000A9B RID: 2715
		[Tooltip("The variable to store the result")]
		public SharedVector2 storeResult;

		// Token: 0x020002AD RID: 685
		public enum Operation
		{
			// Token: 0x04000A9D RID: 2717
			Add,
			// Token: 0x04000A9E RID: 2718
			Subtract,
			// Token: 0x04000A9F RID: 2719
			Scale
		}
	}
}
