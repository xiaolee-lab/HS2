using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x0200063C RID: 1596
	[RequireComponent(typeof(Graphic))]
	public class UIGraphicPulse : MonoBehaviour
	{
		// Token: 0x060025F1 RID: 9713 RVA: 0x000D8394 File Offset: 0x000D6794
		private void Update()
		{
			if (this.isPulsing != this.doPulse)
			{
				this.SetPulsing(this.doPulse);
			}
			if (!this.isPulsing)
			{
				return;
			}
			this.pulseTime += Time.deltaTime;
			this.gfx.color = Color.Lerp(this.defaultColor, this.pulseColor, this.GetAlpha());
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000D83FE File Offset: 0x000D67FE
		private void SetPulsing(bool state)
		{
			this.isPulsing = state;
			if (!this.isPulsing)
			{
				this.gfx.color = this.defaultColor;
			}
			else
			{
				this.pulseTime = 0f;
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000D8433 File Offset: 0x000D6833
		private float GetAlpha()
		{
			return 0.5f + 0.5f * Mathf.Sin((this.pulseTime + this.pulseDuration / 4f) / 3.1415927f * 20f / this.pulseDuration);
		}

		// Token: 0x040025A8 RID: 9640
		public Graphic gfx;

		// Token: 0x040025A9 RID: 9641
		public bool doPulse = true;

		// Token: 0x040025AA RID: 9642
		public Color defaultColor = Color.white;

		// Token: 0x040025AB RID: 9643
		public Color pulseColor = Color.grey;

		// Token: 0x040025AC RID: 9644
		public float pulseDuration = 2f;

		// Token: 0x040025AD RID: 9645
		private bool isPulsing;

		// Token: 0x040025AE RID: 9646
		private float pulseTime;
	}
}
