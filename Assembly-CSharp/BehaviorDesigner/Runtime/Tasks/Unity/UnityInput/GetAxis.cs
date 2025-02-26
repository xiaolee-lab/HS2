using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000180 RID: 384
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the value of the specified axis and stores it in a float.")]
	public class GetAxis : Action
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00024968 File Offset: 0x00022D68
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

		// Token: 0x060007AC RID: 1964 RVA: 0x000249B1 File Offset: 0x00022DB1
		public override void OnReset()
		{
			this.axisName = string.Empty;
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x040006A1 RID: 1697
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x040006A2 RID: 1698
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x040006A3 RID: 1699
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}
