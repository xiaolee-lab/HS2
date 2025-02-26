using System;
using System.Collections;

namespace Illusion.Extensions
{
	// Token: 0x02001075 RID: 4213
	public static class BitArrayExtensions
	{
		// Token: 0x06008D3C RID: 36156 RVA: 0x003B0AD4 File Offset: 0x003AEED4
		public static bool Any(this BitArray array)
		{
			IEnumerator enumerator = array.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					bool flag = (bool)obj;
					if (flag)
					{
						return true;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return false;
		}

		// Token: 0x06008D3D RID: 36157 RVA: 0x003B0B40 File Offset: 0x003AEF40
		public static bool All(this BitArray array)
		{
			IEnumerator enumerator = array.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (!(bool)enumerator.Current)
					{
						return false;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return true;
		}

		// Token: 0x06008D3E RID: 36158 RVA: 0x003B0BAC File Offset: 0x003AEFAC
		public static bool None(this BitArray array)
		{
			return !array.Any();
		}

		// Token: 0x06008D3F RID: 36159 RVA: 0x003B0BB7 File Offset: 0x003AEFB7
		public static void Flip(this BitArray array, int index)
		{
			array.Set(index, !array.Get(index));
		}
	}
}
