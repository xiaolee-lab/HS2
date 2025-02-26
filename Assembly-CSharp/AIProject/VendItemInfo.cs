using System;
using System.Runtime.CompilerServices;

namespace AIProject
{
	// Token: 0x02000F0A RID: 3850
	public class VendItemInfo
	{
		// Token: 0x06007E0F RID: 32271 RVA: 0x0035BC28 File Offset: 0x0035A028
		private VendItemInfo(StuffItemInfo info)
		{
			this.info = info;
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x0035BC4F File Offset: 0x0035A04F
		public VendItemInfo(StuffItemInfo info, VendData.Param param) : this(info)
		{
			this.Rate = param.Rate;
			this.Stocks = param.Stocks;
			this.Percent = param.Percent;
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x0035BC7C File Offset: 0x0035A07C
		public VendItemInfo(StuffItemInfo info, VendSpecialData.Param param) : this(info)
		{
			this.Rate = param.Rate;
			this.Stocks = new int[]
			{
				param.Stock
			};
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06007E12 RID: 32274 RVA: 0x0035BCA6 File Offset: 0x0035A0A6
		private StuffItemInfo info { get; }

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06007E13 RID: 32275 RVA: 0x0035BCAE File Offset: 0x0035A0AE
		public int CategoryID
		{
			[CompilerGenerated]
			get
			{
				return this.info.CategoryID;
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06007E14 RID: 32276 RVA: 0x0035BCBB File Offset: 0x0035A0BB
		public int ID
		{
			[CompilerGenerated]
			get
			{
				return this.info.ID;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06007E15 RID: 32277 RVA: 0x0035BCC8 File Offset: 0x0035A0C8
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.info.Name;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06007E16 RID: 32278 RVA: 0x0035BCD5 File Offset: 0x0035A0D5
		public int[] Stocks { get; } = new int[]
		{
			1
		};

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06007E17 RID: 32279 RVA: 0x0035BCDD File Offset: 0x0035A0DD
		public int Rate { get; }

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06007E18 RID: 32280 RVA: 0x0035BCE5 File Offset: 0x0035A0E5
		public int Percent { get; } = 100;
	}
}
