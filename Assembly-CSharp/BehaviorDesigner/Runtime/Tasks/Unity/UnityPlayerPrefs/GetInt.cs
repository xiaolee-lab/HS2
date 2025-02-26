using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F3 RID: 499
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetInt : Action
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x000285E7 File Offset: 0x000269E7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetInt(this.key.Value, this.defaultValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00028610 File Offset: 0x00026A10
		public override void OnReset()
		{
			this.key = string.Empty;
			this.defaultValue = 0;
			this.storeResult = 0;
		}

		// Token: 0x04000839 RID: 2105
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x0400083A RID: 2106
		[Tooltip("The default value")]
		public SharedInt defaultValue;

		// Token: 0x0400083B RID: 2107
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
