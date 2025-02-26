using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005A8 RID: 1448
public class AutoSetAnchorPosForIphonex : MonoBehaviour
{
	// Token: 0x0600218E RID: 8590 RVA: 0x000B96B0 File Offset: 0x000B7AB0
	private void Awake()
	{
		if (Screen.width == 1125 && Screen.height == 2436)
		{
			CanvasScaler component = this.mCanvas.GetComponent<CanvasScaler>();
			float y = component.referenceResolution.y;
			float num = 0.054187194f * y;
			RectTransform component2 = base.GetComponent<RectTransform>();
			component2.offsetMin = new Vector2(0f, num);
			component2.offsetMax = new Vector2(0f, -num);
		}
		else if (Screen.height == 1125 && Screen.width == 2436)
		{
			CanvasScaler component3 = this.mCanvas.GetComponent<CanvasScaler>();
			float y2 = component3.referenceResolution.y;
			float y3 = 0.056f * y2;
			float num2 = y2 / 1125f * 132f;
			RectTransform component4 = base.GetComponent<RectTransform>();
			component4.offsetMin = new Vector2(num2, y3);
			component4.offsetMax = new Vector2(-num2, 0f);
		}
	}

	// Token: 0x04002123 RID: 8483
	public Canvas mCanvas;
}
