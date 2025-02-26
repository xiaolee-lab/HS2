using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000189 RID: 393
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified key is released.")]
	public class IsKeyUp : Conditional
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x00024BF0 File Offset: 0x00022FF0
		public override TaskStatus OnUpdate()
		{
			return (!Input.GetKeyUp(this.key)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00024C09 File Offset: 0x00023009
		public override void OnReset()
		{
			this.key = KeyCode.None;
		}

		// Token: 0x040006B1 RID: 1713
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}
