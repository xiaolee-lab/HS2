using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02001162 RID: 4450
public class UI_DragWindow : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IEventSystemHandler
{
	// Token: 0x060092F8 RID: 37624 RVA: 0x003CEADC File Offset: 0x003CCEDC
	private void Start()
	{
		if (null == this.rtMove)
		{
			this.rtMove = (base.transform as RectTransform);
		}
		if (null == this.rtDrag)
		{
			this.rtDrag = this.rtMove;
		}
		if (null == this.rtCanvas)
		{
			this.SearchCanvas();
		}
		if (null != this.rtCanvas && null == this.canvas)
		{
			this.canvas = this.rtCanvas.GetComponent<Canvas>();
			if (this.canvas)
			{
				this.cscaler = this.rtCanvas.GetComponent<CanvasScaler>();
			}
		}
		if (this.camCtrl == null && Camera.main)
		{
			this.camCtrl = Camera.main.GetComponent<CameraControl>();
		}
	}

	// Token: 0x060092F9 RID: 37625 RVA: 0x003CEBC4 File Offset: 0x003CCFC4
	private void SearchCanvas()
	{
		GameObject gameObject = base.gameObject;
		for (;;)
		{
			this.canvas = gameObject.GetComponent<Canvas>();
			if (this.canvas)
			{
				break;
			}
			if (null == gameObject.transform.parent)
			{
				return;
			}
			gameObject = gameObject.transform.parent.gameObject;
		}
		this.rtCanvas = (gameObject.transform as RectTransform);
	}

