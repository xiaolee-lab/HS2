using System;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x0200063A RID: 1594
	public class CameraFade : MonoBehaviour
	{
		// Token: 0x060025E4 RID: 9700 RVA: 0x000D7EE0 File Offset: 0x000D62E0
		public void Init()
		{
			this.fadeTexture = new Texture2D(1, 1);
			this.backgroundStyle = new GUIStyle();
			this.backgroundStyle.normal.background = this.fadeTexture;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000D7F10 File Offset: 0x000D6310
		private void OnGUI()
		{
			if (this.currentColor != this.targetColor)
			{
				if (Mathf.Abs(this.currentColor.a - this.targetColor.a) < Mathf.Abs(this.deltaColor.a) * Time.deltaTime)
				{
					this.currentColor = this.targetColor;
					this.SetColor(this.currentColor);
					this.deltaColor = Color.clear;
					if (this.onFadeDone != null)
					{
						this.onFadeDone();
					}
				}
				else
				{
					this.SetColor(this.currentColor + this.deltaColor * Time.deltaTime);
				}
			}
			if (this.currentColor.a > 0f)
			{
				this.EnableAnim(true);
				GUI.depth = -1000;
				GUI.Label(new Rect(-2f, -2f, (float)(Screen.width + 4), (float)(Screen.height + 4)), this.fadeTexture, this.backgroundStyle);
			}
			else
			{
				this.EnableAnim(false);
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000D802B File Offset: 0x000D642B
		private void SetColor(Color newColor)
		{
			this.currentColor = newColor;
			this.fadeTexture.SetPixel(0, 0, this.currentColor);
			this.fadeTexture.Apply();
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000D8054 File Offset: 0x000D6454
		public void StartFadeFrom(Color color, float fadeDuration, Action onFinished = null)
		{
			if (fadeDuration > 0f)
			{
				this.SetColor(color);
				this.onFadeDone = onFinished;
				this.targetColor = new Color(color.r, color.g, color.b, 0f);
				this.SetDeltaColor(fadeDuration);
				this.EnableAnim(true);
			}
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000D80B0 File Offset: 0x000D64B0
		public void StartFadeTo(Color color, float fadeDuration, Action onFinished = null)
		{
			if (fadeDuration > 0f)
			{
				this.SetColor(new Color(color.r, color.g, color.b, 0f));
				this.onFadeDone = onFinished;
				this.targetColor = color;
				this.SetDeltaColor(fadeDuration);
				this.EnableAnim(true);
			}
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000D8109 File Offset: 0x000D6509
		public void StartFadeFromTo(Color colorStart, Color colorEnd, float fadeDuration, Action onFinished = null)
		{
			if (fadeDuration > 0f)
			{
				this.SetColor(colorStart);
				this.onFadeDone = onFinished;
				this.targetColor = colorEnd;
				this.SetDeltaColor(fadeDuration);
				this.EnableAnim(true);
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000D813A File Offset: 0x000D653A
		private void EnableAnim(bool active)
		{
			base.enabled = active;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000D8143 File Offset: 0x000D6543
		private void SetDeltaColor(float duration)
		{
			this.deltaColor = (this.targetColor - this.currentColor) / duration;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000D8162 File Offset: 0x000D6562
		public bool IsFading()
		{
			return base.enabled && this.currentColor != this.targetColor;
		}

		// Token: 0x040025A0 RID: 9632
		private Action onFadeDone;

		// Token: 0x040025A1 RID: 9633
		private const int guiDepth = -1000;

		// Token: 0x040025A2 RID: 9634
		private GUIStyle backgroundStyle;

		// Token: 0x040025A3 RID: 9635
		private Texture2D fadeTexture;

		// Token: 0x040025A4 RID: 9636
		private Color currentColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x040025A5 RID: 9637
		private Color targetColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x040025A6 RID: 9638
		private Color deltaColor = new Color(0f, 0f, 0f, 0f);
	}
}
