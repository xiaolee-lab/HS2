using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A9 RID: 1705
public class ADVFade : MonoBehaviour
{
	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x06002846 RID: 10310 RVA: 0x000EEF0C File Offset: 0x000ED30C
	public Image FilterFront
	{
		get
		{
			return this.filterFront;
		}
	}

	// Token: 0x170005C7 RID: 1479
	// (get) Token: 0x06002847 RID: 10311 RVA: 0x000EEF14 File Offset: 0x000ED314
	public Image FilterBack
	{
		get
		{
			return this.filterBack;
		}
	}

	// Token: 0x170005C8 RID: 1480
	// (get) Token: 0x06002848 RID: 10312 RVA: 0x000EEF1C File Offset: 0x000ED31C
	public int FrontIndex
	{
		get
		{
			return this.frontIndex;
		}
	}

	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x06002849 RID: 10313 RVA: 0x000EEF24 File Offset: 0x000ED324
	public int BackIndex
	{
		get
		{
			return this.backIndex;
		}
	}

	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x0600284A RID: 10314 RVA: 0x000EEF2C File Offset: 0x000ED32C
	public bool IsFadeInEnd
	{
		get
		{
			return this.front.isFadeInEnd || this.back.isFadeInEnd;
		}
	}

	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x0600284B RID: 10315 RVA: 0x000EEF4C File Offset: 0x000ED34C
	public bool IsEnd
	{
		get
		{
			return this.isEnd;
		}
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x000EEF54 File Offset: 0x000ED354
	public void Initialize()
	{
		if (this.front == null)
		{
			this.frontIndex = this.filterFront.rectTransform.GetSiblingIndex();
		}
		if (this.back == null)
		{
			this.backIndex = this.filterBack.rectTransform.GetSiblingIndex();
		}
		this.filterFront.color = this.initColor;
		this.front = new ADVFade.Fade(this.filterFront, this.initColor, 0f, true, true);
		this.filterBack.color = this.initColor;
		this.back = new ADVFade.Fade(this.filterBack, this.initColor, 0f, true, true);
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x000EF001 File Offset: 0x000ED401
	public void SetColor(bool isFront, Color color)
	{
		((!isFront) ? this.back.filter : this.front.filter).color = color;
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x000EF02C File Offset: 0x000ED42C
	public void CrossFadeAlpha(bool isFront, float alpha, float time, bool ignoreTimeScale)
	{
		Color color = (!isFront) ? this.back.filter.color : this.front.filter.color;
		color.a = alpha;
		this.CrossFadeColor(isFront, color, time, ignoreTimeScale);
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x000EF078 File Offset: 0x000ED478
	public void CrossFadeColor(bool isFront, Color color, float time, bool ignoreTimeScale)
	{
		if (isFront)
		{
			this.front = new ADVFade.Fade(this.front.filter, color, time, ignoreTimeScale, true);
		}
		else
		{
			this.back = new ADVFade.Fade(this.back.filter, color, time, ignoreTimeScale, true);
		}
		this.isEnd = false;
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x000EF0D0 File Offset: 0x000ED4D0
	public void LoadSprite(bool isFront, string bundleName, string assetName)
	{
		AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(bundleName, assetName, typeof(Sprite), null);
		Sprite sprite = assetBundleLoadAssetOperation.GetAsset<Sprite>();
		if (sprite == null)
		{
			Texture2D asset = assetBundleLoadAssetOperation.GetAsset<Texture2D>();
			sprite = Sprite.Create(asset, new Rect(0f, 0f, (float)asset.width, (float)asset.height), Vector2.zero);
		}
		if (isFront)
		{
			this.front.filter.sprite = sprite;
			if (!this.frontImageAssetBundleName.IsNullOrEmpty())
			{
				AssetBundleManager.UnloadAssetBundle(this.frontImageAssetBundleName, false, null, false);
			}
			this.frontImageAssetBundleName = bundleName;
		}
		else
		{
			this.back.filter.sprite = sprite;
			if (!this.backImageAssetBundleName.IsNullOrEmpty())
			{
				AssetBundleManager.UnloadAssetBundle(this.backImageAssetBundleName, false, null, false);
			}
			this.backImageAssetBundleName = bundleName;
		}
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x000EF1AC File Offset: 0x000ED5AC
	public void ReleaseSprite(bool isFront)
	{
		if (isFront)
		{
			this.front.filter.sprite = null;
			if (!this.frontImageAssetBundleName.IsNullOrEmpty())
			{
				AssetBundleManager.UnloadAssetBundle(this.frontImageAssetBundleName, false, null, false);
				this.frontImageAssetBundleName = null;
			}
		}
		else
		{
			this.back.filter.sprite = null;
			if (!this.backImageAssetBundleName.IsNullOrEmpty())
			{
				AssetBundleManager.UnloadAssetBundle(this.backImageAssetBundleName, false, null, false);
				this.backImageAssetBundleName = null;
			}
		}
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x000EF230 File Offset: 0x000ED630
	private void Awake()
	{
		this.Initialize();
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x000EF238 File Offset: 0x000ED638
	private void Update()
	{
		this.isEnd = true;
		if (this.front.Update() && this.front.isFadeOutEnd)
		{
			this.filterFront.raycastTarget = false;
		}
		else
		{
			this.isEnd = false;
			this.filterFront.raycastTarget = true;
		}
		if (!this.back.Update() || !this.back.isFadeOutEnd)
		{
			this.isEnd = false;
		}
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x000EF2BC File Offset: 0x000ED6BC
	private void OnDestroy()
	{
		if (Singleton<AssetBundleManager>.IsInstance())
		{
			this.ReleaseSprite(true);
			this.ReleaseSprite(false);
		}
	}

	// Token: 0x040029E2 RID: 10722
	public bool isStartRun = true;

	// Token: 0x040029E3 RID: 10723
	[SerializeField]
	private Image filterFront;

	// Token: 0x040029E4 RID: 10724
	[SerializeField]
	private Image filterBack;

	// Token: 0x040029E5 RID: 10725
	private bool isEnd;

	// Token: 0x040029E6 RID: 10726
	private string frontImageAssetBundleName;

	// Token: 0x040029E7 RID: 10727
	private string backImageAssetBundleName;

	// Token: 0x040029E8 RID: 10728
	private ADVFade.Fade front;

	// Token: 0x040029E9 RID: 10729
	private ADVFade.Fade back;

	// Token: 0x040029EA RID: 10730
	private int frontIndex = -1;

	// Token: 0x040029EB RID: 10731
	private int backIndex = -1;

	// Token: 0x040029EC RID: 10732
	private readonly Color initColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x020006AA RID: 1706
	public class Fade
	{
		// Token: 0x06002855 RID: 10325 RVA: 0x000EF2D8 File Offset: 0x000ED6D8
		public Fade(Image filter, Color color, float time, bool ignoreTimeScale, bool isUpdate = true)
		{
			this.filter = filter;
			this.initColor = filter.color;
			this.color = color;
			this.time = time;
			this.ignoreTimeScale = ignoreTimeScale;
			this.timer = 0f;
			if (isUpdate)
			{
				this.Update();
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000EF330 File Offset: 0x000ED730
		public bool isFadeInEnd
		{
			get
			{
				return this.filter.color.a == 1f;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000EF358 File Offset: 0x000ED758
		public bool isFadeOutEnd
		{
			get
			{
				return this.filter.color.a == 0f;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x000EF37F File Offset: 0x000ED77F
		public bool IsEnd
		{
			get
			{
				return this.time == this.timer;
			}
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000EF390 File Offset: 0x000ED790
		public bool Update()
		{
			float num = Time.deltaTime;
			if (this.ignoreTimeScale)
			{
				num = Time.unscaledDeltaTime;
			}
			this.timer += num;
			this.timer = Mathf.Min(this.timer, this.time);
			float t = (this.time != 0f) ? Mathf.InverseLerp(0f, this.time, this.timer) : 1f;
			this.filter.color = Color.Lerp(this.initColor, this.color, t);
			return this.IsEnd;
		}

		// Token: 0x040029ED RID: 10733
		public Image filter;

		// Token: 0x040029EE RID: 10734
		public Color initColor;

		// Token: 0x040029EF RID: 10735
		public Color color;

		// Token: 0x040029F0 RID: 10736
		public float time;

		// Token: 0x040029F1 RID: 10737
		public float timer;

		// Token: 0x040029F2 RID: 10738
		public bool ignoreTimeScale;
	}
}
