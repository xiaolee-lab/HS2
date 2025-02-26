using System;
using System.Collections.Generic;

namespace SuperScrollView
{
	// Token: 0x020005CA RID: 1482
	public class TreeViewItemData
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000BB995 File Offset: 0x000B9D95
		public int ChildCount
		{
			get
			{
				return this.mChildItemDataList.Count;
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000BB9A2 File Offset: 0x000B9DA2
		public void AddChild(ItemData data)
		{
			this.mChildItemDataList.Add(data);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000BB9B0 File Offset: 0x000B9DB0
		public ItemData GetChild(int index)
		{
			if (index < 0 || index >= this.mChildItemDataList.Count)
			{
				return null;
			}
			return this.mChildItemDataList[index];
		}

		// Token: 0x040021C8 RID: 8648
		public string mName;

		// Token: 0x040021C9 RID: 8649
		public string mIcon;

		// Token: 0x040021CA RID: 8650
		private List<ItemData> mChildItemDataList = new List<ItemData>();
	}
}
