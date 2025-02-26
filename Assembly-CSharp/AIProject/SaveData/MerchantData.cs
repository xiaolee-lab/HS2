using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Definitions;
using Illusion;
using Illusion.Extensions;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000983 RID: 2435
	[MessagePackObject(false)]
	public class MerchantData : ICharacterInfo, IDiffComparer, IParams, ICommandData
	{
		// Token: 0x06004533 RID: 17715 RVA: 0x001AB388 File Offset: 0x001A9788
		public MerchantData()
		{
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x001AB424 File Offset: 0x001A9824
		public MerchantData(MerchantData _data)
		{
			this.Copy(_data);
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x001AB4C4 File Offset: 0x001A98C4
		[IgnoreMember]
		public CharaParams param
		{
			get
			{
				return this.GetCache(ref this._param, () => new CharaParams(this, "M"));
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x001AB4DE File Offset: 0x001A98DE
		[IgnoreMember]
		Params IParams.param
		{
			[CompilerGenerated]
			get
			{
				return this.param;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x001AB4E6 File Offset: 0x001A98E6
		// (set) Token: 0x06004538 RID: 17720 RVA: 0x001AB4EE File Offset: 0x001A98EE
		[Key(0)]
		public System.Version Version { get; set; } = new System.Version();

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x001AB4F7 File Offset: 0x001A98F7
		// (set) Token: 0x0600453A RID: 17722 RVA: 0x001AB4FF File Offset: 0x001A98FF
		[Key(1)]
		public string CharaFileName { get; set; } = string.Empty;

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x001AB508 File Offset: 0x001A9908
		// (set) Token: 0x0600453C RID: 17724 RVA: 0x001AB510 File Offset: 0x001A9910
		[Key(2)]
		public int ChunkID { get; set; }

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x001AB519 File Offset: 0x001A9919
		// (set) Token: 0x0600453E RID: 17726 RVA: 0x001AB521 File Offset: 0x001A9921
		[Key(3)]
		public int AreaID { get; set; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x001AB52A File Offset: 0x001A992A
		// (set) Token: 0x06004540 RID: 17728 RVA: 0x001AB532 File Offset: 0x001A9932
		[Key(4)]
		public int ActionTargetID { get; set; } = -1;

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004541 RID: 17729 RVA: 0x001AB53B File Offset: 0x001A993B
		// (set) Token: 0x06004542 RID: 17730 RVA: 0x001AB543 File Offset: 0x001A9943
		[Key(5)]
		public bool Unlock { get; set; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x001AB54C File Offset: 0x001A994C
		// (set) Token: 0x06004544 RID: 17732 RVA: 0x001AB554 File Offset: 0x001A9954
		[Key(6)]
		public Merchant.ActionType ModeType { get; set; } = Merchant.ActionType.ToWait;

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x001AB55D File Offset: 0x001A995D
		// (set) Token: 0x06004546 RID: 17734 RVA: 0x001AB565 File Offset: 0x001A9965
		[Key(7)]
		public Merchant.ActionType LastNormalModeType { get; set; } = Merchant.ActionType.ToWait;

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004547 RID: 17735 RVA: 0x001AB56E File Offset: 0x001A996E
		// (set) Token: 0x06004548 RID: 17736 RVA: 0x001AB576 File Offset: 0x001A9976
		[Key(8)]
		public Merchant.StateType StateType { get; set; } = Merchant.StateType.Wait;

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x001AB57F File Offset: 0x001A997F
		// (set) Token: 0x0600454A RID: 17738 RVA: 0x001AB587 File Offset: 0x001A9987
		[Key(9)]
		public Vector3 Position { get; set; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x001AB590 File Offset: 0x001A9990
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x001AB598 File Offset: 0x001A9998
		[Key(10)]
		public Quaternion Rotation { get; set; }

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x001AB5A1 File Offset: 0x001A99A1
		// (set) Token: 0x0600454E RID: 17742 RVA: 0x001AB5A9 File Offset: 0x001A99A9
		[Key(11)]
		public List<StuffItem> ItemList { get; set; } = new List<StuffItem>();

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x001AB5B2 File Offset: 0x001A99B2
		// (set) Token: 0x06004550 RID: 17744 RVA: 0x001AB5BA File Offset: 0x001A99BA
		[Key(12)]
		public MerchantActor.MerchantSchedule CurrentSchedule { get; set; }

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x001AB5C3 File Offset: 0x001A99C3
		// (set) Token: 0x06004552 RID: 17746 RVA: 0x001AB5CB File Offset: 0x001A99CB
		[Key(13)]
		public MerchantActor.MerchantSchedule PrevSchedule { get; set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x001AB5D4 File Offset: 0x001A99D4
		// (set) Token: 0x06004554 RID: 17748 RVA: 0x001AB5DC File Offset: 0x001A99DC
		[Key(14)]
		public List<MerchantActor.MerchantSchedule> ScheduleList { get; set; } = new List<MerchantActor.MerchantSchedule>();

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x001AB5E5 File Offset: 0x001A99E5
		// (set) Token: 0x06004556 RID: 17750 RVA: 0x001AB5ED File Offset: 0x001A99ED
		[Key(15)]
		public int PointID { get; set; } = -1;

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x001AB5F6 File Offset: 0x001A99F6
		// (set) Token: 0x06004558 RID: 17752 RVA: 0x001AB5FE File Offset: 0x001A99FE
		[Key(16)]
		public int PointAreaID { get; set; }

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x001AB607 File Offset: 0x001A9A07
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x001AB60F File Offset: 0x001A9A0F
		[Key(17)]
		public int PointGroupID { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x001AB618 File Offset: 0x001A9A18
		// (set) Token: 0x0600455C RID: 17756 RVA: 0x001AB620 File Offset: 0x001A9A20
		[Key(18)]
		public List<MerchantData.VendorItem> vendorItemList { get; set; } = new List<MerchantData.VendorItem>();

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x001AB629 File Offset: 0x001A9A29
		// (set) Token: 0x0600455E RID: 17758 RVA: 0x001AB631 File Offset: 0x001A9A31
		[Key(19)]
		public List<MerchantData.VendorItem> vendorSpItemList { get; set; } = new List<MerchantData.VendorItem>();

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x001AB63A File Offset: 0x001A9A3A
		// (set) Token: 0x06004560 RID: 17760 RVA: 0x001AB642 File Offset: 0x001A9A42
		[Key(20)]
		public int OpenAreaID { get; set; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x001AB64B File Offset: 0x001A9A4B
		// (set) Token: 0x06004562 RID: 17762 RVA: 0x001AB653 File Offset: 0x001A9A53
		[Key(21)]
		public Dictionary<int, MerchantData.VendorItem> vendorSpItemTable { get; set; } = new Dictionary<int, MerchantData.VendorItem>();

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x001AB65C File Offset: 0x001A9A5C
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x001AB664 File Offset: 0x001A9A64
		[Key(22)]
		public bool isThereafterH { get; set; }

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x001AB66D File Offset: 0x001A9A6D
		// (set) Token: 0x06004566 RID: 17766 RVA: 0x001AB675 File Offset: 0x001A9A75
		[Key(23)]
		public float Wetness { get; set; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x001AB67E File Offset: 0x001A9A7E
		// (set) Token: 0x06004568 RID: 17768 RVA: 0x001AB686 File Offset: 0x001A9A86
		[Key(24)]
		public int MapID { get; set; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x001AB68F File Offset: 0x001A9A8F
		// (set) Token: 0x0600456A RID: 17770 RVA: 0x001AB697 File Offset: 0x001A9A97
		[Key(25)]
		public Vector3 PointPosition { get; set; } = Vector3.zero;

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x001AB6A0 File Offset: 0x001A9AA0
		// (set) Token: 0x0600456C RID: 17772 RVA: 0x001AB6A8 File Offset: 0x001A9AA8
		[Key(26)]
		public bool ElapsedDay { get; set; }

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x001AB6B1 File Offset: 0x001A9AB1
		// (set) Token: 0x0600456E RID: 17774 RVA: 0x001AB6B9 File Offset: 0x001A9AB9
		[Key(27)]
		public Vector3 MainPointPosition { get; set; } = Vector3.zero;

		// Token: 0x0600456F RID: 17775 RVA: 0x001AB6C2 File Offset: 0x001A9AC2
		public void UpdateDiff()
		{
			this.ItemList.OrganizeItemList();
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x001AB6D0 File Offset: 0x001A9AD0
		public void Copy(MerchantData _data)
		{
			this.CharaFileName = _data.CharaFileName;
			this.ChunkID = _data.ChunkID;
			this.AreaID = _data.AreaID;
			this.ActionTargetID = _data.ActionTargetID;
			this.Unlock = _data.Unlock;
			this.ModeType = _data.ModeType;
			this.LastNormalModeType = _data.LastNormalModeType;
			this.StateType = _data.StateType;
			this.Position = _data.Position;
			this.Rotation = _data.Rotation;
			this.ItemList = (from x in _data.ItemList
			select new StuffItem(x)).ToList<StuffItem>();
			if (_data.CurrentSchedule != null)
			{
				this.CurrentSchedule = new MerchantActor.MerchantSchedule(_data.CurrentSchedule);
			}
			if (_data.PrevSchedule != null)
			{
				this.PrevSchedule = new MerchantActor.MerchantSchedule(_data.PrevSchedule);
			}
			this.ScheduleList = (from x in _data.ScheduleList
			select new MerchantActor.MerchantSchedule(x)).ToList<MerchantActor.MerchantSchedule>();
			this.PointID = _data.PointID;
			this.PointAreaID = _data.PointAreaID;
			this.PointGroupID = _data.PointGroupID;
			this.vendorItemList = (from x in _data.vendorItemList
			select new MerchantData.VendorItem(x)).ToList<MerchantData.VendorItem>();
			if (_data.vendorSpItemTable != null)
			{
				this.vendorSpItemTable = _data.vendorSpItemTable.ToDictionary((KeyValuePair<int, MerchantData.VendorItem> v) => v.Key, (KeyValuePair<int, MerchantData.VendorItem> v) => new MerchantData.VendorItem(v.Value));
			}
			else if (_data.vendorSpItemList != null)
			{
				this.vendorSpItemTable = _data.vendorSpItemList.Select((MerchantData.VendorItem Value, int Key) => new
				{
					Key,
					Value
				}).ToDictionary(v => v.Key, v => new MerchantData.VendorItem(v.Value));
			}
			this.OpenAreaID = _data.OpenAreaID;
			this.isThereafterH = _data.isThereafterH;
			this.Wetness = _data.Wetness;
			this.MapID = _data.MapID;
			this.PointPosition = _data.PointPosition;
			this.ElapsedDay = _data.ElapsedDay;
			this.MainPointPosition = _data.MainPointPosition;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x001AB971 File Offset: 0x001A9D71
		public void ComplementDiff()
		{
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x001AB974 File Offset: 0x001A9D74
		public void ResetVendor(IReadOnlyDictionary<int, List<VendItemInfo>> vendTable)
		{
			VendItemInfo[] source = vendTable.Keys.SelectMany((int i) => MerchantData.GetSaleChoice(vendTable[i], 2)).ToArray<VendItemInfo>();
			this.vendorItemList.Clear();
			this.vendorItemList.AddRange(source.Select(delegate(VendItemInfo p)
			{
				int num = (p.Stocks.Length != 1) ? Enumerable.Range(p.Stocks[0], p.Stocks[1] - p.Stocks[0]).Shuffle<int>().First<int>() : p.Stocks[0];
				return new MerchantData.VendorItem(p.CategoryID, p.ID, num, p.Rate, num);
			}));
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x001AB9EC File Offset: 0x001A9DEC
		public void ResetSpecialVendor(IReadOnlyDictionary<int, VendItemInfo> specialTable)
		{
			if (this.isSpecialVendorLoad)
			{
				return;
			}
			if (this.vendorSpItemTable == null)
			{
				this.vendorSpItemTable = new Dictionary<int, MerchantData.VendorItem>();
			}
			HashSet<int> hashSet = new HashSet<int>(this.vendorSpItemTable.Keys);
			foreach (KeyValuePair<int, VendItemInfo> keyValuePair in specialTable)
			{
				VendItemInfo value = keyValuePair.Value;
				hashSet.Remove(keyValuePair.Key);
				int num = Mathf.Max(1, value.Stocks[0]);
				MerchantData.VendorItem vendorItem;
				if (this.vendorSpItemTable.TryGetValue(keyValuePair.Key, out vendorItem))
				{
					vendorItem.CategoryID = value.CategoryID;
					vendorItem.ID = value.ID;
					vendorItem.Rate = value.Rate;
					vendorItem.Count += num - Mathf.Max(1, vendorItem.Stock);
					vendorItem.Count = Mathf.Max(0, vendorItem.Count);
					vendorItem.Stock = num;
				}
				else
				{
					this.vendorSpItemTable[keyValuePair.Key] = new MerchantData.VendorItem(value.CategoryID, value.ID, num, value.Rate, num);
				}
			}
			foreach (int key in hashSet)
			{
				this.vendorSpItemTable.Remove(key);
			}
			this.vendorSpItemTable = (from v in this.vendorSpItemTable
			orderby v.Key
			select v).ToDictionary((KeyValuePair<int, MerchantData.VendorItem> v) => v.Key, (KeyValuePair<int, MerchantData.VendorItem> v) => v.Value);
			this.isSpecialVendorLoad = true;
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x001ABC04 File Offset: 0x001AA004
		private static List<VendItemInfo> GetSaleChoice(IReadOnlyCollection<VendItemInfo> vendor, int num)
		{
			List<VendItemInfo> list = new List<VendItemInfo>();
			Dictionary<VendItemInfo, int> dictionary = vendor.ToDictionary((VendItemInfo p) => p, (VendItemInfo p) => p.Percent);
			if (!dictionary.Any<KeyValuePair<VendItemInfo, int>>())
			{
				return list;
			}
			for (int i = 0; i < num; i++)
			{
				VendItemInfo vendItemInfo = Illusion.Utils.ProbabilityCalclator.DetermineFromDict<VendItemInfo>(dictionary);
				if (!dictionary.Remove(vendItemInfo))
				{
					break;
				}
				list.Add(vendItemInfo);
			}
			return list;
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x001ABC98 File Offset: 0x001AA098
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			List<CommandData> list = new List<CommandData>();
			string key = head + "ModeType";
			list.Add(new CommandData(CommandData.Command.String, key, () => this.ModeType.ToString(), null));
			string key2 = head + "LastNormalModeType";
			list.Add(new CommandData(CommandData.Command.String, key2, () => this.LastNormalModeType.ToString(), null));
			string key3 = head + "StateType";
			list.Add(new CommandData(CommandData.Command.String, key3, () => this.StateType.ToString(), null));
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

		// Token: 0x040040E1 RID: 16609
		private CharaParams _param;

		// Token: 0x040040FE RID: 16638
		private bool isSpecialVendorLoad;

		// Token: 0x02000984 RID: 2436
		[MessagePackObject(false)]
		public class VendorItem : StuffItem
		{
			// Token: 0x06004589 RID: 17801 RVA: 0x001AC171 File Offset: 0x001AA571
			public VendorItem(int category, int id, int count, int rate, int stock) : this(category, id, count)
			{
				this.Rate = rate;
				this.Stock = stock;
			}

			// Token: 0x0600458A RID: 17802 RVA: 0x001AC18C File Offset: 0x001AA58C
			public VendorItem(int category, int id, int count)
			{
				this.Stock = 1;
				base..ctor(category, id, count);
			}

			// Token: 0x0600458B RID: 17803 RVA: 0x001AC19E File Offset: 0x001AA59E
			public VendorItem(MerchantData.VendorItem source)
			{
				this.Stock = 1;
				base..ctor(source);
				this.Rate = source.Rate;
				this.Stock = source.Stock;
			}

			// Token: 0x17000D76 RID: 3446
			// (get) Token: 0x0600458C RID: 17804 RVA: 0x001AC1C6 File Offset: 0x001AA5C6
			// (set) Token: 0x0600458D RID: 17805 RVA: 0x001AC1CE File Offset: 0x001AA5CE
			[Key(4)]
			public int Rate { get; set; }

			// Token: 0x17000D77 RID: 3447
			// (get) Token: 0x0600458E RID: 17806 RVA: 0x001AC1D7 File Offset: 0x001AA5D7
			// (set) Token: 0x0600458F RID: 17807 RVA: 0x001AC1DF File Offset: 0x001AA5DF
			[Key(5)]
			public int Stock { get; set; }
		}
	}
}
