using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009F1 RID: 2545
	public class CvsB_ShapeWhole : CvsBase
	{
		// Token: 0x06004AF1 RID: 19185 RVA: 0x001C9B18 File Offset: 0x001C7F18
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x001C9B44 File Offset: 0x001C7F44
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.body.shapeValueBody[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x001C9B8B File Offset: 0x001C7F8B
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001C9B9C File Offset: 0x001C7F9C
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.shapeValueBody[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x001C9BB7 File Offset: 0x001C7FB7
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				0,
				9
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssHeight,
				this.ssHeadSize
			};
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x001C9BE8 File Offset: 0x001C7FE8
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

		// Token: 0x04004520 RID: 17696
		[SerializeField]
		private CustomSliderSet ssHeight;

		// Token: 0x04004521 RID: 17697
		[SerializeField]
		private CustomSliderSet ssHeadSize;

		// Token: 0x04004522 RID: 17698
		private CustomSliderSet[] ssShape;

		// Token: 0x04004523 RID: 17699
		private int[] shapeIdx;
	}
}
