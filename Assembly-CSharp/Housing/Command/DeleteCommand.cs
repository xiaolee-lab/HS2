using System;
using System.Linq;
using Manager;

namespace Housing.Command
{
	// Token: 0x02000895 RID: 2197
	public class DeleteCommand : ICommand
	{
		// Token: 0x06003924 RID: 14628 RVA: 0x00151129 File Offset: 0x0014F529
		public DeleteCommand(ObjectCtrl[] _objectCtrls)
		{
			this.infos = (from v in _objectCtrls
			select new DeleteCommand.Info(v)).ToArray<DeleteCommand.Info>();
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x00151160 File Offset: 0x0014F560
		public void Do()
		{
			foreach (DeleteCommand.Info info in this.infos)
			{
				info.ObjectCtrl.OnDelete();
			}
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x001511AB File Offset: 0x0014F5AB
		public void Redo()
		{
			this.Do();
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.UpdateUI();
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x001511DC File Offset: 0x0014F5DC
		public void Undo()
		{
			foreach (DeleteCommand.Info info in this.infos)
			{
				Singleton<Housing>.Instance.RestoreObject(info.ObjectCtrl, info.Parent, info.Index, true);
			}
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.UpdateUI();
		}

		// Token: 0x040038E9 RID: 14569
		private DeleteCommand.Info[] infos;

		// Token: 0x02000896 RID: 2198
		private class Info
		{
			// Token: 0x06003929 RID: 14633 RVA: 0x00151256 File Offset: 0x0014F656
			public Info(ObjectCtrl _objectCtrl)
			{
				this.ObjectCtrl = _objectCtrl;
				this.Parent = this.ObjectCtrl.Parent;
				this.Index = this.ObjectCtrl.InfoIndex;
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x0600392A RID: 14634 RVA: 0x00151287 File Offset: 0x0014F687
			// (set) Token: 0x0600392B RID: 14635 RVA: 0x0015128F File Offset: 0x0014F68F
			public ObjectCtrl ObjectCtrl { get; private set; }

			// Token: 0x17000A47 RID: 2631
			// (get) Token: 0x0600392C RID: 14636 RVA: 0x00151298 File Offset: 0x0014F698
			// (set) Token: 0x0600392D RID: 14637 RVA: 0x001512A0 File Offset: 0x0014F6A0
			public ObjectCtrl Parent { get; private set; }

			// Token: 0x17000A48 RID: 2632
			// (get) Token: 0x0600392E RID: 14638 RVA: 0x001512A9 File Offset: 0x0014F6A9
			// (set) Token: 0x0600392F RID: 14639 RVA: 0x001512B1 File Offset: 0x0014F6B1
			public int Index { get; private set; }
		}
	}
}
