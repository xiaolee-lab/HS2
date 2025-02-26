using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000972 RID: 2418
	[MessagePackObject(false)]
	public class ItemScrounge : ICommandData
	{
		// Token: 0x060043F4 RID: 17396 RVA: 0x001A8609 File Offset: 0x001A6A09
		public ItemScrounge()
		{
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x001A8634 File Offset: 0x001A6A34
		public ItemScrounge(ItemScrounge other)
		{
			this.isEvent = other.isEvent;
			this.timer = other.timer;
			this.ItemList = (from x in other.ItemList
			select new StuffItem(x)).ToList<StuffItem>();
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x001A86B3 File Offset: 0x001A6AB3
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x001A86BB File Offset: 0x001A6ABB
		[Key(0)]
		public bool isEvent { get; private set; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x001A86C4 File Offset: 0x001A6AC4
		// (set) Token: 0x060043F9 RID: 17401 RVA: 0x001A86CC File Offset: 0x001A6ACC
		[Key(1)]
		public float timer { get; private set; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x001A86D5 File Offset: 0x001A6AD5
		// (set) Token: 0x060043FB RID: 17403 RVA: 0x001A86DD File Offset: 0x001A6ADD
		[Key(2)]
		public List<StuffItem> ItemList { get; private set; } = new List<StuffItem>();

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x001A86E6 File Offset: 0x001A6AE6
		[IgnoreMember]
		public bool isEnd
		{
			[CompilerGenerated]
			get
			{
				return this.timer >= (float)this.timeLimit;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x001A86FA File Offset: 0x001A6AFA
		[IgnoreMember]
		private int timeLimit { get; } = 2400;

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x001A8702 File Offset: 0x001A6B02
		[IgnoreMember]
		public int redZoneTime { get; } = 600;

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x001A870A File Offset: 0x001A6B0A
		[IgnoreMember]
		public float remainingTime
		{
			[CompilerGenerated]
			get
			{
				return (float)this.timeLimit - this.timer;
			}
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x001A871A File Offset: 0x001A6B1A
		public void AddTimer(float add)
		{
			if (!this.isEvent)
			{
				return;
			}
			this.timer = Mathf.Min(this.timer + add, (float)this.timeLimit);
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x001A8742 File Offset: 0x001A6B42
		public void Finish()
		{
			this.timer = (float)this.timeLimit;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x001A8751 File Offset: 0x001A6B51
		public void Reset()
		{
			this.isEvent = false;
			this.timer = 0f;
			this.ItemList.Clear();
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x001A8770 File Offset: 0x001A6B70
		public void Set(IReadOnlyCollection<StuffItem> items)
		{
			this.isEvent = true;
			this.timer = 0f;
			this.ItemList.AddRange(items);
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x001A8790 File Offset: 0x001A6B90
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			List<CommandData> list = new List<CommandData>();
			list.Add(new CommandData(CommandData.Command.BOOL, head + string.Format(".{0}", "isEvent"), () => this.isEvent, null));
			list.Add(new CommandData(CommandData.Command.Int, head + string.Format(".{0}", "timer"), () => this.timer, null));
			string str = head + "ItemList";
			foreach (var <>__AnonType in this.ItemList.Select((StuffItem value, int index) => new
			{
				value,
				index
			}))
			{
				<>__AnonType.value.AddList(list, str + string.Format("[{0}]", <>__AnonType.index));
			}
			return list;
		}
	}
}
