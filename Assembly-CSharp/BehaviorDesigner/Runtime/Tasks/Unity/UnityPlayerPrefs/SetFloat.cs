using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F7 RID: 503
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetFloat : Action
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x000286ED File Offset: 0x00026AED
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetFloat(this.key.Value, this.value.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0002870B File Offset: 0x00026B0B
		public override void OnReset()
		{
			this.key = string.Empty;
			this.value = 0f;
		}

		// Token: 0x04000840 RID: 2112
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000841 RID: 2113
		[Tooltip("The value to set")]
		public SharedFloat value;
	}
}
