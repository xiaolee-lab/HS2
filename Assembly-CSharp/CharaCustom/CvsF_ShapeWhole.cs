using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E8 RID: 2536
	public class CvsF_ShapeWhole : CvsBase
	{
		// Token: 0x06004A97 RID: 19095 RVA: 0x001C7594 File Offset: 0x001C5994
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x001C75C0 File Offset: 0x001C59C0
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x001C7607 File Offset: 0x001C5A07
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x001C7618 File Offset: 0x001C5A18
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x001C7634 File Offset: 0x001C5A34
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				0,
				1,
				2,
				3,
				4
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssFaceBaseW,
				this.ssFaceUpZ,
				this.ssFaceUpY,
				this.ssFaceLowZ,
				this.ssFaceLowW
			};
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x001C7694 File Offset: 0x001C5A94
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeWhole += this.UpdateCustomUI;
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

		// Token: 0x040044DD RID: 17629
		[SerializeField]
		private CustomSliderSet ssFaceBaseW;

		// Token: 0x040044DE RID: 17630
		[SerializeField]
		private CustomSliderSet ssFaceUpZ;

		// Token: 0x040044DF RID: 17631
		[SerializeField]
		private CustomSliderSet ssFaceUpY;

		// Token: 0x040044E0 RID: 17632
		[SerializeField]
		private CustomSliderSet ssFaceLowZ;

		// Token: 0x040044E1 RID: 17633
		[SerializeField]
		private CustomSliderSet ssFaceLowW;

		// Token: 0x040044E2 RID: 17634
		private CustomSliderSet[] ssShape;

		// Token: 0x040044E3 RID: 17635
		private int[] shapeIdx;
	}
}
