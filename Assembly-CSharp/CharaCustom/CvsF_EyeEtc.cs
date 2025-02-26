using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009D7 RID: 2519
	public class CvsF_EyeEtc : CvsBase
	{
		// Token: 0x060049C9 RID: 18889 RVA: 0x001C1ED9 File Offset: 0x001C02D9
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x001C1F03 File Offset: 0x001C0303
		private void CalculateUI()
		{
			this.ssPupilY.SetSliderValue(base.face.pupilY);
			this.ssShadowScale.SetSliderValue(base.face.whiteShadowScale);
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x001C1F31 File Offset: 0x001C0331
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x001C1F40 File Offset: 0x001C0340
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssPupilY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.pupilY));
			this.ssShadowScale.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.whiteShadowScale));
			yield break;
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x001C1F5C File Offset: 0x001C035C
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsEyeEtc += this.UpdateCustomUI;
			this.ssPupilY.onChange = delegate(float value)
			{
				base.face.pupilY = value;
				base.chaCtrl.ChangeEyesBasePosY();
			};
			this.ssPupilY.onSetDefaultValue = (() => base.defChaCtrl.custom.face.pupilY);
			this.ssShadowScale.onChange = delegate(float value)
			{
				base.face.whiteShadowScale = value;
				base.chaCtrl.ChangeEyesShadowRange();
			};
			this.ssShadowScale.onSetDefaultValue = (() => base.defChaCtrl.custom.face.whiteShadowScale);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400445A RID: 17498
		[SerializeField]
		private CustomSliderSet ssPupilY;

		// Token: 0x0400445B RID: 17499
		[SerializeField]
		private CustomSliderSet ssShadowScale;
	}
}
