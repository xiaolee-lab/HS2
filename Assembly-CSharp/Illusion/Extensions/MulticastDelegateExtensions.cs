using System;

namespace Illusion.Extensions
{
	// Token: 0x02001079 RID: 4217
	public static class MulticastDelegateExtensions
	{
		// Token: 0x06008D4C RID: 36172 RVA: 0x003B0F38 File Offset: 0x003AF338
		public static int GetLength(this MulticastDelegate self)
		{
			if (self == null || self.GetInvocationList() == null)
			{
				return 0;
			}
			return self.GetInvocationList().Length;
		}
	}
}
