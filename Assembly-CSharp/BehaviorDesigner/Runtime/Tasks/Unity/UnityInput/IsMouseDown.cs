using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200018A RID: 394
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseDown : Conditional
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00024C1A File Offset: 0x0002301A
		public override TaskStatus OnUpdate()
		{
			return (!Input.GetMouseButtonDown(this.buttonIndex.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00024C38 File Offset: 0x00023038
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x040006B2 RID: 1714
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}
