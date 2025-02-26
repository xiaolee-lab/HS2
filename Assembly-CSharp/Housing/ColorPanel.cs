using System;
using Illusion.Component.UI.ColorPicker;
using Illusion.Extensions;
using UniRx;
using UnityEngine;

namespace Housing
{
	// Token: 0x020008A7 RID: 2215
	public class ColorPanel : MonoBehaviour
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x00152D2E File Offset: 0x0015112E
		public bool isOpen
		{
			get
			{
				return this.cgWindow.alpha != 0f;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x00152D4C File Offset: 0x0015114C
		// (set) Token: 0x0600398D RID: 14733 RVA: 0x00152D54 File Offset: 0x00151154
		private bool Enable
		{
			get
			{
				return this.isOpen;
			}
			set
			{
				if (this.cgWindow != null)
				{
					this.cgWindow.Enable(value, false);
				}
			}
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x00152D70 File Offset: 0x00151170
		public void Setup(Color color, Action<Color> _actUpdateColor, bool _alpha = false)
		{
			this.sampleColor.SafeProc(delegate(UI_SampleColor _sc)
			{
				_sc.SetColor(color);
				_sc.actUpdateColor = _actUpdateColor;
			});
			this.Enable = true;
			this.cmpPickerRect.isAlpha.Value = _alpha;
			this.cmpPickerSliderI.useAlpha.Value = _alpha;
			this.btnClose.ClearState();
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x00152DDD File Offset: 0x001511DD
		public void SetColor(Color color)
		{
			if (this.sampleColor != null)
			{
				this.sampleColor.SetColor(color);
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x00152DF8 File Offset: 0x001511F8
		public void Close()
		{
			this.sampleColor.SafeProc(delegate(UI_SampleColor _sc)
			{
				_sc.actUpdateColor = null;
			});
			this.Enable = false;
			if (this.onClose != null)
			{
				this.onClose();
			}
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x00152E4D File Offset: 0x0015124D
		private void Start()
		{
			if (this.btnClose != null)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.Close();
				});
			}
		}

		// Token: 0x04003933 RID: 14643
		[SerializeField]
		[Tooltip("このキャンバスグループ")]
		private CanvasGroup cgWindow;

		// Token: 0x04003934 RID: 14644
		[SerializeField]
		[Tooltip("閉じるボタン")]
		private ButtonEx btnClose;

		// Token: 0x04003935 RID: 14645
		[SerializeField]
		[Tooltip("サンプルカラーScript")]
		private UI_SampleColor sampleColor;

		// Token: 0x04003936 RID: 14646
		[SerializeField]
		[Tooltip("PickerのRect")]
		[Header("ピッカー関係")]
		private PickerRectA cmpPickerRect;

		// Token: 0x04003937 RID: 14647
		[SerializeField]
		[Tooltip("PickerのSlider")]
		private PickerSliderInput cmpPickerSliderI;

		// Token: 0x04003938 RID: 14648
		public Action onClose;
	}
}
