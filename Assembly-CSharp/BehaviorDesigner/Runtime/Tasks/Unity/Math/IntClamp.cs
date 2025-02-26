using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001AB RID: 427
	[TaskCategory("Unity/Math")]
	[TaskDescription("Clamps the int between two values.")]
	public class IntClamp : Action
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00025C76 File Offset: 0x00024076
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Clamp(this.intVariable.Value, this.minValue.Value, this.maxValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00025CAC File Offset: 0x000240AC
		public override void OnReset()
		{
			this.intVariable = (this.minValue = (this.maxValue = 0));
		}

		// Token: 0x04000727 RID: 1831
		[Tooltip("The int to clamp")]
		public SharedInt intVariable;

		// Token: 0x04000728 RID: 1832
		[Tooltip("The maximum value of the int")]
		public SharedInt minValue;

		// Token: 0x04000729 RID: 1833
		[Tooltip("The maximum value of the int")]
		public SharedInt maxValue;
	}
}
