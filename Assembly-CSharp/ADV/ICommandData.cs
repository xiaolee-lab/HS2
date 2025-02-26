using System;
using System.Collections.Generic;

namespace ADV
{
	// Token: 0x020006B8 RID: 1720
	public interface ICommandData
	{
		// Token: 0x060028A7 RID: 10407
		IEnumerable<CommandData> CreateCommandData(string head);
	}
}
