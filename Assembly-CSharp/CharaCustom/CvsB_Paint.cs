using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009EB RID: 2539
	public class CvsB_Paint : CvsBase
	{
		// Token: 0x06004AB1 RID: 19121 RVA: 0x001C7D6D File Offset: 0x001C616D
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x001C7D98 File Offset: 0x001C6198
		private void CalculateUI()
		{
			this.ssPaintGloss.SetSliderValue(base.body.paintInfo[base.SNo].glossPower);
			this.ssPaintMetallic.SetSliderValue(base.body.paintInfo[base.SNo].metallicPower);
			this.ssPaintW.SetSliderValue(base.body.paintInfo[base.SNo].layout.x);
			this.ssPaintH.SetSliderValue(base.body.paintInfo[base.SNo].layout.y);
			this.ssPaintX.SetSliderValue(base.body.paintInfo[base.SNo].layout.z);
			this.ssPaintY.SetSliderValue(base.body.paintInfo[base.SNo].layout.w);
			this.ssPaintRot.SetSliderValue(base.body.paintInfo[base.SNo].rotation);
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x001C7EB4 File Offset: 0x001C62B4
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscPaintType.SetToggleID(base.body.paintInfo[base.SNo].id);
			this.csPaintColor.SetColor(base.body.paintInfo[base.SNo].color);
			this.sscPaintLayout.SetToggleID(base.body.paintInfo[base.SNo].layoutId);
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x001C7F34 File Offset: 0x001C6334
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssPaintGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].glossPower));
			this.ssPaintMetallic.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].metallicPower));
			this.ssPaintW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].layout.x));
			this.ssPaintH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].layout.y));
			this.ssPaintX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].layout.z));
			this.ssPaintY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].layout.w));
			this.ssPaintRot.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.paintInfo[base.SNo].rotation));
			yield break;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001C7F50 File Offset: 0x001C6350
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsBodyPaint += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_paint, ChaListDefine.KeyType.Unknown);
			this.sscPaintType.CreateList(lst);
			this.sscPaintType.SetToggleID(base.body.paintInfo[base.SNo].id);
			this.sscPaintType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.paintInfo[base.SNo].id != info.id)
				{
					base.body.paintInfo[base.SNo].id = info.id;
					base.chaCtrl.AddUpdateCMBodyTexFlags(false, 0 == base.SNo, 1 == base.SNo, false);
					base.chaCtrl.CreateBodyTexture();
				}
			};
			this.csPaintColor.actUpdateColor = delegate(Color color)
			{
				base.body.paintInfo[base.SNo].color = color;
				base.chaCtrl.AddUpdateCMBodyColorFlags(false, 0 == base.SNo, 1 == base.SNo, false);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintGloss.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].glossPower = value;
				base.chaCtrl.AddUpdateCMBodyGlossFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].glossPower);
			this.ssPaintMetallic.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].metallicPower = value;
				base.chaCtrl.AddUpdateCMBodyGlossFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintMetallic.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].metallicPower);
			List<CustomSelectInfo> lst2 = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.bodypaint_layout, ChaListDefine.KeyType.Unknown);
			this.sscPaintLayout.CreateList(lst2);
			this.sscPaintLayout.SetToggleID(base.body.paintInfo[base.SNo].layoutId);
			this.sscPaintLayout.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.paintInfo[base.SNo].layoutId != info.id)
				{
					base.body.paintInfo[base.SNo].layoutId = info.id;
					base.chaCtrl.AddUpdateCMBodyLayoutFlags(0 == base.SNo, 1 == base.SNo);
					base.chaCtrl.CreateBodyTexture();
				}
			};
			this.ssPaintW.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].layout = new Vector4(value, base.body.paintInfo[base.SNo].layout.y, base.body.paintInfo[base.SNo].layout.z, base.body.paintInfo[base.SNo].layout.w);
				base.chaCtrl.AddUpdateCMBodyLayoutFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintW.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].layout.x);
			this.ssPaintH.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].layout = new Vector4(base.body.paintInfo[base.SNo].layout.x, value, base.body.paintInfo[base.SNo].layout.z, base.body.paintInfo[base.SNo].layout.w);
				base.chaCtrl.AddUpdateCMBodyLayoutFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintH.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].layout.y);
			this.ssPaintX.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].layout = new Vector4(base.body.paintInfo[base.SNo].layout.x, base.body.paintInfo[base.SNo].layout.y, value, base.body.paintInfo[base.SNo].layout.w);
				base.chaCtrl.AddUpdateCMBodyLayoutFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintX.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].layout.z);
			this.ssPaintY.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].layout = new Vector4(base.body.paintInfo[base.SNo].layout.x, base.body.paintInfo[base.SNo].layout.y, base.body.paintInfo[base.SNo].layout.z, value);
				base.chaCtrl.AddUpdateCMBodyLayoutFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintY.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].layout.w);
			this.ssPaintRot.onChange = delegate(float value)
			{
				base.body.paintInfo[base.SNo].rotation = value;
				base.chaCtrl.AddUpdateCMBodyLayoutFlags(0 == base.SNo, 1 == base.SNo);
				base.chaCtrl.CreateBodyTexture();
			};
			this.ssPaintRot.onSetDefaultValue = (() => base.defChaCtrl.custom.body.paintInfo[base.SNo].rotation);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040044E9 RID: 17641
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscPaintType;

		// Token: 0x040044EA RID: 17642
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csPaintColor;

		// Token: 0x040044EB RID: 17643
		[SerializeField]
		private CustomSliderSet ssPaintGloss;

		// Token: 0x040044EC RID: 17644
		[SerializeField]
		private CustomSliderSet ssPaintMetallic;

		// Token: 0x040044ED RID: 17645
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscPaintLayout;

		// Token: 0x040044EE RID: 17646
		[SerializeField]
		private CustomSliderSet ssPaintW;

		// Token: 0x040044EF RID: 17647
		[SerializeField]
		private CustomSliderSet ssPaintH;

		// Token: 0x040044F0 RID: 17648
		[SerializeField]
		private CustomSliderSet ssPaintX;

		// Token: 0x040044F1 RID: 17649
		[SerializeField]
		private CustomSliderSet ssPaintY;

		// Token: 0x040044F2 RID: 17650
		[SerializeField]
		private CustomSliderSet ssPaintRot;

		// Token: 0x040044F3 RID: 17651
		private Dictionary<int, Vector4> dictPaintLayout;
	}
}
