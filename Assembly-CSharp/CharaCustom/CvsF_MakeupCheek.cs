using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009DC RID: 2524
	public class CvsF_MakeupCheek : CvsBase
	{
		// Token: 0x06004A11 RID: 18961 RVA: 0x001C3C89 File Offset: 0x001C2089
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x001C3CB3 File Offset: 0x001C20B3
		private void CalculateUI()
		{
			this.ssCheekGloss.SetSliderValue(base.makeup.cheekGloss);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x001C3CCB File Offset: 0x001C20CB
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscCheekType.SetToggleID(base.makeup.cheekId);
			this.csCheekColor.SetColor(base.makeup.cheekColor);
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x001C3D08 File Offset: 0x001C2108
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssCheekGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.cheekGloss));
			yield break;
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x001C3D24 File Offset: 0x001C2124
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsCheek += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_cheek, ChaListDefine.KeyType.Unknown);
			this.sscCheekType.CreateList(lst);
			this.sscCheekType.SetToggleID(base.makeup.cheekId);
			this.sscCheekType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.makeup.cheekId != info.id)
				{
					base.makeup.cheekId = info.id;
					base.chaCtrl.AddUpdateCMFaceTexFlags(false, false, false, false, true, false, false);
					base.chaCtrl.CreateFaceTexture();
				}
			};
			this.csCheekColor.actUpdateColor = delegate(Color color)
			{
				base.makeup.cheekColor = color;
				base.chaCtrl.AddUpdateCMFaceColorFlags(false, false, false, false, true, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssCheekGloss.onChange = delegate(float value)
			{
				base.makeup.cheekGloss = value;
				base.chaCtrl.AddUpdateCMFaceGlossFlags(false, false, false, true, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssCheekGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.cheekGloss);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004474 RID: 17524
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscCheekType;

		// Token: 0x04004475 RID: 17525
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csCheekColor;

		// Token: 0x04004476 RID: 17526
		[SerializeField]
		private CustomSliderSet ssCheekGloss;
	}
}
