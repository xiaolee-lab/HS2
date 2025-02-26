using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000869 RID: 2153
	public class SliderUI : Slider
	{
		// Token: 0x060036EA RID: 14058 RVA: 0x00145B80 File Offset: 0x00143F80
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (base.onValueChanged != null)
			{
				base.onValueChanged.Invoke(this.value);
			}
			base.OnPointerDown(eventData);
		}
	}
}
