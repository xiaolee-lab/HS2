using System;
using System.Collections.Generic;
using System.Linq;
using Battlehub.UIControls;
using Manager;

namespace Housing.Command
{
	// Token: 0x02000899 RID: 2201
	public class ListDropCommand : ICommand
	{
		// Token: 0x0600393C RID: 14652 RVA: 0x00151598 File Offset: 0x0014F998
		public ListDropCommand(ItemDropArgs _args)
		{
			this.target = (ObjectCtrl)_args.DropTarget;
			this.action = _args.Action;
			this.infos = (from v in _args.DragItems
			select new ListDropCommand.Info((ObjectCtrl)v)).ToArray<ListDropCommand.Info>();
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x001515FC File Offset: 0x0014F9FC
		public void Do()
		{
			ObjectCtrl objectCtrl = this.target;
			ItemDropAction itemDropAction = this.action;
			if (itemDropAction != ItemDropAction.SetLastChild)
			{
				if (itemDropAction != ItemDropAction.SetNextSibling)
				{
					if (itemDropAction == ItemDropAction.SetPrevSibling)
					{
						foreach (ListDropCommand.Info info in this.infos)
						{
							ObjectCtrl objectCtrl2 = info.ObjectCtrl;
							if (objectCtrl2.Parent != objectCtrl.Parent)
							{
								objectCtrl2.OnAttach(objectCtrl.Parent, -1);
							}
							List<IObjectInfo> list = this.ChildrenOf(objectCtrl.Parent);
							int num = list.IndexOf(objectCtrl.ObjectInfo);
							int num2 = list.IndexOf(objectCtrl2.ObjectInfo);
							list.Remove(objectCtrl2.ObjectInfo);
							list.Insert((num <= num2) ? num : (num - 1), objectCtrl2.ObjectInfo);
						}
					}
				}
				else
				{
					for (int j = this.infos.Length - 1; j >= 0; j--)
					{
						ObjectCtrl objectCtrl3 = this.infos[j].ObjectCtrl;
						List<IObjectInfo> list2 = this.ChildrenOf(objectCtrl.Parent);
						int num3 = list2.IndexOf(objectCtrl.ObjectInfo);
						if (objectCtrl3.Parent != objectCtrl.Parent)
						{
							objectCtrl3.OnAttach(objectCtrl.Parent, num3 + 1);
						}
						else
						{
							int num4 = list2.IndexOf(objectCtrl3.ObjectInfo);
							list2.Remove(objectCtrl3.ObjectInfo);
							list2.Insert(num3 + ((num3 >= num4) ? 0 : 1), objectCtrl3.ObjectInfo);
						}
					}
				}
			}
			else
			{
				foreach (ListDropCommand.Info info2 in this.infos)
				{
					info2.ObjectCtrl.OnAttach(this.target, -1);
				}
			}
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x001517D9 File Offset: 0x0014FBD9
		public void Redo()
		{
			this.Do();
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.UpdateUI();
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x001517F8 File Offset: 0x0014FBF8
		public void Undo()
		{
			ItemDropAction itemDropAction = this.action;
			if (itemDropAction != ItemDropAction.SetLastChild)
			{
				if (itemDropAction != ItemDropAction.SetNextSibling)
				{
					if (itemDropAction == ItemDropAction.SetPrevSibling)
					{
						foreach (ListDropCommand.Info info in this.infos)
						{
							if (info.ObjectCtrl.Parent != info.Parent)
							{
								info.ObjectCtrl.OnAttach(info.Parent, info.Index);
							}
							else
							{
								List<IObjectInfo> list = this.ChildrenOf(info.Parent);
								list.Remove(info.ObjectCtrl.ObjectInfo);
								list.Insert(info.Index, info.ObjectCtrl.ObjectInfo);
							}
						}
					}
				}
				else
				{
					foreach (ListDropCommand.Info info2 in this.infos)
					{
						if (info2.ObjectCtrl.Parent != info2.Parent)
						{
							info2.ObjectCtrl.OnAttach(info2.Parent, info2.Index);
						}
						else
						{
							List<IObjectInfo> list2 = this.ChildrenOf(info2.Parent);
							list2.Remove(info2.ObjectCtrl.ObjectInfo);
							list2.Insert(info2.Index, info2.ObjectCtrl.ObjectInfo);
						}
					}
				}
			}
			else
			{
				foreach (ListDropCommand.Info info3 in this.infos)
				{
					info3.ObjectCtrl.OnAttach(info3.Parent, info3.Index);
				}
			}
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.UpdateUI();
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x001519B8 File Offset: 0x0014FDB8
		private List<IObjectInfo> ChildrenOf(ObjectCtrl parent)
		{
			OCFolder ocfolder = parent as OCFolder;
			if (ocfolder != null)
			{
				return ocfolder.OIFolder.Child;
			}
			if (parent != null)
			{
				return parent.CraftInfo.ObjectInfos;
			}
			return Singleton<Housing>.Instance.CraftInfo.ObjectInfos;
		}

		// Token: 0x040038F4 RID: 14580
		private ListDropCommand.Info[] infos;

		// Token: 0x040038F5 RID: 14581
		private ObjectCtrl target;

		// Token: 0x040038F6 RID: 14582
		private ItemDropAction action;

		// Token: 0x0200089A RID: 2202
		private class Info
		{
			// Token: 0x06003942 RID: 14658 RVA: 0x00151A0C File Offset: 0x0014FE0C
			public Info(ObjectCtrl _objectCtrl)
			{
				this.ObjectCtrl = _objectCtrl;
				this.Parent = this.ObjectCtrl.Parent;
				this.Index = this.ObjectCtrl.InfoIndex;
			}

			// Token: 0x17000A4C RID: 2636
			// (get) Token: 0x06003943 RID: 14659 RVA: 0x00151A3D File Offset: 0x0014FE3D
			// (set) Token: 0x06003944 RID: 14660 RVA: 0x00151A45 File Offset: 0x0014FE45
			public ObjectCtrl ObjectCtrl { get; private set; }

			// Token: 0x17000A4D RID: 2637
			// (get) Token: 0x06003945 RID: 14661 RVA: 0x00151A4E File Offset: 0x0014FE4E
			// (set) Token: 0x06003946 RID: 14662 RVA: 0x00151A56 File Offset: 0x0014FE56
			public ObjectCtrl Parent { get; private set; }

			// Token: 0x17000A4E RID: 2638
			// (get) Token: 0x06003947 RID: 14663 RVA: 0x00151A5F File Offset: 0x0014FE5F
			// (set) Token: 0x06003948 RID: 14664 RVA: 0x00151A67 File Offset: 0x0014FE67
			public int Index { get; private set; }
		}
	}
}
