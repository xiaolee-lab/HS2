using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000647 RID: 1607
	[RequireComponent(typeof(Image))]
	public class BarViewSizeAnchorsShadow : BarViewSizeAnchors
	{
		// Token: 0x06002627 RID: 9767 RVA: 0x000D9290 File Offset: 0x000D7690
		public override void UpdateView(float currentValue, float targetValue)
		{
			if (Mathf.Approximately(currentValue, targetValue) || (this.shadowType == BarViewSizeAnchorsShadow.ShadowType.Gaining && targetValue < currentValue) || (this.shadowType == BarViewSizeAnchorsShadow.ShadowType.Losing && targetValue > currentValue))
			{
				this.rectTrans.gameObject.SetActive(false);
				this.isDisplaySizeZero = true;
				return;
			}
			this.isDisplaySizeZero = false;
			this.rectTrans.gameObject.SetActive(true);
			if (this.shadowType == BarViewSizeAnchorsShadow.ShadowType.Gaining)
			{
				base.SetPivot(0f, targetValue);
			}
			else
			{
				base.SetPivot(targetValue, currentValue);
			}
		}

		// Token: 0x040025E0 RID: 9696
		[SerializeField]
		private BarViewSizeAnchorsShadow.ShadowType shadowType;

		// Token: 0x02000648 RID: 1608
		public enum ShadowType
		{
			// Token: 0x040025E2 RID: 9698
			Gaining,
			// Token: 0x040025E3 RID: 9699
			Losing
		}
	}
}
