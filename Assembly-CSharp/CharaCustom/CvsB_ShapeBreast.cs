using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009ED RID: 2541
	public class CvsB_ShapeBreast : CvsBase
	{
		// Token: 0x06004ACF RID: 19151 RVA: 0x001C8D04 File Offset: 0x001C7104
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x001C8D30 File Offset: 0x001C7130
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.body.shapeValueBody[this.shapeIdx[i]]);
			}
			this.ssBustSoftness.SetSliderValue(base.body.bustSoftness);
			this.ssBustWeight.SetSliderValue(base.body.bustWeight);
			this.ssAreolaSize.SetSliderValue(base.body.areolaSize);
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x001C8DB9 File Offset: 0x001C71B9
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x001C8DC8 File Offset: 0x001C71C8
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.shapeValueBody[this.shapeIdx[i]]));
			}
			this.ssBustSoftness.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.bustSoftness));
			this.ssBustWeight.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.bustWeight));
			this.ssAreolaSize.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.areolaSize));
			yield break;
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x001C8DE4 File Offset: 0x001C71E4
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				32
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssBustSize,
				this.ssBustY,
				this.ssBustRotX,
				this.ssBustX,
				this.ssBustRotY,
				this.ssBustSharp,
				this.ssAreolaBulge,
				this.ssNipWeight,
				this.ssNipStand
			};
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x001C8E68 File Offset: 0x001C7268
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsBodyShapeBreast += this.UpdateCustomUI;
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
			this.ssBustSoftness.onChange = delegate(float value)
			{
				base.body.bustSoftness = value;
				base.chaCtrl.UpdateBustSoftness();
			};
			this.ssBustSoftness.onSetDefaultValue = (() => base.defChaCtrl.custom.body.bustSoftness);
			this.ssBustWeight.onChange = delegate(float value)
			{
				base.body.bustWeight = value;
				base.chaCtrl.UpdateBustGravity();
			};
			this.ssBustWeight.onSetDefaultValue = (() => base.defChaCtrl.custom.body.bustWeight);
			this.ssAreolaSize.onChange = delegate(float value)
			{
				base.body.areolaSize = value;
				base.chaCtrl.ChangeNipScale();
			};
			this.ssAreolaSize.onSetDefaultValue = (() => base.defChaCtrl.custom.body.areolaSize);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040044F9 RID: 17657
		[SerializeField]
		private CustomSliderSet ssBustSize;

		// Token: 0x040044FA RID: 17658
		[SerializeField]
		private CustomSliderSet ssBustY;

		// Token: 0x040044FB RID: 17659
		[SerializeField]
		private CustomSliderSet ssBustRotX;

		// Token: 0x040044FC RID: 17660
		[SerializeField]
		private CustomSliderSet ssBustX;

		// Token: 0x040044FD RID: 17661
		[SerializeField]
		private CustomSliderSet ssBustRotY;

		// Token: 0x040044FE RID: 17662
		[SerializeField]
		private CustomSliderSet ssBustSharp;

		// Token: 0x040044FF RID: 17663
		[SerializeField]
		private CustomSliderSet ssAreolaBulge;

		// Token: 0x04004500 RID: 17664
		[SerializeField]
		private CustomSliderSet ssNipWeight;

		// Token: 0x04004501 RID: 17665
		[SerializeField]
		private CustomSliderSet ssNipStand;

		// Token: 0x04004502 RID: 17666
		[SerializeField]
		private CustomSliderSet ssBustSoftness;

		// Token: 0x04004503 RID: 17667
		[SerializeField]
		private CustomSliderSet ssBustWeight;

		// Token: 0x04004504 RID: 17668
		[SerializeField]
		private CustomSliderSet ssAreolaSize;

		// Token: 0x04004505 RID: 17669
		private CustomSliderSet[] ssShape;

		// Token: 0x04004506 RID: 17670
		private int[] shapeIdx;
	}
}
