using System;
using System.Collections.Generic;

namespace SuperScrollView
{
	// Token: 0x020005CD RID: 1485
	public class TreeViewItemCountMgr
	{
		// Token: 0x0600222D RID: 8749 RVA: 0x000BBC3C File Offset: 0x000BA03C
		public void AddTreeItem(int count, bool isExpand)
		{
			TreeViewItemCountData treeViewItemCountData = new TreeViewItemCountData();
			treeViewItemCountData.mTreeItemIndex = this.mTreeItemDataList.Count;
			treeViewItemCountData.mChildCount = count;
			treeViewItemCountData.mIsExpand = isExpand;
			this.mTreeItemDataList.Add(treeViewItemCountData);
			this.mIsDirty = true;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000BBC81 File Offset: 0x000BA081
		public void Clear()
		{
			this.mTreeItemDataList.Clear();
			this.mLastQueryResult = null;
			this.mIsDirty = true;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000BBC9C File Offset: 0x000BA09C
		public TreeViewItemCountData GetTreeItem(int treeIndex)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return null;
			}
			return this.mTreeItemDataList[treeIndex];
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000BBCC4 File Offset: 0x000BA0C4
		public void SetItemChildCount(int treeIndex, int count)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return;
			}
			this.mIsDirty = true;
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[treeIndex];
			treeViewItemCountData.mChildCount = count;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000BBD08 File Offset: 0x000BA108
		public void SetItemExpand(int treeIndex, bool isExpand)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return;
			}
			this.mIsDirty = true;
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[treeIndex];
			treeViewItemCountData.mIsExpand = isExpand;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000BBD4C File Offset: 0x000BA14C
		public void ToggleItemExpand(int treeIndex)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return;
			}
			this.mIsDirty = true;
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[treeIndex];
			treeViewItemCountData.mIsExpand = !treeViewItemCountData.mIsExpand;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000BBD98 File Offset: 0x000BA198
		public bool IsTreeItemExpand(int treeIndex)
		{
			TreeViewItemCountData treeItem = this.GetTreeItem(treeIndex);
			return treeItem != null && treeItem.mIsExpand;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000BBDBC File Offset: 0x000BA1BC
		private void UpdateAllTreeItemDataIndex()
		{
			if (!this.mIsDirty)
			{
				return;
			}
			this.mLastQueryResult = null;
			this.mIsDirty = false;
			int count = this.mTreeItemDataList.Count;
			if (count == 0)
			{
				return;
			}
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[0];
			treeViewItemCountData.mBeginIndex = 0;
			treeViewItemCountData.mEndIndex = ((!treeViewItemCountData.mIsExpand) ? 0 : treeViewItemCountData.mChildCount);
			int mEndIndex = treeViewItemCountData.mEndIndex;
			for (int i = 1; i < count; i++)
			{
				TreeViewItemCountData treeViewItemCountData2 = this.mTreeItemDataList[i];
				treeViewItemCountData2.mBeginIndex = mEndIndex + 1;
				treeViewItemCountData2.mEndIndex = treeViewItemCountData2.mBeginIndex + ((!treeViewItemCountData2.mIsExpand) ? 0 : treeViewItemCountData2.mChildCount);
				mEndIndex = treeViewItemCountData2.mEndIndex;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000BBE88 File Offset: 0x000BA288
		public int TreeViewItemCount
		{
			get
			{
				return this.mTreeItemDataList.Count;
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000BBE98 File Offset: 0x000BA298
		public int GetTotalItemAndChildCount()
		{
			int count = this.mTreeItemDataList.Count;
			if (count == 0)
			{
				return 0;
			}
			this.UpdateAllTreeItemDataIndex();
			return this.mTreeItemDataList[count - 1].mEndIndex + 1;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000BBED4 File Offset: 0x000BA2D4
		public TreeViewItemCountData QueryTreeItemByTotalIndex(int totalIndex)
		{
			if (totalIndex < 0)
			{
				return null;
			}
			int count = this.mTreeItemDataList.Count;
			if (count == 0)
			{
				return null;
			}
			this.UpdateAllTreeItemDataIndex();
			if (this.mLastQueryResult != null && this.mLastQueryResult.mBeginIndex <= totalIndex && this.mLastQueryResult.mEndIndex >= totalIndex)
			{
				return this.mLastQueryResult;
			}
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[num2];
				if (treeViewItemCountData.mBeginIndex <= totalIndex && treeViewItemCountData.mEndIndex >= totalIndex)
				{
					this.mLastQueryResult = treeViewItemCountData;
					return treeViewItemCountData;
				}
				if (totalIndex > treeViewItemCountData.mEndIndex)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return null;
		}

		// Token: 0x040021D4 RID: 8660
		private List<TreeViewItemCountData> mTreeItemDataList = new List<TreeViewItemCountData>();

		// Token: 0x040021D5 RID: 8661
		private TreeViewItemCountData mLastQueryResult;

		// Token: 0x040021D6 RID: 8662
		private bool mIsDirty = true;
	}
}
