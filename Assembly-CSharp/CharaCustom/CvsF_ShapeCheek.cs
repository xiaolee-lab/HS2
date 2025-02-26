using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E1 RID: 2529
	public class CvsF_ShapeCheek : CvsBase
	{
		// Token: 0x06004A5D RID: 19037 RVA: 0x001C5CA2 File Offset: 0x001C40A2
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x001C5CCC File Offset: 0x001C40CC
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x001C5D13 File Offset: 0x001C4113
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x001C5D24 File Offset: 0x001C4124
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x001C5D40 File Offset: 0x001C4140
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				13,
				14,
				15,
				16,
				17,
				18
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssCheekLowY,
				this.ssCheekLowZ,
				this.ssCheekLowW,
				this.ssCheekUpY,
				this.ssCheekUpZ,
				this.ssCheekUpW
			};
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x001C5DA8 File Offset: 0x001C41A8
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeCheek += this.UpdateCustomUI;
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

		// Token: 0x04004496 RID: 17558
		[SerializeField]
		private CustomSliderSet ssCheekLowY;

		// Token: 0x04004497 RID: 17559
		[SerializeField]
		private CustomSliderSet ssCheekLowZ;

		// Token: 0x04004498 RID: 17560
		[SerializeField]
		private CustomSliderSet ssCheekLowW;

		// Token: 0x04004499 RID: 17561
		[SerializeField]
		private CustomSliderSet ssCheekUpY;

		// Token: 0x0400449A RID: 17562
		[SerializeField]
		private CustomSliderSet ssCheekUpZ;

		// Token: 0x0400449B RID: 17563
		[SerializeField]
		private CustomSliderSet ssCheekUpW;

		// Token: 0x0400449C RID: 17564
		private CustomSliderSet[] ssShape;

		// Token: 0x0400449D RID: 17565
		private int[] shapeIdx;
	}
}
