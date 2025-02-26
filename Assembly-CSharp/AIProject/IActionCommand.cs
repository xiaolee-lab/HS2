using System;

namespace AIProject
{
	// Token: 0x02000E2A RID: 3626
	public interface IActionCommand
	{
		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06007166 RID: 29030
		// (set) Token: 0x06007167 RID: 29031
		bool EnabledInput { get; set; }

		// Token: 0x06007168 RID: 29032
		void OnUpdateInput();
	}
}
