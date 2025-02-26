using System;
using System.Collections.Generic;
using System.Linq;

namespace Illusion.Extensions
{
	// Token: 0x0200107C RID: 4220
	public static class IEnumerableExtensions
	{
		// Token: 0x06008D67 RID: 36199 RVA: 0x003B12B7 File Offset: 0x003AF6B7
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> self)
		{
			return from _ in self
			orderby Guid.NewGuid()
			select _;
		}

		// Token: 0x06008D68 RID: 36200 RVA: 0x003B12CB File Offset: 0x003AF6CB
		public static IEnumerable<T> SymmetricExcept<T>(this IEnumerable<T> self, IEnumerable<T> target)
		{
			return self.Except(target).Concat(target.Except(self));
		}

		// Token: 0x06008D69 RID: 36201 RVA: 0x003B12E0 File Offset: 0x003AF6E0
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, T second)
		{
			return first.Concat(new T[]
			{
				second
			});
		}
	}
}
