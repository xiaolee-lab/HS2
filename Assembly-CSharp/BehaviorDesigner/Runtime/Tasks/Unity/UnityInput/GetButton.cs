using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000182 RID: 386
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the state of the specified button.")]
	public class GetButton : Action
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00024A6F File Offset: 0x00022E6F
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetButton(this.buttonName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00024A8D File Offset: 0x00022E8D
		public override void OnReset()
		{
			this.buttonName = "Fire1";
			this.storeResult = false;
		}

		// Token: 0x040006A7 RID: 1703
		[Tooltip("The name of the button")]
		public SharedString buttonName;

		// Token: 0x040006A8 RID: 1704
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}
