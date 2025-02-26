using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D3 RID: 1491
	public class ClickAndLoadMoreDemoScript : MonoBehaviour
	{
		// Token: 0x0600225B RID: 8795 RVA: 0x000BCAFC File Offset: 0x000BAEFC
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000BCBA4 File Offset: 0x000BAFA4
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000BCBB0 File Offset: 0x000BAFB0
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (index == DataSourceMgr.Get.TotalItemCount)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				if (!loopListViewItem.IsInitHandlerCalled)
				{
					loopListViewItem.IsInitHandlerCalled = true;
					ListItem11 component = loopListViewItem.GetComponent<ListItem11>();
					component.mRootButton.onClick.AddListener(new UnityAction(this.OnLoadMoreBtnClicked));
				}
				this.UpdateLoadingTip(loopListViewItem);
				return loopListViewItem;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem2 component2 = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component2.Init();
			}
			component2.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000BCC6C File Offset: 0x000BB06C
		private void UpdateLoadingTip(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			ListItem11 component = item.GetComponent<ListItem11>();
			if (component == null)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.None)
			{
				component.mText.text = "Click to Load More";
				component.mWaitingIcon.SetActive(false);
			}
			else if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000BCCF0 File Offset: 0x000BB0F0
		private void OnLoadMoreBtnClicked()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus != LoadingTipStatus.None)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoadingTipStatus = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip(shownItemByItemIndex);
			DataSourceMgr.Get.RequestLoadMoreDataList(this.mLoadMoreCount, new Action(this.OnDataSourceLoadMoreFinished));
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000BCD68 File Offset: 0x000BB168
		private void OnDataSourceLoadMoreFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus = LoadingTipStatus.None;
				this.mLoopListView.SetListItemCount(DataSourceMgr.Get.TotalItemCount + 1, false);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000BCDC0 File Offset: 0x000BB1C0
		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			if (num < 0)
			{
				return;
			}
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		// Token: 0x040021F9 RID: 8697
		public LoopListView2 mLoopListView;

		// Token: 0x040021FA RID: 8698
		private LoadingTipStatus mLoadingTipStatus;

		// Token: 0x040021FB RID: 8699
		private int mLoadMoreCount = 20;

		// Token: 0x040021FC RID: 8700
		private Button mScrollToButton;

		// Token: 0x040021FD RID: 8701
		private InputField mScrollToInput;

		// Token: 0x040021FE RID: 8702
		private Button mBackButton;
	}
}
