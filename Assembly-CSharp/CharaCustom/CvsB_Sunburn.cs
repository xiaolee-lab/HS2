using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009F4 RID: 2548
	public class CvsB_Sunburn : CvsBase
	{
		// Token: 0x06004B0F RID: 19215 RVA: 0x001CA447 File Offset: 0x001C8847
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x001CA471 File Offset: 0x001C8871
		private void CalculateUI()
		{
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x001CA473 File Offset: 0x001C8873
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscSunburnType.SetToggleID(base.body.sunburnId);
			this.csSunburnColor.SetColor(base.body.sunburnColor);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x001CA4B0 File Offset: 0x001C88B0
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			yield break;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x001CA4CC File Offset: 0x001C88CC
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsSunburn += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.ft_sunburn : ChaListDefine.CategoryNo.mt_sunburn, ChaListDefine.KeyType.Unknown);
			this.sscSunburnType.CreateList(lst);
			this.sscSunburnType.SetToggleID(base.body.sunburnId);
			this.sscSunburnType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.sunburnId != info.id)
				{
					base.body.sunburnId = info.id;
					base.chaCtrl.AddUpdateCMBodyTexFlags(false, false, false, true);
					base.chaCtrl.CreateBodyTexture();
				}
			};
			this.csSunburnColor.actUpdateColor = delegate(Color color)
			{
				base.body.sunburnColor = color;
				base.chaCtrl.AddUpdateCMBodyColorFlags(false, false, false, true);
				base.chaCtrl.CreateBodyTexture();
			};
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400452C RID: 17708
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscSunburnType;

		// Token: 0x0400452D RID: 17709
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csSunburnColor;
	}
}
