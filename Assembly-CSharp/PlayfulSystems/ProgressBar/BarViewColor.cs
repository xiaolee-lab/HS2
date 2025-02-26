using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000641 RID: 1601
	[RequireComponent(typeof(Graphic))]
	public class BarViewColor : ProgressBarProView
	{
		// Token: 0x0600260A RID: 9738 RVA: 0x000D89E5 File Offset: 0x000D6DE5
		private void OnEnable()
		{
			this.flashcolorAlpha = 0f;
			this.UpdateColor();
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000D89F8 File Offset: 0x000D6DF8
		public override void NewChangeStarted(float currentValue, float targetValue)
		{
			if (!this.flashOnGain && !this.flashOnLoss)
			{
				return;
			}
			if (targetValue > currentValue && !this.flashOnGain)
			{
				return;
			}
			if (targetValue < currentValue && !this.flashOnLoss)
			{
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.colorAnim != null)
			{
				base.StopCoroutine(this.colorAnim);
			}
			this.colorAnim = base.StartCoroutine(this.DoBarColorAnim((targetValue >= currentValue) ? this.gainColor : this.lossColor, this.flashTime));
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000D8A9C File Offset: 0x000D6E9C
		private IEnumerator DoBarColorAnim(Color targetColor, float duration)
		{
			float time = 0f;
			while (time < duration)
			{
				this.SetOverrideColor(targetColor, Utils.EaseSinInOut(time / duration, 1f, -1f));
				this.UpdateColor();
				time += Time.deltaTime;
				yield return null;
			}
			this.SetOverrideColor(targetColor, 0f);
			this.UpdateColor();
			this.colorAnim = null;
			yield break;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000D8AC5 File Offset: 0x000D6EC5
		public override void SetBarColor(Color color)
		{
			if (!this.canOverrideColor)
			{
				return;
			}
			this.defaultColor = color;
			this.useGradient = false;
			if (this.colorAnim == null)
			{
				this.UpdateColor();
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000D8AF2 File Offset: 0x000D6EF2
		private void SetOverrideColor(Color color, float alpha)
		{
			this.flashColor = color;
			this.flashcolorAlpha = alpha;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000D8B02 File Offset: 0x000D6F02
		public override void UpdateView(float currentValue, float targetValue)
		{
			this.currentValue = currentValue;
			if (this.colorAnim == null)
			{
				this.UpdateColor();
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000D8B1C File Offset: 0x000D6F1C
		private void UpdateColor()
		{
			this.graphic.canvasRenderer.SetColor(this.GetCurrentColor(this.currentValue));
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000D8B3C File Offset: 0x000D6F3C
		private Color GetCurrentColor(float percentage)
		{
			if (this.flashcolorAlpha >= 1f)
			{
				return this.flashColor;
			}
			if (this.flashcolorAlpha <= 0f)
			{
				return (!this.useGradient) ? this.defaultColor : this.barGradient.Evaluate(percentage);
			}
			return Color.Lerp((!this.useGradient) ? this.defaultColor : this.barGradient.Evaluate(percentage), this.flashColor, this.flashcolorAlpha);
		}

		// Token: 0x040025BD RID: 9661
		[SerializeField]
		protected Graphic graphic;

		// Token: 0x040025BE RID: 9662
		[Header("Color Options")]
		[Tooltip("The default color of the bar can be set by the ProgressBar.SetbarColor()")]
		[SerializeField]
		private bool canOverrideColor;

		// Token: 0x040025BF RID: 9663
		[SerializeField]
		private Color defaultColor = Color.white;

		// Token: 0x040025C0 RID: 9664
		[Tooltip("Change color of the bar automatically based on it's value.")]
		[SerializeField]
		private bool useGradient;

		// Token: 0x040025C1 RID: 9665
		[SerializeField]
		private Gradient barGradient;

		// Token: 0x040025C2 RID: 9666
		private Color flashColor;

		// Token: 0x040025C3 RID: 9667
		private float flashcolorAlpha;

		// Token: 0x040025C4 RID: 9668
		private float currentValue;

		// Token: 0x040025C5 RID: 9669
		[Header("Color Animation")]
		[SerializeField]
		private bool flashOnGain;

		// Token: 0x040025C6 RID: 9670
		[SerializeField]
		private Color gainColor = Color.white;

		// Token: 0x040025C7 RID: 9671
		[SerializeField]
		private bool flashOnLoss;

		// Token: 0x040025C8 RID: 9672
		[SerializeField]
		private Color lossColor = Color.white;

		// Token: 0x040025C9 RID: 9673
		[SerializeField]
		private float flashTime = 0.2f;

		// Token: 0x040025CA RID: 9674
		private Coroutine colorAnim;
	}
}
