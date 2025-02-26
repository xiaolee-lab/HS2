using System;

namespace Studio
{
	// Token: 0x02001284 RID: 4740
	public interface ICommand
	{
		// Token: 0x06009CF3 RID: 40179
		void Do();

		// Token: 0x06009CF4 RID: 40180
		void Undo();

		// Token: 0x06009CF5 RID: 40181
		void Redo();
	}
}
