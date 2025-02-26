using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FadeCtrl
{
	// Token: 0x0200102C RID: 4140
	public class SpriteFadeCtrl : MonoBehaviour
	{
		// Token: 0x06008AB8 RID: 35512 RVA: 0x003A4D3A File Offset: 0x003A313A
		private void Start()
		{
			if (this.imageFade)
			{
				this.imageFade.enabled = false;
			}
		}

		// Token: 0x06008AB9 RID: 35513 RVA: 0x003A4D58 File Offset: 0x003A3158
		public void SetColor(Color _color)
		{
			if (!this.imageFade)
			{
				return;
			}
			this.imageFade.color = _color;
		}

		// Token: 0x06008ABA RID: 35514 RVA: 0x003A4D78 File Offset: 0x003A3178
		public void FadeStart(SpriteFadeCtrl.FadeKind _kind, float _timeFade = -1f)
		{
			this.timeFadeTime = 0f;
			this.imageFade.enabled = true;
			if (_timeFade < 0f)
			{
				this.timeFade = ((_kind == SpriteFadeCtrl.FadeKind.OutIn) ? (this.timeFadeBase * 2f) : this.timeFadeBase);
			}
			else
			{
				this.timeFade = ((_kind == SpriteFadeCtrl.FadeKind.OutIn) ? (_timeFade * 2f) : _timeFade);
			}
			this.kindFade = _kind;
			switch (this.kindFade)
			{
			case SpriteFadeCtrl.FadeKind.Out:
				this.kindFadeProc = SpriteFadeCtrl.FadeKindProc.Out;
				break;
			case SpriteFadeCtrl.FadeKind.In:
				this.kindFadeProc = SpriteFadeCtrl.FadeKindProc.In;
				break;
			case SpriteFadeCtrl.FadeKind.OutIn:
				this.kindFadeProc = SpriteFadeCtrl.FadeKindProc.OutIn;
				break;
			}
		}

		// Token: 0x06008ABB RID: 35515 RVA: 0x003A4E38 File Offset: 0x003A3238
		public SpriteFadeCtrl.FadeKindProc GetFadeKindProc()
		{
			return this.kindFadeProc;
		}

		// Token: 0x06008ABC RID: 35516 RVA: 0x003A4E40 File Offset: 0x003A3240
		public bool IsFade()
		{
			return this.kindFadeProc == SpriteFadeCtrl.FadeKindProc.In || this.kindFadeProc == SpriteFadeCtrl.FadeKindProc.Out || this.kindFadeProc == SpriteFadeCtrl.FadeKindProc.OutIn;
		}

		// Token: 0x06008ABD RID: 35517 RVA: 0x003A4E68 File Offset: 0x003A3268
		public IEnumerator FadeProc()
		{
			float t = 0f;
			while (t < 1f)
			{
				this.timeFadeTime += Time.deltaTime;
				Color c = this.imageFade.color;
				t = this.fadeAnimation.Evaluate(Mathf.Clamp01(this.timeFadeTime / this.timeFade));
				switch (this.kindFade)
				{
				case SpriteFadeCtrl.FadeKind.Out:
					c.a = t;
					break;
				case SpriteFadeCtrl.FadeKind.In:
					c.a = 1f - t;
					break;
				case SpriteFadeCtrl.FadeKind.OutIn:
					c.a = Mathf.Sin(0.017453292f * Mathf.Lerp(0f, 180f, t));
					break;
				}
				this.imageFade.color = c;
				yield return null;
			}
			switch (this.kindFade)
			{
			case SpriteFadeCtrl.FadeKind.Out:
				this.kindFadeProc = SpriteFadeCtrl.FadeKindProc.OutEnd;
				break;
			case SpriteFadeCtrl.FadeKind.In:
				this.kindFadeProc = SpriteFadeCtrl.FadeKindProc.InEnd;
				this.imageFade.enabled = false;
				break;
			case SpriteFadeCtrl.FadeKind.OutIn:
				this.kindFadeProc = SpriteFadeCtrl.FadeKindProc.OutInEnd;
				this.imageFade.enabled = false;
				break;
			}
			yield break;
		}

		// Token: 0x04007121 RID: 28961
		[SerializeField]
		private RawImage imageFade;

		// Token: 0x04007122 RID: 28962
		[SerializeField]
		private float timeFadeBase;

		// Token: 0x04007123 RID: 28963
		[SerializeField]
		private AnimationCurve fadeAnimation;

		// Token: 0x04007124 RID: 28964
		private SpriteFadeCtrl.FadeKind kindFade;

		// Token: 0x04007125 RID: 28965
		private SpriteFadeCtrl.FadeKindProc kindFadeProc;

		// Token: 0x04007126 RID: 28966
		private float timeFade;

		// Token: 0x04007127 RID: 28967
		[SerializeField]
		private float timeFadeTime;

		// Token: 0x0200102D RID: 4141
		public enum FadeKind
		{
			// Token: 0x04007129 RID: 28969
			Out,
			// Token: 0x0400712A RID: 28970
			In,
			// Token: 0x0400712B RID: 28971
			OutIn
		}

		// Token: 0x0200102E RID: 4142
		public enum FadeKindProc
		{
			// Token: 0x0400712D RID: 28973
			None,
			// Token: 0x0400712E RID: 28974
			Out,
			// Token: 0x0400712F RID: 28975
			OutEnd,
			// Token: 0x04007130 RID: 28976
			In,
			// Token: 0x04007131 RID: 28977
			InEnd,
			// Token: 0x04007132 RID: 28978
			OutIn,
			// Token: 0x04007133 RID: 28979
			OutInEnd
		}
	}
}
