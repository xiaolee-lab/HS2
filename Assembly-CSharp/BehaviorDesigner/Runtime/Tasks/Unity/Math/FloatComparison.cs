using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A6 RID: 422
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs comparison between two floats: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class FloatComparison : Conditional
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x00025964 File Offset: 0x00023D64
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case FloatComparison.Operation.LessThan:
				return (this.float1.Value >= this.float2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case FloatComparison.Operation.LessThanOrEqualTo:
				return (this.float1.Value > this.float2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case FloatComparison.Operation.EqualTo:
				return (!Mathf.Approximately(this.float1.Value, this.float2.Value)) ? TaskStatus.Failure : TaskStatus.Success;
			case FloatComparison.Operation.NotEqualTo:
				return Mathf.Approximately(this.float1.Value, this.float2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case FloatComparison.Operation.GreaterThanOrEqualTo:
				return (this.float1.Value < this.float2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			case FloatComparison.Operation.GreaterThan:
				return (this.float1.Value <= this.float2.Value) ? TaskStatus.Failure : TaskStatus.Success;
			default:
				return TaskStatus.Failure;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00025A78 File Offset: 0x00023E78
		public override void OnReset()
		{
			this.operation = FloatComparison.Operation.LessThan;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
		}

		// Token: 0x04000710 RID: 1808
		[Tooltip("The operation to perform")]
		public FloatComparison.Operation operation;

		// Token: 0x04000711 RID: 1809
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x04000712 RID: 1810
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x020001A7 RID: 423
		public enum Operation
		{
			// Token: 0x04000714 RID: 1812
			LessThan,
			// Token: 0x04000715 RID: 1813
			LessThanOrEqualTo,
			// Token: 0x04000716 RID: 1814
			EqualTo,
			// Token: 0x04000717 RID: 1815
			NotEqualTo,
			// Token: 0x04000718 RID: 1816
			GreaterThanOrEqualTo,
			// Token: 0x04000719 RID: 1817
			GreaterThan
		}
	}
}
