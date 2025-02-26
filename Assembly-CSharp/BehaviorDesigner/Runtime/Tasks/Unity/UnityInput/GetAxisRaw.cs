using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000181 RID: 385
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the raw value of the specified axis and stores it in a float.")]
	public class GetAxisRaw : Action
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x000249EC File Offset: 0x00022DEC
		public override TaskStatus OnUpdate()
		{
			float num = Input.GetAxis(this.axisName.Value);
			if (!this.multiplier.IsNone)
			{
				num *= this.multiplier.Value;
			}
			this.storeResult.Value = num;
			return TaskStatus.Success;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00024A35 File Offset: 0x00022E35
		public override void OnReset()
		{
			this.axisName = string.Empty;
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x040006A4 RID: 1700
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x040006A5 RID: 1701
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x040006A6 RID: 1702
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}
