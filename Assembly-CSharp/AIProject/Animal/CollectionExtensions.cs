using System;
using System.Collections.Generic;

namespace AIProject.Animal
{
	// Token: 0x02000B75 RID: 2933
	public static class CollectionExtensions
	{
		// Token: 0x0600570D RID: 22285 RVA: 0x0025AB0D File Offset: 0x00258F0D
		public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> source)
		{
			return source == null || source.Count == 0;
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x0025AB21 File Offset: 0x00258F21
		public static bool IsNullOrEmpty<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source)
		{
			return source == null || source.Count == 0;
		}
	}
}
