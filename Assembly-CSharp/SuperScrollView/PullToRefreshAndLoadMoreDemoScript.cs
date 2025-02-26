using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005DD RID: 1501
	public class PullToRefreshAndLoadMoreDemoScript : MonoBehaviour
	{
		// Token: 0x060022A2 RID: 8866 RVA: 0x000BE708 File Offset: 0x000BCB08
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 2, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000BE7DE File Offset: 0x000BCBDE
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000BE7EC File Offset: 0x000BCBEC
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (index == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				this.UpdateLoadingTip1(loopListViewItem);
				return loopListViewItem;
			}
			if (index == DataSourceMgr.Get.TotalItemCount + 1)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab1");
				this.UpdateLoadingTip2(loopListViewItem);
				return loopListViewItem;
			}
			int num = index - 1;
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
			if (itemDataByIndex == null)
			{
				return null;
			}
			loopListViewItem = listView.NewListViewItem("ItemPrefab2");
			ListItem2 component = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			if (index == DataSourceMgr.Get.TotalItemCount)
			{
				loopListViewItem.Padding = 0f;
			}
			component.SetItemData(itemDataByIndex, num);
			return loopListViewItem;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000BE8B0 File Offset: 0x000BCCB0
		private void UpdateLoadingTip1(LoopListViewItem2 item)
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
			if (this.mLoadingTipStatus1 == LoadingTipStatus.None)
			{
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
			{
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Refresh";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitLoad)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.Loaded)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(false);
				component.mText.text = "Refreshed Success";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000BEA0B File Offset: 0x000BCE0B
		private void OnDraging()
		{
			this.OnDraging1();
			this.OnDraging2();
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000BEA19 File Offset: 0x000BCE19
		private void OnEndDrag()
		{
			this.OnEndDrag1();
			this.OnEndDrag2();
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000BEA28 File Offset: 0x000BCE28
		private void OnDraging1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 != LoadingTipStatus.None && this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			ScrollRect scrollRect = this.mLoopListView.ScrollRect;
			if (scrollRect.content.anchoredPosition3D.y < -this.mLoadingTipItemHeight1)
			{
				if (this.mLoadingTipStatus1 != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus1 = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mLoadingTipItemHeight1, 0f);
			}
			else
			{
				if (this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus1 = LoadingTipStatus.None;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000BEB1C File Offset: 0x000BCF1C
		private void OnEndDrag1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 != LoadingTipStatus.None && this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			this.mLoadingTipStatus1 = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip1(shownItemByItemIndex);
			DataSourceMgr.Get.RequestRefreshDataList(new Action(this.OnDataSourceRefreshFinished));
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000BEBB0 File Offset: 0x000BCFB0
		private void OnDataSourceRefreshFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus1 = LoadingTipStatus.Loaded;
				this.mDataLoadedTipShowLeftTime = 0.7f;
				LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
				if (shownItemByItemIndex == null)
				{
					return;
				}
				this.UpdateLoadingTip1(shownItemByItemIndex);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000BEC18 File Offset: 0x000BD018
		private void Update()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 == LoadingTipStatus.Loaded)
			{
				this.mDataLoadedTipShowLeftTime -= Time.deltaTime;
				if (this.mDataLoadedTipShowLeftTime <= 0f)
				{
					this.mLoadingTipStatus1 = LoadingTipStatus.None;
					LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
					if (shownItemByItemIndex == null)
					{
						return;
					}
					this.UpdateLoadingTip1(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, -this.mLoadingTipItemHeight1, 0f);
					this.mLoopListView.OnItemSizeChanged(0);
				}
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000BECBC File Offset: 0x000BD0BC
		private void UpdateLoadingTip2(LoopListViewItem2 item)
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
			if (this.mLoadingTipStatus2 == LoadingTipStatus.None)
			{
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitRelease)
			{
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Load More";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight2);
			}
			else if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitLoad)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight2);
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000BEDC0 File Offset: 0x000BD1C0
		private void OnDraging2()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 != LoadingTipStatus.None && this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount + 1);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex2 = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex2 == null)
			{
				return;
			}
			float y = this.mLoopListView.GetItemCornerPosInViewPort(shownItemByItemIndex2, ItemCornerEnum.LeftBottom).y;
			if (y + this.mLoopListView.ViewPortSize >= this.mLoadingTipItemHeight2)
			{
				if (this.mLoadingTipStatus2 != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus2 = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip2(shownItemByItemIndex);
			}
			else
			{
				if (this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus2 = LoadingTipStatus.None;
				this.UpdateLoadingTip2(shownItemByItemIndex);
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000BEEA8 File Offset: 0x000BD2A8
		private void OnEndDrag2()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 != LoadingTipStatus.None && this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount + 1);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			this.mLoadingTipStatus2 = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip2(shownItemByItemIndex);
			DataSourceMgr.Get.RequestLoadMoreDataList(this.mLoadMoreCount, new Action(this.OnDataSourceLoadMoreFinished));
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000BEF4C File Offset: 0x000BD34C
		private void OnDataSourceLoadMoreFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus2 = LoadingTipStatus.None;
				this.mLoopListView.SetListItemCount(DataSourceMgr.Get.TotalItemCount + 2, false);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000BEFA4 File Offset: 0x000BD3A4
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
			num++;
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		// Token: 0x04002237 RID: 8759
		public LoopListView2 mLoopListView;

		// Token: 0x04002238 RID: 8760
		private LoadingTipStatus mLoadingTipStatus1;

		// Token: 0x04002239 RID: 8761
		private LoadingTipStatus mLoadingTipStatus2;

		// Token: 0x0400223A RID: 8762
		private float mDataLoadedTipShowLeftTime;

		// Token: 0x0400223B RID: 8763
		private float mLoadingTipItemHeight1 = 100f;

		// Token: 0x0400223C RID: 8764
		private float mLoadingTipItemHeight2 = 100f;

		// Token: 0x0400223D RID: 8765
		private int mLoadMoreCount = 20;

		// Token: 0x0400223E RID: 8766
		private Button mScrollToButton;

		// Token: 0x0400223F RID: 8767
		private Button mAddItemButton;

		// Token: 0x04002240 RID: 8768
		private Button mSetCountButton;

		// Token: 0x04002241 RID: 8769
		private InputField mScrollToInput;

		// Token: 0x04002242 RID: 8770
		private InputField mAddItemInput;

		// Token: 0x04002243 RID: 8771
		private InputField mSetCountInput;

		// Token: 0x04002244 RID: 8772
		private Button mBackButton;
	}
}
