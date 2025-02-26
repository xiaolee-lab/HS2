using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005DC RID: 1500
	public class PullToLoadMoreDemoScript : MonoBehaviour
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x000BE214 File Offset: 0x000BC614
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mLoopListView.mOnBeginDragAction = new Action(this.OnBeginDrag);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000BE301 File Offset: 0x000BC701
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000BE310 File Offset: 0x000BC710
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
				this.UpdateLoadingTip(loopListViewItem);
				return loopListViewItem;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem2 component = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			if (index == DataSourceMgr.Get.TotalItemCount - 1)
			{
				loopListViewItem.Padding = 0f;
			}
			component.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000BE3B4 File Offset: 0x000BC7B4
		private void UpdateLoadingTip(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			ListItem0 component = item.GetComponent<ListItem0>();
			if (component == null)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.None)
			{
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus == LoadingTipStatus.WaitRelease)
			{
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Load More";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight);
			}
			else if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight);
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000BE4B8 File Offset: 0x000BC8B8
		private void OnBeginDrag()
		{
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000BE4BC File Offset: 0x000BC8BC
		private void OnDraging()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus != LoadingTipStatus.None && this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex2 = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount - 1);
			if (shownItemByItemIndex2 == null)
			{
				return;
			}
			float y = this.mLoopListView.GetItemCornerPosInViewPort(shownItemByItemIndex2, ItemCornerEnum.LeftBottom).y;
			if (y + this.mLoopListView.ViewPortSize >= this.mLoadingTipItemHeight)
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip(shownItemByItemIndex);
			}
			else
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.None;
				this.UpdateLoadingTip(shownItemByItemIndex);
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000BE5A4 File Offset: 0x000BC9A4
		private void OnEndDrag()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus != LoadingTipStatus.None && this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			this.mLoadingTipStatus = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip(shownItemByItemIndex);
			DataSourceMgr.Get.RequestLoadMoreDataList(this.mLoadMoreCount, new Action(this.OnDataSourceLoadMoreFinished));
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000BE648 File Offset: 0x000BCA48
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

		// Token: 0x060022A0 RID: 8864 RVA: 0x000BE6A0 File Offset: 0x000BCAA0
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

		// Token: 0x04002230 RID: 8752
		public LoopListView2 mLoopListView;

		// Token: 0x04002231 RID: 8753
		private LoadingTipStatus mLoadingTipStatus;

		// Token: 0x04002232 RID: 8754
		private float mLoadingTipItemHeight = 100f;

		// Token: 0x04002233 RID: 8755
		private int mLoadMoreCount = 20;

		// Token: 0x04002234 RID: 8756
		private Button mScrollToButton;

		// Token: 0x04002235 RID: 8757
		private InputField mScrollToInput;

		// Token: 0x04002236 RID: 8758
		private Button mBackButton;
	}
}
