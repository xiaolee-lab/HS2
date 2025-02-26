using System;
using System.Collections;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x0200063B RID: 1595
	public class CanvasGroupFade : MonoBehaviour
	{
		// Token: 0x060025EE RID: 9710 RVA: 0x000D818C File Offset: 0x000D658C
		public void FadeAlpha(float fromAlpha, float toAlpha, float duration)
		{
			if (this.group == null)
			{
				this.group = base.GetComponent<CanvasGroup>();
			}
			if (this.group != null)
			{
				if (duration > 0f)
				{
					base.StopAllCoroutines();
					base.gameObject.SetActive(true);
					base.StartCoroutine(this.DoFade(fromAlpha, toAlpha, duration));
				}
				else
				{
					this.group.alpha = toAlpha;
					base.gameObject.SetActive(toAlpha > 0f);
				}
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000D8218 File Offset: 0x000D6618
		private IEnumerator DoFade(float fromAlpha, float toAlpha, float duration)
		{
			float time = 0f;
			while (time < duration)
			{
				time += Time.deltaTime;
				this.group.alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);
				yield return null;
			}
			this.group.alpha = toAlpha;
			if (toAlpha == 0f)
			{
				base.gameObject.SetActive(false);
			}
			yield break;
		}

		// Token: 0x040025A7 RID: 9639
		private CanvasGroup group;
	}
}