	// Token: 0x060092FA RID: 37626 RVA: 0x003CEC3C File Offset: 0x003CD03C
	private float GetScreenRate()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		Vector2 one = Vector2.one;
		one.x = num / this.cscaler.referenceResolution.x;
		one.y = num2 / this.cscaler.referenceResolution.y;
		return one.x * (1f - this.cscaler.matchWidthOrHeight) + one.y * this.cscaler.matchWidthOrHeight;
	}

	// Token: 0x060092FB RID: 37627 RVA: 0x003CECC4 File Offset: 0x003CD0C4
	private void CalcDragPosOverlay(PointerEventData ped)
	{
		Vector2 anchoredPosition = ped.position - this.dragStartPosBackup;
		anchoredPosition.x /= this.rtCanvas.localScale.x;
		anchoredPosition.y /= this.rtCanvas.localScale.y;
		float num = (this.rtDrag.rect.size.x != this.rtDrag.sizeDelta.x) ? (this.rtDrag.rect.size.x - this.rtDrag.sizeDelta.x) : this.rtDrag.sizeDelta.x;
		float num2 = (this.rtDrag.rect.size.y != this.rtDrag.sizeDelta.y) ? (this.rtDrag.rect.size.y - this.rtDrag.sizeDelta.y) : this.rtDrag.sizeDelta.y;
		anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f, (float)Screen.width / this.rtCanvas.localScale.x - num);
		anchoredPosition.y = -Mathf.Clamp(-anchoredPosition.y, 0f, (float)Screen.height / this.rtCanvas.localScale.y - num2);
		float num3 = (this.rtMove.rect.size.x != this.rtMove.sizeDelta.x) ? (this.rtMove.rect.size.x - this.rtMove.sizeDelta.x) : this.rtMove.sizeDelta.x;
		float num4 = (this.rtMove.rect.size.y != this.rtMove.sizeDelta.y) ? (this.rtMove.rect.size.y - this.rtMove.sizeDelta.y) : this.rtMove.sizeDelta.y;
		anchoredPosition.x += num3 * this.rtMove.pivot.x;
		anchoredPosition.y += num4 * (this.rtMove.pivot.y - 1f);
		this.rtMove.anchoredPosition = anchoredPosition;
	}

	// Token: 0x060092FC RID: 37628 RVA: 0x003CF000 File Offset: 0x003CD400
	private void CalcDragPosScreenSpace(PointerEventData ped)
	{
		Vector2 anchoredPosition = ped.position - this.dragStartPosBackup;
		float screenRate = this.GetScreenRate();
		anchoredPosition.x /= screenRate;
		anchoredPosition.y /= screenRate;
		float num = (this.rtDrag.rect.size.x != this.rtDrag.sizeDelta.x) ? (this.rtDrag.rect.size.x - this.rtDrag.sizeDelta.x) : this.rtDrag.sizeDelta.x;
		float num2 = (this.rtDrag.rect.size.y != this.rtDrag.sizeDelta.y) ? (this.rtDrag.rect.size.y - this.rtDrag.sizeDelta.y) : this.rtDrag.sizeDelta.y;
		anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f, (float)Screen.width / screenRate - num);
		anchoredPosition.y = -Mathf.Clamp(-anchoredPosition.y, 0f, (float)Screen.height / screenRate - num2);
		float num3 = (this.rtMove.rect.size.x != this.rtMove.sizeDelta.x) ? (this.rtMove.rect.size.x - this.rtMove.sizeDelta.x) : this.rtMove.sizeDelta.x;
		float num4 = (this.rtMove.rect.size.y != this.rtMove.sizeDelta.y) ? (this.rtMove.rect.size.y - this.rtMove.sizeDelta.y) : this.rtMove.sizeDelta.y;
		anchoredPosition.x += num3 * this.rtMove.pivot.x;
		anchoredPosition.y += num4 * (this.rtMove.pivot.y - 1f);
		this.rtMove.anchoredPosition = anchoredPosition;
	}

	// Token: 0x060092FD RID: 37629 RVA: 0x003CF2F8 File Offset: 0x003CD6F8
	private void SetClickPosOverlay(PointerEventData ped)
	{
		Vector2 zero = Vector2.zero;
		float num = (this.rtMove.rect.size.x != this.rtMove.sizeDelta.x) ? (this.rtMove.rect.size.x - this.rtMove.sizeDelta.x) : this.rtMove.sizeDelta.x;
		float num2 = (this.rtMove.rect.size.y != this.rtMove.sizeDelta.y) ? (this.rtMove.rect.size.y - this.rtMove.sizeDelta.y) : this.rtMove.sizeDelta.y;
		zero.x = (this.rtMove.anchoredPosition.x - num * this.rtMove.pivot.x) * this.rtCanvas.localScale.x;
		zero.y = (this.rtMove.anchoredPosition.y - num2 * (this.rtMove.pivot.y - 1f)) * this.rtCanvas.localScale.y;
		this.dragStartPosBackup = ped.position - zero;
	}

	// Token: 0x060092FE RID: 37630 RVA: 0x003CF4B8 File Offset: 0x003CD8B8
	private void SetClickPosScreenSpace(PointerEventData ped)
	{
		float screenRate = this.GetScreenRate();
		Vector2 zero = Vector2.zero;
		float num = (this.rtMove.rect.size.x != this.rtMove.sizeDelta.x) ? (this.rtMove.rect.size.x - this.rtMove.sizeDelta.x) : this.rtMove.sizeDelta.x;
		float num2 = (this.rtMove.rect.size.y != this.rtMove.sizeDelta.y) ? (this.rtMove.rect.size.y - this.rtMove.sizeDelta.y) : this.rtMove.sizeDelta.y;
		zero.x = (this.rtMove.anchoredPosition.x - num * this.rtMove.pivot.x) * screenRate;
		zero.y = (this.rtMove.anchoredPosition.y - num2 * (this.rtMove.pivot.y - 1f)) * screenRate;
		this.dragStartPosBackup = ped.position - zero;
	}

	// Token: 0x060092FF RID: 37631 RVA: 0x003CF658 File Offset: 0x003CDA58
	public void OnPointerDown(PointerEventData ped)
	{
		switch (this.canvas.renderMode)
		{
		case RenderMode.ScreenSpaceOverlay:
			this.SetClickPosOverlay(ped);
			break;
		case RenderMode.ScreenSpaceCamera:
			this.SetClickPosScreenSpace(ped);
			break;
		}
		if (this.camCtrl)
		{
			this.camCtrl.NoCtrlCondition = (() => true);
		}
	}

	// Token: 0x06009300 RID: 37632 RVA: 0x003CF6E4 File Offset: 0x003CDAE4
	public void OnBeginDrag(PointerEventData ped)
	{
		switch (this.canvas.renderMode)
		{
		case RenderMode.ScreenSpaceOverlay:
			this.CalcDragPosOverlay(ped);
			break;
		case RenderMode.ScreenSpaceCamera:
			this.CalcDragPosScreenSpace(ped);
			break;
		}
	}

	// Token: 0x06009301 RID: 37633 RVA: 0x003CF738 File Offset: 0x003CDB38
	public void OnDrag(PointerEventData ped)
	{
		switch (this.canvas.renderMode)
		{
		case RenderMode.ScreenSpaceOverlay:
			this.CalcDragPosOverlay(ped);
			break;
		case RenderMode.ScreenSpaceCamera:
			this.CalcDragPosScreenSpace(ped);
			break;
		}
	}

	// Token: 0x06009302 RID: 37634 RVA: 0x003CF78A File Offset: 0x003CDB8A
	public void OnEndDrag(PointerEventData ped)
	{
		if (this.camCtrl)
		{
			this.camCtrl.NoCtrlCondition = (() => false);
		}
	}

	// Token: 0x06009303 RID: 37635 RVA: 0x003CF7C4 File Offset: 0x003CDBC4
	public void OnPointerUp(PointerEventData ped)
	{
		if (this.camCtrl)
		{
			this.camCtrl.NoCtrlCondition = (() => false);
		}
	}

	// Token: 0x040076E8 RID: 30440
	public RectTransform rtDrag;

	// Token: 0x040076E9 RID: 30441
	public RectTransform rtMove;

	// Token: 0x040076EA RID: 30442
	public RectTransform rtCanvas;

	// Token: 0x040076EB RID: 30443
	private Canvas canvas;

	// Token: 0x040076EC RID: 30444
	private CanvasScaler cscaler;

	// Token: 0x040076ED RID: 30445
	private Vector2 dragStartPosBackup = Vector2.zero;

	// Token: 0x040076EE RID: 30446
	private CameraControl camCtrl;
}
