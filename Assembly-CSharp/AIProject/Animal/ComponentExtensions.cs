using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B71 RID: 2929
	public static class ComponentExtensions
	{
		// Token: 0x06005709 RID: 22281 RVA: 0x0025AA57 File Offset: 0x00258E57
		public static void SetActiveSelf(this Component com, bool active)
		{
			if (com == null || com.gameObject == null)
			{
				return;
			}
			if (com.gameObject.activeSelf != active)
			{
				com.gameObject.SetActive(active);
			}
		}
	}
}
