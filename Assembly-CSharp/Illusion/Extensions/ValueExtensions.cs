using System;
using System.Collections.Generic;

namespace Illusion.Extensions
{
	// Token: 0x02001083 RID: 4227
	public static class ValueExtensions
	{
		// Token: 0x06008DA1 RID: 36257 RVA: 0x003B1B94 File Offset: 0x003AFF94
		public static int Check<T>(this IList<T> list, Func<T, bool> func)
		{
			return Utils.Value.Check(list.Count, (int index) => func(list[index]));
		}

		// Token: 0x06008DA2 RID: 36258 RVA: 0x003B1BD4 File Offset: 0x003AFFD4
		public static int Check<T>(this List<T> list, Func<T, bool> func)
		{
			return Utils.Value.Check(list.Count, (int index) => func(list[index]));
		}

		// Token: 0x06008DA3 RID: 36259 RVA: 0x003B1C14 File Offset: 0x003B0014
		public static int Check<T>(this T[] array, Func<T, bool> func)
		{
			return Utils.Value.Check(array.Length, (int index) => func(array[index]));
		}

		// Token: 0x06008DA4 RID: 36260 RVA: 0x003B1C50 File Offset: 0x003B0050
		public static int Check<T>(this IList<T> list, T value)
		{
			return Utils.Value.Check(list.Count, delegate(int index)
			{
				T t = list[index];
				return t.Equals(value);
			});
		}

		// Token: 0x06008DA5 RID: 36261 RVA: 0x003B1C90 File Offset: 0x003B0090
		public static int Check<T>(this List<T> list, T value)
		{
			return Utils.Value.Check(list.Count, delegate(int index)
			{
				T t = list[index];
				return t.Equals(value);
			});
		}

		// Token: 0x06008DA6 RID: 36262 RVA: 0x003B1CD0 File Offset: 0x003B00D0
		public static int Check<T>(this T[] array, T value)
		{
			return Utils.Value.Check(array.Length, (int index) => array[index].Equals(value));
		}
	}
}
