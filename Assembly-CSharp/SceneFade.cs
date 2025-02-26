using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200117D RID: 4477
public class SceneFade : SimpleFade
{
	// Token: 0x060093CC RID: 37836 RVA: 0x003D1CFC File Offset: 0x003D00FC
	protected override void Awake()
	{
		base.Awake();
		this.image = base.GetComponent<Image>();
		this.canvas = this.image.canvas;
		this.canvasRt = (this.canvas.transform as RectTransform);
		this.sortingOrder = this.canvas.sortingOrder;
	}

	// Token: 0x060093CD RID: 37837 RVA: 0x003D1D54 File Offset: 0x003D0154
	protected override void Update()
	{
		base.Update();
		this.image.color = this._Color;
		this.image.rectTransform.sizeDelta = new Vector2(this.canvasRt.rect.width, this.canvasRt.rect.height);
		int num = this.canvas.sortingOrder;
		this.canvas.sortingOrder = ((!base.IsFadeNow) ? -32768 : this.sortingOrder);
		if (num != this.canvas.sortingOrder)
		{
			this.canvas.enabled = false;
			this.canvas.enabled = true;
		}
	}

	// Token: 0x060093CE RID: 37838 RVA: 0x003D1E0E File Offset: 0x003D020E
	protected override void OnGUI()
	{
	}

	// Token: 0x0400772E RID: 30510
	private Canvas canvas;

	// Token: 0x0400772F RID: 30511
	private RectTransform canvasRt;

	// Token: 0x04007730 RID: 30512
	private Image image;

	// Token: 0x04007731 RID: 30513
	private int sortingOrder = -32768;
}
