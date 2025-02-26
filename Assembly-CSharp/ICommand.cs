using System;

// Token: 0x02000EFA RID: 3834
public interface ICommand
{
	// Token: 0x06007D2E RID: 32046
	void Undo();

	// Token: 0x06007D2F RID: 32047
	void Redo();
}
