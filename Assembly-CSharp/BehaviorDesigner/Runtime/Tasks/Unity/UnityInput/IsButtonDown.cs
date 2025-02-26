using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000186 RID: 390
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified button is pressed.")]
	public class IsButtonDown : Conditional
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x00024B56 File Offset: 0x00022F56
		public override TaskStatus OnUpdate()
		{
			return (!Input.GetButtonDown(this.buttonName.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00024B74 File Offset: 0x00022F74
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x040006AE RID: 1710
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}
