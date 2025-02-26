using System;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200133F RID: 4927
	public class ScrollSizeCorrect : MonoBehaviour
	{
		// Token: 0x0600A4ED RID: 42221 RVA: 0x00434058 File Offset: 0x00432458
		private void OnRectTransformDimensionsChange()
		{
			float size = Mathf.Min(1f, this.transBase.rect.height / this.transTarget.rect.height);
			this.scrollbar.size = size;
			if (this.scrollbar.value == 0f)
			{
				this.scrollbar.value = 0.1f;
				this.scrollbar.value = 0f;
			}
		}

		// Token: 0x040081DB RID: 33243
		[SerializeField]
		private RectTransform transBase;

		// Token: 0x040081DC RID: 33244
		[SerializeField]
		private RectTransform transTarget;

		// Token: 0x040081DD RID: 33245
		[SerializeField]
		private Scrollbar scrollbar;
	}
}
