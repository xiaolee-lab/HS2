using System;
using Studio;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020012D7 RID: 4823
public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x170021F1 RID: 8689
	// (get) Token: 0x0600A0E3 RID: 41187 RVA: 0x0042187D File Offset: 0x0041FC7D
	protected Canvas canvas
	{
		get
		{
			if (this.m_Canvas == null)
			{
				this.m_Canvas = base.GetComponentInParent<Canvas>();
			}
			return this.m_Canvas;
		}
	}

	// Token: 0x170021F2 RID: 8690
	// (get) Token: 0x0600A0E4 RID: 41188 RVA: 0x004218A2 File Offset: 0x0041FCA2
	protected RectTransform rectCanvas
	{
		get
		{
			if (this.m_RectCanvas == null)
			{
				this.m_RectCanvas = (this.canvas.transform as RectTransform);
			}
			return this.m_RectCanvas;
		}
	}

	// Token: 0x170021F3 RID: 8691
	// (get) Token: 0x0600A0E5 RID: 41189 RVA: 0x004218D1 File Offset: 0x0041FCD1
	protected RectTransform rectTransform
	{
		get
		{
			if (this.m_RectTransform == null)
			{
				this.m_RectTransform = (base.transform as RectTransform);
			}
			return this.m_RectTransform;
		}
	}

	// Token: 0x0600A0E6 RID: 41190 RVA: 0x004218FC File Offset: 0x0041FCFC
	public void OnBeginDrag(PointerEventData eventData)
	{
		SortCanvas.select = this.canvas;
		Rect pixelRect = this.canvas.pixelRect;
		Vector2 sizeDelta = this.rectCanvas.sizeDelta;
		Vector2 sizeDelta2 = this.rectTransform.sizeDelta;
		Vector2 anchorMax = this.rectTransform.anchorMax;
		Vector2 pivot = this.rectTransform.pivot;
		this.rectArea.Set(sizeDelta.x * anchorMax.x + sizeDelta2.x * pivot.x, -(sizeDelta.y * anchorMax.y) + sizeDelta2.y * pivot.y, sizeDelta.x - sizeDelta2.x, sizeDelta.y - sizeDelta2.y);
		this.vecRate.x = pixelRect.width / sizeDelta.x;
		this.vecRate.y = pixelRect.height / sizeDelta.y;
	}

	// Token: 0x0600A0E7 RID: 41191 RVA: 0x004219F0 File Offset: 0x0041FDF0
	public void OnDrag(PointerEventData eventData)
	{
		Vector2 vector = eventData.delta;
		vector.x /= this.vecRate.x;
		vector.y /= this.vecRate.y;
		vector += this.rectTransform.anchoredPosition;
		this.rectTransform.anchoredPosition = Rect.NormalizedToPoint(this.rectArea, Rect.PointToNormalized(this.rectArea, vector));
	}

	// Token: 0x0600A0E8 RID: 41192 RVA: 0x00421A6A File Offset: 0x0041FE6A
	public void OnEndDrag(PointerEventData eventData)
	{
	}

	// Token: 0x0600A0E9 RID: 41193 RVA: 0x00421A6C File Offset: 0x0041FE6C
	public void OnPointerDown(PointerEventData eventData)
	{
		SortCanvas.select = this.canvas;
	}

	// Token: 0x04007F15 RID: 32533
	[SerializeField]
	protected Canvas m_Canvas;

	// Token: 0x04007F16 RID: 32534
	protected RectTransform m_RectCanvas;

	// Token: 0x04007F17 RID: 32535
	protected RectTransform m_RectTransform;

	// Token: 0x04007F18 RID: 32536
	protected Rect rectArea = default(Rect);

	// Token: 0x04007F19 RID: 32537
	protected Vector2 vecRate = Vector2.one;
}
