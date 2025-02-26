using System;
using System.Collections.Generic;

namespace ADV
{
	// Token: 0x020006B9 RID: 1721
	internal static class CommandDataExtension
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x000F0BB4 File Offset: 0x000EEFB4
		public static bool AddList(this ICommandData self, List<CommandData> list, string head = null)
		{
			if (self == null || list == null)
			{
				return false;
			}
			list.AddRange(self.CreateCommandData(head));
			return true;
		}
	}
}
