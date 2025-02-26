using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x02000963 RID: 2403
	public static class QueuePool<T>
	{
		// Token: 0x060042AA RID: 17066 RVA: 0x001A3101 File Offset: 0x001A1501
		public static Queue<T> Get()
		{
			return QueuePool<T>._queuePool.Get();
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x001A310D File Offset: 0x001A150D
		public static void Release(Queue<T> toRelease)
		{
			QueuePool<T>._queuePool.Release(toRelease);
		}

		// Token: 0x04003F8F RID: 16271
		private static readonly ObjectPool<Queue<T>> _queuePool = new ObjectPool<Queue<T>>(null, delegate(Queue<T> x)
		{
			x.Clear();
		});
	}
}
