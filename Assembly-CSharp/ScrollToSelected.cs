using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000FE5 RID: 4069
[RequireComponent(typeof(ScrollRect))]
public class ScrollToSelected : MonoBehaviour
{
	// Token: 0x0600885C RID: 34908 RVA: 0x0038D480 File Offset: 0x0038B880
	private void Awake()
	{
		this.m_ScrollRect = base.GetComponent<ScrollRect>();
		this.m_RectTransform = base.GetComponent<RectTransform>();
		this.m_ContentRectTransform = this.m_ScrollRect.content;
	}

	// Token: 0x0600885D RID: 34909 RVA: 0x0038D4AB File Offset: 0x0038B8AB
	private void Update()
	{
		this.UpdateScrollToSelected();
	}

	// Token: 0x0600885E RID: 34910 RVA: 0x0038D4B4 File Offset: 0x0038B8B4
	private void UpdateScrollToSelected()
	{
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject == null)
		{
			return;
		}
		if (currentSelectedGameObject.transform.parent != this.m_ContentRectTransform.transform)
		{
			return;
		}
		this.m_SelectedRectTransform = currentSelectedGameObject.GetComponent<RectTransform>();
		Vector3 vector = this.m_RectTransform.localPosition - this.m_SelectedRectTransform.localPosition;
		float num = this.m_ContentRectTransform.rect.height - this.m_RectTransform.rect.height;
		float num2 = this.m_ContentRectTransform.rect.height - vector.y;
		float num3 = this.m_ScrollRect.normalizedPosition.y * num;
		float num4 = num3 - this.m_SelectedRectTransform.rect.height / 2f + this.m_RectTransform.rect.height;
		float num5 = num3 + this.m_SelectedRectTransform.rect.height / 2f;
		if (num2 > num4)
		{
			float num6 = num2 - num4;
			float num7 = num3 + num6;
			float y = num7 / num;
			this.m_ScrollRect.normalizedPosition = Vector2.Lerp(this.m_ScrollRect.normalizedPosition, new Vector2(0f, y), this.scrollSpeed * Time.deltaTime);
		}
		else if (num2 < num5)
		{
			float num8 = num2 - num5;
			float num9 = num3 + num8;
			float y2 = num9 / num;
			this.m_ScrollRect.normalizedPosition = Vector2.Lerp(this.m_ScrollRect.normalizedPosition, new Vector2(0f, y2), this.scrollSpeed * Time.deltaTime);
		}
	}

	// Token: 0x04006E8B RID: 28299
	public float scrollSpeed = 10f;

	// Token: 0x04006E8C RID: 28300
	private ScrollRect m_ScrollRect;

	// Token: 0x04006E8D RID: 28301
	private RectTransform m_RectTransform;

	// Token: 0x04006E8E RID: 28302
	private RectTransform m_ContentRectTransform;

	// Token: 0x04006E8F RID: 28303
	private RectTransform m_SelectedRectTransform;
}
