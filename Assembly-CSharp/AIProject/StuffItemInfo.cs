using System;
using System.Runtime.CompilerServices;

namespace AIProject
{
	// Token: 0x02000F08 RID: 3848
	public class StuffItemInfo
	{
		// Token: 0x06007DF3 RID: 32243 RVA: 0x0035BAEA File Offset: 0x00359EEA
		public StuffItemInfo(int category, ItemData.Param param, bool isNone)
		{
			this.CategoryID = category;
			this.param = param;
			this.isNone = isNone;
			if (isNone)
			{
				this.Rarelity = (Rarelity)param.Grade;
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06007DF4 RID: 32244 RVA: 0x0035BB19 File Offset: 0x00359F19
		// (set) Token: 0x06007DF5 RID: 32245 RVA: 0x0035BB21 File Offset: 0x00359F21
		private ItemData.Param param { get; set; }

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x06007DF6 RID: 32246 RVA: 0x0035BB2A File Offset: 0x00359F2A
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.param.ID;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x06007DF7 RID: 32247 RVA: 0x0035BB37 File Offset: 0x00359F37
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.param.Name;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06007DF8 RID: 32248 RVA: 0x0035BB44 File Offset: 0x00359F44
		public int IconID
		{
			[CompilerGenerated]
			get
			{
				return this.param.IconID;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06007DF9 RID: 32249 RVA: 0x0035BB51 File Offset: 0x00359F51
		// (set) Token: 0x06007DFA RID: 32250 RVA: 0x0035BB59 File Offset: 0x00359F59
		public int CategoryID { get; private set; }

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06007DFB RID: 32251 RVA: 0x0035BB62 File Offset: 0x00359F62
		// (set) Token: 0x06007DFC RID: 32252 RVA: 0x0035BB6A File Offset: 0x00359F6A
		public Rarelity Rarelity { get; set; }

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06007DFD RID: 32253 RVA: 0x0035BB73 File Offset: 0x00359F73
		// (set) Token: 0x06007DFE RID: 32254 RVA: 0x0035BB7B File Offset: 0x00359F7B
		public int ReactionType { get; set; }

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06007DFF RID: 32255 RVA: 0x0035BB84 File Offset: 0x00359F84
		// (set) Token: 0x06007E00 RID: 32256 RVA: 0x0035BB8C File Offset: 0x00359F8C
		public bool IsAvailableHeroine { get; set; }

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x06007E01 RID: 32257 RVA: 0x0035BB95 File Offset: 0x00359F95
		// (set) Token: 0x06007E02 RID: 32258 RVA: 0x0035BB9D File Offset: 0x00359F9D
		public ThresholdInt EnnuiAddition { get; set; }

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x06007E03 RID: 32259 RVA: 0x0035BBA6 File Offset: 0x00359FA6
		// (set) Token: 0x06007E04 RID: 32260 RVA: 0x0035BBAE File Offset: 0x00359FAE
		public ThresholdInt TasteAdditionNormal { get; set; }

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06007E05 RID: 32261 RVA: 0x0035BBB7 File Offset: 0x00359FB7
		// (set) Token: 0x06007E06 RID: 32262 RVA: 0x0035BBBF File Offset: 0x00359FBF
		public ThresholdInt TasteAdditionEnnui { get; set; }

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06007E07 RID: 32263 RVA: 0x0035BBC8 File Offset: 0x00359FC8
		// (set) Token: 0x06007E08 RID: 32264 RVA: 0x0035BBD0 File Offset: 0x00359FD0
		public ItemEquipableState EquipableState { get; set; }

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06007E09 RID: 32265 RVA: 0x0035BBD9 File Offset: 0x00359FD9
		public int Rate
		{
			[CompilerGenerated]
			get
			{
				return this.param.Rate;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06007E0A RID: 32266 RVA: 0x0035BBE6 File Offset: 0x00359FE6
		public Grade Grade
		{
			[CompilerGenerated]
			get
			{
				return (Grade)this.param.Grade;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x06007E0B RID: 32267 RVA: 0x0035BBF3 File Offset: 0x00359FF3
		public string Explanation
		{
			[CompilerGenerated]
			get
			{
				return this.param.Explanation;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x06007E0C RID: 32268 RVA: 0x0035BC00 File Offset: 0x0035A000
		public bool isTrash
		{
			[CompilerGenerated]
			get
			{
				return this.param.Rate >= 0;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06007E0D RID: 32269 RVA: 0x0035BC13 File Offset: 0x0035A013
		public int nameHash
		{
			[CompilerGenerated]
			get
			{
				return this.param.nameHash;
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06007E0E RID: 32270 RVA: 0x0035BC20 File Offset: 0x0035A020
		public bool isNone { get; }
	}
}
