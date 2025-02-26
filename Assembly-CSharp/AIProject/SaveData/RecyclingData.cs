using System;
using System.Collections.Generic;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x02000986 RID: 2438
	[MessagePackObject(false)]
	public class RecyclingData
	{
		// Token: 0x060045D2 RID: 17874 RVA: 0x001ACC88 File Offset: 0x001AB088
		public RecyclingData()
		{
			if (this.DecidedItemList == null)
			{
				this.DecidedItemList = new List<StuffItem>();
			}
			if (this.CreatedItemList == null)
			{
				this.CreatedItemList = new List<StuffItem>();
			}
			this.CreateCounter = 0f;
			this.CreateCountEnabled = false;
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x001ACCEF File Offset: 0x001AB0EF
		public RecyclingData(RecyclingData data)
		{
			this.Copy(data);
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x001ACD14 File Offset: 0x001AB114
		// (set) Token: 0x060045D5 RID: 17877 RVA: 0x001ACD1C File Offset: 0x001AB11C
		[Key(0)]
		public List<StuffItem> DecidedItemList { get; set; } = new List<StuffItem>();

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x001ACD25 File Offset: 0x001AB125
		// (set) Token: 0x060045D7 RID: 17879 RVA: 0x001ACD2D File Offset: 0x001AB12D
		[Key(1)]
		public List<StuffItem> CreatedItemList { get; set; } = new List<StuffItem>();

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x001ACD36 File Offset: 0x001AB136
		// (set) Token: 0x060045D9 RID: 17881 RVA: 0x001ACD3E File Offset: 0x001AB13E
		[Key(2)]
		public float CreateCounter { get; set; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x001ACD47 File Offset: 0x001AB147
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x001ACD4F File Offset: 0x001AB14F
		[Key(3)]
		public bool CreateCountEnabled { get; set; }

		// Token: 0x060045DC RID: 17884 RVA: 0x001ACD58 File Offset: 0x001AB158
		public void Copy(RecyclingData data)
		{
			if (data == null)
			{
				return;
			}
			this.DecidedItemList.Clear();
			if (!data.DecidedItemList.IsNullOrEmpty<StuffItem>())
			{
				foreach (StuffItem source in data.DecidedItemList)
				{
					this.DecidedItemList.Add(new StuffItem(source));
				}
			}
			this.CreatedItemList.Clear();
			if (!data.CreatedItemList.IsNullOrEmpty<StuffItem>())
			{
				foreach (StuffItem source2 in data.CreatedItemList)
				{
					this.CreatedItemList.Add(new StuffItem(source2));
				}
			}
			this.CreateCounter = data.CreateCounter;
			this.CreateCountEnabled = data.CreateCountEnabled;
		}
	}
}
