using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009DA RID: 2522
	public class CvsF_EyeLR : CvsBase
	{
		// Token: 0x060049ED RID: 18925 RVA: 0x001C2A61 File Offset: 0x001C0E61
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x001C2A8C File Offset: 0x001C0E8C
		private void CalculateUI()
		{
			this.ssPupilEmission.SetSliderValue(base.face.pupil[base.SNo].pupilEmission);
			this.ssPupilW.SetSliderValue(base.face.pupil[base.SNo].pupilW);
			this.ssPupilH.SetSliderValue(base.face.pupil[base.SNo].pupilH);
			this.ssBlackW.SetSliderValue(base.face.pupil[base.SNo].blackW);
			this.ssBlackH.SetSliderValue(base.face.pupil[base.SNo].blackH);
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x001C2B44 File Offset: 0x001C0F44
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.sscPupilType.SetToggleID(base.face.pupil[base.SNo].pupilId);
			this.sscBlackType.SetToggleID(base.face.pupil[base.SNo].blackId);
			this.csWhiteColor.SetColor(base.face.pupil[base.SNo].whiteColor);
			this.csPupilColor.SetColor(base.face.pupil[base.SNo].pupilColor);
			this.csBlackColor.SetColor(base.face.pupil[base.SNo].blackColor);
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x001C2C08 File Offset: 0x001C1008
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssPupilEmission.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.pupil[base.SNo].pupilEmission));
			this.ssPupilW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.pupil[base.SNo].pupilW));
			this.ssPupilH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.pupil[base.SNo].pupilH));
			this.ssBlackW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.pupil[base.SNo].blackW));
			this.ssBlackH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.pupil[base.SNo].blackH));
			yield break;
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x001C2C24 File Offset: 0x001C1024
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsEyeLR += this.UpdateCustomUI;
			this.csWhiteColor.actUpdateColor = delegate(Color color)
			{
				base.face.pupil[base.SNo].whiteColor = color;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].whiteColor = color;
				}
				base.chaCtrl.ChangeWhiteEyesColor((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_eye, ChaListDefine.KeyType.Unknown);
			this.sscPupilType.CreateList(lst);
			this.sscPupilType.SetToggleID(base.face.pupil[base.SNo].pupilId);
			this.sscPupilType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.pupil[base.SNo].pupilId != info.id)
				{
					base.face.pupil[base.SNo].pupilId = info.id;
					if (base.face.pupilSameSetting)
					{
						base.face.pupil[base.SNo ^ 1].pupilId = info.id;
					}
					base.chaCtrl.ChangeEyesKind((!base.face.pupilSameSetting) ? base.SNo : 2);
				}
			};
			this.csPupilColor.actUpdateColor = delegate(Color color)
			{
				base.face.pupil[base.SNo].pupilColor = color;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].pupilColor = color;
				}
				base.chaCtrl.ChangeEyesColor((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssPupilEmission.onChange = delegate(float value)
			{
				base.face.pupil[base.SNo].pupilEmission = value;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].pupilEmission = value;
				}
				base.chaCtrl.ChangeEyesEmission((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssPupilEmission.onSetDefaultValue = (() => base.defChaCtrl.custom.face.pupil[base.SNo].pupilEmission);
			this.ssPupilW.onChange = delegate(float value)
			{
				base.face.pupil[base.SNo].pupilW = value;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].pupilW = value;
				}
				base.chaCtrl.ChangeEyesWH((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssPupilW.onSetDefaultValue = (() => base.defChaCtrl.custom.face.pupil[base.SNo].pupilW);
			this.ssPupilH.onChange = delegate(float value)
			{
				base.face.pupil[base.SNo].pupilH = value;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].pupilH = value;
				}
				base.chaCtrl.ChangeEyesWH((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssPupilH.onSetDefaultValue = (() => base.defChaCtrl.custom.face.pupil[base.SNo].pupilH);
			List<CustomSelectInfo> lst2 = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_eyeblack, ChaListDefine.KeyType.Unknown);
			this.sscBlackType.CreateList(lst2);
			this.sscBlackType.SetToggleID(base.face.pupil[base.SNo].blackId);
			this.sscBlackType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.pupil[base.SNo].blackId != info.id)
				{
					base.face.pupil[base.SNo].blackId = info.id;
					if (base.face.pupilSameSetting)
					{
						base.face.pupil[base.SNo ^ 1].blackId = info.id;
					}
					base.chaCtrl.ChangeBlackEyesKind((!base.face.pupilSameSetting) ? base.SNo : 2);
				}
			};
			this.csBlackColor.actUpdateColor = delegate(Color color)
			{
				base.face.pupil[base.SNo].blackColor = color;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].blackColor = color;
				}
				base.chaCtrl.ChangeBlackEyesColor((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssBlackW.onChange = delegate(float value)
			{
				base.face.pupil[base.SNo].blackW = value;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].blackW = value;
				}
				base.chaCtrl.ChangeBlackEyesWH((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssBlackW.onSetDefaultValue = (() => base.defChaCtrl.custom.face.pupil[base.SNo].blackW);
			this.ssBlackH.onChange = delegate(float value)
			{
				base.face.pupil[base.SNo].blackH = value;
				if (base.face.pupilSameSetting)
				{
					base.face.pupil[base.SNo ^ 1].blackH = value;
				}
				base.chaCtrl.ChangeBlackEyesWH((!base.face.pupilSameSetting) ? base.SNo : 2);
			};
			this.ssBlackH.onSetDefaultValue = (() => base.defChaCtrl.custom.face.pupil[base.SNo].blackH);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004465 RID: 17509
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomColorSet csWhiteColor;

		// Token: 0x04004466 RID: 17510
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscPupilType;

		// Token: 0x04004467 RID: 17511
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomColorSet csPupilColor;

		// Token: 0x04004468 RID: 17512
		[SerializeField]
		private CustomSliderSet ssPupilEmission;

		// Token: 0x04004469 RID: 17513
		[SerializeField]
		private CustomSliderSet ssPupilW;

		// Token: 0x0400446A RID: 17514
		[SerializeField]
		private CustomSliderSet ssPupilH;

		// Token: 0x0400446B RID: 17515
		[Header("【設定04】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscBlackType;

		// Token: 0x0400446C RID: 17516
		[Header("【設定05】----------------------")]
		[SerializeField]
		private CustomColorSet csBlackColor;

		// Token: 0x0400446D RID: 17517
		[SerializeField]
		private CustomSliderSet ssBlackW;

		// Token: 0x0400446E RID: 17518
		[SerializeField]
		private CustomSliderSet ssBlackH;
	}
}
