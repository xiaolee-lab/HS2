using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000091 RID: 145
	public class TreeView : ItemsControl<TreeViewItemDataBindingArgs>
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600020F RID: 527 RVA: 0x0000E4BC File Offset: 0x0000C8BC
		// (remove) Token: 0x06000210 RID: 528 RVA: 0x0000E4F4 File Offset: 0x0000C8F4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemExpandingArgs> ItemExpanding;

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000E52A File Offset: 0x0000C92A
		protected override bool CanScroll
		{
			get
			{
				return base.CanScroll || this.CanReparent;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E540 File Offset: 0x0000C940
		protected override void OnEnableOverride()
		{
			base.OnEnableOverride();
			TreeViewItem.ParentChanged += this.OnTreeViewItemParentChanged;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E559 File Offset: 0x0000C959
		protected override void OnDisableOverride()
		{
			base.OnDisableOverride();
			TreeViewItem.ParentChanged -= this.OnTreeViewItemParentChanged;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E572 File Offset: 0x0000C972
		public TreeViewItem GetTreeViewItem(int siblingIndex)
		{
			return (TreeViewItem)base.GetItemContainer(siblingIndex);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E580 File Offset: 0x0000C980
		public TreeViewItem GetTreeViewItem(object obj)
		{
			return (TreeViewItem)base.GetItemContainer(obj);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E590 File Offset: 0x0000C990
		public void AddChild(object parent, object item)
		{
			if (parent == null)
			{
				base.Add(item);
			}
			else
			{
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(parent);
				if (treeViewItem == null)
				{
					return;
				}
				int num = -1;
				if (treeViewItem.IsExpanded)
				{
					if (treeViewItem.HasChildren)
					{
						TreeViewItem treeViewItem2 = treeViewItem.LastDescendant();
						num = base.IndexOf(treeViewItem2.Item) + 1;
					}
					else
					{
						num = base.IndexOf(treeViewItem.Item) + 1;
					}
				}
				else
				{
					treeViewItem.CanExpand = true;
				}
				if (num > -1)
				{
					TreeViewItem treeViewItem3 = (TreeViewItem)this.Insert(num, item);
					treeViewItem3.Parent = treeViewItem;
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E634 File Offset: 0x0000CA34
		public override void Remove(object item)
		{
			throw new NotSupportedException("This method is not supported for TreeView use RemoveChild instead");
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000E640 File Offset: 0x0000CA40
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
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(parent);
				if (treeViewItem)
				{
					treeViewItem.CanExpand = false;
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E6A4 File Offset: 0x0000CAA4
		public void ChangeParent(object parent, object item)
		{
			if (base.IsDropInProgress)
			{
				return;
			}
			ItemContainer itemContainer = base.GetItemContainer(item);
			if (itemContainer == null)
			{
				return;
			}
			ItemContainer itemContainer2 = base.GetItemContainer(parent);
			ItemContainer[] dragItems = new ItemContainer[]
			{
				itemContainer
			};
			if (this.CanDrop(dragItems, itemContainer2))
			{
				this.Drop(dragItems, itemContainer2, ItemDropAction.SetLastChild);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E6FC File Offset: 0x0000CAFC
		public bool IsExpanded(object item)
		{
			TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(item);
			return !(treeViewItem == null) && treeViewItem.IsExpanded;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000E72C File Offset: 0x0000CB2C
		public void Expand(TreeViewItem item)
		{
			if (this.m_expandSilently)
			{
				return;
			}
			if (this.ItemExpanding != null)
			{
				ItemExpandingArgs itemExpandingArgs = new ItemExpandingArgs(item.Item);
				this.ItemExpanding(this, itemExpandingArgs);
				IEnumerable children = itemExpandingArgs.Children;
				int num = item.transform.GetSiblingIndex();
				int num2 = base.IndexOf(item.Item);
				item.CanExpand = (children != null);
				if (item.CanExpand)
				{
					IEnumerator enumerator = children.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							num++;
							num2++;
							base.InsertItem(num2, obj);
							TreeViewItem treeViewItem = (TreeViewItem)base.InstantiateItemContainer(num);
							treeViewItem.Item = obj;
							treeViewItem.Parent = item;
							this.DataBindItem(obj, treeViewItem);
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
					base.UpdateSelectedItemIndex();
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E830 File Offset: 0x0000CC30
		public void Collapse(TreeViewItem item)
		{
			int siblingIndex = item.transform.GetSiblingIndex();
			int num = base.IndexOf(item.Item);
			this.Collapse(item, siblingIndex + 1, num + 1);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000E864 File Offset: 0x0000CC64
		private void Unselect(List<object> selectedItems, TreeViewItem item, ref int containerIndex, ref int itemIndex)
		{
			for (;;)
			{
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(containerIndex);
				if (treeViewItem == null || treeViewItem.Parent != item)
				{
					break;
				}
				containerIndex++;
				itemIndex++;
				selectedItems.Remove(treeViewItem.Item);
				this.Unselect(selectedItems, treeViewItem, ref containerIndex, ref itemIndex);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000E8CC File Offset: 0x0000CCCC
		private void Collapse(TreeViewItem item, int containerIndex, int itemIndex)
		{
			for (;;)
			{
				TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(containerIndex);
				if (treeViewItem == null || treeViewItem.Parent != item)
				{
					break;
				}
				this.Collapse(treeViewItem, containerIndex + 1, itemIndex + 1);
				base.RemoveItemAt(itemIndex);
				base.DestroyItemContainer(containerIndex);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000E928 File Offset: 0x0000CD28
		protected override ItemContainer InstantiateItemContainerOverride(GameObject container)
		{
			TreeViewItem treeViewItem = container.GetComponent<TreeViewItem>();
			if (treeViewItem == null)
			{
				treeViewItem = container.AddComponent<TreeViewItem>();
				treeViewItem.gameObject.name = "TreeViewItem";
			}
			return treeViewItem;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000E960 File Offset: 0x0000CD60
		protected override void DestroyItem(object item)
		{
			TreeViewItem treeViewItem = (TreeViewItem)base.GetItemContainer(item);
			if (treeViewItem != null)
			{
				this.Collapse(treeViewItem);
				base.DestroyItem(item);
				if (treeViewItem.Parent != null && !treeViewItem.Parent.HasChildren)
				{
					treeViewItem.Parent.CanExpand = false;
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000E9C4 File Offset: 0x0000CDC4
		public override void DataBindItem(object item, ItemContainer itemContainer)
		{
			TreeViewItemDataBindingArgs treeViewItemDataBindingArgs = new TreeViewItemDataBindingArgs();
			treeViewItemDataBindingArgs.Item = item;
			treeViewItemDataBindingArgs.ItemPresenter = ((!(itemContainer.ItemPresenter == null)) ? itemContainer.ItemPresenter : base.gameObject);
			treeViewItemDataBindingArgs.EditorPresenter = ((!(itemContainer.EditorPresenter == null)) ? itemContainer.EditorPresenter : base.gameObject);
			base.RaiseItemDataBinding(treeViewItemDataBindingArgs);
			TreeViewItem treeViewItem = (TreeViewItem)itemContainer;
			treeViewItem.CanExpand = treeViewItemDataBindingArgs.HasChildren;
			treeViewItem.CanEdit = treeViewItemDataBindingArgs.CanEdit;
			treeViewItem.CanDrag = treeViewItemDataBindingArgs.CanDrag;
			treeViewItem.CanDrop = treeViewItemDataBindingArgs.CanBeParent;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000EA6C File Offset: 0x0000CE6C
		protected override bool CanDrop(ItemContainer[] dragItems, ItemContainer dropTarget)
		{
			if (!base.CanDrop(dragItems, dropTarget))
			{
				return false;
			}
			TreeViewItem treeViewItem = (TreeViewItem)dropTarget;
			if (treeViewItem == null)
			{
				return true;
			}
			foreach (ItemContainer itemContainer in dragItems)
			{
				TreeViewItem ancestor = (TreeViewItem)itemContainer;
				if (treeViewItem.IsDescendantOf(ancestor))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000EAD0 File Offset: 0x0000CED0
		private void OnTreeViewItemParentChanged(object sender, ParentChangedEventArgs e)
		{
			TreeViewItem treeViewItem = (TreeViewItem)sender;
			if (!base.CanHandleEvent(treeViewItem))
			{
				return;
			}
			TreeViewItem oldParent = e.OldParent;
			if (base.DropMarker.Action != ItemDropAction.SetLastChild && base.DropMarker.Action != ItemDropAction.None)
			{
				if (oldParent != null && !oldParent.HasChildren)
				{
					oldParent.CanExpand = false;
				}
				return;
			}
			TreeViewItem newParent = e.NewParent;
			if (newParent != null)
			{
				if (newParent.CanExpand)
				{
					newParent.IsExpanded = true;
				}
				else
				{
					newParent.CanExpand = true;
					try
					{
						this.m_expandSilently = true;
						newParent.IsExpanded = true;
					}
					finally
					{
						this.m_expandSilently = false;
					}
				}
			}
			TreeViewItem treeViewItem2 = treeViewItem.FirstChild();
			TreeViewItem treeViewItem3;
			if (newParent != null)
			{
				treeViewItem3 = newParent.LastChild();
				if (treeViewItem3 == null)
				{
					treeViewItem3 = newParent;
				}
			}
			else
			{
				treeViewItem3 = (TreeViewItem)base.LastItemContainer();
			}
			if (treeViewItem3 != treeViewItem)
			{
				TreeViewItem treeViewItem4 = treeViewItem3.LastDescendant();
				if (treeViewItem4 != null)
				{
					treeViewItem3 = treeViewItem4;
				}
				if (!treeViewItem3.IsDescendantOf(treeViewItem))
				{
					base.SetNextSibling(treeViewItem3, treeViewItem);
				}
			}
			if (treeViewItem2 != null)
			{
				this.MoveSubtree(treeViewItem, treeViewItem2);
			}
			if (oldParent != null && !oldParent.HasChildren)
			{
				oldParent.CanExpand = false;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000EC44 File Offset: 0x0000D044
		private void MoveSubtree(TreeViewItem parent, TreeViewItem child)
		{
			int siblingIndex = parent.transform.GetSiblingIndex();
			int num = child.transform.GetSiblingIndex();
			bool flag = false;
			if (siblingIndex < num)
			{
				flag = true;
			}
			TreeViewItem treeViewItem = parent;
			while (child != null && child.IsDescendantOf(parent))
			{
				if (treeViewItem == child)
				{
					break;
				}
				base.SetNextSibling(treeViewItem, child);
				treeViewItem = child;
				if (flag)
				{
					num++;
				}
				child = (TreeViewItem)base.GetItemContainer(num);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000ECC8 File Offset: 0x0000D0C8
		protected override void Drop(ItemContainer[] dragItems, ItemContainer dropTarget, ItemDropAction action)
		{
			TreeViewItem treeViewItem = (TreeViewItem)dropTarget;
			if (action == ItemDropAction.SetLastChild)
			{
				foreach (TreeViewItem treeViewItem2 in dragItems)
				{
					if (treeViewItem != treeViewItem2)
					{
						treeViewItem2.Parent = treeViewItem;
					}
				}
			}
			else if (action == ItemDropAction.SetPrevSibling)
			{
				for (int j = 0; j < dragItems.Length; j++)
				{
					this.SetPrevSibling(treeViewItem, dragItems[j]);
				}
			}
			else if (action == ItemDropAction.SetNextSibling)
			{
				for (int k = dragItems.Length - 1; k >= 0; k--)
				{
					this.SetNextSibling(treeViewItem, dragItems[k]);
				}
			}
			base.UpdateSelectedItemIndex();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000ED74 File Offset: 0x0000D174
		protected override void SetNextSibling(ItemContainer sibling, ItemContainer nextSibling)
		{
			TreeViewItem treeViewItem = (TreeViewItem)sibling;
			TreeViewItem treeViewItem2 = treeViewItem.LastDescendant();
			if (treeViewItem2 == null)
			{
				treeViewItem2 = treeViewItem;
			}
			TreeViewItem treeViewItem3 = (TreeViewItem)nextSibling;
			TreeViewItem treeViewItem4 = treeViewItem3.FirstChild();
			base.SetNextSibling(treeViewItem2, nextSibling);
			if (treeViewItem4 != null)
			{
				this.MoveSubtree(treeViewItem3, treeViewItem4);
			}
			treeViewItem3.Parent = treeViewItem.Parent;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000EDD4 File Offset: 0x0000D1D4
		protected override void SetPrevSibling(ItemContainer sibling, ItemContainer prevSibling)
		{
			TreeViewItem treeViewItem = (TreeViewItem)sibling;
			TreeViewItem treeViewItem2 = (TreeViewItem)prevSibling;
			TreeViewItem treeViewItem3 = treeViewItem2.FirstChild();
			base.SetPrevSibling(sibling, prevSibling);
			if (treeViewItem3 != null)
			{
				this.MoveSubtree(treeViewItem2, treeViewItem3);
			}
			treeViewItem2.Parent = treeViewItem.Parent;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000EE20 File Offset: 0x0000D220
		public void UpdateIndent(object obj)
		{
			TreeViewItem treeViewItem = this.GetTreeViewItem(obj);
			if (treeViewItem == null)
			{
				return;
			}
			treeViewItem.UpdateIndent();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000EE48 File Offset: 0x0000D248
		public void FixScrollRect()
		{
			Canvas.ForceUpdateCanvases();
			RectTransform component = this.Panel.GetComponent<RectTransform>();
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, component.rect.height - 0.01f);
		}

		// Token: 0x04000287 RID: 647
		public int Indent = 20;

		// Token: 0x04000288 RID: 648
		public bool CanReparent = true;

		// Token: 0x04000289 RID: 649
		public bool AutoExpand;

		// Token: 0x0400028A RID: 650
		private bool m_expandSilently;
	}
}
