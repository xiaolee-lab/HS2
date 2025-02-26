using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007F1 RID: 2033
	[MessagePackObject(true)]
	public class ChaFileFace
	{
		// Token: 0x06003280 RID: 12928 RVA: 0x00130865 File Offset: 0x0012EC65
		public ChaFileFace()
		{
			this.MemberInit();
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x00130873 File Offset: 0x0012EC73
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x0013087B File Offset: 0x0012EC7B
		public Version version { get; set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x00130884 File Offset: 0x0012EC84
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x0013088C File Offset: 0x0012EC8C
		public float[] shapeValueFace { get; set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x00130895 File Offset: 0x0012EC95
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x0013089D File Offset: 0x0012EC9D
		public int headId { get; set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x001308A6 File Offset: 0x0012ECA6
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x001308AE File Offset: 0x0012ECAE
		public int skinId { get; set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x001308B7 File Offset: 0x0012ECB7
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x001308BF File Offset: 0x0012ECBF
		public int detailId { get; set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x001308C8 File Offset: 0x0012ECC8
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x001308D0 File Offset: 0x0012ECD0
		public float detailPower { get; set; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x001308D9 File Offset: 0x0012ECD9
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x001308E1 File Offset: 0x0012ECE1
		public int eyebrowId { get; set; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x001308EA File Offset: 0x0012ECEA
		// (set) Token: 0x06003290 RID: 12944 RVA: 0x001308F2 File Offset: 0x0012ECF2
		public Color eyebrowColor { get; set; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x001308FB File Offset: 0x0012ECFB
		// (set) Token: 0x06003292 RID: 12946 RVA: 0x00130903 File Offset: 0x0012ED03
		public Vector4 eyebrowLayout { get; set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x0013090C File Offset: 0x0012ED0C
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x00130914 File Offset: 0x0012ED14
		public float eyebrowTilt { get; set; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x0013091D File Offset: 0x0012ED1D
		// (set) Token: 0x06003296 RID: 12950 RVA: 0x00130925 File Offset: 0x0012ED25
		public ChaFileFace.EyesInfo[] pupil { get; set; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x0013092E File Offset: 0x0012ED2E
		// (set) Token: 0x06003298 RID: 12952 RVA: 0x00130936 File Offset: 0x0012ED36
		public bool pupilSameSetting { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x0013093F File Offset: 0x0012ED3F
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x00130947 File Offset: 0x0012ED47
		public float pupilY { get; set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x00130950 File Offset: 0x0012ED50
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x00130958 File Offset: 0x0012ED58
		public int hlId { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x00130961 File Offset: 0x0012ED61
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x00130969 File Offset: 0x0012ED69
		public Color hlColor { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x00130972 File Offset: 0x0012ED72
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x0013097A File Offset: 0x0012ED7A
		public Vector4 hlLayout { get; set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x00130983 File Offset: 0x0012ED83
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x0013098B File Offset: 0x0012ED8B
		public float hlTilt { get; set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x00130994 File Offset: 0x0012ED94
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x0013099C File Offset: 0x0012ED9C
		public float whiteShadowScale { get; set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x001309A5 File Offset: 0x0012EDA5
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x001309AD File Offset: 0x0012EDAD
		public int eyelashesId { get; set; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x001309B6 File Offset: 0x0012EDB6
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x001309BE File Offset: 0x0012EDBE
		public Color eyelashesColor { get; set; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x001309C7 File Offset: 0x0012EDC7
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x001309CF File Offset: 0x0012EDCF
		public int moleId { get; set; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x001309D8 File Offset: 0x0012EDD8
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x001309E0 File Offset: 0x0012EDE0
		public Color moleColor { get; set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x001309E9 File Offset: 0x0012EDE9
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x001309F1 File Offset: 0x0012EDF1
		public Vector4 moleLayout { get; set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x001309FA File Offset: 0x0012EDFA
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x00130A02 File Offset: 0x0012EE02
		public ChaFileFace.MakeupInfo makeup { get; set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x00130A0B File Offset: 0x0012EE0B
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x00130A13 File Offset: 0x0012EE13
		public int beardId { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x00130A1C File Offset: 0x0012EE1C
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x00130A24 File Offset: 0x0012EE24
		public Color beardColor { get; set; }

		// Token: 0x060032B5 RID: 12981 RVA: 0x00130A30 File Offset: 0x0012EE30
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileFaceVersion;
			this.shapeValueFace = new float[ChaFileDefine.cf_headshapename.Length];
			for (int i = 0; i < this.shapeValueFace.Length; i++)
			{
				this.shapeValueFace[i] = ChaFileDefine.cf_faceInitValue[i];
			}
			this.headId = 0;
			this.skinId = 0;
			this.detailId = 0;
			this.detailPower = 1f;
			this.eyebrowId = 0;
			this.eyebrowColor = new Color(0.05f, 0.05f, 0.05f);
			this.eyebrowLayout = new Vector4(0.5f, 0.375f, 0.666f, 0.666f);
			this.eyebrowTilt = 0.5f;
			this.pupil = new ChaFileFace.EyesInfo[2];
			for (int j = 0; j < this.pupil.Length; j++)
			{
				this.pupil[j] = new ChaFileFace.EyesInfo();
			}
			this.pupilSameSetting = true;
			this.pupilY = 0.5f;
			this.hlId = 0;
			this.hlColor = Color.white;
			this.hlLayout = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
			this.hlTilt = 0.5f;
			this.whiteShadowScale = 0.5f;
			this.eyelashesId = 0;
			this.eyelashesColor = new Color(0.19f, 0.19f, 0.19f);
			this.moleId = 0;
			this.moleColor = Color.black;
			this.moleLayout = new Vector4(0.5f, 0.5f, 0.25f, 0.5f);
			this.makeup = new ChaFileFace.MakeupInfo();
			this.beardId = 0;
			this.beardColor = new Color(0.19f, 0.19f, 0.19f);
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x00130BF8 File Offset: 0x0012EFF8
		public void ComplementWithVersion()
		{
			if (this.version < new Version("0.0.1"))
			{
				this.hlLayout = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
				this.hlTilt = 0.5f;
			}
			if (this.version < new Version("0.0.2"))
			{
				this.beardId = 0;
				this.beardColor = new Color(0.19f, 0.19f, 0.19f);
			}
			this.version = ChaFileDefine.ChaFileFaceVersion;
		}

		// Token: 0x020007F2 RID: 2034
		[MessagePackObject(true)]
		public class EyesInfo
		{
			// Token: 0x060032B7 RID: 12983 RVA: 0x00130C8F File Offset: 0x0012F08F
			public EyesInfo()
			{
				this.MemberInit();
			}

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x060032B8 RID: 12984 RVA: 0x00130C9D File Offset: 0x0012F09D
			// (set) Token: 0x060032B9 RID: 12985 RVA: 0x00130CA5 File Offset: 0x0012F0A5
			public Color whiteColor { get; set; }

			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x060032BA RID: 12986 RVA: 0x00130CAE File Offset: 0x0012F0AE
			// (set) Token: 0x060032BB RID: 12987 RVA: 0x00130CB6 File Offset: 0x0012F0B6
			public int pupilId { get; set; }

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x060032BC RID: 12988 RVA: 0x00130CBF File Offset: 0x0012F0BF
			// (set) Token: 0x060032BD RID: 12989 RVA: 0x00130CC7 File Offset: 0x0012F0C7
			public Color pupilColor { get; set; }

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x060032BE RID: 12990 RVA: 0x00130CD0 File Offset: 0x0012F0D0
			// (set) Token: 0x060032BF RID: 12991 RVA: 0x00130CD8 File Offset: 0x0012F0D8
			public float pupilW { get; set; }

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00130CE1 File Offset: 0x0012F0E1
			// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00130CE9 File Offset: 0x0012F0E9
			public float pupilH { get; set; }

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x060032C2 RID: 12994 RVA: 0x00130CF2 File Offset: 0x0012F0F2
			// (set) Token: 0x060032C3 RID: 12995 RVA: 0x00130CFA File Offset: 0x0012F0FA
			public float pupilEmission { get; set; }

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x060032C4 RID: 12996 RVA: 0x00130D03 File Offset: 0x0012F103
			// (set) Token: 0x060032C5 RID: 12997 RVA: 0x00130D0B File Offset: 0x0012F10B
			public int blackId { get; set; }

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x060032C6 RID: 12998 RVA: 0x00130D14 File Offset: 0x0012F114
			// (set) Token: 0x060032C7 RID: 12999 RVA: 0x00130D1C File Offset: 0x0012F11C
			public Color blackColor { get; set; }

			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x060032C8 RID: 13000 RVA: 0x00130D25 File Offset: 0x0012F125
			// (set) Token: 0x060032C9 RID: 13001 RVA: 0x00130D2D File Offset: 0x0012F12D
			public float blackW { get; set; }

			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x060032CA RID: 13002 RVA: 0x00130D36 File Offset: 0x0012F136
			// (set) Token: 0x060032CB RID: 13003 RVA: 0x00130D3E File Offset: 0x0012F13E
			public float blackH { get; set; }

			// Token: 0x060032CC RID: 13004 RVA: 0x00130D48 File Offset: 0x0012F148
			public void MemberInit()
			{
				this.whiteColor = new Color(0.846f, 0.846f, 0.846f);
				this.pupilId = 0;
				this.pupilColor = new Color(0.33f, 0.33f, 0.33f);
				this.pupilW = 0.666f;
				this.pupilH = 0.666f;
				this.pupilEmission = 0f;
				this.blackId = 0;
				this.blackColor = Color.black;
				this.blackW = 0.8333f;
				this.blackH = 0.8333f;
			}

			// Token: 0x060032CD RID: 13005 RVA: 0x00130DDC File Offset: 0x0012F1DC
			public void Copy(ChaFileFace.EyesInfo src)
			{
				this.whiteColor = src.whiteColor;
				this.pupilId = src.pupilId;
				this.pupilColor = src.pupilColor;
				this.pupilW = src.pupilW;
				this.pupilH = src.pupilH;
				this.pupilEmission = src.pupilEmission;
				this.blackId = src.blackId;
				this.blackColor = src.blackColor;
				this.blackW = src.blackW;
				this.blackH = src.blackH;
			}
		}

		// Token: 0x020007F3 RID: 2035
		[MessagePackObject(true)]
		public class MakeupInfo
		{
			// Token: 0x060032CE RID: 13006 RVA: 0x00130E61 File Offset: 0x0012F261
			public MakeupInfo()
			{
				this.MemberInit();
			}

			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x060032CF RID: 13007 RVA: 0x00130E6F File Offset: 0x0012F26F
			// (set) Token: 0x060032D0 RID: 13008 RVA: 0x00130E77 File Offset: 0x0012F277
			public int eyeshadowId { get; set; }

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x060032D1 RID: 13009 RVA: 0x00130E80 File Offset: 0x0012F280
			// (set) Token: 0x060032D2 RID: 13010 RVA: 0x00130E88 File Offset: 0x0012F288
			public Color eyeshadowColor { get; set; }

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x060032D3 RID: 13011 RVA: 0x00130E91 File Offset: 0x0012F291
			// (set) Token: 0x060032D4 RID: 13012 RVA: 0x00130E99 File Offset: 0x0012F299
			public float eyeshadowGloss { get; set; }

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x060032D5 RID: 13013 RVA: 0x00130EA2 File Offset: 0x0012F2A2
			// (set) Token: 0x060032D6 RID: 13014 RVA: 0x00130EAA File Offset: 0x0012F2AA
			public int cheekId { get; set; }

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x060032D7 RID: 13015 RVA: 0x00130EB3 File Offset: 0x0012F2B3
			// (set) Token: 0x060032D8 RID: 13016 RVA: 0x00130EBB File Offset: 0x0012F2BB
			public Color cheekColor { get; set; }

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x060032D9 RID: 13017 RVA: 0x00130EC4 File Offset: 0x0012F2C4
			// (set) Token: 0x060032DA RID: 13018 RVA: 0x00130ECC File Offset: 0x0012F2CC
			public float cheekGloss { get; set; }

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x060032DB RID: 13019 RVA: 0x00130ED5 File Offset: 0x0012F2D5
			// (set) Token: 0x060032DC RID: 13020 RVA: 0x00130EDD File Offset: 0x0012F2DD
			public int lipId { get; set; }

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x060032DD RID: 13021 RVA: 0x00130EE6 File Offset: 0x0012F2E6
			// (set) Token: 0x060032DE RID: 13022 RVA: 0x00130EEE File Offset: 0x0012F2EE
			public Color lipColor { get; set; }

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x060032DF RID: 13023 RVA: 0x00130EF7 File Offset: 0x0012F2F7
			// (set) Token: 0x060032E0 RID: 13024 RVA: 0x00130EFF File Offset: 0x0012F2FF
			public float lipGloss { get; set; }

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x060032E1 RID: 13025 RVA: 0x00130F08 File Offset: 0x0012F308
			// (set) Token: 0x060032E2 RID: 13026 RVA: 0x00130F10 File Offset: 0x0012F310
			public PaintInfo[] paintInfo { get; set; }

			// Token: 0x060032E3 RID: 13027 RVA: 0x00130F1C File Offset: 0x0012F31C
			public void MemberInit()
			{
				this.eyeshadowId = 0;
				this.eyeshadowColor = Color.white;
				this.cheekId = 0;
				this.cheekColor = Color.white;
				this.lipId = 0;
				this.lipColor = Color.white;
				this.paintInfo = new PaintInfo[2];
				for (int i = 0; i < 2; i++)
				{
					this.paintInfo[i] = new PaintInfo();
				}
			}
		}
	}
}
