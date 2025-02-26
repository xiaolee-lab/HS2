using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SceneAssist
{
	// Token: 0x0200102A RID: 4138
	public class SceneManager : MonoBehaviour
	{
		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x06008A9E RID: 35486 RVA: 0x003A47AE File Offset: 0x003A2BAE
		public Image ImageNowLoading
		{
			get
			{
				return this.imageNowLoading;
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x06008A9F RID: 35487 RVA: 0x003A47B6 File Offset: 0x003A2BB6
		public Slider SliderProgress
		{
			get
			{
				return this.sliderProgress;
			}
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x06008AA0 RID: 35488 RVA: 0x003A47BE File Offset: 0x003A2BBE
		public Image ImageLoadingAnime
		{
			get
			{
				return this.imageLoadingAnime;
			}
		}

		// Token: 0x17001E3D RID: 7741
		// (set) Token: 0x06008AA1 RID: 35489 RVA: 0x003A47C6 File Offset: 0x003A2BC6
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

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x06008AA3 RID: 35491 RVA: 0x003A4838 File Offset: 0x003A2C38
		// (set) Token: 0x06008AA2 RID: 35490 RVA: 0x003A47FF File Offset: 0x003A2BFF
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

		// Token: 0x17001E3F RID: 7743
		// (set) Token: 0x06008AA4 RID: 35492 RVA: 0x003A485C File Offset: 0x003A2C5C
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

		// Token: 0x17001E40 RID: 7744
		// (set) Token: 0x06008AA5 RID: 35493 RVA: 0x003A488C File Offset: 0x003A2C8C
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

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x06008AA6 RID: 35494 RVA: 0x003A4934 File Offset: 0x003A2D34
		// (set) Token: 0x06008AA7 RID: 35495 RVA: 0x003A495C File Offset: 0x003A2D5C
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

		// Token: 0x17001E42 RID: 7746
		// (set) Token: 0x06008AA8 RID: 35496 RVA: 0x003A497C File Offset: 0x003A2D7C
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

		// Token: 0x17001E43 RID: 7747
		// (set) Token: 0x06008AA9 RID: 35497 RVA: 0x003A49BC File Offset: 0x003A2DBC
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

		// Token: 0x06008AAA RID: 35498 RVA: 0x003A49F9 File Offset: 0x003A2DF9
		public void SetAlpha(float _a)
		{
			this.NowLoadingAlpha = _a;
			this.LoadingAnimeAlpha = _a;
		}

		// Token: 0x06008AAB RID: 35499 RVA: 0x003A4A0C File Offset: 0x003A2E0C
		private void Reset()
		{
			this.imageNowLoading = base.GetComponentsInChildren<Image>().FirstOrDefault((Image p) => p.name == "NowLoading");
			this.sliderProgress = base.GetComponentsInChildren<Slider>().FirstOrDefault((Slider p) => p.name == "Progress");
			this.imageLoadingAnime = base.GetComponentsInChildren<Image>().FirstOrDefault((Image p) => p.name == "LoadingAnime");
		}

		// Token: 0x0400711A RID: 28954
		[SerializeField]
		private Image imageNowLoading;

		// Token: 0x0400711B RID: 28955
		[SerializeField]
		private Slider sliderProgress;

		// Token: 0x0400711C RID: 28956
		[SerializeField]
		private Image imageLoadingAnime;
	}
}
