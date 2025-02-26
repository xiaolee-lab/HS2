using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A20 RID: 2592
public class UI_ButtonEx : Button
{
	// Token: 0x06004D1B RID: 19739 RVA: 0x001D9727 File Offset: 0x001D7B27
	protected override void Awake()
	{
		if (this.text == null)
		{
			this.text = base.GetComponentsInChildren<Text>();
		}
	}

	// Token: 0x06004D1C RID: 19740 RVA: 0x001D9740 File Offset: 0x001D7B40
	protected override void Start()
	{
		base.Start();
		if (null != this.overImage)
		{
			this.overImage.enabled = false;
		}
	}

	// Token: 0x06004D1D RID: 19741 RVA: 0x001D9765 File Offset: 0x001D7B65
	protected override void OnEnable()
	{
		base.OnEnable();
		if (null != this.overImage)
		{
			this.overImage.enabled = false;
		}
	}

	// Token: 0x06004D1E RID: 19742 RVA: 0x001D978C File Offset: 0x001D7B8C
	protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		switch (state)
		{
		case Selectable.SelectionState.Normal:
			if (this.text != null)
			{
				foreach (Text text in this.text)
				{
					text.color = new Color(base.colors.normalColor.r, base.colors.normalColor.g, base.colors.normalColor.b, (!this.alpha) ? text.color.a : base.colors.normalColor.a);
				}
			}
			break;
		case Selectable.SelectionState.Highlighted:
			if (this.text != null)
			{
				foreach (Text text2 in this.text)
				{
					text2.color = new Color(base.colors.highlightedColor.r, base.colors.highlightedColor.g, base.colors.highlightedColor.b, (!this.alpha) ? text2.color.a : base.colors.highlightedColor.a);
				}
			}
			break;
		case Selectable.SelectionState.Pressed:
			if (this.text != null)
			{
				foreach (Text text3 in this.text)
				{
					text3.color = new Color(base.colors.pressedColor.r, base.colors.pressedColor.g, base.colors.pressedColor.b, (!this.alpha) ? text3.color.a : base.colors.pressedColor.a);
				}
			}
			break;
		case Selectable.SelectionState.Disabled:
			if (this.text != null)
			{
				foreach (Text text4 in this.text)
				{
					text4.color = new Color(base.colors.disabledColor.r, base.colors.disabledColor.g, base.colors.disabledColor.b, (!this.alpha) ? text4.color.a : base.colors.disabledColor.a);
				}
			}
			break;
		}
	}

	// Token: 0x06004D1F RID: 19743 RVA: 0x001D9AC0 File Offset: 0x001D7EC0
	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		if (null != this.overImage)
		{
			this.overImage.enabled = base.interactable;
		}
	}

	// Token: 0x06004D20 RID: 19744 RVA: 0x001D9AEB File Offset: 0x001D7EEB
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (null != this.overImage)
		{
			this.overImage.enabled = false;
		}
	}

	// Token: 0x04004688 RID: 18056
	[SerializeField]
	private Image overImage;

	// Token: 0x04004689 RID: 18057
	[SerializeField]
	private Text[] text;

	// Token: 0x0400468A RID: 18058
	[SerializeField]
	private bool alpha;
}
