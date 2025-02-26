using System;
using UnityEngine;

namespace Illusion.Extensions
{
	// Token: 0x02001076 RID: 4214
	public static class CanvasGroupExtensions
	{
		// Token: 0x06008D40 RID: 36160 RVA: 0x003B0BCA File Offset: 0x003AEFCA
		public static void Enable(this CanvasGroup canvasGroup, bool enable, bool ignoreParentGroups = false)
		{
			canvasGroup.alpha = ((!enable) ? 0f : 1f);
			canvasGroup.interactable = enable;
			canvasGroup.blocksRaycasts = enable;
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x003B0BF5 File Offset: 0x003AEFF5
		public static void Set(this CanvasGroup canvasGroup, float alpha, bool interactable, bool blocksRaycasts, bool ignoreParentGroups = false)
		{
			canvasGroup.alpha = alpha;
			canvasGroup.interactable = interactable;
			canvasGroup.blocksRaycasts = blocksRaycasts;
			canvasGroup.ignoreParentGroups = ignoreParentGroups;
		}
	}
}
