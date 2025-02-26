using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009F2 RID: 2546
	public class CvsB_Skin : CvsBase
	{
		// Token: 0x06004AF8 RID: 19192 RVA: 0x001C9DE8 File Offset: 0x001C81E8
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x001C9E14 File Offset: 0x001C8214
		private void CalculateUI()
		{
			this.ssDetailPower.SetSliderValue(base.body.detailPower);
			this.ssSkinGloss.SetSliderValue(base.body.skinGlossPower);
			this.ssSkinMetallic.SetSliderValue(base.body.skinMetallicPower);
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x001C9E64 File Offset: 0x001C8264
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscSkinType.SetToggleID(base.body.skinId);
			this.sscDetailType.SetToggleID(base.body.detailId);
			this.csSkinColor.SetColor(base.body.skinColor);
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x001C9EC0 File Offset: 0x001C82C0
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssDetailPower.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.detailPower));
			this.ssSkinGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.skinGlossPower));
			this.ssSkinMetallic.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.skinMetallicPower));
			yield break;
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x001C9EDC File Offset: 0x001C82DC
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsBodySkinType += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_b : ChaListDefine.CategoryNo.mt_skin_b, ChaListDefine.KeyType.Unknown);
			this.sscSkinType.CreateList(lst);
			this.sscSkinType.SetToggleID(base.body.skinId);
			this.sscSkinType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.skinId != info.id)
				{
					base.body.skinId = info.id;
					base.chaCtrl.AddUpdateCMBodyTexFlags(true, false, false, false);
					base.chaCtrl.CreateBodyTexture();
				}
			};
			this.ssDetailPower.onChange = delegate(float value)
			{
				base.body.detailPower = value;
				base.chaCtrl.ChangeBodyDetailPower();
			};
			this.ssDetailPower.onSetDefaultValue = (() => base.defChaCtrl.custom.body.detailPower);
			List<CustomSelectInfo> lst2 = CvsBase.CreateSelectList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.ft_detail_b : ChaListDefine.CategoryNo.mt_detail_b, ChaListDefine.KeyType.Unknown);
			this.sscDetailType.CreateList(lst2);
			this.sscDetailType.SetToggleID(base.body.detailId);
			this.sscDetailType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.detailId != info.id)
				{
					base.body.detailId = info.id;
					base.chaCtrl.AddUpdateCMBodyTexFlags(true, false, false, false);
					base.chaCtrl.CreateBodyTexture();
				}
			};
			this.csSkinColor.actUpdateColor = delegate(Color color)
			{
				base.body.skinColor = color;
				base.chaCtrl.AddUpdateCMBodyColorFlags(true, false, false, false);
				base.chaCtrl.CreateBodyTexture();
				base.chaCtrl.AddUpdateCMFaceColorFlags(true, false, false, false, false, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssSkinGloss.onChange = delegate(float value)
			{
				base.body.skinGlossPower = value;
				base.chaCtrl.ChangeBodyGlossPower();
				base.chaCtrl.ChangeFaceGlossPower();
			};
			this.ssSkinGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.body.skinGlossPower);
			this.ssSkinMetallic.onChange = delegate(float value)
			{
				base.body.skinMetallicPower = value;
				base.chaCtrl.ChangeBodyMetallicPower();
				base.chaCtrl.ChangeFaceMetallicPower();
			};
			this.ssSkinMetallic.onSetDefaultValue = (() => base.defChaCtrl.custom.body.skinMetallicPower);
			this.hcPreset.onClick = delegate(Color color)
			{
				base.body.skinColor = color;
				base.chaCtrl.AddUpdateCMBodyColorFlags(true, false, false, false);
				base.chaCtrl.CreateBodyTexture();
				base.chaCtrl.AddUpdateCMFaceColorFlags(true, false, false, false, false, false, false);
				base.chaCtrl.CreateFaceTexture();
				this.csSkinColor.SetColor(color);
			};
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004524 RID: 17700
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscSkinType;

		// Token: 0x04004525 RID: 17701
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomSliderSet ssDetailPower;

		// Token: 0x04004526 RID: 17702
		[SerializeField]
		private CustomSelectScrollController sscDetailType;

		// Token: 0x04004527 RID: 17703
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomColorSet csSkinColor;

		// Token: 0x04004528 RID: 17704
		[SerializeField]
		private CustomSliderSet ssSkinGloss;

		// Token: 0x04004529 RID: 17705
		[SerializeField]
		private CustomSliderSet ssSkinMetallic;

		// Token: 0x0400452A RID: 17706
		[SerializeField]
		private CustomSkinColorPreset hcPreset;
	}
}
