using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001A4 RID: 420
	[TaskCategory("Unity/Math")]
	[TaskDescription("Stores the absolute value of the float.")]
	public class FloatAbs : Action
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x000258BD File Offset: 0x00023CBD
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Abs(this.floatVariable.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000258DB File Offset: 0x00023CDB
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x0400070C RID: 1804
		[Tooltip("The float to return the absolute value of")]
		public SharedFloat floatVariable;
	}
}
