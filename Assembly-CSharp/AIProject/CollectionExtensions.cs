using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AIProject
{
	// Token: 0x0200096A RID: 2410
	public static class CollectionExtensions
	{
		// Token: 0x060042D6 RID: 17110 RVA: 0x001A52CA File Offset: 0x001A36CA
		public static bool IsNullOrEmpty<T>(this T[] source)
		{
			return source == null || source.Length == 0;
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x001A52DB File Offset: 0x001A36DB
		public static bool IsNullOrEmpty<T>(this List<T> source)
		{
			return source == null || source.Count == 0;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x001A52EF File Offset: 0x001A36EF
		public static bool IsNullOrEmpty<TKey, TSource>(this Dictionary<TKey, TSource> source)
		{
			return source == null || source.Count == 0;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x001A5303 File Offset: 0x001A3703
		public static bool IsNullOrEmpty<TKey, TSource>(this ReadOnlyDictionary<TKey, TSource> source)
		{
			return source == null || source.Count == 0;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x001A5317 File Offset: 0x001A3717
		public static bool IsNullOrEmpty<T>(this Queue<T> source)
		{
			return source == null || source.Count == 0;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x001A532C File Offset: 0x001A372C
		public static bool Exists<T>(this T[] source, Predicate<T> predicate)
		{
			foreach (T obj in source)
			{
				if (predicate(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x001A5368 File Offset: 0x001A3768
		public static bool Exists<T>(this List<T> source, Predicate<T> predicate)
		{
			foreach (T obj in source)
			{
				if (predicate(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x001A53D0 File Offset: 0x001A37D0
		public static bool Exists<TKey, TSource>(this Dictionary<TKey, TSource> source, Predicate<KeyValuePair<TKey, TSource>> predicate)
		{
			foreach (KeyValuePair<TKey, TSource> obj in source)
			{
				if (predicate(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x001A5438 File Offset: 0x001A3838
		public static bool Exists<T>(this Queue<T> source, Predicate<T> predicate)
		{
			foreach (T obj in source)
			{
				if (predicate(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x001A54A0 File Offset: 0x001A38A0
		public static T[] Range<T>(this T[] source, int start, int count)
		{
			if (start < 0 || count <= 0)
			{
				return null;
			}
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = source[i + start];
			}
			return array;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x001A54E8 File Offset: 0x001A38E8
		public static int Sum<T>(this T[] source, Func<T, int> selector)
		{
			if (source == null)
			{
				return 0;
			}
			int num = 0;
			foreach (T arg in source)
			{
				num += selector(arg);
			}
			return num;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x001A5528 File Offset: 0x001A3928
		public static T[] Shuffle<T>(this T[] source)
		{
			if (source == null)
			{
				return null;
			}
			if (source.Length == 0)
			{
				return new T[0];
			}
			int num = source.Length;
			T[] array = new T[num];
			Array.Copy(source, array, num);
			Random random = new Random();
			int num2 = num;
			while (1 < num2)
			{
				num2--;
				int num3 = random.Next(num2 + 1);
				T t = array[num3];
				array[num3] = array[num2];
				array[num2] = t;
			}
			return array;
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x001A55A8 File Offset: 0x001A39A8
		public static T Pop<T>(this List<T> source)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			T result = source.FirstOrDefault<T>();
			source.RemoveAt(0);
			return result;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x001A55D9 File Offset: 0x001A39D9
		public static void PushFront<T>(this List<T> source, T item)
		{
			if (source == null)
			{
				return;
			}
			source.Insert(0, item);
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x001A55EC File Offset: 0x001A39EC
		public static T[] Shuffle<T>(this List<T> source)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return null;
			}
			int count = source.Count;
			T[] array = new T[count];
			Array.Copy(source.ToArray(), array, count);
			Random random = new Random();
			int num = count;
			while (1 < num)
			{
				num--;
				int num2 = random.Next(num + 1);
				T t = array[num2];
				array[num2] = array[num];
				array[num] = t;
			}
			return array;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x001A5668 File Offset: 0x001A3A68
		public static List<T> Range<T>(this List<T> source, int start, int count)
		{
			if (start < 0 || count <= 0)
			{
				return null;
			}
			List<T> list = new List<T>();
			for (int i = 0; i < count; i++)
			{
				list[i] = source[i + start];
			}
			return list;
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x001A56B0 File Offset: 0x001A3AB0
		public static int Sum<T>(this List<T> source, Func<T, int> selector)
		{
			if (source == null)
			{
				return 0;
			}
			int num = 0;
			foreach (T arg in source)
			{
				num += selector(arg);
			}
			return num;
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x001A5718 File Offset: 0x001A3B18
		public static float Sum<T>(this List<T> source, Func<T, float> selector)
		{
			if (source == null)
			{
				return 0f;
			}
			float num = 0f;
			foreach (T arg in source)
			{
				num += selector(arg);
			}
			return num;
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x001A5788 File Offset: 0x001A3B88
		public static T GetElement<T>(this T[] source, int index)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			if (index >= 0 && index < source.Length)
			{
				return source[index];
			}
			return default(T);
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x001A57CC File Offset: 0x001A3BCC
		public static T GetElement<T>(this List<T> source, int index)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			if (index >= 0 && index < source.Count)
			{
				return source[index];
			}
			return default(T);
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x001A5814 File Offset: 0x001A3C14
		public static T GetElement<T>(this ReadOnlyCollection<T> source, int index)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			if (index >= 0 && index < source.Count)
			{
				return source[index];
			}
			return default(T);
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x001A585C File Offset: 0x001A3C5C
		public static KeyValuePair<TKey, TValue> Max<TKey, TValue>(this Dictionary<TKey, TValue> source, Func<KeyValuePair<TKey, TValue>, float> func)
		{
			float num = 0f;
			KeyValuePair<TKey, TValue> result = default(KeyValuePair<TKey, TValue>);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in source)
			{
				float num2 = func(keyValuePair);
				if (num2 > num)
				{
					num = num2;
					result = keyValuePair;
				}
			}
			return result;
		}
	}
}
