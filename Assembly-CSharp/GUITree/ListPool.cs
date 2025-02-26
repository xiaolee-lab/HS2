using System;
using System.Collections.Generic;

namespace GUITree
{
	// Token: 0x02001240 RID: 4672
	internal static class ListPool<T>
	{
		// Token: 0x0600999B RID: 39323 RVA: 0x003F3E68 File Offset: 0x003F2268
		public static List<T> Get()
		{
			return ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x0600999C RID: 39324 RVA: 0x003F3E74 File Offset: 0x003F2274
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x04007AE3 RID: 31459
		private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
