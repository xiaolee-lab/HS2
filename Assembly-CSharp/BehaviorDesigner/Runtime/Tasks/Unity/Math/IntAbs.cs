using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001AA RID: 426
	[TaskCategory("Unity/Math")]
	[TaskDescription("Stores the absolute value of the int.")]
	public class IntAbs : Action
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00025C42 File Offset: 0x00024042
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Abs(this.intVariable.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025C60 File Offset: 0x00024060
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x04000726 RID: 1830
		[Tooltip("The int to return the absolute value of")]
		public SharedInt intVariable;
	}
}
