using System;

namespace SuperScrollView
{
	// Token: 0x020005CC RID: 1484
	public class TreeViewItemCountData
	{
		// Token: 0x0600222A RID: 8746 RVA: 0x000BBBFA File Offset: 0x000B9FFA
		public bool IsChild(int index)
		{
			return index != this.mBeginIndex;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000BBC08 File Offset: 0x000BA008
		public int GetChildIndex(int index)
		{
			if (!this.IsChild(index))
			{
				return -1;
			}
			return index - this.mBeginIndex - 1;
		}

		// Token: 0x040021CF RID: 8655
		public int mTreeItemIndex;

		// Token: 0x040021D0 RID: 8656
		public int mChildCount;

		// Token: 0x040021D1 RID: 8657
		public bool mIsExpand = true;

		// Token: 0x040021D2 RID: 8658
		public int mBeginIndex;

		// Token: 0x040021D3 RID: 8659
		public int mEndIndex;
	}
}
