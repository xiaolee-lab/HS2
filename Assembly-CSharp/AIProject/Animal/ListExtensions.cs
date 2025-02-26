using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B76 RID: 2934
	public static class ListExtensions
	{
		// Token: 0x0600570F RID: 22287 RVA: 0x0025AB38 File Offset: 0x00258F38
		public static T Rand<T>(this List<T> source)
		{
			return (!source.IsNullOrEmpty<T>()) ? source[UnityEngine.Random.Range(0, source.Count)] : default(T);
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x0025AB70 File Offset: 0x00258F70
		public static T GetRand<T>(this List<T> source)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			int index = UnityEngine.Random.Range(0, source.Count);
			T result = source[index];
			source.RemoveAt(index);
			return result;
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x0025ABB0 File Offset: 0x00258FB0
		public static T First<T>(this List<T> source)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			return source[0];
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x0025ABDC File Offset: 0x00258FDC
		public static T Back<T>(this List<T> source)
		{
			if (source.IsNullOrEmpty<T>())
			{
				return default(T);
			}
			return source[source.Count - 1];
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x0025AC0C File Offset: 0x0025900C
		public static bool AddNonContains<T>(this List<T> source, T value)
		{
			if (source == null)
			{
				return false;
			}
			if (source.Contains(value))
			{
				return false;
			}
			source.Add(value);
			return true;
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x0025AC2C File Offset: 0x0025902C
		public static bool InRange<T>(this List<T> source, int i)
		{
			return source != null && 0 <= i && i < source.Count;
		}
	}
}
