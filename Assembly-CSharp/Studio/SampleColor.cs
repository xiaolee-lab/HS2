using System;
using Illusion.Component.UI.ColorPicker;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012D5 RID: 4821
	public class SampleColor : MonoBehaviour
	{
		// Token: 0x0600A0D0 RID: 41168 RVA: 0x0042121C File Offset: 0x0041F61C
		private void Start()
		{
			if (this.image)
			{
				if (this.rect)
				{
					this.rect.SetColor(this.image.color);
					this.rect.updateColorAction += delegate(Color color)
					{
						this.UpdateRectColor(color);
					};
				}
				if (this.slider)
				{
					this.slider.color = this.image.color;
					this.slider.SetInputText();
					this.slider.updateColorAction += delegate(Color color)
					{
						this.UpdateSliderColor(color);
					};
				}
				if (this.preset)
				{
					this.preset.color = this.image.color;
					this.preset.updateColorAction += delegate(Color color)
					{
						this.UpdatePresetsColor(color);
					};
				}
			}
		}

		// Token: 0x0600A0D1 RID: 41169 RVA: 0x004212FC File Offset: 0x0041F6FC
		public void SetColor(Color color)
		{
			this.callUpdate = true;
			this.image.color = color;
			if (this.rect)
			{
				this.rect.SetColor(color);
			}
			if (this.slider)
			{
				this.slider.color = color;
			}
			if (this.preset)
			{
				this.preset.color = color;
			}
			this.callUpdate = false;
		}

		// Token: 0x0600A0D2 RID: 41170 RVA: 0x00421378 File Offset: 0x0041F778
		public void UpdateRectColor(Color color)
		{
			if (this.callUpdate)
			{
				return;
			}
			this.callUpdate = true;
			this.image.color = color;
			if (this.slider)
			{
				this.slider.color = color;
			}
			if (this.preset)
			{
				this.preset.color = color;
			}
			this.actUpdateColor.Call(color);
			this.callUpdate = false;
		}

		// Token: 0x0600A0D3 RID: 41171 RVA: 0x004213F0 File Offset: 0x0041F7F0
		public void UpdateSliderColor(Color color)
		{
			if (this.callUpdate)
			{
				return;
			}
			this.callUpdate = true;
			this.image.color = color;
			if (this.rect)
			{
				this.rect.SetColor(color);
			}
			if (this.preset)
			{
				this.preset.color = color;
			}
			this.actUpdateColor.Call(color);
			this.callUpdate = false;
		}

		// Token: 0x0600A0D4 RID: 41172 RVA: 0x00421468 File Offset: 0x0041F868
		public void UpdatePresetsColor(Color color)
		{
			if (this.callUpdate)
			{
				return;
			}
			this.callUpdate = true;
			this.image.color = color;
			if (this.rect)
			{
				this.rect.SetColor(color);
			}
			if (this.slider)
			{
				this.slider.color = color;
			}
			this.actUpdateColor.Call(color);
			this.callUpdate = false;
		}

		// Token: 0x04007F08 RID: 32520
		[SerializeField]
		private Image image;

		// Token: 0x04007F09 RID: 32521
		[SerializeField]
		private PickerRect rect;

		// Token: 0x04007F0A RID: 32522
		[SerializeField]
		private PickerSliderInput slider;

		// Token: 0x04007F0B RID: 32523
		[SerializeField]
		private ColorPresets preset;

		// Token: 0x04007F0C RID: 32524
		private bool callUpdate;

		// Token: 0x04007F0D RID: 32525
		public Action<Color> actUpdateColor;
	}
}
