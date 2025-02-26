using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F4 RID: 500
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetString : Action
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x00028642 File Offset: 0x00026A42
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetString(this.key.Value, this.defaultValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002866B File Offset: 0x00026A6B
		public override void OnReset()
		{
			this.key = string.Empty;
			this.defaultValue = string.Empty;
			this.storeResult = string.Empty;
		}

		// Token: 0x0400083C RID: 2108
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x0400083D RID: 2109
		[Tooltip("The default value")]
		public SharedString defaultValue;

		// Token: 0x0400083E RID: 2110
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedString storeResult;
	}
}
