using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005CE RID: 1486
	public class GridViewDeleteItemDemoScript2 : MonoBehaviour
	{
		// Token: 0x06002239 RID: 8761 RVA: 0x000BBFA8 File Offset: 0x000BA3A8
		private void Start()
		{
			this.mLoopGridView.InitGridView(DataSourceMgr.Get.TotalItemCount, new Func<LoopGridView, int, int, int, LoopGridViewItem>(this.OnGetItemByRowColumn), null, null);
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mSelectAllButton.onClick.AddListener(new UnityAction(this.OnSelectAllBtnClicked));
			this.mCancelAllButton.onClick.AddListener(new UnityAction(this.OnCancelAllBtnClicked));
			this.mDeleteButton.onClick.AddListener(new UnityAction(this.OnDeleteBtnClicked));
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000BC048 File Offset: 0x000BA448
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000BC054 File Offset: 0x000BA454
		private LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int itemIndex, int row, int column)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(itemIndex);
			if (itemDataByIndex == null)
			{
				return null;
			}
			LoopGridViewItem loopGridViewItem = gridView.NewListViewItem("ItemPrefab0");
			ListItem19 component = loopGridViewItem.GetComponent<ListItem19>();
			if (!loopGridViewItem.IsInitHandlerCalled)
			{
				loopGridViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, itemIndex, row, column);
			return loopGridViewItem;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000BC0AC File Offset: 0x000BA4AC
		private void OnSelectAllBtnClicked()
		{
			DataSourceMgr.Get.CheckAllItem();
			this.mLoopGridView.RefreshAllShownItem();
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000BC0C3 File Offset: 0x000BA4C3
		private void OnCancelAllBtnClicked()
		{
			DataSourceMgr.Get.UnCheckAllItem();
			this.mLoopGridView.RefreshAllShownItem();
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000BC0DC File Offset: 0x000BA4DC
		private void OnDeleteBtnClicked()
		{
			if (!DataSourceMgr.Get.DeleteAllCheckedItem())
			{
				return;
			}
			this.mLoopGridView.SetListItemCount(DataSourceMgr.Get.TotalItemCount, false);
			this.mLoopGridView.RefreshAllShownItem();
		}

		// Token: 0x040021D7 RID: 8663
		public LoopGridView mLoopGridView;

		// Token: 0x040021D8 RID: 8664
		public Button mSelectAllButton;

		// Token: 0x040021D9 RID: 8665
		public Button mCancelAllButton;

		// Token: 0x040021DA RID: 8666
		public Button mDeleteButton;

		// Token: 0x040021DB RID: 8667
		public Button mBackButton;
	}
}
