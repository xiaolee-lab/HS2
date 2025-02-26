using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x0200095F RID: 2399
	public static class ListPool<T>
	{
		// Token: 0x06004299 RID: 17049 RVA: 0x001A2F61 File Offset: 0x001A1361
		public static List<T> Get()
		{
			return ListPool<T>._listPool.Get();
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x001A2F6D File Offset: 0x001A136D
		public static void Release(List<T> toRelease)
		{
			ListPool<T>._listPool.Release(toRelease);
		}

		// Token: 0x04003F88 RID: 16264
		private static readonly ObjectPool<List<T>> _listPool = new ObjectPool<List<T>>(null, delegate(List<T> x)
		{
			x.Clear();
		});
	}
}
