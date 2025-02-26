using System;
using Manager;

namespace Housing.Command
{
	// Token: 0x02000894 RID: 2196
	public class AddFolderCommand : ICommand
	{
		// Token: 0x06003921 RID: 14625 RVA: 0x0015109C File Offset: 0x0014F49C
		public void Do()
		{
			this.objectCtrl = Singleton<Housing>.Instance.AddFolder();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.AddList(this.objectCtrl);
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x001510C8 File Offset: 0x0014F4C8
		public void Redo()
		{
			Singleton<Housing>.Instance.RestoreObject(this.objectCtrl, null, -1, true);
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.AddList(this.objectCtrl);
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x001510F8 File Offset: 0x0014F4F8
		public void Undo()
		{
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.isOnFuncSkip = true;
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RemoveList(this.objectCtrl);
		}

		// Token: 0x040038E8 RID: 14568
		private ObjectCtrl objectCtrl;
	}
}
