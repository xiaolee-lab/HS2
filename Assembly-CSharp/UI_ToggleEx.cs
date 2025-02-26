using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A2D RID: 2605
public class UI_ToggleEx : Toggle
{
	// Token: 0x06004D62 RID: 19810 RVA: 0x001DB208 File Offset: 0x001D9608
	protected override void Awake()
	{
		if (this.text == null)
		{
			this.text = base.GetComponentsInChildren<Text>();
		}
	}

	// Token: 0x06004D63 RID: 19811 RVA: 0x001DB221 File Offset: 0x001D9621
	protected override void Start()
	{
		base.Start();
		this.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
		{
			if (this.text != null)
			{
				foreach (Text text in this.text)
				{
					text.color = ((!isOn) ? base.colors.normalColor : this.selectedColor);
				}
			}
		});
		if (null != this.overImage)
		{
			this.overImage.enabled = false;
		}
	}

	// Token: 0x06004D64 RID: 19812 RVA: 0x001DB25E File Offset: 0x001D965E
	protected override void OnEnable()
	{
		base.OnEnable();
		if (null != this.overImage)
		{
			this.overImage.enabled = false;
		}
	}

	// Token: 0x06004D65 RID: 19813 RVA: 0x001DB284 File Offset: 0x001D9684
	public void SetTextColor(int state)
	{
		switch (state)
		{
		case 0:
			if (this.text != null)
			{
				foreach (Text text in this.text)
				{
					text.color = new Color(base.colors.highlightedColor.r, base.colors.highlightedColor.g, base.colors.highlightedColor.b, (!this.alpha) ? text.color.a : base.colors.highlightedColor.a);
				}
			}
			break;
		case 1:
			if (this.text != null)
			{
				foreach (Text text2 in this.text)
				{
					text2.color = new Color(base.colors.normalColor.r, base.colors.normalColor.g, base.colors.normalColor.b, (!this.alpha) ? text2.color.a : base.colors.normalColor.a);
				}
			}
			break;
		case 2:
			if (this.text != null)
			{
				foreach (Text text3 in this.text)
				{
					text3.color = new Color(base.colors.pressedColor.r, base.colors.pressedColor.g, base.colors.pressedColor.b, (!this.alpha) ? text3.color.a : base.colors.pressedColor.a);
				}
			}
			break;
		case 3:
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

	// Token: 0x06004D66 RID: 19814 RVA: 0x001DB5B0 File Offset: 0x001D99B0
	protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		if (base.isOn)
		{
			return;
		}
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
			if (null != this.baseImageEx)
			{
				this.baseImageEx.color = base.colors.normalColor;
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
			if (null != this.baseImageEx)
			{
				this.baseImageEx.color = base.colors.highlightedColor;
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
			if (null != this.baseImageEx)
			{
				this.baseImageEx.color = base.colors.pressedColor;
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
			if (null != this.baseImageEx)
			{
				this.baseImageEx.color = base.colors.disabledColor;
			}
			break;
		}
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x001DB99C File Offset: 0x001D9D9C
	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		if (null != this.overImage)
		{
			this.overImage.enabled = base.interactable;
		}
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x001DB9C7 File Offset: 0x001D9DC7
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (null != this.overImage)
		{
			this.overImage.enabled = false;
		}
	}

	// Token: 0x040046C3 RID: 18115
	[SerializeField]
	private Image baseImageEx;

	// Token: 0x040046C4 RID: 18116
	[SerializeField]
	private Image overImage;

	// Token: 0x040046C5 RID: 18117
	[SerializeField]
	private Color selectedColor = Color.white;

	// Token: 0x040046C6 RID: 18118
	[SerializeField]
	private Text[] text;

	// Token: 0x040046C7 RID: 18119
	[SerializeField]
	private bool alpha;
}
