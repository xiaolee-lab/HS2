using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x02000517 RID: 1303
	[RequireComponent(typeof(Image))]
	public class ControllerUIElement : MonoBehaviour
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0009A2C7 File Offset: 0x000986C7
		private bool hasEffects
		{
			get
			{
				return this._positiveUIEffect != null || this._negativeUIEffect != null;
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0009A2E9 File Offset: 0x000986E9
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
			this.ClearLabels();
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0009A31C File Offset: 0x0009871C
		public void Activate(float amount)
		{
			amount = Mathf.Clamp(amount, -1f, 1f);
			if (this.hasEffects)
			{
				if (amount < 0f && this._negativeUIEffect != null)
				{
					this._negativeUIEffect.Activate(Mathf.Abs(amount));
				}
				if (amount > 0f && this._positiveUIEffect != null)
				{
					this._positiveUIEffect.Activate(Mathf.Abs(amount));
				}
			}
			else
			{
				if (this._isActive && amount == this._highlightAmount)
				{
					return;
				}
				this._highlightAmount = amount;
				this._color = Color.Lerp(this._origColor, this._highlightColor, this._highlightAmount);
			}
			this._isActive = true;
			this.RedrawImage();
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].Activate(amount);
					}
				}
			}
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0009A43C File Offset: 0x0009883C
		public void Deactivate()
		{
			if (!this._isActive)
			{
				return;
			}
			this._color = this._origColor;
			this._highlightAmount = 0f;
			if (this._positiveUIEffect != null)
			{
				this._positiveUIEffect.Deactivate();
			}
			if (this._negativeUIEffect != null)
			{
				this._negativeUIEffect.Deactivate();
			}
			this._isActive = false;
			this.RedrawImage();
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].Deactivate();
					}
				}
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0009A4FC File Offset: 0x000988FC
		public void SetLabel(string text, AxisRange labelType)
		{
			Text text2;
			switch (labelType)
			{
			case AxisRange.Full:
				text2 = this._label;
				break;
			case AxisRange.Positive:
				text2 = this._positiveLabel;
				break;
			case AxisRange.Negative:
				text2 = this._negativeLabel;
				break;
			default:
				text2 = null;
				break;
			}
			if (text2 != null)
			{
				text2.text = text;
			}
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].SetLabel(text, labelType);
					}
				}
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0009A5AC File Offset: 0x000989AC
		public void ClearLabels()
		{
			if (this._label != null)
			{
				this._label.text = string.Empty;
			}
			if (this._positiveLabel != null)
			{
				this._positiveLabel.text = string.Empty;
			}
			if (this._negativeLabel != null)
			{
				this._negativeLabel.text = string.Empty;
			}
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].ClearLabels();
					}
				}
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0009A667 File Offset: 0x00098A67
		private void RedrawImage()
		{
			this._image.color = this._color;
		}

		// Token: 0x04001BD9 RID: 7129
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001BDA RID: 7130
		[SerializeField]
		private ControllerUIEffect _positiveUIEffect;

		// Token: 0x04001BDB RID: 7131
		[SerializeField]
		private ControllerUIEffect _negativeUIEffect;

		// Token: 0x04001BDC RID: 7132
		[SerializeField]
		private Text _label;

		// Token: 0x04001BDD RID: 7133
		[SerializeField]
		private Text _positiveLabel;

		// Token: 0x04001BDE RID: 7134
		[SerializeField]
		private Text _negativeLabel;

		// Token: 0x04001BDF RID: 7135
		[SerializeField]
		private ControllerUIElement[] _childElements = new ControllerUIElement[0];

		// Token: 0x04001BE0 RID: 7136
		private Image _image;

		// Token: 0x04001BE1 RID: 7137
		private Color _color;

		// Token: 0x04001BE2 RID: 7138
		private Color _origColor;

		// Token: 0x04001BE3 RID: 7139
		private bool _isActive;

		// Token: 0x04001BE4 RID: 7140
		private float _highlightAmount;
	}
}
