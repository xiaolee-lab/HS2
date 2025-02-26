using System;
using Illusion.Component.UI.ColorPicker;
using Illusion.Extensions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012D1 RID: 4817
	public class ColorPalette : Singleton<ColorPalette>
	{
		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x0600A0A1 RID: 41121 RVA: 0x0041FECB File Offset: 0x0041E2CB
		public bool isOpen
		{
			get
			{
				return this.cgWindow.alpha != 0f;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x0600A0A2 RID: 41122 RVA: 0x0041FEE9 File Offset: 0x0041E2E9
		// (set) Token: 0x0600A0A3 RID: 41123 RVA: 0x0041FEF6 File Offset: 0x0041E2F6
		public bool visible
		{
			get
			{
				return this._visible.Value;
			}
			set
			{
				this._visible.Value = value;
			}
		}

		// Token: 0x170021EE RID: 8686
		// (set) Token: 0x0600A0A4 RID: 41124 RVA: 0x0041FF04 File Offset: 0x0041E304
		public bool outsideVisible
		{
			set
			{
				this._outsideVisible = value;
				if (this.cgWindow)
				{
					this.cgWindow.Enable(this._visible.Value && this._outsideVisible, false);
				}
			}
		}

		// Token: 0x0600A0A5 RID: 41125 RVA: 0x0041FF44 File Offset: 0x0041E344
		public void Setup(string winTitle, Color color, Action<Color> _actUpdateColor, bool _useAlpha)
		{
			if (this.textWinTitle && !winTitle.IsNullOrEmpty())
			{
				this.textWinTitle.text = winTitle;
			}
			if (null != this.sampleColor)
			{
				this.sampleColor.SetColor(color);
				this.sampleColor.actUpdateColor = _actUpdateColor;
			}
			this.visible = true;
			this.cmpPickerRect.isAlpha.Value = _useAlpha;
			this.cmpPickerSliderI.useAlpha.Value = _useAlpha;
		}

		// Token: 0x0600A0A6 RID: 41126 RVA: 0x0041FFCC File Offset: 0x0041E3CC
		public void Close()
		{
			if (this.textWinTitle)
			{
				this.textWinTitle.text = string.Empty;
			}
			if (null != this.sampleColor)
			{
				this.sampleColor.actUpdateColor = null;
			}
			if (this.cgWindow)
			{
				this.cgWindow.Enable(false, false);
			}
		}

		// Token: 0x0600A0A7 RID: 41127 RVA: 0x00420033 File Offset: 0x0041E433
		public bool Check(string _text)
		{
			return !_text.IsNullOrEmpty() && this.textWinTitle.text == _text;
		}

		// Token: 0x0600A0A8 RID: 41128 RVA: 0x00420058 File Offset: 0x0041E458
		protected override void Awake()
		{
			this._visible.Subscribe(delegate(bool b)
			{
				if (this.cgWindow)
				{
					this.cgWindow.Enable(b && this._outsideVisible, false);
				}
				if (!b)
				{
					this.Close();
				}
				if (this.isOpen)
				{
					SortCanvas.select = this.canvas;
				}
			});
			if (this.btnClose)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.Close();
				});
			}
			this.Close();
		}

		// Token: 0x04007EE2 RID: 32482
		[SerializeField]
		private Canvas canvas;

		// Token: 0x04007EE3 RID: 32483
		[Tooltip("このキャンバスグループ")]
		[SerializeField]
		private CanvasGroup cgWindow;

		// Token: 0x04007EE4 RID: 32484
		[Tooltip("ウィンドウタイトル")]
		[SerializeField]
		private TextMeshProUGUI textWinTitle;

		// Token: 0x04007EE5 RID: 32485
		[Tooltip("閉じるボタン")]
		[SerializeField]
		private Button btnClose;

		// Token: 0x04007EE6 RID: 32486
		[Tooltip("サンプルカラーScript")]
		[SerializeField]
		private SampleColor sampleColor;

		// Token: 0x04007EE7 RID: 32487
		[Tooltip("PickerのRect")]
		[SerializeField]
		private PickerRectA cmpPickerRect;

		// Token: 0x04007EE8 RID: 32488
		[Tooltip("PickerのSlider")]
		[SerializeField]
		private PickerSliderInput cmpPickerSliderI;

		// Token: 0x04007EE9 RID: 32489
		private BoolReactiveProperty _visible = new BoolReactiveProperty(false);

		// Token: 0x04007EEA RID: 32490
		private bool _outsideVisible = true;
	}
}
