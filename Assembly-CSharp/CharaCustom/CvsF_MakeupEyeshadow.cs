using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009DD RID: 2525
	public class CvsF_MakeupEyeshadow : CvsBase
	{
		// Token: 0x06004A1B RID: 18971 RVA: 0x001C3F89 File Offset: 0x001C2389
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x001C3FB3 File Offset: 0x001C23B3
		private void CalculateUI()
		{
			this.ssEyeshadowGloss.SetSliderValue(base.makeup.eyeshadowGloss);
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x001C3FCB File Offset: 0x001C23CB
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscEyeshadowType.SetToggleID(base.makeup.eyeshadowId);
			this.csEyeshadowColor.SetColor(base.makeup.eyeshadowColor);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x001C4008 File Offset: 0x001C2408
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssEyeshadowGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.makeup.eyeshadowGloss));
			yield break;
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x001C4024 File Offset: 0x001C2424
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsEyeshadow += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_eyeshadow, ChaListDefine.KeyType.Unknown);
			this.sscEyeshadowType.CreateList(lst);
			this.sscEyeshadowType.SetToggleID(base.makeup.eyeshadowId);
			this.sscEyeshadowType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.makeup.eyeshadowId != info.id)
				{
					base.makeup.eyeshadowId = info.id;
					base.chaCtrl.AddUpdateCMFaceTexFlags(false, true, false, false, false, false, false);
					base.chaCtrl.CreateFaceTexture();
				}
			};
			this.csEyeshadowColor.actUpdateColor = delegate(Color color)
			{
				base.makeup.eyeshadowColor = color;
				base.chaCtrl.AddUpdateCMFaceColorFlags(false, true, false, false, false, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssEyeshadowGloss.onChange = delegate(float value)
			{
				base.makeup.eyeshadowGloss = value;
				base.chaCtrl.AddUpdateCMFaceGlossFlags(true, false, false, false, false);
				base.chaCtrl.CreateFaceTexture();
			};
			this.ssEyeshadowGloss.onSetDefaultValue = (() => base.defChaCtrl.custom.face.makeup.eyeshadowGloss);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004477 RID: 17527
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscEyeshadowType;

		// Token: 0x04004478 RID: 17528
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csEyeshadowColor;

		// Token: 0x04004479 RID: 17529
		[SerializeField]
		private CustomSliderSet ssEyeshadowGloss;
	}
}
