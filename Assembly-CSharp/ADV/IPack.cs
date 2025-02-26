using System;
using System.Collections.Generic;

namespace ADV
{
	// Token: 0x020006C3 RID: 1731
	public interface IPack
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002901 RID: 10497
		IParams[] param { get; }

		// Token: 0x06002902 RID: 10498
		List<Program.Transfer> Create();

		// Token: 0x06002903 RID: 10499
		void Receive(TextScenario scenario);

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002904 RID: 10500
		IReadOnlyCollection<CommandData> commandList { get; }

		// Token: 0x06002905 RID: 10501
		void CommandListVisibleEnabledDefault();

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002906 RID: 10502
		bool isCommandListVisibleEnabled { get; }
	}
}
