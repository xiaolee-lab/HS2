using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Definitions;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000985 RID: 2437
	[MessagePackObject(false)]
	public class PlayerData : ICharacterInfo, IDiffComparer, IParams, ICommandData
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x001AC204 File Offset: 0x001AA604
		public PlayerData()
		{
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x001AC318 File Offset: 0x001AA718
		public PlayerData(PlayerData source)
		{
			if (source == null)
			{
				return;
			}
			this.Version = source.Version;
			this.Sex = source.Sex;
			for (int i = 0; i < 2; i++)
			{
				this.CharaFileNames[i] = source.CharaFileNames[i];
			}
			this.Position = source.Position;
			this.Rotation = source.Rotation;
			this.ItemList.Clear();
			foreach (StuffItem source2 in source.ItemList)
			{
				this.ItemList.Add(new StuffItem(source2));
			}
			if (source.LastAcquiredItem != null)
			{
				this.LastAcquiredItem = new StuffItem(source.LastAcquiredItem);
			}
			this.Wetness = source.Wetness;
			this.EquipedGloveItem = new StuffItem(source.EquipedGloveItem);
			this.EquipedShovelItem = new StuffItem(source.EquipedShovelItem);
			this.EquipedPickelItem = new StuffItem(source.EquipedPickelItem);
			this.EquipedNetItem = new StuffItem(source.EquipedNetItem);
			this.EquipedFishingItem = new StuffItem(source.EquipedFishingItem);
			this.EquipedHeadItem = new StuffItem(source.EquipedHeadItem);
			this.EquipedBackItem = new StuffItem(source.EquipedBackItem);
			this.EquipedNeckItem = new StuffItem(source.EquipedNeckItem);
			this.EquipedLampItem = new StuffItem(source.EquipedLampItem);
			this.SpendMoney = source.SpendMoney;
			this.FishingSkill = new Skill(source.FishingSkill);
			this.InventorySlotMax = source.InventorySlotMax;
			this.AreaID = source.AreaID;
			this.ChunkID = source.ChunkID;
			this.CraftPossibleTable.Clear();
			foreach (int item in source.CraftPossibleTable)
			{
				this.CraftPossibleTable.Add(item);
			}
			this.FirstCreatedItemTable.Clear();
			foreach (int item2 in source.FirstCreatedItemTable)
			{
				this.FirstCreatedItemTable.Add(item2);
			}
			this.IsOnbu = source.IsOnbu;
			this.PartnerID = source.PartnerID;
			this.DateEatTrigger = source.DateEatTrigger;
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x001AC6CC File Offset: 0x001AAACC
		[IgnoreMember]
		public CharaParams param
		{
			get
			{
				return this.GetCache(ref this._param, () => new CharaParams(this, "P"));
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x001AC6E6 File Offset: 0x001AAAE6
		[IgnoreMember]
		Params IParams.param
		{
			[CompilerGenerated]
			get
			{
				return this.param;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x001AC6EE File Offset: 0x001AAAEE
		// (set) Token: 0x06004595 RID: 17813 RVA: 0x001AC6F6 File Offset: 0x001AAAF6
		[Key(0)]
		public System.Version Version { get; set; } = new System.Version();

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x001AC6FF File Offset: 0x001AAAFF
		// (set) Token: 0x06004597 RID: 17815 RVA: 0x001AC707 File Offset: 0x001AAB07
		[Key(1)]
		public byte Sex { get; set; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x001AC710 File Offset: 0x001AAB10
		// (set) Token: 0x06004599 RID: 17817 RVA: 0x001AC718 File Offset: 0x001AAB18
		[Key(2)]
		public string[] CharaFileNames { get; set; } = new string[2];

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x001AC721 File Offset: 0x001AAB21
		[IgnoreMember]
		public string CharaFileName
		{
			get
			{
				if (this.Sex == 0)
				{
					return this.CharaFileNames[0];
				}
				return this.CharaFileNames[1];
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x001AC73F File Offset: 0x001AAB3F
		// (set) Token: 0x0600459C RID: 17820 RVA: 0x001AC747 File Offset: 0x001AAB47
		[Key(3)]
		public Vector3 Position { get; set; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x001AC750 File Offset: 0x001AAB50
		// (set) Token: 0x0600459E RID: 17822 RVA: 0x001AC758 File Offset: 0x001AAB58
		[Key(4)]
		public Quaternion Rotation { get; set; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x001AC761 File Offset: 0x001AAB61
		// (set) Token: 0x060045A0 RID: 17824 RVA: 0x001AC769 File Offset: 0x001AAB69
		[Key(5)]
		public List<StuffItem> ItemList { get; set; } = new List<StuffItem>();

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x001AC772 File Offset: 0x001AAB72
		// (set) Token: 0x060045A2 RID: 17826 RVA: 0x001AC77A File Offset: 0x001AAB7A
		[Key(6)]
		public StuffItem LastAcquiredItem { get; set; }

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x001AC783 File Offset: 0x001AAB83
		// (set) Token: 0x060045A4 RID: 17828 RVA: 0x001AC78B File Offset: 0x001AAB8B
		[Key(7)]
		public float Wetness { get; set; }

		// Token: 0x060045A5 RID: 17829 RVA: 0x001AC794 File Offset: 0x001AAB94
		public StuffItem EquipedSearchItem(int id)
		{
			switch (id)
			{
			case 0:
			case 1:
			case 2:
				return this.EquipedGloveItem;
			case 3:
				return this.EquipedShovelItem;
			case 4:
				return this.EquipedPickelItem;
			case 5:
				return this.EquipedNetItem;
			case 6:
				return this.EquipedFishingItem;
			default:
				return null;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x001AC7EC File Offset: 0x001AABEC
		// (set) Token: 0x060045A7 RID: 17831 RVA: 0x001AC7F4 File Offset: 0x001AABF4
		[Key(8)]
		public StuffItem EquipedGloveItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x001AC7FD File Offset: 0x001AABFD
		// (set) Token: 0x060045A9 RID: 17833 RVA: 0x001AC805 File Offset: 0x001AAC05
		[Key(9)]
		public StuffItem EquipedShovelItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x001AC80E File Offset: 0x001AAC0E
		// (set) Token: 0x060045AB RID: 17835 RVA: 0x001AC816 File Offset: 0x001AAC16
		[Key(10)]
		public StuffItem EquipedPickelItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x001AC81F File Offset: 0x001AAC1F
		// (set) Token: 0x060045AD RID: 17837 RVA: 0x001AC827 File Offset: 0x001AAC27
		[Key(11)]
		public StuffItem EquipedNetItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x001AC830 File Offset: 0x001AAC30
		// (set) Token: 0x060045AF RID: 17839 RVA: 0x001AC838 File Offset: 0x001AAC38
		[Key(12)]
		public StuffItem EquipedFishingItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x001AC841 File Offset: 0x001AAC41
		// (set) Token: 0x060045B1 RID: 17841 RVA: 0x001AC849 File Offset: 0x001AAC49
		[Key(13)]
		public StuffItem EquipedHeadItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060045B2 RID: 17842 RVA: 0x001AC852 File Offset: 0x001AAC52
		// (set) Token: 0x060045B3 RID: 17843 RVA: 0x001AC85A File Offset: 0x001AAC5A
		[Key(14)]
		public StuffItem EquipedBackItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x001AC863 File Offset: 0x001AAC63
		// (set) Token: 0x060045B5 RID: 17845 RVA: 0x001AC86B File Offset: 0x001AAC6B
		[Key(15)]
		public StuffItem EquipedNeckItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x001AC874 File Offset: 0x001AAC74
		// (set) Token: 0x060045B7 RID: 17847 RVA: 0x001AC87C File Offset: 0x001AAC7C
		[Key(16)]
		public StuffItem EquipedLampItem { get; set; } = new StuffItem
		{
			ID = -1
		};

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x001AC885 File Offset: 0x001AAC85
		// (set) Token: 0x060045B9 RID: 17849 RVA: 0x001AC88D File Offset: 0x001AAC8D
		[Key(17)]
		public int SpendMoney { get; set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x001AC896 File Offset: 0x001AAC96
		// (set) Token: 0x060045BB RID: 17851 RVA: 0x001AC89E File Offset: 0x001AAC9E
		[Key(18)]
		public Skill FishingSkill { get; set; } = new Skill();

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x001AC8A7 File Offset: 0x001AACA7
		// (set) Token: 0x060045BD RID: 17853 RVA: 0x001AC8AF File Offset: 0x001AACAF
		[Key(19)]
		public int InventorySlotMax { get; set; } = 1;

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x001AC8B8 File Offset: 0x001AACB8
		// (set) Token: 0x060045BF RID: 17855 RVA: 0x001AC8C0 File Offset: 0x001AACC0
		[Key(20)]
		public int AreaID { get; set; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x001AC8C9 File Offset: 0x001AACC9
		// (set) Token: 0x060045C1 RID: 17857 RVA: 0x001AC8D1 File Offset: 0x001AACD1
		[Key(21)]
		public int ChunkID { get; set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x001AC8DA File Offset: 0x001AACDA
		// (set) Token: 0x060045C3 RID: 17859 RVA: 0x001AC8E2 File Offset: 0x001AACE2
		[Key(22)]
		public HashSet<int> CraftPossibleTable { get; set; } = new HashSet<int>();

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x001AC8EB File Offset: 0x001AACEB
		// (set) Token: 0x060045C5 RID: 17861 RVA: 0x001AC8F3 File Offset: 0x001AACF3
		[Key(23)]
		public HashSet<int> FirstCreatedItemTable { get; set; } = new HashSet<int>();

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x001AC8FC File Offset: 0x001AACFC
		// (set) Token: 0x060045C7 RID: 17863 RVA: 0x001AC904 File Offset: 0x001AAD04
		[Key(24)]
		public bool IsOnbu { get; set; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x001AC90D File Offset: 0x001AAD0D
		// (set) Token: 0x060045C9 RID: 17865 RVA: 0x001AC915 File Offset: 0x001AAD15
		[Key(25)]
		public int? PartnerID { get; set; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x001AC91E File Offset: 0x001AAD1E
		// (set) Token: 0x060045CB RID: 17867 RVA: 0x001AC926 File Offset: 0x001AAD26
		[Key(26)]
		public bool DateEatTrigger { get; set; }

		// Token: 0x060045CC RID: 17868 RVA: 0x001AC930 File Offset: 0x001AAD30
		public void Copy(PlayerData source)
		{
			if (source == null)
			{
				return;
			}
			this.Version = source.Version;
			this.Sex = source.Sex;
			for (int i = 0; i < 2; i++)
			{
				this.CharaFileNames[i] = source.CharaFileNames[i];
			}
			this.Position = source.Position;
			this.Rotation = source.Rotation;
			this.ItemList.Clear();
			foreach (StuffItem source2 in source.ItemList)
			{
				this.ItemList.Add(new StuffItem(source2));
			}
			if (source.LastAcquiredItem != null)
			{
				this.LastAcquiredItem = new StuffItem(source.LastAcquiredItem);
			}
			this.Wetness = source.Wetness;
			this.EquipedGloveItem = new StuffItem(source.EquipedGloveItem);
			this.EquipedShovelItem = new StuffItem(source.EquipedShovelItem);
			this.EquipedPickelItem = new StuffItem(source.EquipedPickelItem);
			this.EquipedNetItem = new StuffItem(source.EquipedNetItem);
			this.EquipedFishingItem = new StuffItem(source.EquipedFishingItem);
			this.EquipedHeadItem = new StuffItem(source.EquipedHeadItem);
			this.EquipedBackItem = new StuffItem(source.EquipedBackItem);
			this.EquipedNeckItem = new StuffItem(source.EquipedNeckItem);
			this.EquipedLampItem = new StuffItem(source.EquipedLampItem);
			this.SpendMoney = source.SpendMoney;
			this.FishingSkill = new Skill(source.FishingSkill);
			this.InventorySlotMax = source.InventorySlotMax;
			this.AreaID = source.AreaID;
			this.ChunkID = source.ChunkID;
			this.CraftPossibleTable.Clear();
			foreach (int item in source.CraftPossibleTable)
			{
				this.CraftPossibleTable.Add(item);
			}
			this.FirstCreatedItemTable.Clear();
			foreach (int item2 in source.FirstCreatedItemTable)
			{
				this.FirstCreatedItemTable.Add(item2);
			}
			this.IsOnbu = source.IsOnbu;
			this.PartnerID = source.PartnerID;
			this.DateEatTrigger = source.DateEatTrigger;
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x001ACBDC File Offset: 0x001AAFDC
		public void ComplementDiff()
		{
			if (this.Version < new System.Version("0.0.1"))
			{
				this.IsOnbu = false;
				this.PartnerID = null;
			}
			this.Version = AIProject.Definitions.Version.PlayerDataVersion;
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x001ACC24 File Offset: 0x001AB024
		public void UpdateDiff()
		{
			this.ItemList.OrganizeItemList();
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x001ACC34 File Offset: 0x001AB034
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			List<CommandData> list = new List<CommandData>();
			string key = head + "Sex";
			list.Add(new CommandData(CommandData.Command.Int, key, () => (int)this.Sex, null));
			return list;
		}

		// Token: 0x04004110 RID: 16656
		private CharaParams _param;
	}
}
