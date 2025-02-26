using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x02000516 RID: 1302
	[RequireComponent(typeof(Image))]
	public class ControllerUIEffect : MonoBehaviour
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x0009A1C6 File Offset: 0x000985C6
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0009A1F4 File Offset: 0x000985F4
		public void Activate(float amount)
		{
			amount = Mathf.Clamp01(amount);
			if (this._isActive && amount == this._highlightAmount)
			{
				return;
			}
			this._highlightAmount = amount;
			this._color = Color.Lerp(this._origColor, this._highlightColor, this._highlightAmount);
			this._isActive = true;
			this.RedrawImage();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0009A252 File Offset: 0x00098652
		public void Deactivate()
		{
			if (!this._isActive)
			{
				return;
			}
			this._color = this._origColor;
			this._highlightAmount = 0f;
			this._isActive = false;
			this.RedrawImage();
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0009A284 File Offset: 0x00098684
		private void RedrawImage()
		{
			this._image.color = this._color;
			this._image.enabled = this._isActive;
		}

		// Token: 0x04001BD3 RID: 7123
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001BD4 RID: 7124
		private Image _image;

		// Token: 0x04001BD5 RID: 7125
		private Color _color;

		// Token: 0x04001BD6 RID: 7126
		private Color _origColor;

		// Token: 0x04001BD7 RID: 7127
		private bool _isActive;

		// Token: 0x04001BD8 RID: 7128
		private float _highlightAmount;
	}
}
