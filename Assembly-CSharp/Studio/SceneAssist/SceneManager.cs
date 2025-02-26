using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Studio.SceneAssist
{
	// Token: 0x0200129B RID: 4763
	public class SceneManager : MonoBehaviour
	{
		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06009D6E RID: 40302 RVA: 0x00404C23 File Offset: 0x00403023
		public Image ImageNowLoading
		{
			get
			{
				return this.imageNowLoading;
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06009D6F RID: 40303 RVA: 0x00404C2B File Offset: 0x0040302B
		public Slider SliderProgress
		{
			get
			{
				return this.sliderProgress;
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06009D70 RID: 40304 RVA: 0x00404C33 File Offset: 0x00403033
		public Image ImageLoadingAnime
		{
			get
			{
				return this.imageLoadingAnime;
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (set) Token: 0x06009D71 RID: 40305 RVA: 0x00404C3B File Offset: 0x0040303B
		public bool NowLoadingActive
		{
			set
			{
				if (this.imageNowLoading && this.imageNowLoading.gameObject.activeSelf != value)
				{
					this.imageNowLoading.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06009D73 RID: 40307 RVA: 0x00404CAD File Offset: 0x004030AD
		// (set) Token: 0x06009D72 RID: 40306 RVA: 0x00404C74 File Offset: 0x00403074
		public bool ProgressActive
		{
			get
			{
				return this.sliderProgress != null && this.sliderProgress.IsActive();
			}
			set
			{
				if (this.sliderProgress && this.sliderProgress.gameObject.activeSelf != value)
				{
					this.sliderProgress.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x170021AB RID: 8619
		// (set) Token: 0x06009D74 RID: 40308 RVA: 0x00404CD1 File Offset: 0x004030D1
		public bool LoadingAnimeActive
		{
			set
			{
				if (this.imageLoadingAnime && this.imageLoadingAnime.enabled != value)
				{
					this.imageLoadingAnime.enabled = value;
				}
			}
		}

		// Token: 0x170021AC RID: 8620
		// (set) Token: 0x06009D75 RID: 40309 RVA: 0x00404D00 File Offset: 0x00403100
		public bool Active
		{
			set
			{
				if (this.imageNowLoading && this.imageNowLoading.gameObject.activeSelf != value)
				{
					this.imageNowLoading.gameObject.SetActive(value);
				}
				if (this.sliderProgress && this.sliderProgress.gameObject.activeSelf != value)
				{
					this.sliderProgress.gameObject.SetActive(value);
				}
				if (this.imageLoadingAnime && this.imageLoadingAnime.enabled != value)
				{
					this.imageLoadingAnime.enabled = value;
				}
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06009D76 RID: 40310 RVA: 0x00404DA8 File Offset: 0x004031A8
		// (set) Token: 0x06009D77 RID: 40311 RVA: 0x00404DD0 File Offset: 0x004031D0
		public float ProgressValue
		{
			get
			{
				return (!(this.sliderProgress != null)) ? 1f : this.sliderProgress.value;
			}
			set
			{
				if (this.sliderProgress)
				{
					this.sliderProgress.value = value;
				}
			}
		}

		// Token: 0x170021AE RID: 8622
		// (set) Token: 0x06009D78 RID: 40312 RVA: 0x00404DF0 File Offset: 0x004031F0
		public float NowLoadingAlpha
		{
			set
			{
				if (this.imageNowLoading)
				{
					Color color = this.imageNowLoading.color;
					color.a = value;
					this.imageNowLoading.color = color;
				}
			}
		}

		// Token: 0x170021AF RID: 8623
		// (set) Token: 0x06009D79 RID: 40313 RVA: 0x00404E30 File Offset: 0x00403230
		public float LoadingAnimeAlpha
		{
			set
			{
				if (this.imageLoadingAnime)
				{
					Color color = this.imageLoadingAnime.color;
					color.a = value;
					this.imageLoadingAnime.color = color;
				}
			}
		}

		// Token: 0x06009D7A RID: 40314 RVA: 0x00404E6D File Offset: 0x0040326D
		public void SetAlpha(float _a)
		{
			this.NowLoadingAlpha = _a;
			this.LoadingAnimeAlpha = _a;
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x00404E80 File Offset: 0x00403280
		private void Reset()
		{
			this.imageNowLoading = base.GetComponentsInChildren<Image>().FirstOrDefault((Image p) => p.name == "NowLoading");
			this.sliderProgress = base.GetComponentsInChildren<Slider>().FirstOrDefault((Slider p) => p.name == "Progress");
			this.imageLoadingAnime = base.GetComponentsInChildren<Image>().FirstOrDefault((Image p) => p.name == "LoadingAnime");
		}

		// Token: 0x04007D40 RID: 32064
		[SerializeField]
		private Image imageNowLoading;

		// Token: 0x04007D41 RID: 32065
		[SerializeField]
		private Slider sliderProgress;

		// Token: 0x04007D42 RID: 32066
		[SerializeField]
		private Image imageLoadingAnime;
	}
}
