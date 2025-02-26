using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using Battlehub.UIControls;
using Housing.Command;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008AA RID: 2218
	[Serializable]
	public class ListUICtrl : UIDerived
	{
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060039A1 RID: 14753 RVA: 0x001530F8 File Offset: 0x001514F8
		// (set) Token: 0x060039A2 RID: 14754 RVA: 0x0015310F File Offset: 0x0015150F
		public bool Visible
		{
			get
			{
				return this.canvasGroup.alpha != 0f;
			}
			set
			{
				this.canvasGroup.Enable(value, false);
				if (!value)
				{
					this.virtualizingTreeView.SelectedIndex = -1;
				}
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060039A3 RID: 14755 RVA: 0x00153130 File Offset: 0x00151530
		// (set) Token: 0x060039A4 RID: 14756 RVA: 0x00153138 File Offset: 0x00151538
		public bool isOnFuncSkip { get; set; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x00153141 File Offset: 0x00151541
		public VirtualizingTreeView VirtualizingTreeView
		{
			[CompilerGenerated]
			get
			{
				return this.virtualizingTreeView;
			}
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0015314C File Offset: 0x0015154C
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			this.virtualizingTreeView.ItemDataBinding += this.OnItemDataBinding;
			this.virtualizingTreeView.SelectionChanged += this.OnSelectionChanged;
			this.virtualizingTreeView.ItemsRemoving += this.OnItemsRemoving;
			this.virtualizingTreeView.ItemsRemoved += this.OnItemsRemoved;
			this.virtualizingTreeView.ItemExpanding += this.OnItemExpanding;
			this.virtualizingTreeView.ItemCollapsed += this.OnItemCollapsed;
			this.virtualizingTreeView.ItemBeginDrag += this.OnItemBeginDrag;
			this.virtualizingTreeView.ItemDrop += this.OnItemDrop;
			this.virtualizingTreeView.ItemBeginDrop += this.OnItemBeginDrop;
			this.virtualizingTreeView.ItemEndDrag += this.OnItemEndDrag;
			this.virtualizingTreeView.AutoExpand = true;
			this.buttonDelete.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.virtualizingTreeView.RemoveSelectedItems();
			});
			this.buttonDuplicate.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Duplicate();
			});
			this.buttonFolder.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				Singleton<UndoRedoManager>.Instance.Do(new AddFolderCommand());
			});
			Selection instance = Singleton<Selection>.Instance;
			instance.onSelectFunc = (Action<ObjectCtrl[]>)Delegate.Combine(instance.onSelectFunc, new Action<ObjectCtrl[]>(this.OnSelectFunc));
			this.buttonDelete.interactable = false;
			this.buttonDuplicate.interactable = false;
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x001532FC File Offset: 0x001516FC
		public override void UpdateUI()
		{
			List<ObjectCtrl> objectCtrls = Singleton<Housing>.Instance.ObjectCtrls;
			this.virtualizingTreeView.Items = objectCtrls;
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x00153320 File Offset: 0x00151720
		public void AddList(ObjectCtrl _objectCtrl)
		{
			this.virtualizingTreeView.Add(_objectCtrl);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0015332F File Offset: 0x0015172F
		public void RemoveList(ObjectCtrl _objectCtrl)
		{
			this.virtualizingTreeView.RemoveChild(null, _objectCtrl);
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x00153340 File Offset: 0x00151740
		public void Duplicate()
		{
			IEnumerable selectedItems = this.virtualizingTreeView.SelectedItems;
			if (selectedItems != null)
			{
				ObjectCtrl[] array = (from v in selectedItems.OfType<ObjectCtrl>()
				where v != null
				select v).ToArray<ObjectCtrl>();
				if (!array.IsNullOrEmpty<ObjectCtrl>())
				{
					Singleton<UndoRedoManager>.Instance.Do(new DuplicateCommand(array));
				}
			}
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x001533A8 File Offset: 0x001517A8
		public void UpdateList(bool _select = true)
		{
			this.virtualizingTreeView.SetItems(Singleton<Housing>.Instance.ObjectCtrls, _select);
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x001533C0 File Offset: 0x001517C0
		public void RefreshList()
		{
			this.virtualizingTreeView.Refresh();
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x001533CD File Offset: 0x001517CD
		public void ClearList()
		{
			this.virtualizingTreeView.Items = null;
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x001533DB File Offset: 0x001517DB
		public void Select(ObjectCtrl _objectCtrl)
		{
			this.ExpandedLoop((_objectCtrl != null) ? _objectCtrl.Parent : null);
			this.virtualizingTreeView.SelectedItem = _objectCtrl;
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x00153400 File Offset: 0x00151800
		private void ExpandedLoop(ObjectCtrl _objectCtrl)
		{
			if (_objectCtrl == null)
			{
				return;
			}
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)this.virtualizingTreeView.GetItemContainerData(_objectCtrl);
			if (treeViewItemContainerData == null)
			{
				this.ExpandedLoop(_objectCtrl.Parent);
			}
			else if (treeViewItemContainerData.IsExpanded)
			{
				return;
			}
			this.virtualizingTreeView.Expand(_objectCtrl);
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x00153458 File Offset: 0x00151858
		private void OnSelectFunc(ObjectCtrl[] _objectCtrls)
		{
			bool flag = _objectCtrls.IsNullOrEmpty<ObjectCtrl>();
			this.buttonDelete.interactable = !flag;
			this.buttonDuplicate.interactable = !flag;
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0015348C File Offset: 0x0015188C
		private void OnItemDataBinding(object sender, VirtualizingTreeViewItemDataBindingArgs e)
		{
			ObjectCtrl objectCtrl = e.Item as ObjectCtrl;
			if (objectCtrl == null)
			{
				return;
			}
			Text componentInChildren = e.ItemPresenter.GetComponentInChildren<Text>(true);
			componentInChildren.text = objectCtrl.Name;
			OCItem ocitem = objectCtrl as OCItem;
			componentInChildren.color = ((ocitem == null) ? Color.white : ((!ocitem.IsOverlapNow) ? Color.white : Color.red));
			OCFolder ocfolder = objectCtrl as OCFolder;
			e.HasChildren = (ocfolder != null && ocfolder.Child.Count > 0);
			e.CanBeParent = (ocfolder != null);
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x00153530 File Offset: 0x00151930
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
			if (!e.OldItems.IsNullOrEmpty<object>())
			{
				foreach (object obj in e.OldItems)
				{
					ObjectCtrl objectCtrl = obj as ObjectCtrl;
					if (objectCtrl != null)
					{
						objectCtrl.OnDeselected();
					}
				}
			}
			ObjectCtrl[] array = (from v in e.NewItems
			select v as ObjectCtrl into v
			where v != null
			select v).ToArray<ObjectCtrl>();
			foreach (ObjectCtrl objectCtrl2 in array)
			{
				objectCtrl2.OnSelected();
			}
			if (Singleton<Selection>.IsInstance())
			{
				Singleton<Selection>.Instance.SetSelectObjects(array);
			}
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x00153610 File Offset: 0x00151A10
		private void OnItemsRemoving(object sender, ItemsCancelArgs e)
		{
			HashSet<ObjectCtrl> hashSet = new HashSet<ObjectCtrl>(from v in e.Items
			select v as ObjectCtrl);
			foreach (ObjectCtrl objectCtrl in from v in e.Items
			select v as ObjectCtrl)
			{
				this.CheckParent(hashSet, objectCtrl);
			}
			bool isMessage = false;
			e.Items = hashSet.Where(delegate(ObjectCtrl v)
			{
				bool flag = v.OnRemoving();
				isMessage |= !flag;
				return flag;
			}).ToList<object>();
			if (isMessage)
			{
				MapUIContainer.PushMessageUI("アイテムがいっぱいです", 2, 1, delegate()
				{
				});
			}
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x00153718 File Offset: 0x00151B18
		private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
		{
			HashSet<ObjectCtrl> hashSet = new HashSet<ObjectCtrl>(from v in e.Items
			select v as ObjectCtrl);
			foreach (ObjectCtrl objectCtrl in from v in e.Items
			select v as ObjectCtrl)
			{
				this.CheckParent(hashSet, objectCtrl);
			}
			DeleteCommand deleteCommand = new DeleteCommand(hashSet.ToArray<ObjectCtrl>());
			if (!this.isOnFuncSkip)
			{
				Singleton<UndoRedoManager>.Instance.Do(deleteCommand);
			}
			else
			{
				deleteCommand.Do();
			}
			this.isOnFuncSkip = false;
			IEnumerable selectedItems = this.virtualizingTreeView.SelectedItems;
			ObjectCtrl[] obj = (selectedItems != null) ? selectedItems.OfType<ObjectCtrl>().ToArray<ObjectCtrl>() : null;
			if (Singleton<Selection>.IsInstance())
			{
				Action<ObjectCtrl[]> onSelectFunc = Singleton<Selection>.Instance.onSelectFunc;
				if (onSelectFunc != null)
				{
					onSelectFunc(obj);
				}
			}
			this.RefreshList();
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x00153844 File Offset: 0x00151C44
		private void CheckParent(HashSet<ObjectCtrl> _objects, ObjectCtrl _objectCtrl)
		{
			if (_objectCtrl == null || _objectCtrl.Parent == null)
			{
				return;
			}
			if (_objects.Contains(_objectCtrl.Parent))
			{
				_objects.Remove(_objectCtrl);
				this.RemoveTargetChild(_objects, _objectCtrl as OCFolder);
				return;
			}
			this.CheckParent(_objects, _objectCtrl.Parent);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x00153898 File Offset: 0x00151C98
		private void RemoveTargetChild(HashSet<ObjectCtrl> _objects, OCFolder _ocf)
		{
			if (_ocf == null)
			{
				return;
			}
			foreach (KeyValuePair<IObjectInfo, ObjectCtrl> keyValuePair in _ocf.Child)
			{
				_objects.Remove(keyValuePair.Value);
				this.RemoveTargetChild(_objects, keyValuePair.Value as OCFolder);
			}
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x00153918 File Offset: 0x00151D18
		private void OnItemExpanding(object sender, VirtualizingItemExpandingArgs e)
		{
			OCFolder ocfolder = e.Item as OCFolder;
			if (ocfolder == null)
			{
				return;
			}
			if (ocfolder.Child.Count > 0)
			{
				List<ObjectCtrl> childObjectCtrls = ocfolder.ChildObjectCtrls;
				e.Children = childObjectCtrls;
				e.ChildrenExpand = from v in childObjectCtrls
				select v.ObjectInfo is OIFolder && ((OIFolder)v.ObjectInfo).Expand;
			}
			ocfolder.OIFolder.Expand = true;
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x0015398C File Offset: 0x00151D8C
		private void OnItemCollapsed(object sender, VirtualizingItemCollapsedArgs e)
		{
			OCFolder ocfolder = e.Item as OCFolder;
			if (ocfolder == null)
			{
				return;
			}
			ocfolder.OIFolder.Expand = false;
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x001539B8 File Offset: 0x00151DB8
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x001539BA File Offset: 0x00151DBA
		private void OnItemDrop(object sender, ItemDropArgs args)
		{
			if (args.DropTarget == null)
			{
				return;
			}
			Singleton<UndoRedoManager>.Instance.Do(new ListDropCommand(args));
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x001539D8 File Offset: 0x00151DD8
		private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
		{
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x001539DA File Offset: 0x00151DDA
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x001539DC File Offset: 0x00151DDC
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

		// Token: 0x0400393C RID: 14652
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x0400393D RID: 14653
		[SerializeField]
		[Header("一覧関係")]
		private VirtualizingTreeView virtualizingTreeView;

		// Token: 0x0400393E RID: 14654
		[SerializeField]
		[Header("操作関係")]
		private Button buttonDelete;

		// Token: 0x0400393F RID: 14655
		[SerializeField]
		private Button buttonDuplicate;

		// Token: 0x04003940 RID: 14656
		[SerializeField]
		private Button buttonFolder;
	}
}
