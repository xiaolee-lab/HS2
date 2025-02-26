using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009D5 RID: 2517
	public class CvsF_Beard : CvsBase
	{
		// Token: 0x060049B9 RID: 18873 RVA: 0x001C1AA5 File Offset: 0x001BFEA5
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x001C1ACF File Offset: 0x001BFECF
		private void CalculateUI()
		{
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x001C1AD1 File Offset: 0x001BFED1
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscBeardType.SetToggleID(base.face.beardId);
			this.csBeardColor.SetColor(base.face.beardColor);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x001C1B0C File Offset: 0x001BFF0C
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			yield break;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x001C1B28 File Offset: 0x001BFF28
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsBeard += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.mt_beard, ChaListDefine.KeyType.Unknown);
			this.sscBeardType.CreateList(lst);
			this.sscBeardType.SetToggleID(base.face.beardId);
			this.sscBeardType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.beardId != info.id)
				{
					base.face.beardId = info.id;
					base.chaCtrl.ChangeBeardKind();
				}
			};
			this.csBeardColor.actUpdateColor = delegate(Color color)
			{
				base.face.beardColor = color;
				base.chaCtrl.ChangeBeardColor();
			};
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004456 RID: 17494
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscBeardType;

		// Token: 0x04004457 RID: 17495
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csBeardColor;
	}
}
