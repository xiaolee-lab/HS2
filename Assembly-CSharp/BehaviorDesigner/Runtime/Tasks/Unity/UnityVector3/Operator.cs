using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002BF RID: 703
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Performs a math operation on two Vector3s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x0002E7F0 File Offset: 0x0002CBF0
		public override TaskStatus OnUpdate()
		{
			Operator.Operation operation = this.operation;
			if (operation != Operator.Operation.Add)
			{
				if (operation != Operator.Operation.Subtract)
				{
					if (operation == Operator.Operation.Scale)
					{
						this.storeResult.Value = Vector3.Scale(this.firstVector3.Value, this.secondVector3.Value);
					}
				}
				else
				{
					this.storeResult.Value = this.firstVector3.Value - this.secondVector3.Value;
				}
			}
			else
			{
				this.storeResult.Value = this.firstVector3.Value + this.secondVector3.Value;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002E8A0 File Offset: 0x0002CCA0
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector3 = (this.secondVector3 = (this.storeResult = Vector3.zero));
		}

		// Token: 0x04000ACB RID: 2763
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04000ACC RID: 2764
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04000ACD RID: 2765
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04000ACE RID: 2766
		[Tooltip("The variable to store the result")]
		public SharedVector3 storeResult;

		// Token: 0x020002C0 RID: 704
		public enum Operation
		{
			// Token: 0x04000AD0 RID: 2768
			Add,
			// Token: 0x04000AD1 RID: 2769
			Subtract,
			// Token: 0x04000AD2 RID: 2770
			Scale
		}
	}
}
