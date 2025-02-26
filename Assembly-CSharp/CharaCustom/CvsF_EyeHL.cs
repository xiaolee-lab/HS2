using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009D8 RID: 2520
	public class CvsF_EyeHL : CvsBase
	{
		// Token: 0x060049D3 RID: 18899 RVA: 0x001C2151 File Offset: 0x001C0551
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x001C217C File Offset: 0x001C057C
		private void CalculateUI()
		{
			this.ssHLW.SetSliderValue(base.face.hlLayout.x);
			this.ssHLH.SetSliderValue(base.face.hlLayout.y);
			this.ssHLX.SetSliderValue(base.face.hlLayout.z);
			this.ssHLY.SetSliderValue(base.face.hlLayout.w);
			this.ssHLTilt.SetSliderValue(base.face.hlTilt);
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x001C2217 File Offset: 0x001C0617
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscEyeHLType.SetToggleID(base.face.hlId);
			this.csEyeHLColor.SetColor(base.face.hlColor);
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x001C2254 File Offset: 0x001C0654
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssHLW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.hlLayout.x));
			this.ssHLH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.hlLayout.y));
			this.ssHLX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.hlLayout.z));
			this.ssHLY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.hlLayout.w));
			this.ssHLTilt.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.hlTilt));
			yield break;
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x001C2270 File Offset: 0x001C0670
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsEyeHL += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_eye_hl, ChaListDefine.KeyType.Unknown);
			this.sscEyeHLType.CreateList(lst);
			this.sscEyeHLType.SetToggleID(base.face.hlId);
			this.sscEyeHLType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.hlId != info.id)
				{
					base.face.hlId = info.id;
					base.chaCtrl.ChangeEyesHighlightKind();
				}
			};
			this.csEyeHLColor.actUpdateColor = delegate(Color color)
			{
				base.face.hlColor = color;
				base.chaCtrl.ChangeEyesHighlightColor();
			};
			this.ssHLW.onChange = delegate(float value)
			{
				base.face.hlLayout = new Vector4(value, base.face.hlLayout.y, base.face.hlLayout.z, base.face.hlLayout.w);
				base.chaCtrl.ChangeEyesHighlighLayout();
			};
			this.ssHLW.onSetDefaultValue = (() => base.defChaCtrl.custom.face.hlLayout.x);
			this.ssHLH.onChange = delegate(float value)
			{
				base.face.hlLayout = new Vector4(base.face.hlLayout.x, value, base.face.hlLayout.z, base.face.hlLayout.w);
				base.chaCtrl.ChangeEyesHighlighLayout();
			};
			this.ssHLH.onSetDefaultValue = (() => base.defChaCtrl.custom.face.hlLayout.y);
			this.ssHLX.onChange = delegate(float value)
			{
				base.face.hlLayout = new Vector4(base.face.hlLayout.x, base.face.hlLayout.y, value, base.face.hlLayout.w);
				base.chaCtrl.ChangeEyesHighlighLayout();
			};
			this.ssHLX.onSetDefaultValue = (() => base.defChaCtrl.custom.face.hlLayout.z);
			this.ssHLY.onChange = delegate(float value)
			{
				base.face.hlLayout = new Vector4(base.face.hlLayout.x, base.face.hlLayout.y, base.face.hlLayout.z, value);
				base.chaCtrl.ChangeEyesHighlighLayout();
			};
			this.ssHLY.onSetDefaultValue = (() => base.defChaCtrl.custom.face.hlLayout.w);
			this.ssHLTilt.onChange = delegate(float value)
			{
				base.face.hlTilt = value;
				base.chaCtrl.ChangeEyesHighlighTilt();
			};
			this.ssHLTilt.onSetDefaultValue = (() => base.defChaCtrl.custom.face.hlTilt);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400445C RID: 17500
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscEyeHLType;

		// Token: 0x0400445D RID: 17501
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csEyeHLColor;

		// Token: 0x0400445E RID: 17502
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomSliderSet ssHLW;

		// Token: 0x0400445F RID: 17503
		[SerializeField]
		private CustomSliderSet ssHLH;

		// Token: 0x04004460 RID: 17504
		[SerializeField]
		private CustomSliderSet ssHLX;

		// Token: 0x04004461 RID: 17505
		[SerializeField]
		private CustomSliderSet ssHLY;

		// Token: 0x04004462 RID: 17506
		[SerializeField]
		private CustomSliderSet ssHLTilt;
	}
}
