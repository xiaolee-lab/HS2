using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B6F RID: 2927
	public static class GameObjectExtensions
	{
		// Token: 0x06005707 RID: 22279 RVA: 0x0025A9CF File Offset: 0x00258DCF
		public static void SetActiveSelf(this GameObject obj, bool active)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.activeSelf != active)
			{
				obj.SetActive(active);
			}
		}
	}
}
