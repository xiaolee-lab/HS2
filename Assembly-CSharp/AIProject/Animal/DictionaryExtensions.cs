using System;
using System.Collections.Generic;
using AIProject.Animal.Resources;

namespace AIProject.Animal
{
	// Token: 0x02000B77 RID: 2935
	public static class DictionaryExtensions
	{
		// Token: 0x06005715 RID: 22293 RVA: 0x0025AC4C File Offset: 0x0025904C
		public static KeyValuePair<T1, T2> Rand<T1, T2>(this Dictionary<T1, T2> source)
		{
			if (source.IsNullOrEmpty<T1, T2>())
			{
				return default(KeyValuePair<T1, T2>);
			}
			List<KeyValuePair<T1, T2>> list = ListPool<KeyValuePair<T1, T2>>.Get();
			foreach (KeyValuePair<T1, T2> item in source)
			{
				list.Add(item);
			}
			KeyValuePair<T1, T2> result = list.Rand<KeyValuePair<T1, T2>>();
			ListPool<KeyValuePair<T1, T2>>.Release(list);
			return result;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x0025ACD0 File Offset: 0x002590D0
		public static T2 RandValue<T1, T2>(this Dictionary<T1, T2> source)
		{
			if (source.IsNullOrEmpty<T1, T2>())
			{
				return default(T2);
			}
			List<KeyValuePair<T1, T2>> list = ListPool<KeyValuePair<T1, T2>>.Get();
			foreach (KeyValuePair<T1, T2> item in source)
			{
				list.Add(item);
			}
			T2 value = list.Rand<KeyValuePair<T1, T2>>().Value;
			ListPool<KeyValuePair<T1, T2>>.Release(list);
			return value;
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x0025AD5C File Offset: 0x0025915C
		public static T1 RandKey<T1, T2>(this Dictionary<T1, T2> source)
		{
			if (source.IsNullOrEmpty<T1, T2>())
			{
				return default(T1);
			}
			List<KeyValuePair<T1, T2>> list = ListPool<KeyValuePair<T1, T2>>.Get();
			foreach (KeyValuePair<T1, T2> item in source)
			{
				list.Add(item);
			}
			T1 key = list.Rand<KeyValuePair<T1, T2>>().Key;
			ListPool<KeyValuePair<T1, T2>>.Release(list);
			return key;
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x0025ADE8 File Offset: 0x002591E8
		public static KeyValuePair<T1, T2> GetPair<T1, T2>(this Dictionary<T1, T2> source, T1 key)
		{
			T2 value;
			source.TryGetValue(key, out value);
			return new KeyValuePair<T1, T2>(key, value);
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x0025AE06 File Offset: 0x00259206
		public static bool ActiveInState(this KeyValuePair<int, AnimalPlayState> source)
		{
			return source.Value != null && 0 <= source.Key && source.Value.MainStateInfo.ActiveInState;
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x0025AE35 File Offset: 0x00259235
		public static bool ActiveOutState(this KeyValuePair<int, AnimalPlayState> source)
		{
			return source.Value != null && 0 <= source.Key && source.Value.MainStateInfo.ActiveOutState;
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x0025AE64 File Offset: 0x00259264
		public static float AddValue<T1>(this Dictionary<T1, float> source, T1 key, float add)
		{
			float num = 0f;
			if (source.TryGetValue(key, out num))
			{
				num = (source[key] = num + add);
			}
			return num;
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x0025AE94 File Offset: 0x00259294
		public static int AddValue<T1>(this Dictionary<T1, int> source, T1 key, int add)
		{
			int num = 0;
			if (source.TryGetValue(key, out num))
			{
				num = (source[key] = num + add);
			}
			return num;
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x0025AEC0 File Offset: 0x002592C0
		public static bool TryAddValue<T1>(this Dictionary<T1, float> source, T1 key, float add, out float get)
		{
			get = 0f;
			if (source.TryGetValue(key, out get))
			{
				source[key] = (get += add);
				return true;
			}
			return false;
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x0025AEF4 File Offset: 0x002592F4
		public static bool TryAddValue<T1>(this Dictionary<T1, int> source, T1 key, int add, out int get)
		{
			get = 0;
			if (source.TryGetValue(key, out get))
			{
				source[key] = (get += add);
				return true;
			}
			return false;
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x0025AF24 File Offset: 0x00259324
		public static bool AddNonContains<T1, T2>(this Dictionary<T1, T2> source, T1 key, T2 value)
		{
			if (source == null)
			{
				return false;
			}
			if (source.ContainsKey(key))
			{
				return false;
			}
			source[key] = value;
			return true;
		}
	}
}
