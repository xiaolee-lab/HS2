using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02001163 RID: 4451
public class UI_FixInputFieldCaret : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06009308 RID: 37640 RVA: 0x003CF810 File Offset: 0x003CDC10
	public void OnSelect(BaseEventData eventData)
	{
		InputField component = base.gameObject.GetComponent<InputField>();
		if (null != component)
		{
			RectTransform rectTransform = null;
			if (component.textComponent)
			{
				rectTransform = (component.textComponent.transform as RectTransform);
			}
			RectTransform rectTransform2 = (RectTransform)base.transform.Find(base.gameObject.name + " Input Caret");
			if (rectTransform != null && rectTransform2 != null)
			{
				Vector2 anchoredPosition = rectTransform.anchoredPosition;
				anchoredPosition.y += this.correctY;
				rectTransform2.anchoredPosition = anchoredPosition;
			}
		}
	}

	// Token: 0x040076F2 RID: 30450
	public float correctY;
}
