using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x0200064A RID: 1610
	[RequireComponent(typeof(Text))]
	public class BarViewValueText : ProgressBarProView
	{
		// Token: 0x0600262D RID: 9773 RVA: 0x000D9410 File Offset: 0x000D7810
		public override bool CanUpdateView(float currentValue, float targetValue)
		{
			float roundedDisplayValue = this.GetRoundedDisplayValue(currentValue);
			if (currentValue >= 0f && Mathf.Approximately(this.lastDisplayValue, roundedDisplayValue))
			{
				return false;
			}
			this.lastDisplayValue = this.GetRoundedDisplayValue(currentValue);
			return true;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000D9454 File Offset: 0x000D7854
		public override void UpdateView(float currentValue, float targetValue)
		{
			this.text.text = string.Concat(new string[]
			{
				this.prefix,
				this.FormatNumber(this.GetRoundedDisplayValue(currentValue)),
				this.numberUnit,
				(!this.showMaxValue) ? string.Empty : (" / " + this.FormatNumber(this.maxValue) + this.numberUnit),
				this.suffix
			});
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000D94D6 File Offset: 0x000D78D6
		private float GetDisplayValue(float num)
		{
			return Mathf.Lerp(this.minValue, this.maxValue, num);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000D94EC File Offset: 0x000D78EC
		private float GetRoundedDisplayValue(float num)
		{
			float displayValue = this.GetDisplayValue(num);
			if (this.numDecimals == 0)
			{
				return Mathf.Round(displayValue);
			}
			float num2 = Mathf.Pow(10f, (float)this.numDecimals);
			return Mathf.Round(displayValue * num2) / num2;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000D9531 File Offset: 0x000D7931
		private string FormatNumber(float num)
		{
			return num.ToString("N" + this.numDecimals);
		}

		// Token: 0x040025E9 RID: 9705
		[SerializeField]
		private Text text;

		// Token: 0x040025EA RID: 9706
		[SerializeField]
		private string prefix = string.Empty;

		// Token: 0x040025EB RID: 9707
		[SerializeField]
		private float minValue;

		// Token: 0x040025EC RID: 9708
		[SerializeField]
		private float maxValue = 100f;

		// Token: 0x040025ED RID: 9709
		[SerializeField]
		private int numDecimals;

		// Token: 0x040025EE RID: 9710
		[SerializeField]
		private bool showMaxValue;

		// Token: 0x040025EF RID: 9711
		[SerializeField]
		private string numberUnit = "%";

		// Token: 0x040025F0 RID: 9712
		[SerializeField]
		private string suffix = string.Empty;

		// Token: 0x040025F1 RID: 9713
		private float lastDisplayValue;
	}
}
