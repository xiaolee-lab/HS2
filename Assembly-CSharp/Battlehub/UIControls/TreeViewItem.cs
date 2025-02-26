using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x02000095 RID: 149
	public class TreeViewItem : ItemContainer
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000243 RID: 579 RVA: 0x0000F3F4 File Offset: 0x0000D7F4
		// (remove) Token: 0x06000244 RID: 580 RVA: 0x0000F428 File Offset: 0x0000D828
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler<ParentChangedEventArgs> ParentChanged;

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000F45C File Offset: 0x0000D85C
		private TreeView TreeView
		{
			get
			{
				if (this.m_treeView == null)
				{
					this.m_treeView = base.GetComponentInParent<TreeView>();
				}
				return this.m_treeView;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000F481 File Offset: 0x0000D881
		public int Indent
		{
			get
			{
				return this.m_indent;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000F489 File Offset: 0x0000D889
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000F494 File Offset: 0x0000D894
		public TreeViewItem Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				if (this.m_parent == value)
				{
					return;
				}
				TreeViewItem parent = this.m_parent;
				this.m_parent = value;
				if (this.m_parent != null && this.TreeView != null && this.m_itemLayout != null)
				{
					this.m_indent = this.m_parent.m_indent + this.TreeView.Indent;
					this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
					int siblingIndex = base.transform.GetSiblingIndex();
					this.SetIndent(this, ref siblingIndex);
				}
				else
				{
					this.ZeroIndent();
				}
				if (this.TreeView != null && TreeViewItem.ParentChanged != null)
				{
					TreeViewItem.ParentChanged(this, new ParentChangedEventArgs(parent, this.m_parent));
				}
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000F5AC File Offset: 0x0000D9AC
		public void UpdateIndent()
		{
			if (this.m_parent != null && this.TreeView != null && this.m_itemLayout != null)
			{
				this.m_indent = this.m_parent.m_indent + this.TreeView.Indent;
				this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
				int siblingIndex = base.transform.GetSiblingIndex();
				this.SetIndent(this, ref siblingIndex);
			}
			else
			{
				this.ZeroIndent();
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000F670 File Offset: 0x0000DA70
		private void ZeroIndent()
		{
			this.m_indent = 0;
			if (this.m_itemLayout != null)
			{
				this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000F6DC File Offset: 0x0000DADC
		private void SetIndent(TreeViewItem parent, ref int siblingIndex)
		{
			for (;;)
			{
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(siblingIndex + 1);
				if (treeViewItem == null)
				{
					break;
				}
				if (treeViewItem.Parent != parent)
				{
					return;
				}
				treeViewItem.m_indent = parent.m_indent + this.TreeView.Indent;
				treeViewItem.m_itemLayout.padding.left = treeViewItem.m_indent;
				siblingIndex++;
				this.SetIndent(treeViewItem, ref siblingIndex);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000F75D File Offset: 0x0000DB5D
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000F765 File Offset: 0x0000DB65
		public override bool IsSelected
		{
			get
			{
				return base.IsSelected;
			}
			set
			{
				if (base.IsSelected != value)
				{
					this.m_toggle.isOn = value;
					base.IsSelected = value;
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000F786 File Offset: 0x0000DB86
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000F790 File Offset: 0x0000DB90
		public bool CanExpand
		{
			get
			{
				return this.m_canExpand;
			}
			set
			{
				if (this.m_canExpand != value)
				{
					this.m_canExpand = value;
					if (this.m_expander != null)
					{
						this.m_expander.CanExpand = this.m_canExpand;
					}
					if (!this.m_canExpand)
					{
						this.IsExpanded = false;
					}
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000F7E4 File Offset: 0x0000DBE4
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000F7EC File Offset: 0x0000DBEC
		public bool IsExpanded
		{
			get
			{
				return this.m_isExpanded;
			}
			set
			{
				if (this.m_isExpanded != value)
				{
					this.m_isExpanded = (value && this.m_canExpand);
					if (this.m_expander != null)
					{
						this.m_expander.IsOn = (value && this.m_canExpand);
					}
					if (this.TreeView != null)
					{
						if (this.m_isExpanded)
						{
							this.TreeView.Expand(this);
						}
						else
						{
							this.TreeView.Collapse(this);
						}
					}
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000F880 File Offset: 0x0000DC80
		public bool HasChildren
		{
			get
			{
				int siblingIndex = base.transform.GetSiblingIndex();
				if (this.TreeView == null)
				{
					return false;
				}
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(siblingIndex + 1);
				return treeViewItem != null && treeViewItem.Parent == this;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000F8DC File Offset: 0x0000DCDC
		public bool IsDescendantOf(TreeViewItem ancestor)
		{
			if (ancestor == null)
			{
				return true;
			}
			if (ancestor == this)
			{
				return false;
			}
			TreeViewItem treeViewItem = this;
			while (treeViewItem != null)
			{
				if (ancestor == treeViewItem)
				{
					return true;
				}
				treeViewItem = treeViewItem.Parent;
			}
			return false;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000F930 File Offset: 0x0000DD30
		public TreeViewItem FirstChild()
		{
			if (!this.HasChildren)
			{
				return null;
			}
			int num = base.transform.GetSiblingIndex();
			num++;
			return (TreeViewItem)this.TreeView.GetItemContainer(num);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000F970 File Offset: 0x0000DD70
		public TreeViewItem NextChild(TreeViewItem currentChild)
		{
			if (currentChild == null)
			{
				throw new ArgumentNullException("currentChild");
			}
			int num = currentChild.transform.GetSiblingIndex();
			num++;
			TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(num);
			while (treeViewItem != null && treeViewItem.IsDescendantOf(this))
			{
				if (treeViewItem.Parent == this)
				{
					return treeViewItem;
				}
				num++;
				treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(num);
			}
			return null;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000FA00 File Offset: 0x0000DE00
		public TreeViewItem LastChild()
		{
			if (!this.HasChildren)
			{
				return null;
			}
			int num = base.transform.GetSiblingIndex();
			TreeViewItem result = null;
			for (;;)
			{
				num++;
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(num);
				if (treeViewItem == null || !treeViewItem.IsDescendantOf(this))
				{
					break;
				}
				if (treeViewItem.Parent == this)
				{
					result = treeViewItem;
				}
			}
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000FA70 File Offset: 0x0000DE70
		public TreeViewItem LastDescendant()
		{
			if (!this.HasChildren)
			{
				return null;
			}
			int num = base.transform.GetSiblingIndex();
			TreeViewItem result = null;
			for (;;)
			{
				num++;
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(num);
				if (treeViewItem == null || !treeViewItem.IsDescendantOf(this))
				{
					break;
				}
				result = treeViewItem;
			}
			return result;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000FAD0 File Offset: 0x0000DED0
		protected override void AwakeOverride()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			this.m_toggle.interactable = false;
			this.m_toggle.isOn = this.IsSelected;
			this.m_expander = base.GetComponentInChildren<TreeViewExpander>();
			if (this.m_expander != null)
			{
				this.m_expander.CanExpand = this.m_canExpand;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000FB34 File Offset: 0x0000DF34
		protected override void StartOverride()
		{
			if (this.TreeView != null)
			{
				this.m_toggle.isOn = this.TreeView.IsItemSelected(base.Item);
				this.m_isSelected = this.m_toggle.isOn;
			}
			if (this.Parent != null)
			{
				this.m_indent = this.Parent.m_indent + this.TreeView.Indent;
				this.m_itemLayout.padding = new RectOffset(this.m_indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
			}
			if (this.CanExpand && this.TreeView.AutoExpand)
			{
				this.IsExpanded = true;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000FC18 File Offset: 0x0000E018
		public override void Clear()
		{
			base.Clear();
			this.m_parent = null;
			this.ZeroIndent();
			this.m_isSelected = false;
			this.m_toggle.isOn = this.m_isSelected;
			this.m_isExpanded = false;
			this.m_canExpand = false;
			this.m_expander.IsOn = false;
			this.m_expander.CanExpand = false;
		}

		// Token: 0x04000295 RID: 661
		private TreeViewExpander m_expander;

		// Token: 0x04000296 RID: 662
		[SerializeField]
		private HorizontalLayoutGroup m_itemLayout;

		// Token: 0x04000297 RID: 663
		private Toggle m_toggle;

		// Token: 0x04000298 RID: 664
		private TreeView m_treeView;

		// Token: 0x04000299 RID: 665
		private int m_indent;

		// Token: 0x0400029A RID: 666
		private TreeViewItem m_parent;

		// Token: 0x0400029B RID: 667
		private bool m_canExpand;

		// Token: 0x0400029C RID: 668
		private bool m_isExpanded;
	}
}
