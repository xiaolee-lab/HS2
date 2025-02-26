using System;

namespace Housing
{
	// Token: 0x020008CA RID: 2250
	public interface ICommand
	{
		// Token: 0x06003AE2 RID: 15074
		void Do();

		// Token: 0x06003AE3 RID: 15075
		void Undo();

		// Token: 0x06003AE4 RID: 15076
		void Redo();
	}
}
