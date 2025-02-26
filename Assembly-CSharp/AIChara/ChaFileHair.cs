using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007F5 RID: 2037
	[MessagePackObject(true)]
	public class ChaFileHair
	{
		// Token: 0x06003311 RID: 13073 RVA: 0x001312A4 File Offset: 0x0012F6A4
		public ChaFileHair()
		{
			this.MemberInit();
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x001312B2 File Offset: 0x0012F6B2
		// (set) Token: 0x06003313 RID: 13075 RVA: 0x001312BA File Offset: 0x0012F6BA
		public Version version { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x001312C3 File Offset: 0x0012F6C3
		// (set) Token: 0x06003315 RID: 13077 RVA: 0x001312CB File Offset: 0x0012F6CB
		public bool sameSetting { get; set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x001312D4 File Offset: 0x0012F6D4
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x001312DC File Offset: 0x0012F6DC
		public bool autoSetting { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x001312E5 File Offset: 0x0012F6E5
		// (set) Token: 0x06003319 RID: 13081 RVA: 0x001312ED File Offset: 0x0012F6ED
		public bool ctrlTogether { get; set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x001312F6 File Offset: 0x0012F6F6
		// (set) Token: 0x0600331B RID: 13083 RVA: 0x001312FE File Offset: 0x0012F6FE
		public ChaFileHair.PartsInfo[] parts { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x00131307 File Offset: 0x0012F707
		// (set) Token: 0x0600331D RID: 13085 RVA: 0x0013130F File Offset: 0x0012F70F
		public int kind { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x00131318 File Offset: 0x0012F718
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x00131320 File Offset: 0x0012F720
		public int shaderType { get; set; }

		// Token: 0x06003320 RID: 13088 RVA: 0x0013132C File Offset: 0x0012F72C
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileHairVersion;
			this.sameSetting = true;
			this.autoSetting = true;
			this.ctrlTogether = false;
			this.parts = new ChaFileHair.PartsInfo[Enum.GetValues(typeof(ChaFileDefine.HairKind)).Length];
			for (int i = 0; i < this.parts.Length; i++)
			{
				this.parts[i] = new ChaFileHair.PartsInfo();
			}
			this.kind = 0;
			this.shaderType = 0;
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x001313AC File Offset: 0x0012F7AC
		public void ComplementWithVersion()
		{
			if (this.version < new Version("0.0.1"))
			{
				for (int i = 0; i < this.parts.Length; i++)
				{
					this.parts[i].acsColorInfo = new ChaFileHair.PartsInfo.ColorInfo[4];
					for (int j = 0; j < this.parts[i].acsColorInfo.Length; j++)
					{
						this.parts[i].acsColorInfo[j] = new ChaFileHair.PartsInfo.ColorInfo();
					}
				}
			}
			if (this.version < new Version("0.0.2"))
			{
				this.sameSetting = true;
				this.autoSetting = true;
				this.ctrlTogether = false;
			}
			if (this.version < new Version("0.0.3"))
			{
				ChaFileHair.PartsInfo.BundleInfo bundleInfo;
				if (this.parts[0].id == 4)
				{
					if (this.parts[0].dictBundle.TryGetValue(0, out bundleInfo))
					{
						bundleInfo.rotRate = new Vector3(bundleInfo.rotRate.x, 1f - bundleInfo.rotRate.y, bundleInfo.rotRate.z);
					}
				}
				else if (this.parts[0].id == 5)
				{
					if (this.parts[0].dictBundle.TryGetValue(0, out bundleInfo))
					{
						float value = Mathf.Lerp(3f, 30f, bundleInfo.rotRate.z);
						float z = Mathf.InverseLerp(-30f, 30f, value);
						bundleInfo.rotRate = new Vector3(bundleInfo.rotRate.x, bundleInfo.rotRate.y, z);
					}
					if (this.parts[0].dictBundle.TryGetValue(1, out bundleInfo))
					{
						float value2 = Mathf.Lerp(0.1f, -0.1f, bundleInfo.moveRate.z);
						float z2 = Mathf.InverseLerp(0.1f, -0.4f, value2);
						bundleInfo.moveRate = new Vector3(bundleInfo.moveRate.x, bundleInfo.moveRate.y, z2);
					}
					if (this.parts[0].dictBundle.TryGetValue(2, out bundleInfo))
					{
						float value3 = Mathf.Lerp(-25f, 45f, bundleInfo.rotRate.x);
						float x = Mathf.InverseLerp(-25f, 50f, value3);
						bundleInfo.rotRate = new Vector3(x, bundleInfo.rotRate.y, bundleInfo.rotRate.z);
					}
					if (this.parts[0].dictBundle.TryGetValue(3, out bundleInfo))
					{
						float value4 = Mathf.Lerp(-0.1f, -0.4f, bundleInfo.moveRate.z);
						float num = Mathf.InverseLerp(-0.1f, 0.4f, value4);
						bundleInfo.moveRate = new Vector3(bundleInfo.moveRate.x, bundleInfo.moveRate.y, num);
						value4 = Mathf.Lerp(-22.5f, 45f, bundleInfo.rotRate.x);
						num = Mathf.InverseLerp(45f, -22.5f, value4);
						bundleInfo.rotRate = new Vector3(num, bundleInfo.rotRate.y, bundleInfo.rotRate.z);
					}
					if (this.parts[0].dictBundle.TryGetValue(4, out bundleInfo))
					{
						float value5 = Mathf.Lerp(-22.5f, 45f, bundleInfo.rotRate.x);
						float x2 = Mathf.InverseLerp(45f, -22.5f, value5);
						bundleInfo.rotRate = new Vector3(x2, bundleInfo.rotRate.y, bundleInfo.rotRate.z);
					}
				}
				else if (this.parts[0].id == 8 && this.parts[0].dictBundle.TryGetValue(0, out bundleInfo))
				{
					bundleInfo.rotRate = new Vector3(bundleInfo.rotRate.x, 1f - bundleInfo.rotRate.y, bundleInfo.rotRate.z);
				}
				if (this.parts[1].id == 7 && this.parts[0].dictBundle.TryGetValue(0, out bundleInfo))
				{
					float value6 = Mathf.Lerp(-70f, 35f, bundleInfo.rotRate.y);
					float y = Mathf.InverseLerp(70f, -35f, value6);
					bundleInfo.rotRate = new Vector3(bundleInfo.rotRate.x, y, bundleInfo.rotRate.z);
				}
			}
			this.version = ChaFileDefine.ChaFileHairVersion;
		}

		// Token: 0x020007F6 RID: 2038
		[MessagePackObject(true)]
		public class PartsInfo
		{
			// Token: 0x06003322 RID: 13090 RVA: 0x001318BA File Offset: 0x0012FCBA
			public PartsInfo()
			{
				this.MemberInit();
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x06003323 RID: 13091 RVA: 0x001318C8 File Offset: 0x0012FCC8
			// (set) Token: 0x06003324 RID: 13092 RVA: 0x001318D0 File Offset: 0x0012FCD0
			public int id { get; set; }

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x06003325 RID: 13093 RVA: 0x001318D9 File Offset: 0x0012FCD9
			// (set) Token: 0x06003326 RID: 13094 RVA: 0x001318E1 File Offset: 0x0012FCE1
			public Color baseColor { get; set; }

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x06003327 RID: 13095 RVA: 0x001318EA File Offset: 0x0012FCEA
			// (set) Token: 0x06003328 RID: 13096 RVA: 0x001318F2 File Offset: 0x0012FCF2
			public Color topColor { get; set; }

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x06003329 RID: 13097 RVA: 0x001318FB File Offset: 0x0012FCFB
			// (set) Token: 0x0600332A RID: 13098 RVA: 0x00131903 File Offset: 0x0012FD03
			public Color underColor { get; set; }

			// Token: 0x170008FB RID: 2299
			// (get) Token: 0x0600332B RID: 13099 RVA: 0x0013190C File Offset: 0x0012FD0C
			// (set) Token: 0x0600332C RID: 13100 RVA: 0x00131914 File Offset: 0x0012FD14
			public Color specular { get; set; }

			// Token: 0x170008FC RID: 2300
			// (get) Token: 0x0600332D RID: 13101 RVA: 0x0013191D File Offset: 0x0012FD1D
			// (set) Token: 0x0600332E RID: 13102 RVA: 0x00131925 File Offset: 0x0012FD25
			public float metallic { get; set; }

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x0600332F RID: 13103 RVA: 0x0013192E File Offset: 0x0012FD2E
			// (set) Token: 0x06003330 RID: 13104 RVA: 0x00131936 File Offset: 0x0012FD36
			public float smoothness { get; set; }

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x06003331 RID: 13105 RVA: 0x0013193F File Offset: 0x0012FD3F
			// (set) Token: 0x06003332 RID: 13106 RVA: 0x00131947 File Offset: 0x0012FD47
			public ChaFileHair.PartsInfo.ColorInfo[] acsColorInfo { get; set; }

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x06003333 RID: 13107 RVA: 0x00131950 File Offset: 0x0012FD50
			// (set) Token: 0x06003334 RID: 13108 RVA: 0x00131958 File Offset: 0x0012FD58
			public int bundleId { get; set; }

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06003335 RID: 13109 RVA: 0x00131961 File Offset: 0x0012FD61
			// (set) Token: 0x06003336 RID: 13110 RVA: 0x00131969 File Offset: 0x0012FD69
			public Dictionary<int, ChaFileHair.PartsInfo.BundleInfo> dictBundle { get; set; }

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06003337 RID: 13111 RVA: 0x00131972 File Offset: 0x0012FD72
			// (set) Token: 0x06003338 RID: 13112 RVA: 0x0013197A File Offset: 0x0012FD7A
			public int meshType { get; set; }

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x06003339 RID: 13113 RVA: 0x00131983 File Offset: 0x0012FD83
			// (set) Token: 0x0600333A RID: 13114 RVA: 0x0013198B File Offset: 0x0012FD8B
			public Color meshColor { get; set; }

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x0600333B RID: 13115 RVA: 0x00131994 File Offset: 0x0012FD94
			// (set) Token: 0x0600333C RID: 13116 RVA: 0x0013199C File Offset: 0x0012FD9C
			public Vector4 meshLayout { get; set; }

			// Token: 0x0600333D RID: 13117 RVA: 0x001319A8 File Offset: 0x0012FDA8
			public void MemberInit()
			{
				this.id = 0;
				this.baseColor = new Color(0.2f, 0.2f, 0.2f);
				this.topColor = new Color(0.039f, 0.039f, 0.039f);
				this.underColor = new Color(0.565f, 0.565f, 0.565f);
				this.specular = new Color(0.3f, 0.3f, 0.3f);
				this.metallic = 0f;
				this.smoothness = 0f;
				this.acsColorInfo = new ChaFileHair.PartsInfo.ColorInfo[4];
				for (int i = 0; i < this.acsColorInfo.Length; i++)
				{
					this.acsColorInfo[i] = new ChaFileHair.PartsInfo.ColorInfo();
				}
				this.bundleId = -1;
				this.dictBundle = new Dictionary<int, ChaFileHair.PartsInfo.BundleInfo>();
				this.meshType = 0;
				this.meshColor = new Color(1f, 1f, 1f, 1f);
				this.meshLayout = new Vector4(1f, 1f, 0f, 0f);
			}

			// Token: 0x020007F7 RID: 2039
			[MessagePackObject(true)]
			public class BundleInfo
			{
				// Token: 0x0600333E RID: 13118 RVA: 0x00131AC3 File Offset: 0x0012FEC3
				public BundleInfo()
				{
					this.MemberInit();
				}

				// Token: 0x17000904 RID: 2308
				// (get) Token: 0x0600333F RID: 13119 RVA: 0x00131AD1 File Offset: 0x0012FED1
				// (set) Token: 0x06003340 RID: 13120 RVA: 0x00131AD9 File Offset: 0x0012FED9
				public Vector3 moveRate { get; set; }

				// Token: 0x17000905 RID: 2309
				// (get) Token: 0x06003341 RID: 13121 RVA: 0x00131AE2 File Offset: 0x0012FEE2
				// (set) Token: 0x06003342 RID: 13122 RVA: 0x00131AEA File Offset: 0x0012FEEA
				public Vector3 rotRate { get; set; }

				// Token: 0x17000906 RID: 2310
				// (get) Token: 0x06003343 RID: 13123 RVA: 0x00131AF3 File Offset: 0x0012FEF3
				// (set) Token: 0x06003344 RID: 13124 RVA: 0x00131AFB File Offset: 0x0012FEFB
				public bool noShake { get; set; }

				// Token: 0x06003345 RID: 13125 RVA: 0x00131B04 File Offset: 0x0012FF04
				public void MemberInit()
				{
					this.moveRate = Vector3.zero;
					this.rotRate = Vector3.zero;
					this.noShake = false;
				}
			}

			// Token: 0x020007F8 RID: 2040
			[MessagePackObject(true)]
			public class ColorInfo
			{
				// Token: 0x06003346 RID: 13126 RVA: 0x00131B23 File Offset: 0x0012FF23
				public ColorInfo()
				{
					this.MemberInit();
				}

				// Token: 0x17000907 RID: 2311
				// (get) Token: 0x06003347 RID: 13127 RVA: 0x00131B31 File Offset: 0x0012FF31
				// (set) Token: 0x06003348 RID: 13128 RVA: 0x00131B39 File Offset: 0x0012FF39
				public Color color { get; set; }

				// Token: 0x06003349 RID: 13129 RVA: 0x00131B42 File Offset: 0x0012FF42
				public void MemberInit()
				{
					this.color = Color.white;
				}
			}
		}
	}
}
