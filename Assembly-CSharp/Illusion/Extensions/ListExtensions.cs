using System;
using System.Collections.Generic;

namespace Illusion.Extensions
{
	// Token: 0x0200107E RID: 4222
	public static class ListExtensions
	{
		// Token: 0x06008D6D RID: 36205 RVA: 0x003B136C File Offset: 0x003AF76C
		public static T Peek<T>(this IList<T> self)
		{
			return self[0];
		}

		// Token: 0x06008D6E RID: 36206 RVA: 0x003B1378 File Offset: 0x003AF778
		public static T Pop<T>(this IList<T> self)
		{
			T result = self[0];
			self.RemoveAt(0);
			return result;
		}

		// Token: 0x06008D6F RID: 36207 RVA: 0x003B1395 File Offset: 0x003AF795
		public static void Push<T>(this IList<T> self, T item)
		{
			self.Insert(0, item);
		}

		// Token: 0x06008D70 RID: 36208 RVA: 0x003B13A0 File Offset: 0x003AF7A0
		public static T Dequeue<T>(this IList<T> self)
		{
			T result = self[0];
			self.RemoveAt(0);
			return result;
		}

		// Token: 0x06008D71 RID: 36209 RVA: 0x003B13BD File Offset: 0x003AF7BD
		public static void Enqueue<T>(this IList<T> self, T item)
		{
			self.Add(item);
		}
	}
}
