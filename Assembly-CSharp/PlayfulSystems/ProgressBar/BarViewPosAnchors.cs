using System;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000643 RID: 1603
	public class BarViewPosAnchors : BarViewSizeAnchors
	{
		// Token: 0x06002618 RID: 9752 RVA: 0x000D906C File Offset: 0x000D746C
		public override void UpdateView(float currentValue, float targetValue)
		{
			base.SetPivot(currentValue, currentValue);
		}
	}
}
