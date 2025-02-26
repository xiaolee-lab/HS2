using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009DE RID: 2526
	public class CvsF_MakeupLip : CvsBase
	{
		// Token: 0x06004A25 RID: 18981 RVA: 0x001C4289 File Offset: 0x001C2689
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x001C42B3 File Offset: 0x001C26B3
		private void CalculateUI()
		{
			this.ssLipGloss.SetSliderValue(base.makeup.lipGloss);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x001C42CB File Offset: 0x001C26CB
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscLipType.SetToggleID(base.makeup.lipId);
			this.csLipColor.SetColor(base.makeup.lipColor);
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x001C4308 File Offset: 0x001C2708
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssLipGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.lipGloss));
			yield break;
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x001C4324 File Offset: 0x001C2724
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsLip += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_lip, ChaListDefine.KeyType.Unknown);
			this.sscLipType.CreateList(lst);
			this.sscLipType.SetToggleID(base.makeup.lipId);
			this.sscLipType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.makeup.lipId != info.id)
				{
					base.makeup.lipId = info.id;
					base.chaCtrl.AddUpdateCMFaceTexFlags(false, false, false, false, false, true, false);
					base.chaCtrl.CreateFaceTexture();
				}
			};
			this.csLipColor.actUpdateColor = delegate(Color color)
			{
				base.makeup.lipColor = color;
				base.chaCtrl.AddUpdateCMFaceColorFlags(false, false, false, false, false, true, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssLipGloss.onChange = delegate(float value)
			{
				base.makeup.lipGloss = value;
				base.chaCtrl.AddUpdateCMFaceGlossFlags(false, false, false, false, true);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssLipGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.lipGloss);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400447A RID: 17530
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscLipType;

		// Token: 0x0400447B RID: 17531
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csLipColor;

		// Token: 0x0400447C RID: 17532
		[SerializeField]
		private CustomSliderSet ssLipGloss;
	}
}
