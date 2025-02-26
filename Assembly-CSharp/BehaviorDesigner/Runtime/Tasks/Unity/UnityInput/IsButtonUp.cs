using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000187 RID: 391
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified button is released.")]
	public class IsButtonUp : Conditional
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x00024B8E File Offset: 0x00022F8E
		public override TaskStatus OnUpdate()
		{
			return (!Input.GetButtonUp(this.buttonName.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00024BAC File Offset: 0x00022FAC
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x040006AF RID: 1711
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}
