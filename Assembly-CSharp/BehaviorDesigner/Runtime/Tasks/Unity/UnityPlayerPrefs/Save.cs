using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F6 RID: 502
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Saves the PlayerPrefs.")]
	public class Save : Action
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x000286DD File Offset: 0x00026ADD
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.Save();
			return TaskStatus.Success;
		}
	}
}
