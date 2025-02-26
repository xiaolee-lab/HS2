using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Battlehub.UIControls
{
	// Token: 0x020000AC RID: 172
	public class VirtualizingTreeView : VirtualizingItemsControl<VirtualizingTreeViewItemDataBindingArgs>
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060003D2 RID: 978 RVA: 0x00016C9C File Offset: 0x0001509C
		// (remove) Token: 0x060003D3 RID: 979 RVA: 0x00016CD4 File Offset: 0x000150D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<VirtualizingItemExpandingArgs> ItemExpanding;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060003D4 RID: 980 RVA: 0x00016D0C File Offset: 0x0001510C
		// (remove) Token: 0x060003D5 RID: 981 RVA: 0x00016D44 File Offset: 0x00015144
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<VirtualizingItemCollapsedArgs> ItemCollapsed;

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00016D7A File Offset: 0x0001517A
		protected override bool CanScroll
		{
			get
			{
				return base.CanScroll || this.CanReparent;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00016D90 File Offset: 0x00015190
		protected override void OnEnableOverride()
		{
			base.OnEnableOverride();
			TreeViewItemContainerData.ParentChanged += this.OnTreeViewItemParentChanged;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00016DA9 File Offset: 0x000151A9
		protected override void OnDisableOverride()
		{
			base.OnDisableOverride();
			TreeViewItemContainerData.ParentChanged -= this.OnTreeViewItemParentChanged;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00016DC4 File Offset: 0x000151C4
		protected override ItemContainerData InstantiateItemContainerData(object item)
		{
			return new TreeViewItemContainerData
			{
				Item = item
			};
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00016DE0 File Offset: 0x000151E0
		public void AddChild(object parent, object item)
		{
			if (parent == null)
			{
				base.Add(item);
			}
			else
			{
				VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(parent);
				int num = -1;
				TreeViewItemContainerData treeViewItemContainerData;
				if (virtualizingTreeViewItem == null)
				{
					treeViewItemContainerData = (TreeViewItemContainerData)base.GetItemContainerData(parent);
					if (treeViewItemContainerData == null)
					{
						return;
					}
					if (treeViewItemContainerData.IsExpanded)
					{
						num = ((!treeViewItemContainerData.HasChildren(this)) ? (base.IndexOf(treeViewItemContainerData.Item) + 1) : (base.IndexOf(treeViewItemContainerData.LastDescendant(this).Item) + 1));
					}
					else
					{
						treeViewItemContainerData.CanExpand = true;
					}
				}
				else
				{
					if (virtualizingTreeViewItem.IsExpanded)
					{
						if (virtualizingTreeViewItem.HasChildren)
						{
							TreeViewItemContainerData treeViewItemContainerData2 = virtualizingTreeViewItem.LastDescendant();
							num = base.IndexOf(treeViewItemContainerData2.Item) + 1;
						}
						else
						{
							num = base.IndexOf(virtualizingTreeViewItem.Item) + 1;
						}
					}
					else
					{
						virtualizingTreeViewItem.CanExpand = true;
					}
					treeViewItemContainerData = virtualizingTreeViewItem.TreeViewItemData;
				}
				if (num > -1)
				{
					TreeViewItemContainerData treeViewItemContainerData3 = (TreeViewItemContainerData)this.Insert(num, item);
					VirtualizingTreeViewItem virtualizingTreeViewItem2 = (VirtualizingTreeViewItem)base.GetItemContainer(item);
					if (virtualizingTreeViewItem2 != null)
					{
						virtualizingTreeViewItem2.Parent = treeViewItemContainerData;
					}
					else
					{
						treeViewItemContainerData3.Parent = treeViewItemContainerData;
					}
				}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00016F1A File Offset: 0x0001531A
		public override void Remove(object item)
		{
			throw new NotSupportedException("Use Remove Child instead");
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00016F26 File Offset: 0x00015326
		public void RemoveChild(object parent, object item)
		{
			base.Remove(item);
			this.DataBindItem(parent);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00016F38 File Offset: 0x00015338
		[Obsolete("Use RemoveChild(object parent, object item) instead")]
		public void RemoveChild(object parent, object item, bool isLastChild)
		{
			if (parent == null)
			{
				base.Remove(item);
			}
			else if (base.GetItemContainer(item) != null)
			{
				base.Remove(item);
			}
			else if (isLastChild)
			{
				VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(parent);
				if (virtualizingTreeViewItem)
				{
					virtualizingTreeViewItem.CanExpand = false;
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00016F9C File Offset: 0x0001539C
		public void ChangeParent(object parent, object item)
		{
			if (base.IsDropInProgress)
			{
				return;
			}
			ItemContainerData itemContainerData = base.GetItemContainerData(item);
			if (parent == null)
			{
				if (itemContainerData == null)
				{
					base.Add(item);
				}
				else
				{
					ItemContainerData[] dragItems = new ItemContainerData[]
					{
						itemContainerData
					};
					if (this.CanDrop(dragItems, null))
					{
						this.Drop(dragItems, null, ItemDropAction.SetLastChild);
					}
				}
			}
			else
			{
				ItemContainerData itemContainerData2 = base.GetItemContainerData(parent);
				if (itemContainerData2 == null)
				{
					this.DestroyItems(new object[]
					{
						item
					}, false);
					return;
				}
				ItemContainerData[] dragItems2 = new ItemContainerData[]
				{
					itemContainerData
				};
				if (this.CanDrop(dragItems2, itemContainerData2))
				{
					this.Drop(dragItems2, itemContainerData2, ItemDropAction.SetLastChild);
				}
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00017040 File Offset: 0x00015440
		public bool IsExpanded(object item)
		{
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)base.GetItemContainerData(item);
			return treeViewItemContainerData != null && treeViewItemContainerData.IsExpanded;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00017068 File Offset: 0x00015468
		public bool Expand(object item)
		{
			VirtualizingTreeViewItem treeViewItem = this.GetTreeViewItem(item);
			if (treeViewItem != null)
			{
				treeViewItem.IsExpanded = true;
			}
			else
			{
				if ((TreeViewItemContainerData)base.GetItemContainerData(item) == null)
				{
					return false;
				}
				this.Internal_Expand(item);
			}
			return true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000170B4 File Offset: 0x000154B4
		public void Internal_Expand(object item)
		{
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)base.GetItemContainerData(item);
			if (treeViewItemContainerData == null)
			{
				throw new ArgumentException("TreeViewItemContainerData not found", "item");
			}
			if (treeViewItemContainerData.IsExpanded)
			{
				return;
			}
			treeViewItemContainerData.IsExpanded = true;
			if (this.m_expandSilently)
			{
				return;
			}
			if (this.ItemExpanding != null)
			{
				VirtualizingItemExpandingArgs virtualizingItemExpandingArgs = new VirtualizingItemExpandingArgs(treeViewItemContainerData.Item);
				this.ItemExpanding(this, virtualizingItemExpandingArgs);
				IEnumerable enumerable = (virtualizingItemExpandingArgs.Children != null) ? virtualizingItemExpandingArgs.Children.OfType<object>().ToArray<object>() : null;
				int num = base.IndexOf(treeViewItemContainerData.Item);
				VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(treeViewItemContainerData.Item);
				if (virtualizingTreeViewItem != null)
				{
					virtualizingTreeViewItem.CanExpand = (enumerable != null);
				}
				else
				{
					treeViewItemContainerData.CanExpand = (enumerable != null);
				}
				if (treeViewItemContainerData.CanExpand)
				{
					IEnumerator enumerator = enumerable.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object item2 = enumerator.Current;
							num++;
							TreeViewItemContainerData treeViewItemContainerData2 = (TreeViewItemContainerData)this.Insert(num, item2);
							VirtualizingTreeViewItem virtualizingTreeViewItem2 = (VirtualizingTreeViewItem)base.GetItemContainer(item2);
							if (virtualizingTreeViewItem2 != null)
							{
								virtualizingTreeViewItem2.Parent = treeViewItemContainerData;
							}
							else
							{
								treeViewItemContainerData2.Parent = treeViewItemContainerData;
							}
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
					if (virtualizingItemExpandingArgs.ChildrenExpand != null)
					{
						object[] array = virtualizingItemExpandingArgs.Children.OfType<object>().ToArray<object>();
						bool[] array2 = virtualizingItemExpandingArgs.ChildrenExpand.OfType<bool>().ToArray<bool>();
						for (int i = 0; i < array.Length; i++)
						{
							if (array2[i])
							{
								this.Expand(array[i]);
							}
						}
					}
					base.UpdateSelectedItemIndex();
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00017298 File Offset: 0x00015698
		public void Collapse(object item)
		{
			VirtualizingTreeViewItem treeViewItem = this.GetTreeViewItem(item);
			if (treeViewItem != null)
			{
				treeViewItem.IsExpanded = false;
			}
			else
			{
				this.Internal_Collapse(item);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000172CC File Offset: 0x000156CC
		public void Internal_Collapse(object item)
		{
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)base.GetItemContainerData(item);
			if (treeViewItemContainerData == null)
			{
				throw new ArgumentException("TreeViewItemContainerData not found", "item");
			}
			if (!treeViewItemContainerData.IsExpanded)
			{
				return;
			}
			treeViewItemContainerData.IsExpanded = false;
			int num = base.IndexOf(treeViewItemContainerData.Item);
			List<object> list = new List<object>();
			this.Collapse(treeViewItemContainerData, num + 1, list);
			if (list.Count > 0)
			{
				bool unselect = false;
				base.DestroyItems(list.ToArray(), unselect);
			}
			this.SelectedItems = this.SelectedItems;
			if (this.ItemCollapsed != null)
			{
				this.ItemCollapsed(this, new VirtualizingItemCollapsedArgs(item));
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00017374 File Offset: 0x00015774
		private void Collapse(object[] items)
		{
			List<object> list = new List<object>();
			for (int i = 0; i < items.Length; i++)
			{
				int num = base.IndexOf(items[i]);
				if (num >= 0)
				{
					TreeViewItemContainerData item = (TreeViewItemContainerData)base.GetItemContainerData(num);
					this.Collapse(item, num + 1, list);
				}
			}
			if (list.Count > 0)
			{
				bool unselect = false;
				base.DestroyItems(list.ToArray(), unselect);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000173E8 File Offset: 0x000157E8
		private void Collapse(TreeViewItemContainerData item, int itemIndex, List<object> itemsToDestroy)
		{
			for (;;)
			{
				TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)base.GetItemContainerData(itemIndex);
				if (treeViewItemContainerData == null || !treeViewItemContainerData.IsDescendantOf(item))
				{
					break;
				}
				itemsToDestroy.Add(treeViewItemContainerData.Item);
				itemIndex++;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00017430 File Offset: 0x00015830
		public override void DataBindItem(object item, VirtualizingItemContainer itemContainer)
		{
			itemContainer.Clear();
			if (item != null)
			{
				VirtualizingTreeViewItemDataBindingArgs virtualizingTreeViewItemDataBindingArgs = new VirtualizingTreeViewItemDataBindingArgs();
				virtualizingTreeViewItemDataBindingArgs.Item = item;
				virtualizingTreeViewItemDataBindingArgs.ItemPresenter = ((!(itemContainer.ItemPresenter == null)) ? itemContainer.ItemPresenter : base.gameObject);
				virtualizingTreeViewItemDataBindingArgs.EditorPresenter = ((!(itemContainer.EditorPresenter == null)) ? itemContainer.EditorPresenter : base.gameObject);
				base.RaiseItemDataBinding(virtualizingTreeViewItemDataBindingArgs);
				VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)itemContainer;
				virtualizingTreeViewItem.CanExpand = virtualizingTreeViewItemDataBindingArgs.HasChildren;
				virtualizingTreeViewItem.CanEdit = (this.CanEdit && virtualizingTreeViewItemDataBindingArgs.CanEdit);
				virtualizingTreeViewItem.CanDrag = (this.CanDrag && virtualizingTreeViewItemDataBindingArgs.CanDrag);
				virtualizingTreeViewItem.CanBeParent = virtualizingTreeViewItemDataBindingArgs.CanBeParent;
				virtualizingTreeViewItem.UpdateIndent();
			}
			else
			{
				VirtualizingTreeViewItem virtualizingTreeViewItem2 = (VirtualizingTreeViewItem)itemContainer;
				virtualizingTreeViewItem2.CanExpand = false;
				virtualizingTreeViewItem2.CanEdit = false;
				virtualizingTreeViewItem2.CanDrag = false;
				virtualizingTreeViewItem2.UpdateIndent();
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00017530 File Offset: 0x00015930
		private void OnTreeViewItemParentChanged(object sender, VirtualizingParentChangedEventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)sender;
			TreeViewItemContainerData oldParent = e.OldParent;
			if (base.DropMarker.Action != ItemDropAction.SetLastChild && base.DropMarker.Action != ItemDropAction.None)
			{
				if (oldParent != null && !oldParent.HasChildren(this))
				{
					VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(oldParent.Item);
					if (virtualizingTreeViewItem != null)
					{
						virtualizingTreeViewItem.CanExpand = false;
					}
					else
					{
						oldParent.CanExpand = false;
					}
				}
				return;
			}
			TreeViewItemContainerData newParent = e.NewParent;
			VirtualizingTreeViewItem virtualizingTreeViewItem2 = null;
			if (newParent != null)
			{
				virtualizingTreeViewItem2 = (VirtualizingTreeViewItem)base.GetItemContainer(newParent.Item);
			}
			if (virtualizingTreeViewItem2 != null)
			{
				if (virtualizingTreeViewItem2.CanExpand)
				{
					virtualizingTreeViewItem2.IsExpanded = true;
				}
				else
				{
					virtualizingTreeViewItem2.CanExpand = true;
					try
					{
						this.m_expandSilently = true;
						virtualizingTreeViewItem2.IsExpanded = true;
					}
					finally
					{
						this.m_expandSilently = false;
					}
				}
			}
			else if (newParent != null)
			{
				newParent.CanExpand = true;
				newParent.IsExpanded = true;
			}
			TreeViewItemContainerData treeViewItemContainerData2 = treeViewItemContainerData.FirstChild(this);
			TreeViewItemContainerData treeViewItemContainerData3;
			if (newParent != null)
			{
				treeViewItemContainerData3 = newParent.LastChild(this);
				if (treeViewItemContainerData3 == null)
				{
					treeViewItemContainerData3 = newParent;
				}
			}
			else
			{
				treeViewItemContainerData3 = (TreeViewItemContainerData)base.LastItemContainerData();
			}
			if (treeViewItemContainerData3 != treeViewItemContainerData)
			{
				TreeViewItemContainerData treeViewItemContainerData4 = treeViewItemContainerData3.LastDescendant(this);
				if (treeViewItemContainerData4 != null)
				{
					treeViewItemContainerData3 = treeViewItemContainerData4;
				}
				if (!treeViewItemContainerData3.IsDescendantOf(treeViewItemContainerData))
				{
					base.SetNextSiblingInternal(treeViewItemContainerData3, treeViewItemContainerData);
				}
			}
			if (treeViewItemContainerData2 != null)
			{
				this.MoveSubtree(treeViewItemContainerData, treeViewItemContainerData2);
			}
			if (oldParent != null && !oldParent.HasChildren(this))
			{
				VirtualizingTreeViewItem virtualizingTreeViewItem3 = (VirtualizingTreeViewItem)base.GetItemContainer(oldParent.Item);
				if (virtualizingTreeViewItem3 != null)
				{
					virtualizingTreeViewItem3.CanExpand = false;
				}
				else
				{
					oldParent.CanExpand = false;
				}
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00017714 File Offset: 0x00015B14
		private void MoveSubtree(TreeViewItemContainerData parent, TreeViewItemContainerData child)
		{
			int num = base.IndexOf(parent.Item);
			int num2 = base.IndexOf(child.Item);
			bool flag = false;
			if (num < num2)
			{
				flag = true;
			}
			TreeViewItemContainerData treeViewItemContainerData = parent;
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(treeViewItemContainerData.Item);
			if (virtualizingTreeViewItem != null)
			{
				virtualizingTreeViewItem.UpdateIndent();
			}
			while (child != null && child.IsDescendantOf(parent))
			{
				if (treeViewItemContainerData == child)
				{
					break;
				}
				base.SetNextSiblingInternal(treeViewItemContainerData, child);
				virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(child.Item);
				if (virtualizingTreeViewItem != null)
				{
					virtualizingTreeViewItem.UpdateIndent();
				}
				treeViewItemContainerData = child;
				if (flag)
				{
					num2++;
				}
				child = (TreeViewItemContainerData)base.GetItemContainerData(num2);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000177DC File Offset: 0x00015BDC
		protected override bool CanDrop(ItemContainerData[] dragItems, ItemContainerData dropTarget)
		{
			if (base.CanDrop(dragItems, dropTarget))
			{
				TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)dropTarget;
				foreach (TreeViewItemContainerData treeViewItemContainerData2 in dragItems)
				{
					if (treeViewItemContainerData == treeViewItemContainerData2 || (treeViewItemContainerData != null && treeViewItemContainerData.IsDescendantOf(treeViewItemContainerData2)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00017838 File Offset: 0x00015C38
		protected override void Drop(ItemContainerData[] dragItems, ItemContainerData dropTarget, ItemDropAction action)
		{
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)dropTarget;
			if (action == ItemDropAction.SetLastChild)
			{
				foreach (TreeViewItemContainerData treeViewItemContainerData2 in dragItems)
				{
					if (treeViewItemContainerData != null && (treeViewItemContainerData == treeViewItemContainerData2 || treeViewItemContainerData.IsDescendantOf(treeViewItemContainerData2)))
					{
						break;
					}
					this.SetParent(treeViewItemContainerData, treeViewItemContainerData2);
				}
			}
			else if (action == ItemDropAction.SetPrevSibling)
			{
				for (int j = 0; j < dragItems.Length; j++)
				{
					this.SetPrevSiblingInternal(treeViewItemContainerData, dragItems[j]);
				}
			}
			else if (action == ItemDropAction.SetNextSibling)
			{
				for (int k = dragItems.Length - 1; k >= 0; k--)
				{
					this.SetNextSiblingInternal(treeViewItemContainerData, dragItems[k]);
				}
			}
			base.UpdateSelectedItemIndex();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000178FC File Offset: 0x00015CFC
		protected override void SetNextSiblingInternal(ItemContainerData sibling, ItemContainerData nextSibling)
		{
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)sibling;
			TreeViewItemContainerData treeViewItemContainerData2 = treeViewItemContainerData.LastDescendant(this);
			if (treeViewItemContainerData2 == null)
			{
				treeViewItemContainerData2 = treeViewItemContainerData;
			}
			TreeViewItemContainerData treeViewItemContainerData3 = (TreeViewItemContainerData)nextSibling;
			TreeViewItemContainerData treeViewItemContainerData4 = treeViewItemContainerData3.FirstChild(this);
			base.SetNextSiblingInternal(treeViewItemContainerData2, nextSibling);
			if (treeViewItemContainerData4 != null)
			{
				this.MoveSubtree(treeViewItemContainerData3, treeViewItemContainerData4);
			}
			this.SetParent(treeViewItemContainerData.Parent, treeViewItemContainerData3);
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(treeViewItemContainerData3.Item);
			if (virtualizingTreeViewItem != null)
			{
				virtualizingTreeViewItem.UpdateIndent();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001797C File Offset: 0x00015D7C
		protected override void SetPrevSiblingInternal(ItemContainerData sibling, ItemContainerData prevSibling)
		{
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)sibling;
			TreeViewItemContainerData treeViewItemContainerData2 = (TreeViewItemContainerData)prevSibling;
			TreeViewItemContainerData treeViewItemContainerData3 = treeViewItemContainerData2.FirstChild(this);
			base.SetPrevSiblingInternal(sibling, prevSibling);
			if (treeViewItemContainerData3 != null)
			{
				this.MoveSubtree(treeViewItemContainerData2, treeViewItemContainerData3);
			}
			this.SetParent(treeViewItemContainerData.Parent, treeViewItemContainerData2);
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(treeViewItemContainerData2.Item);
			if (virtualizingTreeViewItem != null)
			{
				virtualizingTreeViewItem.UpdateIndent();
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000179E8 File Offset: 0x00015DE8
		private void SetParent(TreeViewItemContainerData parent, TreeViewItemContainerData child)
		{
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(child.Item);
			if (virtualizingTreeViewItem != null)
			{
				virtualizingTreeViewItem.Parent = parent;
			}
			else
			{
				child.Parent = parent;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00017A28 File Offset: 0x00015E28
		public void UpdateIndent(object obj)
		{
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(obj);
			if (virtualizingTreeViewItem == null)
			{
				return;
			}
			virtualizingTreeViewItem.UpdateIndent();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00017A58 File Offset: 0x00015E58
		protected override void DestroyItems(object[] items, bool unselect)
		{
			TreeViewItemContainerData[] source = (from item in items
			select base.GetItemContainerData(item)).OfType<TreeViewItemContainerData>().ToArray<TreeViewItemContainerData>();
			TreeViewItemContainerData[] array = (from container in source
			where container.Parent != null
			select container.Parent).ToArray<TreeViewItemContainerData>();
			this.Collapse(items);
			base.DestroyItems(items, unselect);
			foreach (TreeViewItemContainerData treeViewItemContainerData in array)
			{
				if (!treeViewItemContainerData.HasChildren(this))
				{
					VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.GetItemContainer(treeViewItemContainerData.Item);
					if (virtualizingTreeViewItem != null)
					{
						virtualizingTreeViewItem.CanExpand = false;
					}
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00017B2F File Offset: 0x00015F2F
		public VirtualizingTreeViewItem GetTreeViewItem(object item)
		{
			return base.GetItemContainer(item) as VirtualizingTreeViewItem;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00017B40 File Offset: 0x00015F40
		public void ScrollIntoView(object obj)
		{
			int num = base.IndexOf(obj);
			if (num < 0)
			{
				throw new InvalidOperationException(string.Format("item {0} does not exist or not visible", obj));
			}
			VirtualizingScrollRect componentInChildren = base.GetComponentInChildren<VirtualizingScrollRect>();
			componentInChildren.Index = num;
		}

		// Token: 0x04000335 RID: 821
		public int Indent = 20;

		// Token: 0x04000336 RID: 822
		public bool CanReparent = true;

		// Token: 0x04000337 RID: 823
		public bool AutoExpand;

		// Token: 0x04000338 RID: 824
		private bool m_expandSilently;
	}
}
