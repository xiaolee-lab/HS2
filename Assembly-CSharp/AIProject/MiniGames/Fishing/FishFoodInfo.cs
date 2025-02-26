using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F1C RID: 3868
	public class FishFoodInfo
	{
		// Token: 0x06007F25 RID: 32549 RVA: 0x0036230C File Offset: 0x0036070C
		public FishFoodInfo()
		{
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x00362339 File Offset: 0x00360739
		public FishFoodInfo(int _itemID, string _foodName, int _rarelity1HitRange, int _rarelity2HitRange, int _rarelity3HitRange)
		{
			this.Initialize(_itemID, _foodName, _rarelity1HitRange, _rarelity2HitRange, _rarelity3HitRange);
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x00362373 File Offset: 0x00360773
		public FishFoodInfo(StuffItem _stuffItem, Sprite _icon, FishFoodInfo _fishFoodInfo, bool _isInfinity)
		{
			this.Initialize(_stuffItem, _icon, _fishFoodInfo, _isInfinity);
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06007F28 RID: 32552 RVA: 0x003623AB File Offset: 0x003607AB
		// (set) Token: 0x06007F29 RID: 32553 RVA: 0x003623B3 File Offset: 0x003607B3
		public int CategoryID { get; private set; } = -1;

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06007F2A RID: 32554 RVA: 0x003623BC File Offset: 0x003607BC
		// (set) Token: 0x06007F2B RID: 32555 RVA: 0x003623C4 File Offset: 0x003607C4
		public int ItemID { get; private set; } = -1;

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06007F2C RID: 32556 RVA: 0x003623CD File Offset: 0x003607CD
		// (set) Token: 0x06007F2D RID: 32557 RVA: 0x003623D5 File Offset: 0x003607D5
		public string FoodName { get; private set; } = string.Empty;

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06007F2E RID: 32558 RVA: 0x003623DE File Offset: 0x003607DE
		// (set) Token: 0x06007F2F RID: 32559 RVA: 0x003623E6 File Offset: 0x003607E6
		public int[] RarelityHitRange { get; private set; } = new int[3];

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06007F30 RID: 32560 RVA: 0x003623EF File Offset: 0x003607EF
		// (set) Token: 0x06007F31 RID: 32561 RVA: 0x003623F7 File Offset: 0x003607F7
		public StuffItem StuffItem { get; private set; }

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06007F32 RID: 32562 RVA: 0x00362400 File Offset: 0x00360800
		// (set) Token: 0x06007F33 RID: 32563 RVA: 0x00362408 File Offset: 0x00360808
		public Sprite Icon { get; private set; }

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06007F34 RID: 32564 RVA: 0x00362411 File Offset: 0x00360811
		// (set) Token: 0x06007F35 RID: 32565 RVA: 0x00362419 File Offset: 0x00360819
		public bool IsInfinity { get; private set; }

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06007F36 RID: 32566 RVA: 0x00362422 File Offset: 0x00360822
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this._count;
			}
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x0036242C File Offset: 0x0036082C
		public void UseFood()
		{
			if (this.StuffItem == null || this.RemoveItem == null || this._count <= 0 || !Singleton<Map>.IsInstance())
			{
				return;
			}
			List<StuffItem> itemList = Singleton<Map>.Instance.Player.PlayerData.ItemList;
			itemList.RemoveItem(this.RemoveItem);
			this._count--;
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x00362498 File Offset: 0x00360898
		public void Clear()
		{
			this.CategoryID = -1;
			this.ItemID = -1;
			this.FoodName = string.Empty;
			this.RarelityHitRange[0] = 0;
			this.RarelityHitRange[1] = 0;
			this.RarelityHitRange[2] = 0;
			this.StuffItem = null;
			this.Icon = null;
			this.RemoveItem = null;
		}

		// Token: 0x06007F39 RID: 32569 RVA: 0x003624F0 File Offset: 0x003608F0
		public void Initialize(int _itemID, string _foodName, int _rarelity1HitRange, int _rarelity2HitRange, int _rarelity3HitRange)
		{
			this.CategoryID = -1;
			this.ItemID = _itemID;
			this.FoodName = (_foodName ?? string.Empty);
			this.RarelityHitRange[0] = _rarelity1HitRange;
			this.RarelityHitRange[1] = _rarelity2HitRange;
			this.RarelityHitRange[2] = _rarelity3HitRange;
			this.StuffItem = null;
			this.Icon = null;
		}

		// Token: 0x06007F3A RID: 32570 RVA: 0x00362549 File Offset: 0x00360949
		public void Initialize(FishFoodInfo _info)
		{
			this.Initialize(_info.ItemID, _info.FoodName, _info.RarelityHitRange[0], _info.RarelityHitRange[1], _info.RarelityHitRange[2]);
		}

		// Token: 0x06007F3B RID: 32571 RVA: 0x00362578 File Offset: 0x00360978
		public void Initialize(StuffItem _stuffItem, Sprite _icon, FishFoodInfo _fishFoodInfo, bool _isInfinity)
		{
			this.Initialize(_fishFoodInfo);
			this.CategoryID = ((_stuffItem == null) ? -1 : _stuffItem.CategoryID);
			this._count = ((_stuffItem == null) ? 0 : _stuffItem.Count);
			this.StuffItem = _stuffItem;
			this.Icon = _icon;
			this.IsInfinity = _isInfinity;
			this.RemoveItem = ((!_isInfinity) ? new StuffItem(_stuffItem) : null);
			if (this.RemoveItem != null)
			{
				this.RemoveItem.Count = 1;
			}
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x00362603 File Offset: 0x00360A03
		public void AddCount(int _addCount)
		{
			this._count += _addCount;
		}

		// Token: 0x0400666F RID: 26223
		private StuffItem RemoveItem;

		// Token: 0x04006671 RID: 26225
		private int _count;
	}
}
