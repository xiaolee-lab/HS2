using System;
using System.Collections.Generic;
using System.Linq;

namespace Housing.Extension
{
	// Token: 0x0200088E RID: 2190
	public static class IsNullOrEmptyExtension
	{
		// Token: 0x06003892 RID: 14482 RVA: 0x0014F1C4 File Offset: 0x0014D5C4
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return ((enumerable != null) ? new bool?(enumerable.Any<T>()) : null) != true;
		}
	}
}
