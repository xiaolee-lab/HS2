using System;
using System.Text;

namespace AIProject
{
	// Token: 0x02000961 RID: 2401
	public class StringBuilderPool
	{
		// Token: 0x060042A5 RID: 17061 RVA: 0x001A30B3 File Offset: 0x001A14B3
		public static StringBuilder Get()
		{
			return StringBuilderPool._pool.Get();
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x001A30BF File Offset: 0x001A14BF
		public static void Release(StringBuilder toRelease)
		{
			StringBuilderPool._pool.Release(toRelease);
		}

		// Token: 0x04003F8D RID: 16269
		private static readonly ObjectPool<StringBuilder> _pool = new ObjectPool<StringBuilder>(null, delegate(StringBuilder x)
		{
			x.Clear();
		});
	}
}
