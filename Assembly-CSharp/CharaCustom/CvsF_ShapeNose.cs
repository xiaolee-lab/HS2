using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E7 RID: 2535
	public class CvsF_ShapeNose : CvsBase
	{
		// Token: 0x06004A90 RID: 19088 RVA: 0x001C7234 File Offset: 0x001C5634
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x001C7260 File Offset: 0x001C5660
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x001C72A7 File Offset: 0x001C56A7
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x001C72B8 File Offset: 0x001C56B8
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x001C72D4 File Offset: 0x001C56D4
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				32,
				33,
				34,
				35,
				36,
				37,
				38,
				39,
				40,
				41,
				42,
				43,
				44,
				45,
				46
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssNoseAllY,
				this.ssNoseAllZ,
				this.ssNoseAllRotX,
				this.ssNoseAllW,
				this.ssNoseBridgeH,
				this.ssNoseBridgeW,
				this.ssNoseBridgeForm,
				this.ssNoseWingW,
				this.ssNoseWingY,
				this.ssNoseWingZ,
				this.ssNoseWingRotX,
				this.ssNoseWingRotZ,
				this.ssNoseH,
				this.ssNoseRotX,
				this.ssNoseSize
			};
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x001C7394 File Offset: 0x001C5794
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeNose += this.UpdateCustomUI;
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

		// Token: 0x040044CC RID: 17612
		[SerializeField]
		private CustomSliderSet ssNoseAllY;

		// Token: 0x040044CD RID: 17613
		[SerializeField]
		private CustomSliderSet ssNoseAllZ;

		// Token: 0x040044CE RID: 17614
		[SerializeField]
		private CustomSliderSet ssNoseAllRotX;

		// Token: 0x040044CF RID: 17615
		[SerializeField]
		private CustomSliderSet ssNoseAllW;

		// Token: 0x040044D0 RID: 17616
		[SerializeField]
		private CustomSliderSet ssNoseBridgeH;

		// Token: 0x040044D1 RID: 17617
		[SerializeField]
		private CustomSliderSet ssNoseBridgeW;

		// Token: 0x040044D2 RID: 17618
		[SerializeField]
		private CustomSliderSet ssNoseBridgeForm;

		// Token: 0x040044D3 RID: 17619
		[SerializeField]
		private CustomSliderSet ssNoseWingW;

		// Token: 0x040044D4 RID: 17620
		[SerializeField]
		private CustomSliderSet ssNoseWingY;

		// Token: 0x040044D5 RID: 17621
		[SerializeField]
		private CustomSliderSet ssNoseWingZ;

		// Token: 0x040044D6 RID: 17622
		[SerializeField]
		private CustomSliderSet ssNoseWingRotX;

		// Token: 0x040044D7 RID: 17623
		[SerializeField]
		private CustomSliderSet ssNoseWingRotZ;

		// Token: 0x040044D8 RID: 17624
		[SerializeField]
		private CustomSliderSet ssNoseH;

		// Token: 0x040044D9 RID: 17625
		[SerializeField]
		private CustomSliderSet ssNoseRotX;

		// Token: 0x040044DA RID: 17626
		[SerializeField]
		private CustomSliderSet ssNoseSize;

		// Token: 0x040044DB RID: 17627
		private CustomSliderSet[] ssShape;

		// Token: 0x040044DC RID: 17628
		private int[] shapeIdx;
	}
}
