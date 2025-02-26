using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000644 RID: 1604
	[RequireComponent(typeof(RectTransform))]
	public class BarViewPosImageFill : ProgressBarProView
	{
		// Token: 0x0600261A RID: 9754 RVA: 0x000D907E File Offset: 0x000D747E
		public override void UpdateView(float currentValue, float targetValue)
		{
			this.rectTrans.anchorMin = this.GetAnchor(currentValue);
			this.rectTrans.anchorMax = this.GetAnchor(currentValue);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000D90A4 File Offset: 0x000D74A4
		private Vector2 GetAnchor(float currentDisplay)
		{
			switch (this.referenceImage.fillMethod)
			{
			case Image.FillMethod.Horizontal:
				return this.GetAnchorHorizontal(currentDisplay, this.referenceImage.fillOrigin);
			case Image.FillMethod.Vertical:
				return this.GetAnchorVertical(currentDisplay, this.referenceImage.fillOrigin);
			case Image.FillMethod.Radial360:
				return this.GetAnchorRadial360(currentDisplay, this.referenceImage.fillOrigin, this.referenceImage.fillClockwise);
			}
			return Vector2.one * 0.5f;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000D9130 File Offset: 0x000D7530
		private Vector2 GetAnchorHorizontal(float fillAmount, int fillOrigin)
		{
			float x;
			if (fillOrigin == 1)
			{
				x = 1f - fillAmount;
			}
			else
			{
				x = fillAmount;
			}
			return new Vector2(x, 0.5f + 0.5f * this.offset);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000D916C File Offset: 0x000D756C
		private Vector2 GetAnchorVertical(float fillAmount, int fillOrigin)
		{
			float y;
			if (fillOrigin == 1)
			{
				y = 1f - fillAmount;
			}
			else
			{
				y = fillAmount;
			}
			return new Vector2(0.5f + 0.5f * this.offset, y);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000D91A8 File Offset: 0x000D75A8
		private Vector2 GetAnchorRadial360(float fillAmount, int fillOrigin, bool fillClockwise)
		{
			float num = 360f * ((!fillClockwise) ? fillAmount : (-fillAmount));
			if (fillOrigin == 0)
			{
				num += ((!fillClockwise) ? 90f : -90f);
			}
			else if (fillOrigin == 3)
			{
				num += ((!fillClockwise) ? 180f : -180f);
			}
			else if (fillOrigin == 2)
			{
				num += ((!fillClockwise) ? 270f : -270f);
			}
			return this.GetPointOnCircle(num);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000D9238 File Offset: 0x000D7638
		private Vector2 GetPointOnCircle(float degrees)
		{
			float num = 0.25f + 0.25f * this.offset;
			return new Vector2(0.5f + num * Mathf.Cos(0.017453292f * degrees), 0.5f + num * Mathf.Sin(0.017453292f * degrees));
		}

		// Token: 0x040025D1 RID: 9681
		[SerializeField]
		private RectTransform rectTrans;

		// Token: 0x040025D2 RID: 9682
		[SerializeField]
		private Image referenceImage;

		// Token: 0x040025D3 RID: 9683
		[Range(-1f, 1f)]
		[SerializeField]
		private float offset;
	}
}
