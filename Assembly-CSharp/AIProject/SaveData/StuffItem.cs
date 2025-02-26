using System;
using System.Collections.Generic;
using ADV;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x02000989 RID: 2441
	[MessagePackObject(false)]
	public class StuffItem : ICommandData
	{
		// Token: 0x060045FB RID: 17915 RVA: 0x001ABF22 File Offset: 0x001AA322
		public StuffItem()
		{
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x001ABF2A File Offset: 0x001AA32A
		public StuffItem(int category, int id, int count)
		{
			this.CategoryID = category;
			this.ID = id;
			this.Count = count;
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x001ABF47 File Offset: 0x001AA347
		public StuffItem(StuffItem source)
		{
			this.CategoryID = source.CategoryID;
			this.ID = source.ID;
			this.Count = source.Count;
			this.LatestDateTime = source.LatestDateTime;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x001ABF7F File Offset: 0x001AA37F
		// (set) Token: 0x060045FF RID: 17919 RVA: 0x001ABF87 File Offset: 0x001AA387
		[Key(0)]
		public int CategoryID { get; set; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x001ABF90 File Offset: 0x001AA390
		// (set) Token: 0x06004601 RID: 17921 RVA: 0x001ABF98 File Offset: 0x001AA398
		[Key(1)]
		public int ID { get; set; }

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x001ABFA1 File Offset: 0x001AA3A1
		// (set) Token: 0x06004603 RID: 17923 RVA: 0x001ABFA9 File Offset: 0x001AA3A9
		[Key(2)]
		public int Count { get; set; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x001ABFB2 File Offset: 0x001AA3B2
		// (set) Token: 0x06004605 RID: 17925 RVA: 0x001ABFBA File Offset: 0x001AA3BA
		[Key(3)]
		public DateTime LatestDateTime { get; set; }

		// Token: 0x06004606 RID: 17926 RVA: 0x001ABFC4 File Offset: 0x001AA3C4
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			return new CommandData[]
			{
				new CommandData(CommandData.Command.Int, head + string.Format(".{0}", "CategoryID"), () => this.CategoryID, null),
				new CommandData(CommandData.Command.Int, head + string.Format(".{0}", "ID"), () => this.ID, null),
				new CommandData(CommandData.Command.Int, head + string.Format(".{0}", "Count"), () => this.Count, null),
				new CommandData(CommandData.Command.String, head + string.Format(".{0}", "LatestDateTime"), () => this.LatestDateTime.ToString(), null)
			};
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x001AC083 File Offset: 0x001AA483
		public static int RemoveStorages(StuffItem item, IReadOnlyCollection<List<StuffItem>> items)
		{
			return StuffItem.RemoveStorages(item, item.Count, items);
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x001AC094 File Offset: 0x001AA494
		public static int RemoveStorages(StuffItem item, int count, IReadOnlyCollection<List<StuffItem>> items)
		{
			foreach (List<StuffItem> self in items)
			{
				self.RemoveItem(item, ref count);
				if (count <= 0)
				{
					break;
				}
			}
			return count;
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x001AC0F8 File Offset: 0x001AA4F8
		public static StuffItem CreateSystemItem(int id, int category = 0, int count = 1)
		{
			return new StuffItem(category, id, count);
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x001AC102 File Offset: 0x001AA502
		public bool MatchItem(ItemIDKeyPair key)
		{
			return this.CategoryID == key.categoryID && this.ID == key.itemID;
		}
	}
}
