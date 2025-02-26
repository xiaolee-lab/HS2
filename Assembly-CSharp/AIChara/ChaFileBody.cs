using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007F4 RID: 2036
	[MessagePackObject(true)]
	public class ChaFileBody
	{
		// Token: 0x060032E4 RID: 13028 RVA: 0x00130F8A File Offset: 0x0012F38A
		public ChaFileBody()
		{
			this.MemberInit();
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x00130F98 File Offset: 0x0012F398
		// (set) Token: 0x060032E6 RID: 13030 RVA: 0x00130FA0 File Offset: 0x0012F3A0
		public Version version { get; set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x00130FA9 File Offset: 0x0012F3A9
		// (set) Token: 0x060032E8 RID: 13032 RVA: 0x00130FB1 File Offset: 0x0012F3B1
		public float[] shapeValueBody { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x00130FBA File Offset: 0x0012F3BA
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x00130FC2 File Offset: 0x0012F3C2
		public float bustSoftness { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x00130FCB File Offset: 0x0012F3CB
		// (set) Token: 0x060032EC RID: 13036 RVA: 0x00130FD3 File Offset: 0x0012F3D3
		public float bustWeight { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x00130FDC File Offset: 0x0012F3DC
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x00130FE4 File Offset: 0x0012F3E4
		public int skinId { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x00130FED File Offset: 0x0012F3ED
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x00130FF5 File Offset: 0x0012F3F5
		public int detailId { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x00130FFE File Offset: 0x0012F3FE
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x00131006 File Offset: 0x0012F406
		public float detailPower { get; set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x0013100F File Offset: 0x0012F40F
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x00131017 File Offset: 0x0012F417
		public Color skinColor { get; set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x00131020 File Offset: 0x0012F420
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x00131028 File Offset: 0x0012F428
		public float skinGlossPower { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x00131031 File Offset: 0x0012F431
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x00131039 File Offset: 0x0012F439
		public float skinMetallicPower { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x00131042 File Offset: 0x0012F442
		// (set) Token: 0x060032FA RID: 13050 RVA: 0x0013104A File Offset: 0x0012F44A
		public int sunburnId { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x00131053 File Offset: 0x0012F453
		// (set) Token: 0x060032FC RID: 13052 RVA: 0x0013105B File Offset: 0x0012F45B
		public Color sunburnColor { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x00131064 File Offset: 0x0012F464
		// (set) Token: 0x060032FE RID: 13054 RVA: 0x0013106C File Offset: 0x0012F46C
		public PaintInfo[] paintInfo { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x00131075 File Offset: 0x0012F475
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x0013107D File Offset: 0x0012F47D
		public int nipId { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x00131086 File Offset: 0x0012F486
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x0013108E File Offset: 0x0012F48E
		public Color nipColor { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x00131097 File Offset: 0x0012F497
		// (set) Token: 0x06003304 RID: 13060 RVA: 0x0013109F File Offset: 0x0012F49F
		public float nipGlossPower { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x001310A8 File Offset: 0x0012F4A8
		// (set) Token: 0x06003306 RID: 13062 RVA: 0x001310B0 File Offset: 0x0012F4B0
		public float areolaSize { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x001310B9 File Offset: 0x0012F4B9
		// (set) Token: 0x06003308 RID: 13064 RVA: 0x001310C1 File Offset: 0x0012F4C1
		public int underhairId { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x001310CA File Offset: 0x0012F4CA
		// (set) Token: 0x0600330A RID: 13066 RVA: 0x001310D2 File Offset: 0x0012F4D2
		public Color underhairColor { get; set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x001310DB File Offset: 0x0012F4DB
		// (set) Token: 0x0600330C RID: 13068 RVA: 0x001310E3 File Offset: 0x0012F4E3
		public Color nailColor { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x001310EC File Offset: 0x0012F4EC
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x001310F4 File Offset: 0x0012F4F4
		public float nailGlossPower { get; set; }

		// Token: 0x0600330F RID: 13071 RVA: 0x00131100 File Offset: 0x0012F500
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileBodyVersion;
			this.shapeValueBody = new float[ChaFileDefine.cf_bodyshapename.Length];
			for (int i = 0; i < this.shapeValueBody.Length; i++)
			{
				this.shapeValueBody[i] = ChaFileDefine.cf_bodyInitValue[i];
			}
			this.bustSoftness = 0.5f;
			this.bustWeight = 0.5f;
			this.skinId = 0;
			this.detailId = 0;
			this.detailPower = 0.5f;
			this.skinColor = new Color(0.8f, 0.7f, 0.64f);
			this.skinGlossPower = 0.7f;
			this.skinMetallicPower = 0f;
			this.sunburnId = 0;
			this.sunburnColor = Color.white;
			this.paintInfo = new PaintInfo[2];
			for (int j = 0; j < 2; j++)
			{
				this.paintInfo[j] = new PaintInfo();
			}
			this.nipId = 0;
			this.nipColor = new Color(0.76f, 0.52f, 0.52f);
			this.nipGlossPower = 0.6f;
			this.areolaSize = 0.7f;
			this.underhairId = 0;
			this.underhairColor = new Color(0.05f, 0.05f, 0.05f);
			this.nailColor = new Color(1f, 0.92f, 0.92f);
			this.nailGlossPower = 0.6f;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x0013126B File Offset: 0x0012F66B
		public void ComplementWithVersion()
		{
			if (this.version < new Version("0.0.1"))
			{
				this.bustWeight *= 0.1f;
			}
			this.version = ChaFileDefine.ChaFileBodyVersion;
		}
	}
}
