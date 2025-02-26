using System;
using System.Collections.Generic;
using System.Linq;
using AIProject.SaveData;
using Manager;

namespace AIProject
{
	// Token: 0x02000988 RID: 2440
	public static class StuffItemExtensions
	{
		// Token: 0x060045EC RID: 17900 RVA: 0x001AD1EC File Offset: 0x001AB5EC
		public static StuffItem FindItem(this List<StuffItem> self, StuffItem item)
		{
			return (item != null) ? self.Find((StuffItem p) => p.CategoryID == item.CategoryID && p.ID == item.ID) : null;
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x001AD22C File Offset: 0x001AB62C
		public static StuffItem[] FindItems(this IEnumerable<StuffItem> self, StuffItem item)
		{
			return (item != null) ? (from node in self
			where item.CategoryID == node.CategoryID && item.ID == node.ID
			select node).ToArray<StuffItem>() : new StuffItem[0];
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x001AD273 File Offset: 0x001AB673
		public static bool AddItem(this ICollection<StuffItem> self, StuffItem item)
		{
			return self.AddItem(item, item.Count);
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x001AD284 File Offset: 0x001AB684
		public static bool AddItem(this ICollection<StuffItem> self, StuffItem item, int count)
		{
			item.LatestDateTime = DateTime.Now;
			int itemSlotMax = Singleton<Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			foreach (StuffItem stuffItem in from x in self.FindItems(item)
			orderby x.Count descending
			select x)
			{
				int num = itemSlotMax - stuffItem.Count;
				if (num != 0)
				{
					int num2 = count - num;
					if (num2 > 0)
					{
						stuffItem.Count = itemSlotMax;
						count = num2;
					}
					else
					{
						stuffItem.Count += count;
						count = 0;
					}
					stuffItem.LatestDateTime = item.LatestDateTime;
					if (count <= 0)
					{
						break;
					}
				}
			}
			bool result = false;
			while (count > 0)
			{
				result = true;
				int num3 = count - itemSlotMax;
				if (num3 <= 0)
				{
					item.Count = count;
					self.Add(item);
					count = 0;
					break;
				}
				item.Count = itemSlotMax;
				self.Add(new StuffItem(item));
				count = num3;
			}
			return result;
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x001AD3C8 File Offset: 0x001AB7C8
		public static bool AddItem(this ICollection<StuffItem> self, StuffItem item, int count, int maxSlot)
		{
			item.LatestDateTime = DateTime.Now;
			int itemSlotMax = Singleton<Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			foreach (StuffItem stuffItem in from x in self.FindItems(item)
			orderby x.Count descending
			select x)
			{
				int num = itemSlotMax - stuffItem.Count;
				if (num != 0)
				{
					int num2 = count - num;
					if (num2 > 0)
					{
						stuffItem.Count = itemSlotMax;
						count = num2;
					}
					else
					{
						stuffItem.Count += count;
						count = 0;
					}
					stuffItem.LatestDateTime = item.LatestDateTime;
					if (count <= 0)
					{
						break;
					}
				}
			}
			bool result = false;
			while (count > 0 && self.Count < maxSlot)
			{
				result = true;
				int num3 = count - itemSlotMax;
				if (num3 <= 0)
				{
					item.Count = count;
					self.Add(item);
					count = 0;
					break;
				}
				item.Count = itemSlotMax;
				self.Add(new StuffItem(item));
				count = num3;
			}
			return result;
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x001AD518 File Offset: 0x001AB918
		public static bool RemoveItem(this List<StuffItem> self, StuffItem item)
		{
			int count = item.Count;
			return self.RemoveItem(item, ref count);
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x001AD538 File Offset: 0x001AB938
		public static bool RemoveItem(this List<StuffItem> self, StuffItem item, ref int count)
		{
			bool flag = false;
			foreach (StuffItem stuffItem in from x in self.FindItems(item)
			orderby x.Count
			select x)
			{
				int num = count - stuffItem.Count;
				if (num < 0)
				{
					stuffItem.Count -= count;
					count = 0;
					break;
				}
				stuffItem.Count = 0;
				count = num;
				flag = true;
			}
			if (flag)
			{
				self.RemoveAll((StuffItem x) => x.Count <= 0);
			}
			return flag;
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x001AD614 File Offset: 0x001ABA14
		public static bool CanAddItem(this IReadOnlyCollection<StuffItem> self, int capacity, StuffItem item)
		{
			return self.CanAddItem(capacity, item, item.Count);
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x001AD624 File Offset: 0x001ABA24
		public static bool CanAddItem(this IReadOnlyCollection<StuffItem> self, int capacity, StuffItem item, out int possible)
		{
			return self.CanAddItem(capacity, item, item.Count, out possible);
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x001AD638 File Offset: 0x001ABA38
		public static bool CanAddItem(this IReadOnlyCollection<StuffItem> self, int capacity, StuffItem item, int count)
		{
			int num;
			return self.CanAddItem(capacity, item, count, out num);
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x001AD650 File Offset: 0x001ABA50
		public static bool CanAddItem(this IReadOnlyCollection<StuffItem> self, int capacity, StuffItem item, int count, out int possible)
		{
			int ItemSlotMax = Singleton<Resources>.Instance.DefinePack.MapDefines.ItemSlotMax;
			int num = self.FindItems(item).Sum((StuffItem x) => ItemSlotMax - x.Count);
			int num2 = count - num;
			if (num2 <= 0)
			{
				num2 = 0;
			}
			int num3 = num2 / ItemSlotMax + ((num2 % ItemSlotMax <= 0) ? 0 : 1);
			int num4 = capacity - self.Count - num3;
			possible = num;
			if (num4 > 0)
			{
				possible += ItemSlotMax * num4;
			}
			if (num4 >= 0 && num2 > 0)
			{
				possible += ItemSlotMax - num2;
			}
			return num4 >= 0 || (possible > 0 && possible >= count);
		}
	}
}
