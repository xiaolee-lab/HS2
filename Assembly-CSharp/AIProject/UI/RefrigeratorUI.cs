using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIProject.SaveData;
using Manager;

namespace AIProject.UI
{
	// Token: 0x02000EAD RID: 3757
	public class RefrigeratorUI : ItemBoxUI
	{
		// Token: 0x06007A7C RID: 31356 RVA: 0x00337C10 File Offset: 0x00336010
		public override IEnumerator SetStorage(ItemBoxUI.ItemBoxDataPack pack, Action<List<StuffItem>> action)
		{
			ItemBoxUI.SelectedElement sel = pack.sel;
			if (sel != ItemBoxUI.SelectedElement.Inventory)
			{
				if (sel == ItemBoxUI.SelectedElement.ItemBox)
				{
					while (Singleton<Game>.Instance.Environment == null)
					{
						yield return null;
					}
					action(Singleton<Game>.Instance.Environment.ItemListInPantry);
				}
			}
			else
			{
				while (Singleton<Map>.Instance.Player == null)
				{
					yield return null;
				}
				action(Singleton<Map>.Instance.Player.PlayerData.ItemList);
			}
			yield break;
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x00337C32 File Offset: 0x00336032
		public override void ViewCategorize(out int[] categorize, out List<StuffItem> viewList, List<StuffItem> itemList)
		{
			categorize = this.categorize;
			viewList = this.Convert(itemList).ToList<StuffItem>();
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06007A7E RID: 31358 RVA: 0x00337C4A File Offset: 0x0033604A
		private int[] categorize
		{
			get
			{
				return this.GetCache(ref this._categorize, delegate
				{
					HashSet<int> hashSet = new HashSet<int>();
					hashSet.Add(0);
					foreach (int num in Singleton<Resources>.Instance.GameInfo.GetItemCategories())
					{
						Dictionary<int, StuffItemInfo> itemTable = Singleton<Resources>.Instance.GameInfo.GetItemTable(num);
						if (itemTable != null)
						{
							if (this.IsCategorize(itemTable.Values))
							{
								hashSet.Add(num);
							}
						}
					}
					return hashSet.ToArray<int>();
				});
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06007A7F RID: 31359 RVA: 0x00337C64 File Offset: 0x00336064
		private StuffItemInfo[] craftCheck
		{
			get
			{
				return this.GetCache(ref this._craftCheck, delegate
				{
					Resources.GameInfoTables gameInfo = Singleton<Resources>.Instance.GameInfo;
					IReadOnlyDictionary<int, RecipeDataInfo[]> cookTable = gameInfo.recipe.cookTable;
					IEnumerable<int> second = cookTable.Values.SelectMany((RecipeDataInfo[] x) => x.SelectMany((RecipeDataInfo y) => from z in y.NeedList
					select z.nameHash));
					return (from nameHash in cookTable.Keys.Concat(second).Distinct<int>()
					select gameInfo.FindItemInfo(nameHash)).ToArray<StuffItemInfo>();
				});
			}
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x00337C8F File Offset: 0x0033608F
		private IEnumerable<StuffItem> Convert(IEnumerable<StuffItem> itemList)
		{
			return from item in itemList
			where Singleton<Resources>.Instance.GameInfo.CanEat(item) || this.craftCheck.Any((StuffItemInfo p) => p.CategoryID == item.CategoryID && p.ID == item.ID)
			select item;
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x00337CA3 File Offset: 0x003360A3
		private bool IsCategorize(IEnumerable<StuffItemInfo> itemList)
		{
			return (from item in itemList
			where Singleton<Resources>.Instance.GameInfo.CanEat(item) || this.craftCheck.Any((StuffItemInfo p) => p.CategoryID == item.CategoryID && p.ID == item.ID)
			select item).Any<StuffItemInfo>();
		}

		// Token: 0x04006262 RID: 25186
		private int[] _categorize;

		// Token: 0x04006263 RID: 25187
		private StuffItemInfo[] _craftCheck;
	}
}
