using System;
using Illusion.Component.UI.ColorPicker;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009CA RID: 2506
	public class CustomColorCtrl : MonoBehaviour
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x001BDC55 File Offset: 0x001BC055
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x001BDC5C File Offset: 0x001BC05C
		public bool isOpen
		{
			get
			{
				return this.cgWindow.alpha != 0f;
			}
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x001BDC7C File Offset: 0x001BC07C
		public void Setup(CustomColorSet ccSet, Color color, Action<Color> _actUpdateColor, bool _useAlpha)
		{
			this.linkCustomColorSet = ccSet;
			if (null != this.sampleColor)
			{
				this.sampleColor.SetColor(color);
				this.sampleColor.actUpdateColor = _actUpdateColor;
			}
			this.customBase.customCtrl.showColorCvs = true;
			this.cmpPickerRect.isAlpha.Value = _useAlpha;
			this.cmpPickerSliderI.useAlpha.Value = _useAlpha;
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x001BDCEE File Offset: 0x001BC0EE
		public void EnableAlpha(bool enable)
		{
			this.cmpPickerRect.isAlpha.Value = enable;
			this.cmpPickerSliderI.useAlpha.Value = enable;
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001BDD12 File Offset: 0x001BC112
		public void SetColor(CustomColorSet ccSet, Color color)
		{
			if (ccSet != this.linkCustomColorSet)
			{
				return;
			}
			if (null != this.sampleColor)
			{
				this.sampleColor.SetColor(color);
			}
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x001BDD43 File Offset: 0x001BC143
		public void Close()
		{
			if (null != this.sampleColor)
			{
				this.sampleColor.actUpdateColor = null;
			}
			this.customBase.customCtrl.showColorCvs = false;
			this.linkCustomColorSet = null;
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x001BDD7A File Offset: 0x001BC17A
		private void Start()
		{
			if (this.btnClose)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.Close();
				});
			}
		}

		// Token: 0x040043DC RID: 17372
		[Tooltip("このキャンバスグループ")]
		[SerializeField]
		private CanvasGroup cgWindow;

		// Token: 0x040043DD RID: 17373
		[Tooltip("閉じるボタン")]
		[SerializeField]
		private Button btnClose;

		// Token: 0x040043DE RID: 17374
		[Tooltip("サンプルカラーScript")]
		[SerializeField]
		private UI_SampleColor sampleColor;

		// Token: 0x040043DF RID: 17375
		[Tooltip("PickerのRect")]
		[SerializeField]
		private PickerRectA cmpPickerRect;

		// Token: 0x040043E0 RID: 17376
		[Tooltip("PickerのSlider")]
		[SerializeField]
		private PickerSliderInput cmpPickerSliderI;

		// Token: 0x040043E1 RID: 17377
		public CustomColorSet linkCustomColorSet;
	}
}
