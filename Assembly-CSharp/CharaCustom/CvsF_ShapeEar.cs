using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E3 RID: 2531
	public class CvsF_ShapeEar : CvsBase
	{
		// Token: 0x06004A6B RID: 19051 RVA: 0x001C62C0 File Offset: 0x001C46C0
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x001C62EC File Offset: 0x001C46EC
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x001C6333 File Offset: 0x001C4733
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x001C6344 File Offset: 0x001C4744
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x001C6360 File Offset: 0x001C4760
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				54,
				55,
				56,
				57,
				58
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssEarSize,
				this.ssEarRotY,
				this.ssEarRotZ,
				this.ssEarUpForm,
				this.ssEarLowForm
			};
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x001C63C0 File Offset: 0x001C47C0
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeEar += this.UpdateCustomUI;
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				int idx = this.shapeIdx[i];
				this.ssShape[i].onChange = delegate(float value)
				{
					this.face.shapeValueFace[idx] = value;
					this.chaCtrl.SetShapeFaceValue(idx, value);
				};
				this.ssShape[i].onSetDefaultValue = (() => this.defChaCtrl.custom.face.shapeValueFace[idx]);
			}
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040044A8 RID: 17576
		[SerializeField]
		private CustomSliderSet ssEarSize;

		// Token: 0x040044A9 RID: 17577
		[SerializeField]
		private CustomSliderSet ssEarRotY;

		// Token: 0x040044AA RID: 17578
		[SerializeField]
		private CustomSliderSet ssEarRotZ;

		// Token: 0x040044AB RID: 17579
		[SerializeField]
		private CustomSliderSet ssEarUpForm;

		// Token: 0x040044AC RID: 17580
		[SerializeField]
		private CustomSliderSet ssEarLowForm;

		// Token: 0x040044AD RID: 17581
		private CustomSliderSet[] ssShape;

		// Token: 0x040044AE RID: 17582
		private int[] shapeIdx;
	}
}
