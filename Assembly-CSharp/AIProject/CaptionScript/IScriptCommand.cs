using System;
using System.Collections.Generic;

namespace AIProject.CaptionScript
{
	// Token: 0x02000E31 RID: 3633
	public interface IScriptCommand
	{
		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x060071C5 RID: 29125
		string Tag { get; }

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x060071C6 RID: 29126
		bool IsBefore { get; }

		// Token: 0x060071C7 RID: 29127
		bool Execute(Dictionary<string, string> dic, int line);

		// Token: 0x060071C8 RID: 29128
		Dictionary<string, string> Analysis(List<string> list);
	}
}
