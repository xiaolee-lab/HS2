using System;
using System.Collections;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009EC RID: 2540
	public class CvsB_ShapeArm : CvsBase
	{
		// Token: 0x06004AC8 RID: 19144 RVA: 0x001C8A25 File Offset: 0x001C6E25
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x001C8A50 File Offset: 0x001C6E50
		private void CalculateUI()
		{
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetSliderValue(base.body.shapeValueBody[this.shapeIdx[i]]);
			}
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x001C8A97 File Offset: 0x001C6E97
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x001C8AA8 File Offset: 0x001C6EA8
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			for (int i = 0; i < this.ssShape.Length; i++)
			{
				this.ssShape[i].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.body.shapeValueBody[this.shapeIdx[i]]));
			}
			yield break;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x001C8AC3 File Offset: 0x001C6EC3
		public void Awake()
		{
			this.shapeIdx = new int[]
			{
				29,
				30,
				31
			};
			this.ssShape = new CustomSliderSet[]
			{
				this.ssShoulder,
				this.ssArmUp,
				this.ssArmLow
			};
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x001C8B04 File Offset: 0x001C6F04
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

		// Token: 0x040044F4 RID: 17652
		[SerializeField]
		private CustomSliderSet ssShoulder;

		// Token: 0x040044F5 RID: 17653
		[SerializeField]
		private CustomSliderSet ssArmUp;

		// Token: 0x040044F6 RID: 17654
		[SerializeField]
		private CustomSliderSet ssArmLow;

		// Token: 0x040044F7 RID: 17655
		private CustomSliderSet[] ssShape;

		// Token: 0x040044F8 RID: 17656
		private int[] shapeIdx;
	}
}
