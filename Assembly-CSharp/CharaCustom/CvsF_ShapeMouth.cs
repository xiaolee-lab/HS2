using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E6 RID: 2534
	public class CvsF_ShapeMouth : CvsBase
	{
		// Token: 0x06004A89 RID: 19081 RVA: 0x001C6F24 File Offset: 0x001C5324
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x001C6F50 File Offset: 0x001C5350
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x001C6F97 File Offset: 0x001C5397
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x001C6FA8 File Offset: 0x001C53A8
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x001C6FC4 File Offset: 0x001C53C4
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				47,
				48,
				49,
				50,
				51,
				52,
				53
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssMouthY,
				this.ssMouthW,
				this.ssMouthH,
				this.ssMouthZ,
				this.ssMouthUpForm,
				this.ssMouthLowForm,
				this.ssMouthCornerForm
			};
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x001C7034 File Offset: 0x001C5434
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeMouth += this.UpdateCustomUI;
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

		// Token: 0x040044C3 RID: 17603
		[SerializeField]
		private CustomSliderSet ssMouthY;

		// Token: 0x040044C4 RID: 17604
		[SerializeField]
		private CustomSliderSet ssMouthW;

		// Token: 0x040044C5 RID: 17605
		[SerializeField]
		private CustomSliderSet ssMouthH;

		// Token: 0x040044C6 RID: 17606
		[SerializeField]
		private CustomSliderSet ssMouthZ;

		// Token: 0x040044C7 RID: 17607
		[SerializeField]
		private CustomSliderSet ssMouthUpForm;

		// Token: 0x040044C8 RID: 17608
		[SerializeField]
		private CustomSliderSet ssMouthLowForm;

		// Token: 0x040044C9 RID: 17609
		[SerializeField]
		private CustomSliderSet ssMouthCornerForm;

		// Token: 0x040044CA RID: 17610
		private CustomSliderSet[] ssShape;

		// Token: 0x040044CB RID: 17611
		private int[] shapeIdx;
	}
}
