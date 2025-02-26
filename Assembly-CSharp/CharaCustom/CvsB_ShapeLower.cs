using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009EF RID: 2543
	public class CvsB_ShapeLower : CvsBase
	{
		// Token: 0x06004AE3 RID: 19171 RVA: 0x001C94F0 File Offset: 0x001C78F0
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x001C951C File Offset: 0x001C791C
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.body.shapeValueBody[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x001C9563 File Offset: 0x001C7963
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x001C9574 File Offset: 0x001C7974
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.shapeValueBody[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x001C9590 File Offset: 0x001C7990
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				18,
				19,
				20,
				21,
				22,
				23,
				24
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssWaistY,
				this.ssWaistUpW,
				this.ssWaistUpZ,
				this.ssWaistLowW,
				this.ssWaistLowZ,
				this.ssHip,
				this.ssHipRotX
			};
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x001C9600 File Offset: 0x001C7A00
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

		// Token: 0x0400450D RID: 17677
		[SerializeField]
		private CustomSliderSet ssWaistY;

		// Token: 0x0400450E RID: 17678
		[SerializeField]
		private CustomSliderSet ssWaistUpW;

		// Token: 0x0400450F RID: 17679
		[SerializeField]
		private CustomSliderSet ssWaistUpZ;

		// Token: 0x04004510 RID: 17680
		[SerializeField]
		private CustomSliderSet ssWaistLowW;

		// Token: 0x04004511 RID: 17681
		[SerializeField]
		private CustomSliderSet ssWaistLowZ;

		// Token: 0x04004512 RID: 17682
		[SerializeField]
		private CustomSliderSet ssHip;

		// Token: 0x04004513 RID: 17683
		[SerializeField]
		private CustomSliderSet ssHipRotX;

		// Token: 0x04004514 RID: 17684
		private CustomSliderSet[] ssShape;

		// Token: 0x04004515 RID: 17685
		private int[] shapeIdx;
	}
}
