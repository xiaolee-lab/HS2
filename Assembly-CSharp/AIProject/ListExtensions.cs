using System;
using System.Collections.Generic;
using System.Linq;

namespace AIProject
{
	// Token: 0x0200095E RID: 2398
	public static class ListExtensions
	{
		// Token: 0x06004294 RID: 17044 RVA: 0x001A2EC4 File Offset: 0x001A12C4
		public static void PushFront<T>(this IList<T> self, T item)
		{
			self.Insert(0, item);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x001A2ED0 File Offset: 0x001A12D0
		public static T PopFront<T>(this IList<T> self)
		{
			if (self.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			T result = self[0];
			self.RemoveAt(0);
			return result;
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x001A2F02 File Offset: 0x001A1302
		public static void PushBack<T>(this IList<T> self, T item)
		{
			self.Add(item);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x001A2F0C File Offset: 0x001A130C
		public static T PopBack<T>(this IList<T> self)
		{
			if (self.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			var <>__AnonType = self.Select((T value, int index) => new
			{
				value,
				index
			}).Last();
			self.RemoveAt(<>__AnonType.index);
			return <>__AnonType.value;
		}
	}
}
