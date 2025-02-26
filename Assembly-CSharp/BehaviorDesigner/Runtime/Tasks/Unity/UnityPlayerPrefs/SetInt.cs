using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F8 RID: 504
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetInt : Action
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x00028735 File Offset: 0x00026B35
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetInt(this.key.Value, this.value.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00028753 File Offset: 0x00026B53
		public override void OnReset()
		{
			this.key = string.Empty;
			this.value = 0;
		}

		// Token: 0x04000842 RID: 2114
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000843 RID: 2115
		[Tooltip("The value to set")]
		public SharedInt value;
	}
}
