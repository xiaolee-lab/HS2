using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D4 RID: 1492
	public class DeleteItemDemoScript : MonoBehaviour
	{
		// Token: 0x06002263 RID: 8803 RVA: 0x000BCE08 File Offset: 0x000BB208
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mSelectAllButton.onClick.AddListener(new UnityAction(this.OnSelectAllBtnClicked));
			this.mCancelAllButton.onClick.AddListener(new UnityAction(this.OnCancelAllBtnClicked));
			this.mDeleteButton.onClick.AddListener(new UnityAction(this.OnDeleteBtnClicked));
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000BCEA7 File Offset: 0x000BB2A7
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000BCEB4 File Offset: 0x000BB2B4
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index >= DataSourceMgr.Get.TotalItemCount)
			{
				return null;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem3 component = loopListViewItem.GetComponent<ListItem3>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000BCF22 File Offset: 0x000BB322
		private void OnSelectAllBtnClicked()
		{
			DataSourceMgr.Get.CheckAllItem();
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000BCF39 File Offset: 0x000BB339
		private void OnCancelAllBtnClicked()
		{
			DataSourceMgr.Get.UnCheckAllItem();
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000BCF50 File Offset: 0x000BB350
		private void OnDeleteBtnClicked()
		{
			if (!DataSourceMgr.Get.DeleteAllCheckedItem())
			{
				return;
			}
			this.mLoopListView.SetListItemCount(DataSourceMgr.Get.TotalItemCount, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x040021FF RID: 8703
		public LoopListView2 mLoopListView;

		// Token: 0x04002200 RID: 8704
		public Button mSelectAllButton;

		// Token: 0x04002201 RID: 8705
		public Button mCancelAllButton;

		// Token: 0x04002202 RID: 8706
		public Button mDeleteButton;

		// Token: 0x04002203 RID: 8707
		public Button mBackButton;
	}
}
