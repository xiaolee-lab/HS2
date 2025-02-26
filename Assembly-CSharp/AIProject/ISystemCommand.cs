using System;

namespace AIProject
{
	// Token: 0x02000E2C RID: 3628
	public interface ISystemCommand
	{
		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06007175 RID: 29045
		// (set) Token: 0x06007176 RID: 29046
		bool EnabledInput { get; set; }

		// Token: 0x06007177 RID: 29047
		void OnUpdateInput();
	}
}
