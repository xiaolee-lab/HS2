using System;
using UnityEngine;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000645 RID: 1605
	[RequireComponent(typeof(RectTransform))]
	public class BarViewSizeAnchors : ProgressBarProView
	{
		// Token: 0x06002621 RID: 9761 RVA: 0x000D8DC5 File Offset: 0x000D71C5
		public override bool CanUpdateView(float currentValue, float targetValue)
		{
			return base.isActiveAndEnabled || this.isDisplaySizeZero;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000D8DDC File Offset: 0x000D71DC
		public override void UpdateView(float currentValue, float targetValue)
		{
			if (this.hideOnEmpty && currentValue <= 0f)
			{
				this.rectTrans.gameObject.SetActive(false);
				this.isDisplaySizeZero = true;
				return;
			}
			this.isDisplaySizeZero = false;
			this.rectTrans.gameObject.SetActive(true);
			this.SetPivot(0f, currentValue);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000D8E3C File Offset: 0x000D723C
		protected void SetPivot(float startEdge, float endEdge)
		{
			if (this.useDiscreteSteps)
			{
				startEdge = Mathf.Round(startEdge * (float)this.numSteps) / (float)this.numSteps;
				endEdge = Mathf.Round(endEdge * (float)this.numSteps) / (float)this.numSteps;
			}
			this.UpdateTracker();
			switch (this.fillType)
			{
			case BarViewSizeAnchors.FillType.LeftToRight:
				this.rectTrans.anchorMin = new Vector2(startEdge, this.rectTrans.anchorMin.y);
				this.rectTrans.anchorMax = new Vector2(endEdge, this.rectTrans.anchorMax.y);
				break;
			case BarViewSizeAnchors.FillType.RightToLeft:
				this.rectTrans.anchorMin = new Vector2(1f - endEdge, this.rectTrans.anchorMin.y);
				this.rectTrans.anchorMax = new Vector2(1f - startEdge, this.rectTrans.anchorMax.y);
				break;
			case BarViewSizeAnchors.FillType.TopToBottom:
				this.rectTrans.anchorMin = new Vector2(this.rectTrans.anchorMin.x, 1f - endEdge);
				this.rectTrans.anchorMax = new Vector2(this.rectTrans.anchorMax.x, 1f - startEdge);
				break;
			case BarViewSizeAnchors.FillType.BottomToTop:
				this.rectTrans.anchorMin = new Vector2(this.rectTrans.anchorMin.x, startEdge);
				this.rectTrans.anchorMax = new Vector2(this.rectTrans.anchorMax.x, endEdge);
				break;
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000D9000 File Offset: 0x000D7400
		private void UpdateTracker()
		{
			if (this.fillType == BarViewSizeAnchors.FillType.LeftToRight || this.fillType == BarViewSizeAnchors.FillType.RightToLeft)
			{
				this.m_Tracker.Add(this, this.rectTrans, DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX);
			}
			else
			{
				this.m_Tracker.Add(this, this.rectTrans, DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY);
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000D9057 File Offset: 0x000D7457
		private void OnDisable()
		{
			this.m_Tracker.Clear();
		}

		// Token: 0x040025D4 RID: 9684
		[SerializeField]
		protected RectTransform rectTrans;

		// Token: 0x040025D5 RID: 9685
		[SerializeField]
		protected BarViewSizeAnchors.FillType fillType;

		// Token: 0x040025D6 RID: 9686
		[SerializeField]
		private bool hideOnEmpty = true;

		// Token: 0x040025D7 RID: 9687
		[SerializeField]
		private bool useDiscreteSteps;

		// Token: 0x040025D8 RID: 9688
		[SerializeField]
		private int numSteps = 10;

		// Token: 0x040025D9 RID: 9689
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x040025DA RID: 9690
		protected bool isDisplaySizeZero;

		// Token: 0x02000646 RID: 1606
		public enum FillType
		{
			// Token: 0x040025DC RID: 9692
			LeftToRight,
			// Token: 0x040025DD RID: 9693
			RightToLeft,
			// Token: 0x040025DE RID: 9694
			TopToBottom,
			// Token: 0x040025DF RID: 9695
			BottomToTop
		}
	}
}
