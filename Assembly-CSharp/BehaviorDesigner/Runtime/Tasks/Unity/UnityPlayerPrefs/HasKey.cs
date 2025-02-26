using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001F5 RID: 501
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Retruns success if the specified key exists.")]
	public class HasKey : Conditional
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x000286A5 File Offset: 0x00026AA5
		public override TaskStatus OnUpdate()
		{
			return (!PlayerPrefs.HasKey(this.key.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000286C3 File Offset: 0x00026AC3
		public override void OnReset()
		{
			this.key = string.Empty;
		}

		// Token: 0x0400083F RID: 2111
		[Tooltip("The key to check")]
		public SharedString key;
	}
}
