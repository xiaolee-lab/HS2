using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x02000807 RID: 2055
	[MessagePackObject(true)]
	public class ChaFileGameInfo2
	{
		// Token: 0x0600338B RID: 13195 RVA: 0x00132BC5 File Offset: 0x00130FC5
		public ChaFileGameInfo2()
		{
			this.MemberInit();
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x00132BF6 File Offset: 0x00130FF6
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x00132BFE File Offset: 0x00130FFE
		public Version version { get; set; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x00132C07 File Offset: 0x00131007
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x00132C0F File Offset: 0x0013100F
		public int Favor
		{
			get
			{
				return this.favor;
			}
			set
			{
				this.favor = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x00132C20 File Offset: 0x00131020
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x00132C28 File Offset: 0x00131028
		public int Enjoyment
		{
			get
			{
				return this.enjoyment;
			}
			set
			{
				this.enjoyment = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x00132C39 File Offset: 0x00131039
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x00132C41 File Offset: 0x00131041
		public int Aversion
		{
			get
			{
				return this.aversion;
			}
			set
			{
				this.aversion = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x00132C52 File Offset: 0x00131052
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x00132C5A File Offset: 0x0013105A
		public int Slavery
		{
			get
			{
				return this.slavery;
			}
			set
			{
				this.slavery = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x00132C6B File Offset: 0x0013106B
		// (set) Token: 0x06003397 RID: 13207 RVA: 0x00132C73 File Offset: 0x00131073
		public int Broken
		{
			get
			{
				return this.broken;
			}
			set
			{
				this.broken = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x00132C84 File Offset: 0x00131084
		// (set) Token: 0x06003399 RID: 13209 RVA: 0x00132C8C File Offset: 0x0013108C
		public int Dependence
		{
			get
			{
				return this.dependence;
			}
			set
			{
				this.dependence = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x00132C9D File Offset: 0x0013109D
		// (set) Token: 0x0600339B RID: 13211 RVA: 0x00132CA5 File Offset: 0x001310A5
		public int Dirty
		{
			get
			{
				return this.dirty;
			}
			set
			{
				this.dirty = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x00132CB6 File Offset: 0x001310B6
		// (set) Token: 0x0600339D RID: 13213 RVA: 0x00132CBE File Offset: 0x001310BE
		public int Tiredness
		{
			get
			{
				return this.tiredness;
			}
			set
			{
				this.tiredness = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x00132CCF File Offset: 0x001310CF
		// (set) Token: 0x0600339F RID: 13215 RVA: 0x00132CD7 File Offset: 0x001310D7
		public int Toilet
		{
			get
			{
				return this.toilet;
			}
			set
			{
				this.toilet = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x00132CE8 File Offset: 0x001310E8
		// (set) Token: 0x060033A1 RID: 13217 RVA: 0x00132CF0 File Offset: 0x001310F0
		public int Libido
		{
			get
			{
				return this.libido;
			}
			set
			{
				this.libido = Mathf.Clamp(value, 0, 100);
			}
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00132D04 File Offset: 0x00131104
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileGameInfoVersion;
			this.favor = 0;
			this.enjoyment = 0;
			this.aversion = 0;
			this.slavery = 0;
			this.broken = 0;
			this.dependence = 0;
			this.nowState = ChaFileDefine.State.Blank;
			this.nowDrawState = ChaFileDefine.State.Blank;
			this.lockNowState = false;
			this.lockBroken = false;
			this.lockDependence = false;
			this.dirty = 0;
			this.tiredness = 0;
			this.toilet = 0;
			this.libido = 0;
			this.alertness = 0;
			this.calcState = ChaFileDefine.State.Blank;
			this.escapeFlag = 0;
			this.escapeExperienced = false;
			this.firstHFlag = false;
			this.genericVoice = new bool[2][];
			this.genericVoice[0] = new bool[13];
			this.genericVoice[1] = new bool[13];
			this.genericBrokenVoice = false;
			this.genericDependencepVoice = false;
			this.genericAnalVoice = false;
			this.genericPainVoice = false;
			this.genericFlag = false;
			this.genericBefore = false;
			this.inviteVoice = new bool[5];
			this.hCount = 0;
			this.map = new HashSet<int>();
			this.arriveRoom50 = false;
			this.arriveRoom80 = false;
			this.arriveRoomHAfter = false;
			this.resistH = 0;
			this.resistPain = 0;
			this.resistAnal = 0;
			this.usedItem = 0;
			this.isChangeParameter = false;
			this.isConcierge = false;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00132E5C File Offset: 0x0013125C
		public void Copy(ChaFileGameInfo2 src)
		{
			this.version = src.version;
			this.favor = src.favor;
			this.enjoyment = src.enjoyment;
			this.aversion = src.aversion;
			this.slavery = src.slavery;
			this.broken = src.broken;
			this.dependence = src.dependence;
			this.nowState = src.nowState;
			this.nowDrawState = src.nowDrawState;
			this.lockNowState = src.lockNowState;
			this.lockBroken = src.lockBroken;
			this.lockDependence = src.lockDependence;
			this.dirty = src.dirty;
			this.tiredness = src.tiredness;
			this.toilet = src.toilet;
			this.libido = src.libido;
			this.alertness = src.alertness;
			this.calcState = src.calcState;
			this.escapeFlag = src.escapeFlag;
			this.escapeExperienced = src.escapeExperienced;
			this.firstHFlag = src.firstHFlag;
			Array.Copy(src.genericVoice, this.genericVoice, src.genericVoice.Length);
			this.genericBrokenVoice = src.genericBrokenVoice;
			this.genericDependencepVoice = src.genericDependencepVoice;
			this.genericAnalVoice = src.genericAnalVoice;
			this.genericPainVoice = src.genericPainVoice;
			this.genericFlag = src.genericFlag;
			this.genericBefore = src.genericBefore;
			Array.Copy(src.inviteVoice, this.inviteVoice, src.inviteVoice.Length);
			this.hCount = src.hCount;
			this.map = new HashSet<int>(src.map);
			this.arriveRoom50 = src.arriveRoom50;
			this.arriveRoom80 = src.arriveRoom80;
			this.arriveRoomHAfter = src.arriveRoomHAfter;
			this.resistH = src.resistH;
			this.resistPain = src.resistPain;
			this.resistAnal = src.resistAnal;
			this.usedItem = src.usedItem;
			this.isChangeParameter = src.isChangeParameter;
			this.isConcierge = src.isConcierge;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00133068 File Offset: 0x00131468
		public void ComplementWithVersion()
		{
			this.version = ChaFileDefine.ChaFileGameInfoVersion;
		}

		// Token: 0x040033BE RID: 13246
		[IgnoreMember]
		public static readonly string BlockName = "GameInfo2";

		// Token: 0x040033C0 RID: 13248
		private int favor;

		// Token: 0x040033C1 RID: 13249
		private int enjoyment;

		// Token: 0x040033C2 RID: 13250
		private int aversion;

		// Token: 0x040033C3 RID: 13251
		private int slavery;

		// Token: 0x040033C4 RID: 13252
		private int broken;

		// Token: 0x040033C5 RID: 13253
		private int dependence;

		// Token: 0x040033C6 RID: 13254
		public ChaFileDefine.State nowState;

		// Token: 0x040033C7 RID: 13255
		public ChaFileDefine.State nowDrawState;

		// Token: 0x040033C8 RID: 13256
		public bool lockNowState;

		// Token: 0x040033C9 RID: 13257
		public bool lockBroken;

		// Token: 0x040033CA RID: 13258
		public bool lockDependence;

		// Token: 0x040033CB RID: 13259
		private int dirty;

		// Token: 0x040033CC RID: 13260
		private int tiredness;

		// Token: 0x040033CD RID: 13261
		private int toilet;

		// Token: 0x040033CE RID: 13262
		private int libido;

		// Token: 0x040033CF RID: 13263
		public int alertness;

		// Token: 0x040033D0 RID: 13264
		public ChaFileDefine.State calcState;

		// Token: 0x040033D1 RID: 13265
		public byte escapeFlag;

		// Token: 0x040033D2 RID: 13266
		public bool escapeExperienced;

		// Token: 0x040033D3 RID: 13267
		public bool firstHFlag;

		// Token: 0x040033D4 RID: 13268
		public bool[][] genericVoice = new bool[2][];

		// Token: 0x040033D5 RID: 13269
		public bool genericBrokenVoice;

		// Token: 0x040033D6 RID: 13270
		public bool genericDependencepVoice;

		// Token: 0x040033D7 RID: 13271
		public bool genericAnalVoice;

		// Token: 0x040033D8 RID: 13272
		public bool genericPainVoice;

		// Token: 0x040033D9 RID: 13273
		public bool genericFlag;

		// Token: 0x040033DA RID: 13274
		public bool genericBefore;

		// Token: 0x040033DB RID: 13275
		public bool[] inviteVoice = new bool[5];

		// Token: 0x040033DC RID: 13276
		public int hCount;

		// Token: 0x040033DD RID: 13277
		public HashSet<int> map = new HashSet<int>();

		// Token: 0x040033DE RID: 13278
		public bool arriveRoom50;

		// Token: 0x040033DF RID: 13279
		public bool arriveRoom80;

		// Token: 0x040033E0 RID: 13280
		public bool arriveRoomHAfter;

		// Token: 0x040033E1 RID: 13281
		public int resistH;

		// Token: 0x040033E2 RID: 13282
		public int resistPain;

		// Token: 0x040033E3 RID: 13283
		public int resistAnal;

		// Token: 0x040033E4 RID: 13284
		public int usedItem;

		// Token: 0x040033E5 RID: 13285
		public bool isChangeParameter;

		// Token: 0x040033E6 RID: 13286
		public bool isConcierge;
	}
}
