using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x0200080A RID: 2058
	[MessagePackObject(true)]
	public class ChaFileStatus
	{
		// Token: 0x060033D4 RID: 13268 RVA: 0x00133402 File Offset: 0x00131802
		public ChaFileStatus()
		{
			this.MemberInit();
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x00133410 File Offset: 0x00131810
		// (set) Token: 0x060033D6 RID: 13270 RVA: 0x00133418 File Offset: 0x00131818
		public Version version { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x00133421 File Offset: 0x00131821
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x00133429 File Offset: 0x00131829
		public byte[] clothesState { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x00133432 File Offset: 0x00131832
		// (set) Token: 0x060033DA RID: 13274 RVA: 0x0013343A File Offset: 0x0013183A
		public bool[] showAccessory { get; set; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x00133443 File Offset: 0x00131843
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x0013344B File Offset: 0x0013184B
		public int eyebrowPtn { get; set; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x00133454 File Offset: 0x00131854
		// (set) Token: 0x060033DE RID: 13278 RVA: 0x0013345C File Offset: 0x0013185C
		public float eyebrowOpenMax { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x00133465 File Offset: 0x00131865
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x0013346D File Offset: 0x0013186D
		public int eyesPtn { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x00133476 File Offset: 0x00131876
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x0013347E File Offset: 0x0013187E
		public float eyesOpenMax { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x00133487 File Offset: 0x00131887
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x0013348F File Offset: 0x0013188F
		public bool eyesBlink { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x00133498 File Offset: 0x00131898
		// (set) Token: 0x060033E6 RID: 13286 RVA: 0x001334A0 File Offset: 0x001318A0
		public bool eyesYure { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x001334A9 File Offset: 0x001318A9
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x001334B1 File Offset: 0x001318B1
		public int mouthPtn { get; set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x001334BA File Offset: 0x001318BA
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x001334C2 File Offset: 0x001318C2
		public float mouthOpenMin { get; set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x001334CB File Offset: 0x001318CB
		// (set) Token: 0x060033EC RID: 13292 RVA: 0x001334D3 File Offset: 0x001318D3
		public float mouthOpenMax { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x001334DC File Offset: 0x001318DC
		// (set) Token: 0x060033EE RID: 13294 RVA: 0x001334E4 File Offset: 0x001318E4
		public bool mouthFixed { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x001334ED File Offset: 0x001318ED
		// (set) Token: 0x060033F0 RID: 13296 RVA: 0x001334F5 File Offset: 0x001318F5
		public bool mouthAdjustWidth { get; set; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x001334FE File Offset: 0x001318FE
		// (set) Token: 0x060033F2 RID: 13298 RVA: 0x00133506 File Offset: 0x00131906
		public byte tongueState { get; set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x0013350F File Offset: 0x0013190F
		// (set) Token: 0x060033F4 RID: 13300 RVA: 0x00133517 File Offset: 0x00131917
		public int eyesLookPtn { get; set; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x00133520 File Offset: 0x00131920
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x00133528 File Offset: 0x00131928
		public int eyesTargetType { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x00133531 File Offset: 0x00131931
		// (set) Token: 0x060033F8 RID: 13304 RVA: 0x00133539 File Offset: 0x00131939
		public float eyesTargetAngle { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x00133542 File Offset: 0x00131942
		// (set) Token: 0x060033FA RID: 13306 RVA: 0x0013354A File Offset: 0x0013194A
		public float eyesTargetRange { get; set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x00133553 File Offset: 0x00131953
		// (set) Token: 0x060033FC RID: 13308 RVA: 0x0013355B File Offset: 0x0013195B
		public float eyesTargetRate { get; set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x00133564 File Offset: 0x00131964
		// (set) Token: 0x060033FE RID: 13310 RVA: 0x0013356C File Offset: 0x0013196C
		public int neckLookPtn { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x00133575 File Offset: 0x00131975
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x0013357D File Offset: 0x0013197D
		public int neckTargetType { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x00133586 File Offset: 0x00131986
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x0013358E File Offset: 0x0013198E
		public float neckTargetAngle { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x00133597 File Offset: 0x00131997
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x0013359F File Offset: 0x0013199F
		public float neckTargetRange { get; set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x001335A8 File Offset: 0x001319A8
		// (set) Token: 0x06003406 RID: 13318 RVA: 0x001335B0 File Offset: 0x001319B0
		public float neckTargetRate { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x001335B9 File Offset: 0x001319B9
		// (set) Token: 0x06003408 RID: 13320 RVA: 0x001335C1 File Offset: 0x001319C1
		public bool disableMouthShapeMask { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x001335CA File Offset: 0x001319CA
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x001335D2 File Offset: 0x001319D2
		public bool[,] disableBustShapeMask { get; set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x001335DB File Offset: 0x001319DB
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x001335E3 File Offset: 0x001319E3
		public float nipStandRate { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x001335EC File Offset: 0x001319EC
		// (set) Token: 0x0600340E RID: 13326 RVA: 0x001335F4 File Offset: 0x001319F4
		public float skinTuyaRate { get; set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x001335FD File Offset: 0x001319FD
		// (set) Token: 0x06003410 RID: 13328 RVA: 0x00133605 File Offset: 0x00131A05
		public float hohoAkaRate { get; set; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x0013360E File Offset: 0x00131A0E
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x00133616 File Offset: 0x00131A16
		public float tearsRate { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x0013361F File Offset: 0x00131A1F
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x00133627 File Offset: 0x00131A27
		public bool hideEyesHighlight { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x00133630 File Offset: 0x00131A30
		// (set) Token: 0x06003416 RID: 13334 RVA: 0x00133638 File Offset: 0x00131A38
		public byte[] siruLv { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x00133641 File Offset: 0x00131A41
		// (set) Token: 0x06003418 RID: 13336 RVA: 0x00133649 File Offset: 0x00131A49
		public float wetRate { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x00133652 File Offset: 0x00131A52
		// (set) Token: 0x0600341A RID: 13338 RVA: 0x0013365A File Offset: 0x00131A5A
		public bool visibleSon { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x00133663 File Offset: 0x00131A63
		// (set) Token: 0x0600341C RID: 13340 RVA: 0x0013366B File Offset: 0x00131A6B
		public bool visibleSonAlways { get; set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x00133674 File Offset: 0x00131A74
		// (set) Token: 0x0600341E RID: 13342 RVA: 0x0013367C File Offset: 0x00131A7C
		public bool visibleHeadAlways { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600341F RID: 13343 RVA: 0x00133685 File Offset: 0x00131A85
		// (set) Token: 0x06003420 RID: 13344 RVA: 0x0013368D File Offset: 0x00131A8D
		public bool visibleBodyAlways { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x00133696 File Offset: 0x00131A96
		// (set) Token: 0x06003422 RID: 13346 RVA: 0x0013369E File Offset: 0x00131A9E
		public bool visibleSimple { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x001336A7 File Offset: 0x00131AA7
		// (set) Token: 0x06003424 RID: 13348 RVA: 0x001336AF File Offset: 0x00131AAF
		public bool visibleGomu { get; set; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x001336B8 File Offset: 0x00131AB8
		// (set) Token: 0x06003426 RID: 13350 RVA: 0x001336C0 File Offset: 0x00131AC0
		public Color simpleColor { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x001336C9 File Offset: 0x00131AC9
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x001336D1 File Offset: 0x00131AD1
		public bool[] enableShapeHand { get; set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x001336DA File Offset: 0x00131ADA
		// (set) Token: 0x0600342A RID: 13354 RVA: 0x001336E2 File Offset: 0x00131AE2
		public int[,] shapeHandPtn { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x001336EB File Offset: 0x00131AEB
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x001336F3 File Offset: 0x00131AF3
		public float[] shapeHandBlendValue { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x001336FC File Offset: 0x00131AFC
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x00133704 File Offset: 0x00131B04
		public float siriAkaRate { get; set; }

		// Token: 0x0600342F RID: 13359 RVA: 0x00133710 File Offset: 0x00131B10
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileStatusVersion;
			this.clothesState = new byte[Enum.GetValues(typeof(ChaFileDefine.ClothesKind)).Length];
			this.showAccessory = new bool[20];
			for (int i = 0; i < this.showAccessory.Length; i++)
			{
				this.showAccessory[i] = true;
			}
			this.eyebrowPtn = 0;
			this.eyebrowOpenMax = 1f;
			this.eyesPtn = 0;
			this.eyesOpenMax = 1f;
			this.eyesBlink = true;
			this.eyesYure = false;
			this.mouthPtn = 0;
			this.mouthOpenMin = 0f;
			this.mouthOpenMax = 1f;
			this.mouthFixed = false;
			this.mouthAdjustWidth = true;
			this.tongueState = 0;
			this.eyesLookPtn = 0;
			this.eyesTargetType = 0;
			this.eyesTargetAngle = 0f;
			this.eyesTargetRange = 1f;
			this.eyesTargetRate = 1f;
			this.neckLookPtn = 0;
			this.neckTargetType = 0;
			this.neckTargetAngle = 0f;
			this.neckTargetRange = 1f;
			this.neckTargetRate = 1f;
			this.disableMouthShapeMask = false;
			this.disableBustShapeMask = new bool[2, ChaFileDefine.cf_BustShapeMaskID.Length];
			this.nipStandRate = 0f;
			this.skinTuyaRate = 0f;
			this.hohoAkaRate = 0f;
			this.tearsRate = 0f;
			this.hideEyesHighlight = false;
			this.siruLv = new byte[Enum.GetValues(typeof(ChaFileDefine.SiruParts)).Length];
			this.wetRate = 0f;
			this.visibleSon = false;
			this.visibleSonAlways = true;
			this.visibleHeadAlways = true;
			this.visibleBodyAlways = true;
			this.visibleSimple = false;
			this.visibleGomu = false;
			this.simpleColor = new Color(0.188f, 0.286f, 0.8f, 0.5f);
			this.enableShapeHand = new bool[2];
			this.shapeHandPtn = new int[2, 2];
			this.shapeHandBlendValue = new float[2];
			this.siriAkaRate = 0f;
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x00133928 File Offset: 0x00131D28
		public void Copy(ChaFileStatus src)
		{
			this.clothesState = src.clothesState;
			this.showAccessory = src.showAccessory;
			this.eyebrowPtn = src.eyebrowPtn;
			this.eyebrowOpenMax = src.eyebrowOpenMax;
			this.eyesPtn = src.eyesPtn;
			this.eyesOpenMax = src.eyesOpenMax;
			this.eyesBlink = src.eyesBlink;
			this.eyesYure = src.eyesYure;
			this.mouthPtn = src.mouthPtn;
			this.mouthOpenMin = src.mouthOpenMin;
			this.mouthOpenMax = src.mouthOpenMax;
			this.mouthFixed = src.mouthFixed;
			this.mouthAdjustWidth = src.mouthAdjustWidth;
			this.tongueState = src.tongueState;
			this.eyesLookPtn = src.eyesLookPtn;
			this.eyesTargetType = src.eyesTargetType;
			this.eyesTargetAngle = src.eyesTargetAngle;
			this.eyesTargetRange = src.eyesTargetRange;
			this.eyesTargetRate = src.eyesTargetRate;
			this.neckLookPtn = src.neckLookPtn;
			this.neckTargetType = src.neckTargetType;
			this.neckTargetAngle = src.neckTargetAngle;
			this.neckTargetRange = src.neckTargetRange;
			this.neckTargetRate = src.neckTargetRate;
			this.disableMouthShapeMask = src.disableMouthShapeMask;
			this.disableBustShapeMask = src.disableBustShapeMask;
			this.nipStandRate = src.nipStandRate;
			this.skinTuyaRate = src.skinTuyaRate;
			this.hohoAkaRate = src.hohoAkaRate;
			this.tearsRate = src.tearsRate;
			this.hideEyesHighlight = src.hideEyesHighlight;
			this.siruLv = src.siruLv;
			this.wetRate = src.wetRate;
			this.visibleSon = src.visibleSon;
			this.visibleSonAlways = src.visibleSonAlways;
			this.visibleHeadAlways = src.visibleHeadAlways;
			this.visibleBodyAlways = src.visibleBodyAlways;
			this.visibleSimple = src.visibleSimple;
			this.visibleGomu = src.visibleGomu;
			this.simpleColor = src.simpleColor;
			this.enableShapeHand = src.enableShapeHand;
			this.shapeHandPtn = src.shapeHandPtn;
			this.shapeHandBlendValue = src.shapeHandBlendValue;
			this.siriAkaRate = src.siriAkaRate;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x00133B45 File Offset: 0x00131F45
		public void ComplementWithVersion()
		{
			if (this.version < new Version("0.0.1"))
			{
			}
			this.version = ChaFileDefine.ChaFileStatusVersion;
		}

		// Token: 0x040033F8 RID: 13304
		[IgnoreMember]
		public static readonly string BlockName = "Status";
	}
}
