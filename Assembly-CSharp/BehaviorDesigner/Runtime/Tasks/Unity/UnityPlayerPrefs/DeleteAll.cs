using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F0 RID: 496
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Deletes all entries from the PlayerPrefs.")]
	public class DeleteAll : Action
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x00028547 File Offset: 0x00026947
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteAll();
			return TaskStatus.Success;
		}
	}
}
