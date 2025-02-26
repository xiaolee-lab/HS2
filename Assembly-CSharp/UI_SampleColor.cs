using System;
using Illusion.Component.UI.ColorPicker;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A2C RID: 2604
public class UI_SampleColor : MonoBehaviour
{
	// Token: 0x06004D59 RID: 19801 RVA: 0x001DAEE0 File Offset: 0x001D92E0
	private void Start()
	{
		if (null != this.image)
		{
			if (null != this.rect)
			{
				this.rect.SetColor(this.image.color);
				this.rect.updateColorAction += delegate(Color color)
				{
					this.UpdateRectColor(color);
				};
			}
			if (null != this.slider)
			{
				this.slider.color = this.image.color;
				this.slider.SetInputText();
				this.slider.updateColorAction += delegate(Color color)
				{
					this.UpdateSliderColor(color);
				};
			}
			if (null != this.preset)
			{
				this.preset.color = this.image.color;
				this.preset.updateColorAction += delegate(Color color)
				{
					this.UpdatePresetsColor(color);
				};
			}
		}
	}

	// Token: 0x06004D5A RID: 19802 RVA: 0x001DAFC4 File Offset: 0x001D93C4
	public void SetColor(Color color)
	{
		this.callUpdate = true;
		this.image.color = color;
		if (null != this.rect)
		{
			this.rect.SetColor(color);
		}
		if (null != this.slider)
		{
			this.slider.color = color;
		}
		if (null != this.preset)
		{
			this.preset.color = color;
		}
		this.callUpdate = false;
	}

	// Token: 0x06004D5B RID: 19803 RVA: 0x001DB044 File Offset: 0x001D9444
	public void UpdateRectColor(Color color)
	{
		if (this.callUpdate)
		{
			return;
		}
		this.callUpdate = true;
		this.image.color = color;
		if (null != this.slider)
		{
			this.slider.color = color;
		}
		if (null != this.preset)
		{
			this.preset.color = color;
		}
		if (this.actUpdateColor != null)
		{
			this.actUpdateColor(color);
		}
		this.callUpdate = false;
	}

	// Token: 0x06004D5C RID: 19804 RVA: 0x001DB0CC File Offset: 0x001D94CC
	public void UpdateSliderColor(Color color)
	{
		if (this.callUpdate)
		{
			return;
		}
		this.callUpdate = true;
		this.image.color = color;
		if (null != this.rect)
		{
			this.rect.SetColor(color);
		}
		if (null != this.preset)
		{
			this.preset.color = color;
		}
		if (this.actUpdateColor != null)
		{
			this.actUpdateColor(color);
		}
		this.callUpdate = false;
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x001DB154 File Offset: 0x001D9554
	public void UpdatePresetsColor(Color color)
	{
		if (this.callUpdate)
		{
			return;
		}
		this.callUpdate = true;
		this.image.color = color;
		if (null != this.rect)
		{
			this.rect.SetColor(color);
		}
		if (null != this.slider)
		{
			this.slider.color = color;
		}
		if (this.actUpdateColor != null)
		{
			this.actUpdateColor(color);
		}
		this.callUpdate = false;
	}

	// Token: 0x040046BD RID: 18109
	[SerializeField]
	private Image image;

	// Token: 0x040046BE RID: 18110
	[SerializeField]
	private PickerRect rect;

	// Token: 0x040046BF RID: 18111
	[SerializeField]
	private PickerSliderInput slider;

	// Token: 0x040046C0 RID: 18112
	[SerializeField]
	private UI_ColorPresets preset;

	// Token: 0x040046C1 RID: 18113
	private bool callUpdate;

	// Token: 0x040046C2 RID: 18114
	public Action<Color> actUpdateColor;
}
