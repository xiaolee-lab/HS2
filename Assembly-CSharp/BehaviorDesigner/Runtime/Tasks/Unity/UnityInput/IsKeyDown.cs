using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000188 RID: 392
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified key is pressed.")]
	public class IsKeyDown : Conditional
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x00024BC6 File Offset: 0x00022FC6
		public override TaskStatus OnUpdate()
		{
			return (!Input.GetKeyDown(this.key)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00024BDF File Offset: 0x00022FDF
		public override void OnReset()
		{
			this.key = KeyCode.None;
		}

		// Token: 0x040006B0 RID: 1712
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}
