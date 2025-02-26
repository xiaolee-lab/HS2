using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x02000809 RID: 2057
	[MessagePackObject(true)]
	public class ChaFileParameter2
	{
		// Token: 0x060033C2 RID: 13250 RVA: 0x001332D4 File Offset: 0x001316D4
		public ChaFileParameter2()
		{
			this.MemberInit();
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x001332E2 File Offset: 0x001316E2
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x001332EA File Offset: 0x001316EA
		public Version version { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x001332F3 File Offset: 0x001316F3
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x001332FB File Offset: 0x001316FB
		public int personality { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x00133304 File Offset: 0x00131704
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x0013330C File Offset: 0x0013170C
		public float voiceRate { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x00133315 File Offset: 0x00131715
		[IgnoreMember]
		public float voicePitch
		{
			get
			{
				return Mathf.Lerp(0.94f, 1.06f, this.voiceRate);
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x0013332C File Offset: 0x0013172C
		// (set) Token: 0x060033CB RID: 13259 RVA: 0x00133334 File Offset: 0x00131734
		public byte trait { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x0013333D File Offset: 0x0013173D
		// (set) Token: 0x060033CD RID: 13261 RVA: 0x00133345 File Offset: 0x00131745
		public byte mind { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x0013334E File Offset: 0x0013174E
		// (set) Token: 0x060033CF RID: 13263 RVA: 0x00133356 File Offset: 0x00131756
		public byte hAttribute { get; set; }

		// Token: 0x060033D0 RID: 13264 RVA: 0x0013335F File Offset: 0x0013175F
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileParameterVersion2;
			this.personality = 0;
			this.voiceRate = 0.5f;
			this.trait = 0;
			this.mind = 0;
			this.hAttribute = 0;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x00133394 File Offset: 0x00131794
		public void Copy(ChaFileParameter2 src)
		{
			this.version = src.version;
			this.personality = src.personality;
			this.voiceRate = src.voiceRate;
			this.trait = src.trait;
			this.mind = src.mind;
			this.hAttribute = src.hAttribute;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x001333E9 File Offset: 0x001317E9
		public void ComplementWithVersion()
		{
			this.version = ChaFileDefine.ChaFileParameterVersion2;
		}

		// Token: 0x040033F1 RID: 13297
		[IgnoreMember]
		public static readonly string BlockName = "Parameter2";
	}
}
