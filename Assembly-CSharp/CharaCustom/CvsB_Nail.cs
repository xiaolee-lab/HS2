using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E9 RID: 2537
	public class CvsB_Nail : CvsBase
	{
		// Token: 0x06004A9E RID: 19102 RVA: 0x001C7894 File Offset: 0x001C5C94
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x001C78BE File Offset: 0x001C5CBE
		private void CalculateUI()
		{
			this.ssNailGloss.SetSliderValue(base.body.nailGlossPower);
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x001C78D6 File Offset: 0x001C5CD6
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.csNailColor.SetColor(base.body.nailColor);
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x001C78FC File Offset: 0x001C5CFC
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssNailGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.nailGlossPower));
			yield break;
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x001C7918 File Offset: 0x001C5D18
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsNail += this.UpdateCustomUI;
			this.csNailColor.actUpdateColor = delegate(Color color)
			{
				base.body.nailColor = color;
				base.chaCtrl.ChangeNailColor();
			};
			this.ssNailGloss.onChange = delegate(float value)
			{
				base.body.nailGlossPower = value;
				base.chaCtrl.ChangeNailGloss();
			};
			this.ssNailGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.body.nailGlossPower);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040044E4 RID: 17636
		[SerializeField]
		private CustomColorSet csNailColor;

		// Token: 0x040044E5 RID: 17637
		[SerializeField]
		private CustomSliderSet ssNailGloss;
	}
}
