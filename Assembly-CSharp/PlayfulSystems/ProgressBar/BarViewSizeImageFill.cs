using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000649 RID: 1609
	[RequireComponent(typeof(Image))]
	public class BarViewSizeImageFill : ProgressBarProView
	{
		// Token: 0x06002629 RID: 9769 RVA: 0x000D933A File Offset: 0x000D773A
		public override bool CanUpdateView(float currentValue, float targetValue)
		{
			return base.isActiveAndEnabled || this.isDisplaySizeZero;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000D9350 File Offset: 0x000D7750
		public override void UpdateView(float currentValue, float targetValue)
		{
			if (this.hideOnEmpty && currentValue <= 0f)
			{
				this.image.gameObject.SetActive(false);
				this.isDisplaySizeZero = true;
				return;
			}
			this.isDisplaySizeZero = false;
			this.image.gameObject.SetActive(true);
			this.image.fillAmount = this.GetDisplayValue(currentValue);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000D93B6 File Offset: 0x000D77B6
		private float GetDisplayValue(float display)
		{
			if (!this.useDiscreteSteps)
			{
				return display;
			}
			return Mathf.Round(display * (float)this.numSteps) / (float)this.numSteps;
		}

		// Token: 0x040025E4 RID: 9700
		[SerializeField]
		protected Image image;

		// Token: 0x040025E5 RID: 9701
		[SerializeField]
		private bool hideOnEmpty = true;

		// Token: 0x040025E6 RID: 9702
		[SerializeField]
		private bool useDiscreteSteps;

		// Token: 0x040025E7 RID: 9703
		[SerializeField]
		private int numSteps = 10;

		// Token: 0x040025E8 RID: 9704
		private bool isDisplaySizeZero;
	}
}
