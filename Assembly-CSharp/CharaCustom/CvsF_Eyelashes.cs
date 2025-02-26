using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009D9 RID: 2521
	public class CvsF_Eyelashes : CvsBase
	{
		// Token: 0x060049E5 RID: 18917 RVA: 0x001C2846 File Offset: 0x001C0C46
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x001C2870 File Offset: 0x001C0C70
		private void CalculateUI()
		{
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x001C2872 File Offset: 0x001C0C72
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscEyelashesType.SetToggleID(base.face.eyelashesId);
			this.csEyelashesColor.SetColor(base.face.eyelashesColor);
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x001C28AC File Offset: 0x001C0CAC
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			yield break;
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x001C28C8 File Offset: 0x001C0CC8
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsEyelashes += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_eyelash, ChaListDefine.KeyType.Unknown);
			this.sscEyelashesType.CreateList(lst);
			this.sscEyelashesType.SetToggleID(base.face.eyelashesId);
			this.sscEyelashesType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.eyelashesId != info.id)
				{
					base.face.eyelashesId = info.id;
					base.chaCtrl.ChangeEyelashesKind();
				}
			};
			this.csEyelashesColor.actUpdateColor = delegate(Color color)
			{
				base.face.eyelashesColor = color;
				base.chaCtrl.ChangeEyelashesColor();
			};
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004463 RID: 17507
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscEyelashesType;

		// Token: 0x04004464 RID: 17508
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csEyelashesColor;
	}
}
