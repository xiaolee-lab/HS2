using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A2 RID: 418
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a math operation on two bools: AND, OR, NAND, or XOR.")]
	public class BoolOperator : Action
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x000257A4 File Offset: 0x00023BA4
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case BoolOperator.Operation.AND:
				this.storeResult.Value = (this.bool1.Value && this.bool2.Value);
				break;
			case BoolOperator.Operation.OR:
				this.storeResult.Value = (this.bool1.Value || this.bool2.Value);
				break;
			case BoolOperator.Operation.NAND:
				this.storeResult.Value = (!this.bool1.Value || !this.bool2.Value);
				break;
			case BoolOperator.Operation.XOR:
				this.storeResult.Value = (this.bool1.Value ^ this.bool2.Value);
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00025888 File Offset: 0x00023C88
		public override void OnReset()
		{
			this.operation = BoolOperator.Operation.AND;
			this.bool1.Value = false;
			this.bool2.Value = false;
			this.storeResult.Value = false;
		}

		// Token: 0x04000703 RID: 1795
		[Tooltip("The operation to perform")]
		public BoolOperator.Operation operation;

		// Token: 0x04000704 RID: 1796
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x04000705 RID: 1797
		[Tooltip("The second bool")]
		public SharedBool bool2;

		// Token: 0x04000706 RID: 1798
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;

		// Token: 0x020001A3 RID: 419
		public enum Operation
		{
			// Token: 0x04000708 RID: 1800
			AND,
			// Token: 0x04000709 RID: 1801
			OR,
			// Token: 0x0400070A RID: 1802
			NAND,
			// Token: 0x0400070B RID: 1803
			XOR
		}
	}
}
