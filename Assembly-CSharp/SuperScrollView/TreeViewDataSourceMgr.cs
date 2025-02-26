using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005CB RID: 1483
	public class TreeViewDataSourceMgr : MonoBehaviour
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000BB9FB File Offset: 0x000B9DFB
		public static TreeViewDataSourceMgr Get
		{
			get
			{
				if (TreeViewDataSourceMgr.instance == null)
				{
					TreeViewDataSourceMgr.instance = UnityEngine.Object.FindObjectOfType<TreeViewDataSourceMgr>();
				}
				return TreeViewDataSourceMgr.instance;
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000BBA1C File Offset: 0x000B9E1C
		private void Awake()
		{
			this.Init();
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000BBA24 File Offset: 0x000B9E24
		public void Init()
		{
			this.DoRefreshDataSource();
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000BBA2C File Offset: 0x000B9E2C
		public TreeViewItemData GetItemDataByIndex(int index)
		{
			if (index < 0 || index >= this.mItemDataList.Count)
			{
				return null;
			}
			return this.mItemDataList[index];
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000BBA54 File Offset: 0x000B9E54
		public ItemData GetItemChildDataByIndex(int itemIndex, int childIndex)
		{
			TreeViewItemData itemDataByIndex = this.GetItemDataByIndex(itemIndex);
			if (itemDataByIndex == null)
			{
				return null;
			}
			return itemDataByIndex.GetChild(childIndex);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x000BBA78 File Offset: 0x000B9E78
		public int TreeViewItemCount
		{
			get
			{
				return this.mItemDataList.Count;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x000BBA88 File Offset: 0x000B9E88
		public int TotalTreeViewItemAndChildCount
		{
			get
			{
				int count = this.mItemDataList.Count;
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					num += this.mItemDataList[i].ChildCount;
				}
				return num;
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000BBACC File Offset: 0x000B9ECC
		private void DoRefreshDataSource()
		{
			this.mItemDataList.Clear();
			for (int i = 0; i < this.mTreeViewItemCount; i++)
			{
				TreeViewItemData treeViewItemData = new TreeViewItemData();
				treeViewItemData.mName = "Item" + i;
				treeViewItemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
				this.mItemDataList.Add(treeViewItemData);
				int num = this.mTreeViewChildItemCount;
				for (int j = 1; j <= num; j++)
				{
					ItemData itemData = new ItemData();
					itemData.mName = string.Concat(new object[]
					{
						"Item",
						i,
						":Child",
						j
					});
					itemData.mDesc = "Item Desc For " + itemData.mName;
					itemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
					itemData.mStarCount = UnityEngine.Random.Range(0, 6);
					itemData.mFileSize = UnityEngine.Random.Range(20, 999);
					treeViewItemData.AddChild(itemData);
				}
			}
		}

		// Token: 0x040021CB RID: 8651
		private List<TreeViewItemData> mItemDataList = new List<TreeViewItemData>();

		// Token: 0x040021CC RID: 8652
		private static TreeViewDataSourceMgr instance;

		// Token: 0x040021CD RID: 8653
		private int mTreeViewItemCount = 20;

		// Token: 0x040021CE RID: 8654
		private int mTreeViewChildItemCount = 30;
	}
}
