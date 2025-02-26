using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x0200095B RID: 2395
	public static class DictionaryPool<TKey, TValue>
	{
		// Token: 0x0600428E RID: 17038 RVA: 0x001A2D6B File Offset: 0x001A116B
		public static Dictionary<TKey, TValue> Get()
		{
			return DictionaryPool<TKey, TValue>._dictionaryPool.Get();
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x001A2D77 File Offset: 0x001A1177
		public static void Release(Dictionary<TKey, TValue> toRelease)
		{
			DictionaryPool<TKey, TValue>._dictionaryPool.Release(toRelease);
		}

		// Token: 0x04003F84 RID: 16260
		private static readonly ObjectPool<Dictionary<TKey, TValue>> _dictionaryPool = new ObjectPool<Dictionary<TKey, TValue>>(null, delegate(Dictionary<TKey, TValue> x)
		{
			x.Clear();
		});
	}
}
