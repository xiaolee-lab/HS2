using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E5 RID: 2533
	public class CvsF_ShapeEyes : CvsBase
	{
		// Token: 0x06004A82 RID: 19074 RVA: 0x001C6BDA File Offset: 0x001C4FDA
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x001C6C04 File Offset: 0x001C5004
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x001C6C4B File Offset: 0x001C504B
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x001C6C5C File Offset: 0x001C505C
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x001C6C78 File Offset: 0x001C5078
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				19,
				20,
				21,
				22,
				23,
				24,
				25,
				26,
				27,
				28,
				29,
				30,
				31
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssEyeY,
				this.ssEyeX,
				this.ssEyeZ,
				this.ssEyeW,
				this.ssEyeH,
				this.ssEyeRotZ,
				this.ssEyeRotY,
				this.ssEyeInX,
				this.ssEyeOutX,
				this.ssEyeInY,
				this.ssEyeOutY,
				this.ssEyelidForm01,
				this.ssEyelidForm02
			};
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x001C6D24 File Offset: 0x001C5124
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeEyes += this.UpdateCustomUI;
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

		// Token: 0x040044B4 RID: 17588
		[SerializeField]
		private CustomSliderSet ssEyeY;

		// Token: 0x040044B5 RID: 17589
		[SerializeField]
		private CustomSliderSet ssEyeX;

		// Token: 0x040044B6 RID: 17590
		[SerializeField]
		private CustomSliderSet ssEyeZ;

		// Token: 0x040044B7 RID: 17591
		[SerializeField]
		private CustomSliderSet ssEyeW;

		// Token: 0x040044B8 RID: 17592
		[SerializeField]
		private CustomSliderSet ssEyeH;

		// Token: 0x040044B9 RID: 17593
		[SerializeField]
		private CustomSliderSet ssEyeRotZ;

		// Token: 0x040044BA RID: 17594
		[SerializeField]
		private CustomSliderSet ssEyeRotY;

		// Token: 0x040044BB RID: 17595
		[SerializeField]
		private CustomSliderSet ssEyeInX;

		// Token: 0x040044BC RID: 17596
		[SerializeField]
		private CustomSliderSet ssEyeOutX;

		// Token: 0x040044BD RID: 17597
		[SerializeField]
		private CustomSliderSet ssEyeInY;

		// Token: 0x040044BE RID: 17598
		[SerializeField]
		private CustomSliderSet ssEyeOutY;

		// Token: 0x040044BF RID: 17599
		[SerializeField]
		private CustomSliderSet ssEyelidForm01;

		// Token: 0x040044C0 RID: 17600
		[SerializeField]
		private CustomSliderSet ssEyelidForm02;

		// Token: 0x040044C1 RID: 17601
		private CustomSliderSet[] ssShape;

		// Token: 0x040044C2 RID: 17602
		private int[] shapeIdx;
	}
}
