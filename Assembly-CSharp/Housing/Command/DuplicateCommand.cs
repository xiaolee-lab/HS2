using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Battlehub.UIControls;
using Manager;

namespace Housing.Command
{
	// Token: 0x02000897 RID: 2199
	public class DuplicateCommand : ICommand
	{
		// Token: 0x06003930 RID: 14640 RVA: 0x001512BA File Offset: 0x0014F6BA
		public DuplicateCommand(ObjectCtrl[] _objectCtrls)
		{
			this.infos = (from v in _objectCtrls
			select new DuplicateCommand.Info(v)).ToArray<DuplicateCommand.Info>();
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x001512F0 File Offset: 0x0014F6F0
		private VirtualizingTreeView VirtualizingTreeView
		{
			[CompilerGenerated]
			get
			{
				VirtualizingTreeView result;
				if ((result = this.virtualizingTreeView) == null)
				{
					result = (this.virtualizingTreeView = Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.VirtualizingTreeView);
				}
				return result;
			}
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00151328 File Offset: 0x0014F728
		public void Do()
		{
			bool flag = false;
			foreach (DuplicateCommand.Info info in this.infos)
			{
				info.ObjectCtrl = Singleton<Housing>.Instance.DuplicateObject(info.Source);
				if (info.ObjectCtrl != null)
				{
					flag |= Singleton<Housing>.Instance.CheckOverlap(info.ObjectCtrl);
					this.VirtualizingTreeView.AddChild(info.ObjectCtrl.Parent, info.ObjectCtrl);
				}
			}
			if (flag)
			{
				this.VirtualizingTreeView.Refresh();
			}
			this.VirtualizingTreeView.SelectedItems = from v in this.infos
			select v.ObjectCtrl;
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00151404 File Offset: 0x0014F804
		public void Redo()
		{
			bool flag = false;
			foreach (DuplicateCommand.Info info in this.infos)
			{
				if (info.ObjectCtrl != null)
				{
					flag |= Singleton<Housing>.Instance.RestoreObject(info.ObjectCtrl, info.Source.Parent, -1, true);
					VirtualizingTreeView virtualizingTreeView = this.VirtualizingTreeView;
					if (virtualizingTreeView != null)
					{
						virtualizingTreeView.AddChild(info.ObjectCtrl.Parent, info.ObjectCtrl);
					}
				}
			}
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
			if (flag)
			{
				Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x001514B8 File Offset: 0x0014F8B8
		public void Undo()
		{
			foreach (DuplicateCommand.Info info in this.infos)
			{
				if (info.ObjectCtrl != null)
				{
					Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.isOnFuncSkip = true;
					VirtualizingTreeView virtualizingTreeView = this.VirtualizingTreeView;
					if (virtualizingTreeView != null)
					{
						virtualizingTreeView.RemoveChild(info.ObjectCtrl.Parent, info.ObjectCtrl);
					}
				}
			}
			Singleton<CraftScene>.Instance.UICtrl.AddUICtrl.Reselect();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x040038EE RID: 14574
		private DuplicateCommand.Info[] infos;

		// Token: 0x040038EF RID: 14575
		private VirtualizingTreeView virtualizingTreeView;

		// Token: 0x02000898 RID: 2200
		private class Info
		{
			// Token: 0x06003937 RID: 14647 RVA: 0x00151566 File Offset: 0x0014F966
			public Info(ObjectCtrl _objectCtrl)
			{
				this.Source = _objectCtrl;
			}

			// Token: 0x17000A4A RID: 2634
			// (get) Token: 0x06003938 RID: 14648 RVA: 0x00151575 File Offset: 0x0014F975
			// (set) Token: 0x06003939 RID: 14649 RVA: 0x0015157D File Offset: 0x0014F97D
			public ObjectCtrl Source { get; private set; }

			// Token: 0x17000A4B RID: 2635
			// (get) Token: 0x0600393A RID: 14650 RVA: 0x00151586 File Offset: 0x0014F986
			// (set) Token: 0x0600393B RID: 14651 RVA: 0x0015158E File Offset: 0x0014F98E
			public ObjectCtrl ObjectCtrl { get; set; }
		}
	}
}
