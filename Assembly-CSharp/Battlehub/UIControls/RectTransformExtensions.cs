using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000078 RID: 120
	public static class RectTransformExtensions
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00009A70 File Offset: 0x00007E70
		public static void Stretch(this RectTransform rt)
		{
			rt.anchorMin = new Vector2(0f, 0f);
			rt.anchorMax = new Vector2(1f, 1f);
			rt.offsetMin = Vector2.zero;
			rt.offsetMax = Vector2.zero;
		}
	}
}
