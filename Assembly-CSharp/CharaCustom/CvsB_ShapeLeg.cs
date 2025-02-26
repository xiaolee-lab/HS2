using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009EE RID: 2542
	public class CvsB_ShapeLeg : CvsBase
	{
		// Token: 0x06004ADC RID: 19164 RVA: 0x001C91FC File Offset: 0x001C75FC
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001C9228 File Offset: 0x001C7628
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.body.shapeValueBody[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x001C926F File Offset: 0x001C766F
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x001C9280 File Offset: 0x001C7680
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.shapeValueBody[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x001C929C File Offset: 0x001C769C
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				25,
				26,
				27,
				28
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssThighUp,
				this.ssThighLow,
				this.ssCalf,
				this.ssAnkle
			};
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x001C92F0 File Offset: 0x001C76F0
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsBodyShapeWhole += this.UpdateCustomUI;
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				int idx = this.shapeIdx[i];
				this.ssShape[i].onChange = delegate(float value)
				{
					this.body.shapeValueBody[idx] = value;
					this.chaCtrl.SetShapeBodyValue(idx, value);
				};
				this.ssShape[i].onSetDefaultValue = (() => this.defChaCtrl.custom.body.shapeValueBody[idx]);
			}
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x04004507 RID: 17671
		[SerializeField]
		private CustomSliderSet ssThighUp;

		// Token: 0x04004508 RID: 17672
		[SerializeField]
		private CustomSliderSet ssThighLow;

		// Token: 0x04004509 RID: 17673
		[SerializeField]
		private CustomSliderSet ssCalf;

		// Token: 0x0400450A RID: 17674
		[SerializeField]
		private CustomSliderSet ssAnkle;

		// Token: 0x0400450B RID: 17675
		private CustomSliderSet[] ssShape;

		// Token: 0x0400450C RID: 17676
		private int[] shapeIdx;
	}
}
