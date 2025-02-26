using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x02000808 RID: 2056
	[MessagePackObject(true)]
	public class ChaFileParameter
	{
		// Token: 0x060033A6 RID: 13222 RVA: 0x00133081 File Offset: 0x00131481
		public ChaFileParameter()
		{
			this.MemberInit();
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x0013308F File Offset: 0x0013148F
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x00133097 File Offset: 0x00131497
		public Version version { get; set; }

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x001330A0 File Offset: 0x001314A0
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x001330A8 File Offset: 0x001314A8
		public byte sex { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x001330B1 File Offset: 0x001314B1
		// (set) Token: 0x060033AC RID: 13228 RVA: 0x001330B9 File Offset: 0x001314B9
		public string fullname { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x001330C2 File Offset: 0x001314C2
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x001330CA File Offset: 0x001314CA
		public int personality { get; set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x001330D3 File Offset: 0x001314D3
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x001330DB File Offset: 0x001314DB
		public byte birthMonth { get; set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x001330E4 File Offset: 0x001314E4
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x001330EC File Offset: 0x001314EC
		public byte birthDay { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x001330F5 File Offset: 0x001314F5
		[IgnoreMember]
		public string strBirthDay
		{
			get
			{
				return ChaFileDefine.GetBirthdayStr((int)this.birthMonth, (int)this.birthDay, "ja-JP");
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x0013310D File Offset: 0x0013150D
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x00133115 File Offset: 0x00131515
		public float voiceRate { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x0013311E File Offset: 0x0013151E
		[IgnoreMember]
		public float voicePitch
		{
			get
			{
				return Mathf.Lerp(0.94f, 1.06f, this.voiceRate);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x00133135 File Offset: 0x00131535
		// (set) Token: 0x060033B8 RID: 13240 RVA: 0x0013313D File Offset: 0x0013153D
		public HashSet<int> hsWish { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x00133146 File Offset: 0x00131546
		[IgnoreMember]
		public int wish01
		{
			get
			{
				if (this.hsWish.Count == 0)
				{
					return -1;
				}
				return this.hsWish.ToArray<int>()[0];
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x00133167 File Offset: 0x00131567
		[IgnoreMember]
		public int wish02
		{
			get
			{
				if (1 >= this.hsWish.Count)
				{
					return -1;
				}
				return this.hsWish.ToArray<int>()[1];
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x00133189 File Offset: 0x00131589
		[IgnoreMember]
		public int wish03
		{
			get
			{
				if (2 >= this.hsWish.Count)
				{
					return -1;
				}
				return this.hsWish.ToArray<int>()[2];
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x001331AB File Offset: 0x001315AB
		// (set) Token: 0x060033BD RID: 13245 RVA: 0x001331B3 File Offset: 0x001315B3
		public bool futanari { get; set; }

		// Token: 0x060033BE RID: 13246 RVA: 0x001331BC File Offset: 0x001315BC
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileParameterVersion;
			this.sex = 0;
			this.fullname = string.Empty;
			this.personality = 0;
			this.birthMonth = 1;
			this.birthDay = 1;
			this.voiceRate = 0.5f;
			this.hsWish = new HashSet<int>();
			this.futanari = false;
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x00133218 File Offset: 0x00131618
		public void Copy(ChaFileParameter src)
		{
			this.version = src.version;
			this.sex = src.sex;
			this.fullname = src.fullname;
			this.personality = src.personality;
			this.birthMonth = src.birthMonth;
			this.birthDay = src.birthDay;
			this.voiceRate = src.voiceRate;
			this.hsWish = new HashSet<int>(src.hsWish);
			this.futanari = src.futanari;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x00133296 File Offset: 0x00131696
		public void ComplementWithVersion()
		{
			if (this.version < new Version("0.0.1"))
			{
				this.hsWish = new HashSet<int>();
			}
			this.version = ChaFileDefine.ChaFileParameterVersion;
		}

		// Token: 0x040033E7 RID: 13287
		[IgnoreMember]
		public static readonly string BlockName = "Parameter";
	}
}
