using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007EC RID: 2028
	[MessagePackObject(true)]
	public class ChaFileAccessory
	{
		// Token: 0x06003239 RID: 12857 RVA: 0x0012FE66 File Offset: 0x0012E266
		public ChaFileAccessory()
		{
			this.MemberInit();
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x0012FE74 File Offset: 0x0012E274
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x0012FE7C File Offset: 0x0012E27C
		public Version version { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x0012FE85 File Offset: 0x0012E285
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x0012FE8D File Offset: 0x0012E28D
		public ChaFileAccessory.PartsInfo[] parts { get; set; }

		// Token: 0x0600323E RID: 12862 RVA: 0x0012FE98 File Offset: 0x0012E298
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileAccessoryVersion;
			this.parts = new ChaFileAccessory.PartsInfo[20];
			for (int i = 0; i < this.parts.Length; i++)
			{
				this.parts[i] = new ChaFileAccessory.PartsInfo();
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x0012FEE3 File Offset: 0x0012E2E3
		public void ComplementWithVersion()
		{
			this.version = ChaFileDefine.ChaFileAccessoryVersion;
		}

		// Token: 0x020007ED RID: 2029
		[MessagePackObject(true)]
		public class PartsInfo
		{
			// Token: 0x06003240 RID: 12864 RVA: 0x0012FEF0 File Offset: 0x0012E2F0
			public PartsInfo()
			{
				this.MemberInit();
			}

			// Token: 0x17000898 RID: 2200
			// (get) Token: 0x06003241 RID: 12865 RVA: 0x0012FEFE File Offset: 0x0012E2FE
			// (set) Token: 0x06003242 RID: 12866 RVA: 0x0012FF06 File Offset: 0x0012E306
			public int type { get; set; }

			// Token: 0x17000899 RID: 2201
			// (get) Token: 0x06003243 RID: 12867 RVA: 0x0012FF0F File Offset: 0x0012E30F
			// (set) Token: 0x06003244 RID: 12868 RVA: 0x0012FF17 File Offset: 0x0012E317
			public int id { get; set; }

			// Token: 0x1700089A RID: 2202
			// (get) Token: 0x06003245 RID: 12869 RVA: 0x0012FF20 File Offset: 0x0012E320
			// (set) Token: 0x06003246 RID: 12870 RVA: 0x0012FF28 File Offset: 0x0012E328
			public string parentKey { get; set; }

			// Token: 0x1700089B RID: 2203
			// (get) Token: 0x06003247 RID: 12871 RVA: 0x0012FF31 File Offset: 0x0012E331
			// (set) Token: 0x06003248 RID: 12872 RVA: 0x0012FF39 File Offset: 0x0012E339
			public Vector3[,] addMove { get; set; }

			// Token: 0x1700089C RID: 2204
			// (get) Token: 0x06003249 RID: 12873 RVA: 0x0012FF42 File Offset: 0x0012E342
			// (set) Token: 0x0600324A RID: 12874 RVA: 0x0012FF4A File Offset: 0x0012E34A
			public ChaFileAccessory.PartsInfo.ColorInfo[] colorInfo { get; set; }

			// Token: 0x1700089D RID: 2205
			// (get) Token: 0x0600324B RID: 12875 RVA: 0x0012FF53 File Offset: 0x0012E353
			// (set) Token: 0x0600324C RID: 12876 RVA: 0x0012FF5B File Offset: 0x0012E35B
			public int hideCategory { get; set; }

			// Token: 0x1700089E RID: 2206
			// (get) Token: 0x0600324D RID: 12877 RVA: 0x0012FF64 File Offset: 0x0012E364
			// (set) Token: 0x0600324E RID: 12878 RVA: 0x0012FF6C File Offset: 0x0012E36C
			public int hideTiming { get; set; }

			// Token: 0x1700089F RID: 2207
			// (get) Token: 0x0600324F RID: 12879 RVA: 0x0012FF75 File Offset: 0x0012E375
			// (set) Token: 0x06003250 RID: 12880 RVA: 0x0012FF7D File Offset: 0x0012E37D
			public bool noShake { get; set; }

			// Token: 0x170008A0 RID: 2208
			// (get) Token: 0x06003251 RID: 12881 RVA: 0x0012FF86 File Offset: 0x0012E386
			// (set) Token: 0x06003252 RID: 12882 RVA: 0x0012FF8E File Offset: 0x0012E38E
			[IgnoreMember]
			public bool partsOfHead { get; set; }

			// Token: 0x06003253 RID: 12883 RVA: 0x0012FF98 File Offset: 0x0012E398
			public void MemberInit()
			{
				this.type = 120;
				this.id = 0;
				this.parentKey = string.Empty;
				this.addMove = new Vector3[2, 3];
				for (int i = 0; i < 2; i++)
				{
					this.addMove[i, 0] = Vector3.zero;
					this.addMove[i, 1] = Vector3.zero;
					this.addMove[i, 2] = Vector3.one;
				}
				this.colorInfo = new ChaFileAccessory.PartsInfo.ColorInfo[4];
				for (int j = 0; j < this.colorInfo.Length; j++)
				{
					this.colorInfo[j] = new ChaFileAccessory.PartsInfo.ColorInfo();
				}
				this.hideCategory = 0;
				this.hideTiming = 1;
				this.partsOfHead = false;
				this.noShake = false;
			}

			// Token: 0x020007EE RID: 2030
			[MessagePackObject(true)]
			public class ColorInfo
			{
				// Token: 0x06003254 RID: 12884 RVA: 0x00130071 File Offset: 0x0012E471
				public ColorInfo()
				{
					this.MemberInit();
				}

				// Token: 0x170008A1 RID: 2209
				// (get) Token: 0x06003255 RID: 12885 RVA: 0x0013007F File Offset: 0x0012E47F
				// (set) Token: 0x06003256 RID: 12886 RVA: 0x00130087 File Offset: 0x0012E487
				public Color color { get; set; }

				// Token: 0x170008A2 RID: 2210
				// (get) Token: 0x06003257 RID: 12887 RVA: 0x00130090 File Offset: 0x0012E490
				// (set) Token: 0x06003258 RID: 12888 RVA: 0x00130098 File Offset: 0x0012E498
				public float glossPower { get; set; }

				// Token: 0x170008A3 RID: 2211
				// (get) Token: 0x06003259 RID: 12889 RVA: 0x001300A1 File Offset: 0x0012E4A1
				// (set) Token: 0x0600325A RID: 12890 RVA: 0x001300A9 File Offset: 0x0012E4A9
				public float metallicPower { get; set; }

				// Token: 0x170008A4 RID: 2212
				// (get) Token: 0x0600325B RID: 12891 RVA: 0x001300B2 File Offset: 0x0012E4B2
				// (set) Token: 0x0600325C RID: 12892 RVA: 0x001300BA File Offset: 0x0012E4BA
				public float smoothnessPower { get; set; }

				// Token: 0x0600325D RID: 12893 RVA: 0x001300C3 File Offset: 0x0012E4C3
				public void MemberInit()
				{
					this.color = Color.white;
					this.glossPower = 0.5f;
					this.metallicPower = 0.5f;
					this.smoothnessPower = 0.5f;
				}
			}
		}
	}
}
