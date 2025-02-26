using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009E2 RID: 2530
	public class CvsF_ShapeChin : CvsBase
	{
		// Token: 0x06004A64 RID: 19044 RVA: 0x001C5FA8 File Offset: 0x001C43A8
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x001C5FD4 File Offset: 0x001C43D4
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.face.shapeValueFace[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x001C601B File Offset: 0x001C441B
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x001C602C File Offset: 0x001C442C
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.shapeValueFace[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x001C6048 File Offset: 0x001C4448
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				12
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssChinW,
				this.ssChinY,
				this.ssChinZ,
				this.ssChinRot,
				this.ssChinLowY,
				this.ssChinTipW,
				this.ssChinTipY,
				this.ssChinTipZ
			};
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x001C60C0 File Offset: 0x001C44C0
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceShapeChin += this.UpdateCustomUI;
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

		// Token: 0x0400449E RID: 17566
		[SerializeField]
		private CustomSliderSet ssChinW;

		// Token: 0x0400449F RID: 17567
		[SerializeField]
		private CustomSliderSet ssChinY;

		// Token: 0x040044A0 RID: 17568
		[SerializeField]
		private CustomSliderSet ssChinZ;

		// Token: 0x040044A1 RID: 17569
		[SerializeField]
		private CustomSliderSet ssChinRot;

		// Token: 0x040044A2 RID: 17570
		[SerializeField]
		private CustomSliderSet ssChinLowY;

		// Token: 0x040044A3 RID: 17571
		[SerializeField]
		private CustomSliderSet ssChinTipW;

		// Token: 0x040044A4 RID: 17572
		[SerializeField]
		private CustomSliderSet ssChinTipY;

		// Token: 0x040044A5 RID: 17573
		[SerializeField]
		private CustomSliderSet ssChinTipZ;

		// Token: 0x040044A6 RID: 17574
		private CustomSliderSet[] ssShape;

		// Token: 0x040044A7 RID: 17575
		private int[] shapeIdx;
	}
}
