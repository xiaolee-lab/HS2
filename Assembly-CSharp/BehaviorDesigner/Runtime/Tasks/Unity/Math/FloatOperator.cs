using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A8 RID: 424
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a math operation on two floats: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class FloatOperator : Action
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00025AAC File Offset: 0x00023EAC
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case FloatOperator.Operation.Add:
				this.storeResult.Value = this.float1.Value + this.float2.Value;
				break;
			case FloatOperator.Operation.Subtract:
				this.storeResult.Value = this.float1.Value - this.float2.Value;
				break;
			case FloatOperator.Operation.Multiply:
				this.storeResult.Value = this.float1.Value * this.float2.Value;
				break;
			case FloatOperator.Operation.Divide:
				this.storeResult.Value = this.float1.Value / this.float2.Value;
				break;
			case FloatOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(this.float1.Value, this.float2.Value);
				break;
			case FloatOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(this.float1.Value, this.float2.Value);
				break;
			case FloatOperator.Operation.Modulo:
				this.storeResult.Value = this.float1.Value % this.float2.Value;
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00025C01 File Offset: 0x00024001
		public override void OnReset()
		{
			this.operation = FloatOperator.Operation.Add;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
			this.storeResult.Value = 0f;
		}

		// Token: 0x0400071A RID: 1818
		[Tooltip("The operation to perform")]
		public FloatOperator.Operation operation;

		// Token: 0x0400071B RID: 1819
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x0400071C RID: 1820
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x0400071D RID: 1821
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;

		// Token: 0x020001A9 RID: 425
		public enum Operation
		{
			// Token: 0x0400071F RID: 1823
			Add,
			// Token: 0x04000720 RID: 1824
			Subtract,
			// Token: 0x04000721 RID: 1825
			Multiply,
			// Token: 0x04000722 RID: 1826
			Divide,
			// Token: 0x04000723 RID: 1827
			Min,
			// Token: 0x04000724 RID: 1828
			Max,
			// Token: 0x04000725 RID: 1829
			Modulo
		}
	}
}
