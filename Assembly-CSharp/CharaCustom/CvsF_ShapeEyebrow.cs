using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E4 RID: 2532
	public class CvsF_ShapeEyebrow : CvsBase
	{
		// Token: 0x06004A72 RID: 19058 RVA: 0x001C65C0 File Offset: 0x001C49C0
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x001C65EC File Offset: 0x001C49EC
		private void CalculateUI()
		{
			this.ssEyebrowW.SetSliderValue(base.face.eyebrowLayout.z);
			this.ssEyebrowH.SetSliderValue(base.face.eyebrowLayout.w);
			this.ssEyebrowX.SetSliderValue(base.face.eyebrowLayout.x);
			this.ssEyebrowY.SetSliderValue(base.face.eyebrowLayout.y);
			this.ssEyebrowTilt.SetSliderValue(base.face.eyebrowTilt);
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x001C6687 File Offset: 0x001C4A87
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x001C6698 File Offset: 0x001C4A98
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssEyebrowW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.eyebrowLayout.z));
			this.ssEyebrowH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.eyebrowLayout.w));
			this.ssEyebrowX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.eyebrowLayout.x));
			this.ssEyebrowY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.eyebrowLayout.y));
			this.ssEyebrowTilt.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.eyebrowTilt));
			yield break;
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x001C66B4 File Offset: 0x001C4AB4
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeEyebrow += this.UpdateCustomUI;
			this.ssEyebrowW.onChange = delegate(float value)
			{
				base.face.eyebrowLayout = new Vector4(base.face.eyebrowLayout.x, base.face.eyebrowLayout.y, value, base.face.eyebrowLayout.w);
				base.chaCtrl.ChangeEyebrowLayout();
			};
			this.ssEyebrowW.onSetDefaultValue = (() => base.defChaCtrl.custom.face.eyebrowLayout.z);
			this.ssEyebrowH.onChange = delegate(float value)
			{
				base.face.eyebrowLayout = new Vector4(base.face.eyebrowLayout.x, base.face.eyebrowLayout.y, base.face.eyebrowLayout.z, value);
				base.chaCtrl.ChangeEyebrowLayout();
			};
			this.ssEyebrowH.onSetDefaultValue = (() => base.defChaCtrl.custom.face.eyebrowLayout.w);
			this.ssEyebrowX.onChange = delegate(float value)
			{
				base.face.eyebrowLayout = new Vector4(value, base.face.eyebrowLayout.y, base.face.eyebrowLayout.z, base.face.eyebrowLayout.w);
				base.chaCtrl.ChangeEyebrowLayout();
			};
			this.ssEyebrowX.onSetDefaultValue = (() => base.defChaCtrl.custom.face.eyebrowLayout.x);
			this.ssEyebrowY.onChange = delegate(float value)
			{
				base.face.eyebrowLayout = new Vector4(base.face.eyebrowLayout.x, value, base.face.eyebrowLayout.z, base.face.eyebrowLayout.w);
				base.chaCtrl.ChangeEyebrowLayout();
			};
			this.ssEyebrowY.onSetDefaultValue = (() => base.defChaCtrl.custom.face.eyebrowLayout.y);
			this.ssEyebrowTilt.onChange = delegate(float value)
			{
				base.face.eyebrowTilt = value;
				base.chaCtrl.ChangeEyebrowTilt();
			};
			this.ssEyebrowTilt.onSetDefaultValue = (() => base.defChaCtrl.custom.face.eyebrowTilt);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040044AF RID: 17583
		[SerializeField]
		private CustomSliderSet ssEyebrowW;

		// Token: 0x040044B0 RID: 17584
		[SerializeField]
		private CustomSliderSet ssEyebrowH;

		// Token: 0x040044B1 RID: 17585
		[SerializeField]
		private CustomSliderSet ssEyebrowX;

		// Token: 0x040044B2 RID: 17586
		[SerializeField]
		private CustomSliderSet ssEyebrowY;

		// Token: 0x040044B3 RID: 17587
		[SerializeField]
		private CustomSliderSet ssEyebrowTilt;
	}
}
