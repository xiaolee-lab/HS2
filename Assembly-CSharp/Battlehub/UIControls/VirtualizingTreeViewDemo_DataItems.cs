using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x02000099 RID: 153
	public class VirtualizingTreeViewDemo_DataItems : MonoBehaviour
	{
		// Token: 0x06000270 RID: 624 RVA: 0x00010398 File Offset: 0x0000E798
		private void Start()
		{
			this.TreeView.ItemDataBinding += this.OnItemDataBinding;
			this.TreeView.SelectionChanged += this.OnSelectionChanged;
			this.TreeView.ItemsRemoved += this.OnItemsRemoved;
			this.TreeView.ItemExpanding += this.OnItemExpanding;
			this.TreeView.ItemBeginDrag += this.OnItemBeginDrag;
			this.TreeView.ItemDrop += this.OnItemDrop;
			this.TreeView.ItemBeginDrop += this.OnItemBeginDrop;
			this.TreeView.ItemEndDrag += this.OnItemEndDrag;
			this.m_dataItems = new List<DataItem>();
			for (int i = 0; i < 100; i++)
			{
				DataItem item = new DataItem("DataItem " + i);
				this.m_dataItems.Add(item);
			}
			this.TreeView.Items = this.m_dataItems;
			if (this.m_buttons != null)
			{
				this.m_buttons.SetActive(false);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000104CC File Offset: 0x0000E8CC
		private void OnDestroy()
		{
			this.TreeView.ItemDataBinding -= this.OnItemDataBinding;
			this.TreeView.SelectionChanged -= this.OnSelectionChanged;
			this.TreeView.ItemsRemoved -= this.OnItemsRemoved;
			this.TreeView.ItemExpanding -= this.OnItemExpanding;
			this.TreeView.ItemBeginDrag -= this.OnItemBeginDrag;
			this.TreeView.ItemBeginDrop -= this.OnItemBeginDrop;
			this.TreeView.ItemDrop -= this.OnItemDrop;
			this.TreeView.ItemEndDrag -= this.OnItemEndDrag;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00010594 File Offset: 0x0000E994
		private void OnItemExpanding(object sender, VirtualizingItemExpandingArgs e)
		{
			DataItem dataItem = (DataItem)e.Item;
			if (dataItem.Children.Count > 0)
			{
				e.Children = dataItem.Children;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000105CA File Offset: 0x0000E9CA
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
			if (this.m_buttons != null)
			{
				this.m_buttons.SetActive(this.TreeView.SelectedItem != null);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000105FC File Offset: 0x0000E9FC
		private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
		{
			for (int i = 0; i < e.Items.Length; i++)
			{
				DataItem dataItem = (DataItem)e.Items[i];
				if (dataItem.Parent != null)
				{
					dataItem.Parent.Children.Remove(dataItem);
				}
				this.m_dataItems.Remove(dataItem);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0001065C File Offset: 0x0000EA5C
		private void OnItemDataBinding(object sender, VirtualizingTreeViewItemDataBindingArgs e)
		{
			DataItem dataItem = e.Item as DataItem;
			if (dataItem != null)
			{
				Text componentInChildren = e.ItemPresenter.GetComponentInChildren<Text>(true);
				componentInChildren.text = dataItem.Name;
				Image image = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
				image.sprite = Resources.Load<Sprite>("IconNew");
				e.HasChildren = (dataItem.Children.Count > 0);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000106C6 File Offset: 0x0000EAC6
		private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000106C8 File Offset: 0x0000EAC8
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000106CA File Offset: 0x0000EACA
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000106CC File Offset: 0x0000EACC
		private List<DataItem> ChildrenOf(DataItem parent)
		{
			if (parent == null)
			{
				return this.m_dataItems;
			}
			return parent.Children;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000106E4 File Offset: 0x0000EAE4
		private void OnItemDrop(object sender, ItemDropArgs args)
		{
			if (args.DropTarget == null)
			{
				return;
			}
			this.TreeView.ItemDropStdHandler(args, (DataItem item) => item.Parent, delegate(DataItem item, DataItem parent)
			{
				item.Parent = parent;
			}, (DataItem item, DataItem parent) => this.ChildrenOf(parent).IndexOf(item), delegate(DataItem item, DataItem parent)
			{
				this.ChildrenOf(parent).Remove(item);
			}, delegate(DataItem item, DataItem parent, int i)
			{
				this.ChildrenOf(parent).Insert(i, item);
			});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00010767 File Offset: 0x0000EB67
		public void ScrollIntoView()
		{
			this.TreeView.ScrollIntoView(this.TreeView.SelectedItem);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00010780 File Offset: 0x0000EB80
		public void Add()
		{
			IEnumerator enumerator = this.TreeView.SelectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DataItem dataItem = (DataItem)obj;
					DataItem dataItem2 = new DataItem("New Item");
					dataItem.Children.Add(dataItem2);
					dataItem2.Parent = dataItem;
					this.TreeView.AddChild(dataItem, dataItem2);
					this.TreeView.Expand(dataItem);
					DataItem dataItem3 = new DataItem("New Sub Item");
					dataItem2.Children.Add(dataItem3);
					dataItem3.Parent = dataItem2;
					this.TreeView.AddChild(dataItem2, dataItem3);
					this.TreeView.Expand(dataItem2);
					this.m_counter++;
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
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010864 File Offset: 0x0000EC64
		public void Remove()
		{
			foreach (DataItem dataItem in this.TreeView.SelectedItems.OfType<object>().ToArray<object>())
			{
				this.TreeView.RemoveChild(dataItem.Parent, dataItem);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000108B8 File Offset: 0x0000ECB8
		public void Collapse()
		{
			IEnumerator enumerator = this.TreeView.SelectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DataItem item = (DataItem)obj;
					this.TreeView.Collapse(item);
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
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00010928 File Offset: 0x0000ED28
		public void Expand()
		{
			IEnumerator enumerator = this.TreeView.SelectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DataItem item2 = (DataItem)obj;
					this.TreeView.ExpandAll(item2, (DataItem item) => item.Parent, (DataItem item) => item.Children);
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
		}

		// Token: 0x040002A3 RID: 675
		public VirtualizingTreeView TreeView;

		// Token: 0x040002A4 RID: 676
		private List<DataItem> m_dataItems;

		// Token: 0x040002A5 RID: 677
		[SerializeField]
		private GameObject m_buttons;

		// Token: 0x040002A6 RID: 678
		private int m_counter;
	}
}
