using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005D7 RID: 1495
	public class GridViewSampleDemo : MonoBehaviour
	{
		// Token: 0x0600227A RID: 8826 RVA: 0x000BD5BC File Offset: 0x000BB9BC
		private void Start()
		{
			int num = this.mItemTotalCount / 3;
			if (this.mItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.InitListView(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000BD600 File Offset: 0x000BBA00
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
		{
			if (rowIndex < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("RowPrefab");
			ListItem15 component = loopListViewItem.GetComponent<ListItem15>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < 3; i++)
			{
				int num = rowIndex * 3 + i;
				if (num >= this.mItemTotalCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					component.mItemList[i].gameObject.SetActive(true);
					component.mItemList[i].mNameText.text = "Item" + num;
				}
			}
			return loopListViewItem;
		}

		// Token: 0x04002215 RID: 8725
		public LoopListView2 mLoopListView;

		// Token: 0x04002216 RID: 8726
		private const int mItemCountPerRow = 3;

		// Token: 0x04002217 RID: 8727
		private int mItemTotalCount = 100;
	}
}
