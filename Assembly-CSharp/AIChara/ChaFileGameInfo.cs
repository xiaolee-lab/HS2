using System;
using System.Collections.Generic;
using MessagePack;

namespace AIChara
{
	// Token: 0x02000805 RID: 2053
	[MessagePackObject(true)]
	public class ChaFileGameInfo
	{
		// Token: 0x0600335B RID: 13147 RVA: 0x001325BB File Offset: 0x001309BB
		public ChaFileGameInfo()
		{
			this.MemberInit();
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x001325C9 File Offset: 0x001309C9
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x001325D1 File Offset: 0x001309D1
		public Version version { get; set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x001325DA File Offset: 0x001309DA
		// (set) Token: 0x0600335F RID: 13151 RVA: 0x001325E2 File Offset: 0x001309E2
		public bool gameRegistration { get; set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x001325EB File Offset: 0x001309EB
		// (set) Token: 0x06003361 RID: 13153 RVA: 0x001325F3 File Offset: 0x001309F3
		public ChaFileGameInfo.MinMaxInfo tempBound { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x001325FC File Offset: 0x001309FC
		// (set) Token: 0x06003363 RID: 13155 RVA: 0x00132604 File Offset: 0x00130A04
		public ChaFileGameInfo.MinMaxInfo moodBound { get; set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x0013260D File Offset: 0x00130A0D
		// (set) Token: 0x06003365 RID: 13157 RVA: 0x00132615 File Offset: 0x00130A15
		public Dictionary<int, int> flavorState { get; set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x0013261E File Offset: 0x00130A1E
		// (set) Token: 0x06003367 RID: 13159 RVA: 0x00132626 File Offset: 0x00130A26
		public int totalFlavor { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x0013262F File Offset: 0x00130A2F
		// (set) Token: 0x06003369 RID: 13161 RVA: 0x00132637 File Offset: 0x00130A37
		public Dictionary<int, float> desireDefVal { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x00132640 File Offset: 0x00130A40
		// (set) Token: 0x0600336B RID: 13163 RVA: 0x00132648 File Offset: 0x00130A48
		public Dictionary<int, float> desireBuffVal { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x00132651 File Offset: 0x00130A51
		// (set) Token: 0x0600336D RID: 13165 RVA: 0x00132659 File Offset: 0x00130A59
		public int phase { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x00132662 File Offset: 0x00130A62
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x0013266A File Offset: 0x00130A6A
		public Dictionary<int, int> normalSkill { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x00132673 File Offset: 0x00130A73
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x0013267B File Offset: 0x00130A7B
		public Dictionary<int, int> hSkill { get; set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x00132684 File Offset: 0x00130A84
		// (set) Token: 0x06003373 RID: 13171 RVA: 0x0013268C File Offset: 0x00130A8C
		public int favoritePlace { get; set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x00132695 File Offset: 0x00130A95
		// (set) Token: 0x06003375 RID: 13173 RVA: 0x0013269D File Offset: 0x00130A9D
		public int lifestyle { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x001326A6 File Offset: 0x00130AA6
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x001326AE File Offset: 0x00130AAE
		public int morality { get; set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x001326B7 File Offset: 0x00130AB7
		// (set) Token: 0x06003379 RID: 13177 RVA: 0x001326BF File Offset: 0x00130ABF
		public int motivation { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x001326C8 File Offset: 0x00130AC8
		// (set) Token: 0x0600337B RID: 13179 RVA: 0x001326D0 File Offset: 0x00130AD0
		public int immoral { get; set; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x001326D9 File Offset: 0x00130AD9
		// (set) Token: 0x0600337D RID: 13181 RVA: 0x001326E1 File Offset: 0x00130AE1
		public bool isHAddTaii0 { get; set; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x001326EA File Offset: 0x00130AEA
		// (set) Token: 0x0600337F RID: 13183 RVA: 0x001326F2 File Offset: 0x00130AF2
		public bool isHAddTaii1 { get; set; }

		// Token: 0x06003380 RID: 13184 RVA: 0x001326FC File Offset: 0x00130AFC
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileGameInfoVersion;
			this.gameRegistration = false;
			this.tempBound = new ChaFileGameInfo.MinMaxInfo();
			this.moodBound = new ChaFileGameInfo.MinMaxInfo();
			this.flavorState = new Dictionary<int, int>();
			for (int i = 0; i < 8; i++)
			{
				this.flavorState[i] = 0;
			}
			this.totalFlavor = 0;
			this.desireDefVal = new Dictionary<int, float>();
			this.desireBuffVal = new Dictionary<int, float>();
			for (int j = 0; j < 16; j++)
			{
				this.desireDefVal[j] = 0f;
				this.desireBuffVal[j] = 0f;
			}
			this.phase = 0;
			this.normalSkill = new Dictionary<int, int>();
			this.hSkill = new Dictionary<int, int>();
			for (int k = 0; k < 5; k++)
			{
				this.normalSkill[k] = -1;
				this.hSkill[k] = -1;
			}
			this.favoritePlace = -1;
			this.lifestyle = -1;
			this.morality = 50;
			this.motivation = 0;
			this.immoral = 0;
			this.isHAddTaii0 = false;
			this.isHAddTaii1 = false;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x00132828 File Offset: 0x00130C28
		public void Copy(ChaFileGameInfo src)
		{
			this.version = src.version;
			this.gameRegistration = src.gameRegistration;
			this.tempBound.Copy(src.tempBound);
			this.moodBound.Copy(src.moodBound);
			this.flavorState = new Dictionary<int, int>(src.flavorState);
			this.totalFlavor = src.totalFlavor;
			this.desireDefVal = new Dictionary<int, float>(src.desireDefVal);
			this.desireBuffVal = new Dictionary<int, float>(src.desireBuffVal);
			this.phase = src.phase;
			this.normalSkill = new Dictionary<int, int>(src.normalSkill);
			this.hSkill = new Dictionary<int, int>(src.hSkill);
			this.favoritePlace = src.favoritePlace;
			this.lifestyle = src.lifestyle;
			this.morality = src.morality;
			this.motivation = src.motivation;
			this.immoral = src.immoral;
			this.isHAddTaii0 = src.isHAddTaii0;
			this.isHAddTaii1 = src.isHAddTaii1;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x00132930 File Offset: 0x00130D30
		public void ComplementWithVersion()
		{
			if (this.flavorState == null || this.flavorState.Count == 0)
			{
				this.flavorState = new Dictionary<int, int>();
				for (int i = 0; i < 8; i++)
				{
					this.flavorState[i] = 0;
				}
			}
			if (this.desireDefVal == null || this.desireDefVal.Count == 0)
			{
				this.desireDefVal = new Dictionary<int, float>();
				for (int j = 0; j < 16; j++)
				{
					this.desireDefVal[j] = 0f;
				}
			}
			if (this.desireBuffVal == null || this.desireBuffVal.Count == 0)
			{
				this.desireBuffVal = new Dictionary<int, float>();
				for (int k = 0; k < 16; k++)
				{
					this.desireBuffVal[k] = 0f;
				}
			}
			if (this.tempBound.lower == 0f && this.tempBound.upper == 0f)
			{
				this.tempBound.lower = 20f;
				this.tempBound.upper = 80f;
			}
			if (this.moodBound.lower == 0f && this.moodBound.upper == 0f)
			{
				this.moodBound.lower = 20f;
				this.moodBound.upper = 80f;
			}
			if (this.phase < 3)
			{
				this.lifestyle = -1;
			}
			if (this.normalSkill == null || this.normalSkill.Count == 0)
			{
				this.normalSkill = new Dictionary<int, int>();
				for (int l = 0; l < 5; l++)
				{
					this.normalSkill[l] = -1;
				}
			}
			if (this.hSkill == null || this.hSkill.Count == 0)
			{
				this.hSkill = new Dictionary<int, int>();
				for (int m = 0; m < 5; m++)
				{
					this.hSkill[m] = -1;
				}
			}
			this.version = ChaFileDefine.ChaFileGameInfoVersion;
		}

		// Token: 0x040033A9 RID: 13225
		[IgnoreMember]
		public static readonly string BlockName = "GameInfo";

		// Token: 0x02000806 RID: 2054
		[MessagePackObject(true)]
		public class MinMaxInfo
		{
			// Token: 0x06003384 RID: 13188 RVA: 0x00132B63 File Offset: 0x00130F63
			public MinMaxInfo()
			{
				this.MemberInit();
			}

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x06003385 RID: 13189 RVA: 0x00132B71 File Offset: 0x00130F71
			// (set) Token: 0x06003386 RID: 13190 RVA: 0x00132B79 File Offset: 0x00130F79
			public float lower { get; set; }

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x06003387 RID: 13191 RVA: 0x00132B82 File Offset: 0x00130F82
			// (set) Token: 0x06003388 RID: 13192 RVA: 0x00132B8A File Offset: 0x00130F8A
			public float upper { get; set; }

			// Token: 0x06003389 RID: 13193 RVA: 0x00132B93 File Offset: 0x00130F93
			public void MemberInit()
			{
				this.lower = 20f;
				this.upper = 80f;
			}

			// Token: 0x0600338A RID: 13194 RVA: 0x00132BAB File Offset: 0x00130FAB
			public void Copy(ChaFileGameInfo.MinMaxInfo src)
			{
				this.lower = src.lower;
				this.upper = src.upper;
			}
		}
	}
}
