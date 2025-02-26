using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009EA RID: 2538
	public class CvsB_Nip : CvsBase
	{
		// Token: 0x06004AA7 RID: 19111 RVA: 0x001C7AB5 File Offset: 0x001C5EB5
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x001C7ADF File Offset: 0x001C5EDF
		private void CalculateUI()
		{
			this.ssNipGloss.SetSliderValue(base.body.nipGlossPower);
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x001C7AF7 File Offset: 0x001C5EF7
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscNipType.SetToggleID(base.body.nipId);
			this.csNipColor.SetColor(base.body.nipColor);
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x001C7B34 File Offset: 0x001C5F34
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssNipGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.nipGlossPower));
			yield break;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x001C7B50 File Offset: 0x001C5F50
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsNip += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_nip, ChaListDefine.KeyType.Unknown);
			this.sscNipType.CreateList(lst);
			this.sscNipType.SetToggleID(base.body.nipId);
			this.sscNipType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.nipId != info.id)
				{
					base.body.nipId = info.id;
					base.chaCtrl.ChangeNipKind();
				}
			};
			this.csNipColor.actUpdateColor = delegate(Color color)
			{
				base.body.nipColor = color;
				base.chaCtrl.ChangeNipColor();
			};
			this.ssNipGloss.onChange = delegate(float value)
			{
				base.body.nipGlossPower = value;
				base.chaCtrl.ChangeNipGloss();
			};
			this.ssNipGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.body.nipGlossPower);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040044E6 RID: 17638
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscNipType;

		// Token: 0x040044E7 RID: 17639
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csNipColor;

		// Token: 0x040044E8 RID: 17640
		[SerializeField]
		private CustomSliderSet ssNipGloss;
	}
}
