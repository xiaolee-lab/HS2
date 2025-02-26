using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009F5 RID: 2549
	public class CvsB_Underhair : CvsBase
	{
		// Token: 0x06004B17 RID: 19223 RVA: 0x001CA6A9 File Offset: 0x001C8AA9
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x001CA6D3 File Offset: 0x001C8AD3
		private void CalculateUI()
		{
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x001CA6D5 File Offset: 0x001C8AD5
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscUnderhairType.SetToggleID(base.body.underhairId);
			this.csUnderhairColor.SetColor(base.body.underhairColor);
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x001CA710 File Offset: 0x001C8B10
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			yield break;
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x001CA72C File Offset: 0x001C8B2C
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsUnderhair += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_underhair, ChaListDefine.KeyType.Unknown);
			this.sscUnderhairType.CreateList(lst);
			this.sscUnderhairType.SetToggleID(base.body.underhairId);
			this.sscUnderhairType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.body.underhairId != info.id)
				{
					base.body.underhairId = info.id;
					base.chaCtrl.ChangeUnderHairKind();
				}
			};
			this.csUnderhairColor.actUpdateColor = delegate(Color color)
			{
				base.body.underhairColor = color;
				base.chaCtrl.ChangeUnderHairColor();
			};
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400452E RID: 17710
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscUnderhairType;

		// Token: 0x0400452F RID: 17711
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csUnderhairColor;
	}
}
