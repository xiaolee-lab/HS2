using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001AE RID: 430
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a math operation on two integers: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class IntOperator : Action
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00025E14 File Offset: 0x00024214
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case IntOperator.Operation.Add:
				this.storeResult.Value = this.integer1.Value + this.integer2.Value;
				break;
			case IntOperator.Operation.Subtract:
				this.storeResult.Value = this.integer1.Value - this.integer2.Value;
				break;
			case IntOperator.Operation.Multiply:
				this.storeResult.Value = this.integer1.Value * this.integer2.Value;
				break;
			case IntOperator.Operation.Divide:
				this.storeResult.Value = this.integer1.Value / this.integer2.Value;
				break;
			case IntOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(this.integer1.Value, this.integer2.Value);
				break;
			case IntOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(this.integer1.Value, this.integer2.Value);
				break;
			case IntOperator.Operation.Modulo:
				this.storeResult.Value = this.integer1.Value % this.integer2.Value;
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00025F69 File Offset: 0x00024369
		public override void OnReset()
		{
			this.operation = IntOperator.Operation.Add;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
			this.storeResult.Value = 0;
		}

		// Token: 0x04000734 RID: 1844
		[Tooltip("The operation to perform")]
		public IntOperator.Operation operation;

		// Token: 0x04000735 RID: 1845
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x04000736 RID: 1846
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x04000737 RID: 1847
		[RequiredField]
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;

		// Token: 0x020001AF RID: 431
		public enum Operation
		{
			// Token: 0x04000739 RID: 1849
			Add,
			// Token: 0x0400073A RID: 1850
			Subtract,
			// Token: 0x0400073B RID: 1851
			Multiply,
			// Token: 0x0400073C RID: 1852
			Divide,
			// Token: 0x0400073D RID: 1853
			Min,
			// Token: 0x0400073E RID: 1854
			Max,
			// Token: 0x0400073F RID: 1855
			Modulo
		}
	}
}
