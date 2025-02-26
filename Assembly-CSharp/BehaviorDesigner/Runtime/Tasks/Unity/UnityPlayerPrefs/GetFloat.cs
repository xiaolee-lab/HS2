using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F2 RID: 498
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetFloat : Action
	{
		// Token: 0x0600092F RID: 2351 RVA: 0x00028584 File Offset: 0x00026984
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetFloat(this.key.Value, this.defaultValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000285AD File Offset: 0x000269AD
		public override void OnReset()
		{
			this.key = string.Empty;
			this.defaultValue = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x04000836 RID: 2102
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000837 RID: 2103
		[Tooltip("The default value")]
		public SharedFloat defaultValue;

		// Token: 0x04000838 RID: 2104
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
