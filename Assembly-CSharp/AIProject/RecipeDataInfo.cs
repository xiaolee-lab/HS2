using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Manager;

namespace AIProject
{
	// Token: 0x02000F0E RID: 3854
	public class RecipeDataInfo
	{
		// Token: 0x06007E2F RID: 32303 RVA: 0x0035BDBC File Offset: 0x0035A1BC
		public RecipeDataInfo(RecipeData.Param param)
		{
			this.nameHash = param.nameHash;
			this.NeedList = (from s in param.NeedList
			select new RecipeDataInfo.NeedData(s) into p
			where !p.hasError
			select p).ToArray<RecipeDataInfo.NeedData>();
			this.CreateSum = param.CreateSum;
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06007E30 RID: 32304 RVA: 0x0035BE4A File Offset: 0x0035A24A
		public int nameHash { get; } = -1;

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06007E31 RID: 32305 RVA: 0x0035BE52 File Offset: 0x0035A252
		public int CreateSum { get; } = 1;

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06007E32 RID: 32306 RVA: 0x0035BE5A File Offset: 0x0035A25A
		public RecipeDataInfo.NeedData[] NeedList { get; }

		// Token: 0x02000F0F RID: 3855
		public class NeedData
		{
			// Token: 0x06007E35 RID: 32309 RVA: 0x0035BE78 File Offset: 0x0035A278
			public NeedData(RecipeData.NeedData needData)
			{
				this.info = Singleton<Resources>.Instance.GameInfo.FindItemInfo(needData.nameHash);
				if (this.hasError)
				{
					return;
				}
				this.Sum = needData.Sum;
			}

			// Token: 0x170018F2 RID: 6386
			// (get) Token: 0x06007E36 RID: 32310 RVA: 0x0035BEC5 File Offset: 0x0035A2C5
			private StuffItemInfo info { get; }

			// Token: 0x170018F3 RID: 6387
			// (get) Token: 0x06007E37 RID: 32311 RVA: 0x0035BECD File Offset: 0x0035A2CD
			public bool hasError
			{
				[CompilerGenerated]
				get
				{
					return this.info == null;
				}
			}

			// Token: 0x170018F4 RID: 6388
			// (get) Token: 0x06007E38 RID: 32312 RVA: 0x0035BED8 File Offset: 0x0035A2D8
			public string Name
			{
				[CompilerGenerated]
				get
				{
					return this.info.Name;
				}
			}

			// Token: 0x170018F5 RID: 6389
			// (get) Token: 0x06007E39 RID: 32313 RVA: 0x0035BEE5 File Offset: 0x0035A2E5
			public int nameHash
			{
				[CompilerGenerated]
				get
				{
					return this.info.nameHash;
				}
			}

			// Token: 0x170018F6 RID: 6390
			// (get) Token: 0x06007E3A RID: 32314 RVA: 0x0035BEF2 File Offset: 0x0035A2F2
			public int Sum { get; } = 1;

			// Token: 0x170018F7 RID: 6391
			// (get) Token: 0x06007E3B RID: 32315 RVA: 0x0035BEFA File Offset: 0x0035A2FA
			public int CategoryID
			{
				[CompilerGenerated]
				get
				{
					return this.info.CategoryID;
				}
			}

			// Token: 0x170018F8 RID: 6392
			// (get) Token: 0x06007E3C RID: 32316 RVA: 0x0035BF07 File Offset: 0x0035A307
			public int ID
			{
				[CompilerGenerated]
				get
				{
					return this.info.ID;
				}
			}
		}
	}
}
