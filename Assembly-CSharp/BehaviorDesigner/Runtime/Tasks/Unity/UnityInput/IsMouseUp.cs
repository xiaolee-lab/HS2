using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200018B RID: 395
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseUp : Conditional
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x00024C4E File Offset: 0x0002304E
		public override TaskStatus OnUpdate()
		{
			return (!Input.GetMouseButtonUp(this.buttonIndex.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00024C6C File Offset: 0x0002306C
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x040006B3 RID: 1715
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}
