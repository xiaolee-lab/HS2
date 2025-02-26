using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020000AF RID: 175
	public class VirtualizingTreeViewItem : VirtualizingItemContainer
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00018281 File Offset: 0x00016681
		private VirtualizingTreeView TreeView
		{
			get
			{
				return base.ItemsControl as VirtualizingTreeView;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001828E File Offset: 0x0001668E
		public float Indent
		{
			get
			{
				return (float)this.m_treeViewItemData.Indent;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001829C File Offset: 0x0001669C
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x000182A4 File Offset: 0x000166A4
		public override object Item
		{
			get
			{
				return base.Item;
			}
			set
			{
				base.Item = value;
				this.m_treeViewItemData = (TreeViewItemContainerData)this.TreeView.GetItemContainerData(value);
				if (this.m_treeViewItemData == null)
				{
					this.m_treeViewItemData = new TreeViewItemContainerData();
					base.name = "Null";
					return;
				}
				this.UpdateIndent();
				if (this.m_expander != null)
				{
					this.m_expander.CanExpand = this.m_treeViewItemData.CanExpand;
					this.m_expander.IsOn = (this.m_treeViewItemData.IsExpanded && this.m_treeViewItemData.CanExpand);
				}
				base.name = base.Item.ToString() + " " + this.m_treeViewItemData.ToString();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001836D File Offset: 0x0001676D
		public TreeViewItemContainerData TreeViewItemData
		{
			get
			{
				return this.m_treeViewItemData;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00018375 File Offset: 0x00016775
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00018393 File Offset: 0x00016793
		public TreeViewItemContainerData Parent
		{
			get
			{
				return (this.m_treeViewItemData == null) ? null : this.m_treeViewItemData.Parent;
			}
			set
			{
				if (this.m_treeViewItemData == null)
				{
					return;
				}
				if (this.m_treeViewItemData.Parent == value)
				{
					return;
				}
				this.m_treeViewItemData.Parent = value;
				this.UpdateIndent();
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000183C8 File Offset: 0x000167C8
		public void UpdateIndent()
		{
			if (this.Parent != null && this.TreeView != null && this.m_itemLayout != null)
			{
				this.m_treeViewItemData.Indent = this.Parent.Indent + this.TreeView.Indent;
				this.m_itemLayout.padding = new RectOffset(this.m_treeViewItemData.Indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
				int num = this.TreeView.IndexOf(this.Item);
				this.SetIndent(this, ref num);
			}
			else
			{
				this.ZeroIndent();
				int num2 = this.TreeView.IndexOf(this.Item);
				if (this.HasChildren)
				{
					this.SetIndent(this, ref num2);
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000184BC File Offset: 0x000168BC
		private void ZeroIndent()
		{
			if (this.m_treeViewItemData != null)
			{
				this.m_treeViewItemData.Indent = 0;
			}
			if (this.m_itemLayout != null)
			{
				this.m_itemLayout.padding = new RectOffset(0, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00018534 File Offset: 0x00016934
		private void SetIndent(VirtualizingTreeViewItem parent, ref int itemIndex)
		{
			for (;;)
			{
				object itemAt = this.TreeView.GetItemAt(itemIndex + 1);
				VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)this.TreeView.GetItemContainer(itemAt);
				if (virtualizingTreeViewItem == null)
				{
					break;
				}
				if (virtualizingTreeViewItem.Item == null)
				{
					return;
				}
				if (virtualizingTreeViewItem.Parent != parent.m_treeViewItemData)
				{
					return;
				}
				virtualizingTreeViewItem.m_treeViewItemData.Indent = parent.m_treeViewItemData.Indent + this.TreeView.Indent;
				virtualizingTreeViewItem.m_itemLayout.padding.left = virtualizingTreeViewItem.m_treeViewItemData.Indent;
				itemIndex++;
				this.SetIndent(virtualizingTreeViewItem, ref itemIndex);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000185DD File Offset: 0x000169DD
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x000185E5 File Offset: 0x000169E5
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
					if (this.m_toggle != null)
					{
						this.m_toggle.isOn = value;
					}
					base.IsSelected = value;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00018617 File Offset: 0x00016A17
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00018638 File Offset: 0x00016A38
		public bool CanExpand
		{
			get
			{
				return this.m_treeViewItemData != null && this.m_treeViewItemData.CanExpand;
			}
			set
			{
				if (this.m_treeViewItemData == null)
				{
					return;
				}
				if (this.m_treeViewItemData.CanExpand != value)
				{
					this.m_treeViewItemData.CanExpand = value;
					if (this.m_expander != null)
					{
						this.m_expander.CanExpand = this.m_treeViewItemData.CanExpand;
					}
					if (!this.m_treeViewItemData.CanExpand)
					{
						this.IsExpanded = false;
					}
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000186AC File Offset: 0x00016AAC
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x000186C8 File Offset: 0x00016AC8
		public bool IsExpanded
		{
			get
			{
				return this.m_treeViewItemData != null && this.m_treeViewItemData.IsExpanded;
			}
			set
			{
				if (this.m_treeViewItemData == null)
				{
					return;
				}
				if (this.m_treeViewItemData.IsExpanded != value)
				{
					if (this.m_expander != null)
					{
						this.m_expander.IsOn = (value && this.CanExpand);
					}
					if (this.TreeView != null)
					{
						if (value && this.CanExpand)
						{
							this.TreeView.Internal_Expand(this.m_treeViewItemData.Item);
						}
						else
						{
							this.TreeView.Internal_Collapse(this.m_treeViewItemData.Item);
						}
					}
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00018770 File Offset: 0x00016B70
		public bool HasChildren
		{
			get
			{
				return this.m_treeViewItemData != null && this.m_treeViewItemData.HasChildren(this.TreeView);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00018790 File Offset: 0x00016B90
		public TreeViewItemContainerData FirstChild()
		{
			return this.m_treeViewItemData.FirstChild(this.TreeView);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000187A3 File Offset: 0x00016BA3
		public TreeViewItemContainerData NextChild(TreeViewItemContainerData currentChild)
		{
			return this.m_treeViewItemData.NextChild(this.TreeView, currentChild);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000187B7 File Offset: 0x00016BB7
		public TreeViewItemContainerData LastChild()
		{
			return this.m_treeViewItemData.LastChild(this.TreeView);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000187CA File Offset: 0x00016BCA
		public TreeViewItemContainerData LastDescendant()
		{
			return this.m_treeViewItemData.LastDescendant(this.TreeView);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000187E0 File Offset: 0x00016BE0
		protected override void AwakeOverride()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			this.m_toggle.interactable = false;
			this.m_toggle.isOn = this.IsSelected;
			this.m_expander = base.GetComponentInChildren<TreeViewExpander>();
			if (this.m_expander != null)
			{
				this.m_expander.CanExpand = this.CanExpand;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00018844 File Offset: 0x00016C44
		protected override void StartOverride()
		{
			if (this.TreeView != null)
			{
				this.m_toggle.isOn = this.TreeView.IsItemSelected(this.Item);
				this.m_isSelected = this.m_toggle.isOn;
			}
			if (this.Parent != null)
			{
				this.m_treeViewItemData.Indent = this.Parent.Indent + this.TreeView.Indent;
				this.m_itemLayout.padding = new RectOffset(this.m_treeViewItemData.Indent, this.m_itemLayout.padding.right, this.m_itemLayout.padding.top, this.m_itemLayout.padding.bottom);
			}
			if (this.CanExpand && this.TreeView.AutoExpand)
			{
				this.IsExpanded = true;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00018929 File Offset: 0x00016D29
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x0400033F RID: 831
		private TreeViewExpander m_expander;

		// Token: 0x04000340 RID: 832
		[SerializeField]
		private HorizontalLayoutGroup m_itemLayout;

		// Token: 0x04000341 RID: 833
		private Toggle m_toggle;

		// Token: 0x04000342 RID: 834
		private TreeViewItemContainerData m_treeViewItemData;
	}
}
