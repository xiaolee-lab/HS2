using System;
using Manager;

namespace Housing.Command
{
	// Token: 0x02000893 RID: 2195
	public class AddItemCommand : ICommand
	{
		// Token: 0x0600391C RID: 14620 RVA: 0x00150F2E File Offset: 0x0014F32E
		public AddItemCommand(int _id)
		{
			this.id = _id;
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x00150F40 File Offset: 0x0014F340
		public void Do()
		{
			this.objectCtrl = Singleton<Housing>.Instance.AddObject(this.id);
			bool flag = Singleton<Housing>.Instance.CheckOverlap(this.objectCtrl as OCItem);
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.AddList(this.objectCtrl);
			if (flag)
			{
				Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
			}
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x00150FC4 File Offset: 0x0014F3C4
		public void Redo()
		{
			bool flag = Singleton<Housing>.Instance.RestoreObject(this.objectCtrl, null, -1, true);
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.AddList(this.objectCtrl);
			if (flag)
			{
				Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
			}
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x00151030 File Offset: 0x0014F430
		public void Undo()
		{
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.isOnFuncSkip = true;
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RemoveList(this.objectCtrl);
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x040038E6 RID: 14566
		private int id;

		// Token: 0x040038E7 RID: 14567
		private ObjectCtrl objectCtrl;
	}
}
