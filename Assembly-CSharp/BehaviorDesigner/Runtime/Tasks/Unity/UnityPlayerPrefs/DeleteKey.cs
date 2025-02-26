using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F1 RID: 497
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Deletes the specified key from the PlayerPrefs.")]
	public class DeleteKey : Action
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00028557 File Offset: 0x00026957
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteKey(this.key.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002856A File Offset: 0x0002696A
		public override void OnReset()
		{
			this.key = string.Empty;
		}

		// Token: 0x04000835 RID: 2101
		[Tooltip("The key to delete")]
		public SharedString key;
	}
}
