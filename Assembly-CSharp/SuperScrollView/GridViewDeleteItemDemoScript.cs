using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D5 RID: 1493
	public class GridViewDeleteItemDemoScript : MonoBehaviour
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x000BCF9C File Offset: 0x000BB39C
		private void Start()
		{
			this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			int num = this.mListItemTotalCount / 3;
			if (this.mListItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.InitListView(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mSelectAllButton.onClick.AddListener(new UnityAction(this.OnSelectAllBtnClicked));
			this.mCancelAllButton.onClick.AddListener(new UnityAction(this.OnCancelAllBtnClicked));
			this.mDeleteButton.onClick.AddListener(new UnityAction(this.OnDeleteBtnClicked));
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000BD05D File Offset: 0x000BB45D
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000BD06C File Offset: 0x000BB46C
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem10 component = loopListViewItem.GetComponent<ListItem10>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < 3; i++)
			{
				int num = index * 3 + i;
				if (num >= this.mListItemTotalCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
					if (itemDataByIndex != null)
					{
						component.mItemList[i].gameObject.SetActive(true);
						component.mItemList[i].SetItemData(itemDataByIndex, num);
					}
					else
					{
						component.mItemList[i].gameObject.SetActive(false);
					}
				}
			}
			return loopListViewItem;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000BD139 File Offset: 0x000BB539
		private void OnSelectAllBtnClicked()
		{
			DataSourceMgr.Get.CheckAllItem();
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000BD150 File Offset: 0x000BB550
		private void OnCancelAllBtnClicked()
		{
			DataSourceMgr.Get.UnCheckAllItem();
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000BD168 File Offset: 0x000BB568
		private void OnDeleteBtnClicked()
		{
			if (!DataSourceMgr.Get.DeleteAllCheckedItem())
			{
				return;
			}
			this.SetListItemTotalCount(DataSourceMgr.Get.TotalItemCount);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000BD198 File Offset: 0x000BB598
		private void SetListItemTotalCount(int count)
		{
			this.mListItemTotalCount = count;
			if (this.mListItemTotalCount < 0)
			{
				this.mListItemTotalCount = 0;
			}
			if (this.mListItemTotalCount > DataSourceMgr.Get.TotalItemCount)
			{
				this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			}
			int num = this.mListItemTotalCount / 3;
			if (this.mListItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.SetListItemCount(num, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x04002204 RID: 8708
		public LoopListView2 mLoopListView;

		// Token: 0x04002205 RID: 8709
		public Button mSelectAllButton;

		// Token: 0x04002206 RID: 8710
		public Button mCancelAllButton;

		// Token: 0x04002207 RID: 8711
		public Button mDeleteButton;

		// Token: 0x04002208 RID: 8712
		public Button mBackButton;

		// Token: 0x04002209 RID: 8713
		private const int mItemCountPerRow = 3;

		// Token: 0x0400220A RID: 8714
		private int mListItemTotalCount;
	}
}
