using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F9 RID: 505
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetString : Action
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00028779 File Offset: 0x00026B79
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetString(this.key.Value, this.value.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00028797 File Offset: 0x00026B97
		public override void OnReset()
		{
			this.key = string.Empty;
			this.value = string.Empty;
		}

		// Token: 0x04000844 RID: 2116
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000845 RID: 2117
		[Tooltip("The value to set")]
		public SharedString value;
	}
}
