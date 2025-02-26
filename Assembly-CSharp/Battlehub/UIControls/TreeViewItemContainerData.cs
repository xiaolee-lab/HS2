using System;
using System.Diagnostics;

namespace Battlehub.UIControls
{
	// Token: 0x020000AB RID: 171
	public class TreeViewItemContainerData : ItemContainerData
	{
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060003BF RID: 959 RVA: 0x00016978 File Offset: 0x00014D78
		// (remove) Token: 0x060003C0 RID: 960 RVA: 0x000169AC File Offset: 0x00014DAC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler<VirtualizingParentChangedEventArgs> ParentChanged;

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000169E0 File Offset: 0x00014DE0
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x000169E8 File Offset: 0x00014DE8
		public TreeViewItemContainerData Parent
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
				TreeViewItemContainerData parent = this.m_parent;
				this.m_parent = value;
				if (TreeViewItemContainerData.ParentChanged != null)
				{
					TreeViewItemContainerData.ParentChanged(this, new VirtualizingParentChangedEventArgs(parent, this.m_parent));
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00016A31 File Offset: 0x00014E31
		public object ParentItem
		{
			get
			{
				if (this.m_parent == null)
				{
					return null;
				}
				return this.m_parent.Item;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00016A4B File Offset: 0x00014E4B
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00016A53 File Offset: 0x00014E53
		public int Indent { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00016A5C File Offset: 0x00014E5C
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00016A64 File Offset: 0x00014E64
		public bool CanExpand { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00016A6D File Offset: 0x00014E6D
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00016A75 File Offset: 0x00014E75
		public bool IsExpanded { get; set; }

		// Token: 0x060003CA RID: 970 RVA: 0x00016A80 File Offset: 0x00014E80
		public bool IsDescendantOf(TreeViewItemContainerData ancestor)
		{
			if (ancestor == null)
			{
				return true;
			}
			if (ancestor == this)
			{
				return false;
			}
			for (TreeViewItemContainerData treeViewItemContainerData = this; treeViewItemContainerData != null; treeViewItemContainerData = treeViewItemContainerData.Parent)
			{
				if (ancestor == treeViewItemContainerData)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00016ABC File Offset: 0x00014EBC
		public bool HasChildren(VirtualizingTreeView treeView)
		{
			if (treeView == null)
			{
				return false;
			}
			int num = treeView.IndexOf(base.Item);
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)treeView.GetItemContainerData(num + 1);
			return treeViewItemContainerData != null && treeViewItemContainerData.Parent == this;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00016B08 File Offset: 0x00014F08
		public TreeViewItemContainerData FirstChild(VirtualizingTreeView treeView)
		{
			if (!this.HasChildren(treeView))
			{
				return null;
			}
			int num = treeView.IndexOf(base.Item);
			num++;
			return (TreeViewItemContainerData)treeView.GetItemContainerData(num);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00016B44 File Offset: 0x00014F44
		public TreeViewItemContainerData NextChild(VirtualizingTreeView treeView, TreeViewItemContainerData currentChild)
		{
			if (currentChild == null)
			{
				throw new ArgumentNullException("currentChild");
			}
			int num = treeView.IndexOf(currentChild.Item);
			num++;
			TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)treeView.GetItemContainerData(num);
			while (treeViewItemContainerData != null && treeViewItemContainerData.IsDescendantOf(this))
			{
				if (treeViewItemContainerData.Parent == this)
				{
					return treeViewItemContainerData;
				}
				num++;
				treeViewItemContainerData = (TreeViewItemContainerData)treeView.GetItemContainerData(num);
			}
			return null;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00016BB8 File Offset: 0x00014FB8
		public TreeViewItemContainerData LastChild(VirtualizingTreeView treeView)
		{
			if (!this.HasChildren(treeView))
			{
				return null;
			}
			int num = treeView.IndexOf(base.Item);
			TreeViewItemContainerData result = null;
			for (;;)
			{
				num++;
				TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)treeView.GetItemContainerData(num);
				if (treeViewItemContainerData == null || !treeViewItemContainerData.IsDescendantOf(this))
				{
					break;
				}
				if (treeViewItemContainerData.Parent == this)
				{
					result = treeViewItemContainerData;
				}
			}
			return result;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00016C1C File Offset: 0x0001501C
		public TreeViewItemContainerData LastDescendant(VirtualizingTreeView treeView)
		{
			if (!this.HasChildren(treeView))
			{
				return null;
			}
			int num = treeView.IndexOf(base.Item);
			TreeViewItemContainerData result = null;
			for (;;)
			{
				num++;
				TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)treeView.GetItemContainerData(num);
				if (treeViewItemContainerData == null || !treeViewItemContainerData.IsDescendantOf(this))
				{
					break;
				}
				result = treeViewItemContainerData;
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00016C71 File Offset: 0x00015071
		public override string ToString()
		{
			return "Data: " + base.Item;
		}

		// Token: 0x0400032F RID: 815
		private TreeViewItemContainerData m_parent;
	}
}
