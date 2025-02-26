using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001AC RID: 428
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs comparison between two integers: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class IntComparison : Conditional
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x00025CE0 File Offset: 0x000240E0
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case IntComparison.Operation.LessThan:
				return (this.integer1.Value >= this.integer2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case IntComparison.Operation.LessThanOrEqualTo:
				return (this.integer1.Value > this.integer2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case IntComparison.Operation.EqualTo:
				return (this.integer1.Value != this.integer2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case IntComparison.Operation.NotEqualTo:
				return (this.integer1.Value == this.integer2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case IntComparison.Operation.GreaterThanOrEqualTo:
				return (this.integer1.Value < this.integer2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case IntComparison.Operation.GreaterThan:
				return (this.integer1.Value <= this.integer2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			default:
				return TaskStatus.Failure;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00025DEA File Offset: 0x000241EA
		public override void OnReset()
		{
			this.operation = IntComparison.Operation.LessThan;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
		}

		// Token: 0x0400072A RID: 1834
		[Tooltip("The operation to perform")]
		public IntComparison.Operation operation;

		// Token: 0x0400072B RID: 1835
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x0400072C RID: 1836
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x020001AD RID: 429
		public enum Operation
		{
			// Token: 0x0400072E RID: 1838
			LessThan,
			// Token: 0x0400072F RID: 1839
			LessThanOrEqualTo,
			// Token: 0x04000730 RID: 1840
			EqualTo,
			// Token: 0x04000731 RID: 1841
			NotEqualTo,
			// Token: 0x04000732 RID: 1842
			GreaterThanOrEqualTo,
			// Token: 0x04000733 RID: 1843
			GreaterThan
		}
	}
}
