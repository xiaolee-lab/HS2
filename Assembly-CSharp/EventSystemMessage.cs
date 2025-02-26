using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02001170 RID: 4464
public class EventSystemMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler, IEventSystemHandler
{
	// Token: 0x06009354 RID: 37716 RVA: 0x003D0A1B File Offset: 0x003CEE1B
	public void OnPointerEnter(PointerEventData eventData)
	{
		MonoBehaviour.print("OnPointerEnter : " + eventData);
	}

	// Token: 0x06009355 RID: 37717 RVA: 0x003D0A2D File Offset: 0x003CEE2D
	public void OnPointerExit(PointerEventData eventData)
	{
		MonoBehaviour.print("OnPointerExit : " + eventData);
	}

	// Token: 0x06009356 RID: 37718 RVA: 0x003D0A3F File Offset: 0x003CEE3F
	public void OnPointerDown(PointerEventData eventData)
	{
		MonoBehaviour.print("OnPointerDown : " + eventData);
	}

	// Token: 0x06009357 RID: 37719 RVA: 0x003D0A51 File Offset: 0x003CEE51
	public void OnPointerUp(PointerEventData eventData)
	{
		MonoBehaviour.print("OnPointerUp : " + eventData);
	}

	// Token: 0x06009358 RID: 37720 RVA: 0x003D0A63 File Offset: 0x003CEE63
	public void OnPointerClick(PointerEventData eventData)
	{
		MonoBehaviour.print("OnPointerClick : " + eventData);
	}

	// Token: 0x06009359 RID: 37721 RVA: 0x003D0A75 File Offset: 0x003CEE75
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		MonoBehaviour.print("OnInitializePotentialDrag : " + eventData);
	}

	// Token: 0x0600935A RID: 37722 RVA: 0x003D0A87 File Offset: 0x003CEE87
	public void OnBeginDrag(PointerEventData eventData)
	{
		MonoBehaviour.print("OnBeginDrag : " + eventData);
	}

	// Token: 0x0600935B RID: 37723 RVA: 0x003D0A99 File Offset: 0x003CEE99
	public void OnDrag(PointerEventData eventData)
	{
		MonoBehaviour.print("OnDrag : " + eventData);
	}

	// Token: 0x0600935C RID: 37724 RVA: 0x003D0AAB File Offset: 0x003CEEAB
	public void OnEndDrag(PointerEventData eventData)
	{
		MonoBehaviour.print("OnEndDrag : " + eventData);
	}

	// Token: 0x0600935D RID: 37725 RVA: 0x003D0ABD File Offset: 0x003CEEBD
	public void OnDrop(PointerEventData eventData)
	{
		MonoBehaviour.print("OnDrop : " + eventData);
	}

	// Token: 0x0600935E RID: 37726 RVA: 0x003D0ACF File Offset: 0x003CEECF
	public void OnScroll(PointerEventData eventData)
	{
		MonoBehaviour.print("OnScroll : " + eventData);
	}

	// Token: 0x0600935F RID: 37727 RVA: 0x003D0AE1 File Offset: 0x003CEEE1
	public void OnUpdateSelected(BaseEventData eventData)
	{
		if (!this._UpdateSelected)
		{
			MonoBehaviour.print("OnUpdateSelected : " + eventData);
			this._UpdateSelected = true;
		}
	}

	// Token: 0x06009360 RID: 37728 RVA: 0x003D0B05 File Offset: 0x003CEF05
	public void OnSelect(BaseEventData eventData)
	{
		MonoBehaviour.print("OnSelect : " + eventData);
		this._UpdateSelected = false;
	}

	// Token: 0x06009361 RID: 37729 RVA: 0x003D0B1E File Offset: 0x003CEF1E
	public void OnDeselect(BaseEventData eventData)
	{
		MonoBehaviour.print("OnDeselect : " + eventData);
	}

	// Token: 0x06009362 RID: 37730 RVA: 0x003D0B30 File Offset: 0x003CEF30
	public void OnMove(AxisEventData eventData)
	{
		MonoBehaviour.print("OnMove : " + eventData);
	}

	// Token: 0x06009363 RID: 37731 RVA: 0x003D0B42 File Offset: 0x003CEF42
	public void OnSubmit(BaseEventData eventData)
	{
		MonoBehaviour.print("OnSubmit : " + eventData);
	}

	// Token: 0x06009364 RID: 37732 RVA: 0x003D0B54 File Offset: 0x003CEF54
	public void OnCancel(BaseEventData eventData)
	{
		MonoBehaviour.print("OnCancel : " + eventData);
	}

	// Token: 0x0400770D RID: 30477
	private bool _UpdateSelected;
}
