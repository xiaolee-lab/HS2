using System;
using UnityEngine.UI;

namespace AIProject.Animal
{
	// Token: 0x02000B73 RID: 2931
	public static class SelectableExtension
	{
		// Token: 0x0600570B RID: 22283 RVA: 0x0025AAB5 File Offset: 0x00258EB5
		public static void SetInteractable(this Selectable sel, bool active)
		{
			if (sel != null || sel.interactable != active)
			{
				sel.interactable = active;
			}
		}
	}
}
