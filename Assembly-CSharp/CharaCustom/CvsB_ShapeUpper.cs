using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009F0 RID: 2544
	public class CvsB_ShapeUpper : CvsBase
	{
		// Token: 0x06004AEA RID: 19178 RVA: 0x001C9800 File Offset: 0x001C7C00
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x001C982C File Offset: 0x001C7C2C
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.body.shapeValueBody[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x001C9873 File Offset: 0x001C7C73
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001C9884 File Offset: 0x001C7C84
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.shapeValueBody[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x001C98A0 File Offset: 0x001C7CA0
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				10,
				11,
				12,
				13,
				14,
				15,
				16,
				17
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssNeckW,
				this.ssNeckZ,
				this.ssBodyShoulderW,
				this.ssBodyShoulderZ,
				this.ssBodyUpW,
				this.ssBodyUpZ,
				this.ssBodyLowW,
				this.ssBodyLowZ
			};
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x001C9918 File Offset: 0x001C7D18
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsBodyShapeUpper += this.UpdateCustomUI;
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

		// Token: 0x04004516 RID: 17686
		[SerializeField]
		private CustomSliderSet ssNeckW;

		// Token: 0x04004517 RID: 17687
		[SerializeField]
		private CustomSliderSet ssNeckZ;

		// Token: 0x04004518 RID: 17688
		[SerializeField]
		private CustomSliderSet ssBodyShoulderW;

		// Token: 0x04004519 RID: 17689
		[SerializeField]
		private CustomSliderSet ssBodyShoulderZ;

		// Token: 0x0400451A RID: 17690
		[SerializeField]
		private CustomSliderSet ssBodyUpW;

		// Token: 0x0400451B RID: 17691
		[SerializeField]
		private CustomSliderSet ssBodyUpZ;

		// Token: 0x0400451C RID: 17692
		[SerializeField]
		private CustomSliderSet ssBodyLowW;

		// Token: 0x0400451D RID: 17693
		[SerializeField]
		private CustomSliderSet ssBodyLowZ;

		// Token: 0x0400451E RID: 17694
		private CustomSliderSet[] ssShape;

		// Token: 0x0400451F RID: 17695
		private int[] shapeIdx;
	}
}
