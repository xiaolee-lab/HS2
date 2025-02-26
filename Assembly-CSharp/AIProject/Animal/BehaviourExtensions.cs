using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B72 RID: 2930
	public static class BehaviourExtensions
	{
		// Token: 0x0600570A RID: 22282 RVA: 0x0025AA94 File Offset: 0x00258E94
		public static void SetEnableSelf(this Behaviour beh, bool enabled)
		{
			if (beh != null && beh.enabled != enabled)
			{
				beh.enabled = enabled;
			}
		}
	}
}
