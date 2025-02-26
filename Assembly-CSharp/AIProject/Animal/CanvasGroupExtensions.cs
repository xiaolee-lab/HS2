using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B79 RID: 2937
	public static class CanvasGroupExtensions
	{
		// Token: 0x06005721 RID: 22305 RVA: 0x0025AF5A File Offset: 0x0025935A
		public static bool SetBlocksRaycasts(this CanvasGroup canvas, bool active)
		{
			if (canvas == null)
			{
				return false;
			}
			if (canvas.blocksRaycasts == active)
			{
				return false;
			}
			canvas.blocksRaycasts = active;
			return true;
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x0025AF80 File Offset: 0x00259380
		public static bool SetInteractable(this CanvasGroup canvas, bool active)
		{
			if (canvas == null)
			{
				return false;
			}
			if (canvas.interactable == active)
			{
				return false;
			}
			canvas.interactable = active;
			return true;
		}
	}
}
