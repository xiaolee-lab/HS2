using System;
using System.Collections.Generic;
using System.Linq;
using Manager;

namespace AIProject.SaveData
{
	// Token: 0x0200098E RID: 2446
	public static class DiffComparer
	{
		// Token: 0x06004649 RID: 17993 RVA: 0x001AE0D4 File Offset: 0x001AC4D4
		public static void OrganizeItemList(this List<StuffItem> itemList)
		{
			var array = itemList.Select((StuffItem value, int index) => new
			{
				value,
				index
			}).ToArray();
			List<Tuple<int, StuffItem>> list = ListPool<Tuple<int, StuffItem>>.Get();
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				<>__AnonType10<StuffItem, int> item = array2[i];
				Tuple<int, StuffItem> tuple = list.Find((Tuple<int, StuffItem> x) => x.Item2.CategoryID == item.value.CategoryID && x.Item2.ID == item.value.ID);
				if (tuple == null)
				{
					list.Add(new Tuple<int, StuffItem>(item.index, new StuffItem(item.value)));
				}
				else
				{
					tuple.Item2.Count += item.value.Count;
					if (tuple.Item2.Count > Singleton<Resources>.Instance.DefinePack.MapDefines.ItemStackUpperLimit)
					{
						int num = tuple.Item2.Count - 99;
						tuple.Item2.Count -= num;
						StuffItem item3 = new StuffItem(tuple.Item2.CategoryID, tuple.Item2.ID, num);
						int item2 = array.Last().index + 1;
						list.Add(new Tuple<int, StuffItem>(item2, item3));
					}
				}
			}
			list = (from x in list
			orderby x.Item2.ID
			select x).ToList<Tuple<int, StuffItem>>();
			List<StuffItem> list2 = ListPool<StuffItem>.Get();
			foreach (Tuple<int, StuffItem> tuple2 in list)
			{
				list2.Add(tuple2.Item2);
			}
			itemList.Clear();
			itemList.AddRange(list2.ToArray());
			ListPool<StuffItem>.Release(list2);
			ListPool<Tuple<int, StuffItem>>.Release(list);
		}
	}
}
