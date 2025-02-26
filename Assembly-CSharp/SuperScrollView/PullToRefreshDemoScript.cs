using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005DF RID: 1503
	public class PullToRefreshDemoScript : MonoBehaviour
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x000BEFFC File Offset: 0x000BD3FC
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mLoopListView.mOnBeginDragAction = new Action(this.OnBeginDrag);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.mSetCountButton = GameObject.Find("ButtonPanel/buttonGroup1/SetCountButton").GetComponent<Button>();
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mAddItemButton = GameObject.Find("ButtonPanel/buttonGroup3/AddItemButton").GetComponent<Button>();
			this.mSetCountInput = GameObject.Find("ButtonPanel/buttonGroup1/SetCountInputField").GetComponent<InputField>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mAddItemInput = GameObject.Find("ButtonPanel/buttonGroup3/AddItemInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mAddItemButton.onClick.AddListener(new UnityAction(this.OnAddItemBtnClicked));
			this.mSetCountButton.onClick.AddListener(new UnityAction(this.OnSetItemCountBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000BF175 File Offset: 0x000BD575
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000BF184 File Offset: 0x000BD584
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index > DataSourceMgr.Get.TotalItemCount)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (index == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				this.UpdateLoadingTip(loopListViewItem);
				return loopListViewItem;
			}
			int num = index - 1;
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
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
			component.SetItemData(itemDataByIndex, num);
			return loopListViewItem;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000BF214 File Offset: 0x000BD614
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
				component.mText.text = "Release to Refresh";
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
			else if (this.mLoadingTipStatus == LoadingTipStatus.Loaded)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(false);
				component.mText.text = "Refreshed Success";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight);
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000BF36F File Offset: 0x000BD76F
		private void OnBeginDrag()
		{
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000BF374 File Offset: 0x000BD774
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			ScrollRect scrollRect = this.mLoopListView.ScrollRect;
			if (scrollRect.content.anchoredPosition3D.y < -this.mLoadingTipItemHeight)
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mLoadingTipItemHeight, 0f);
			}
			else
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.None;
				this.UpdateLoadingTip(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000BF468 File Offset: 0x000BD868
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
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
			DataSourceMgr.Get.RequestRefreshDataList(new Action(this.OnDataSourceRefreshFinished));
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000BF4FC File Offset: 0x000BD8FC
		private void OnDataSourceRefreshFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus = LoadingTipStatus.Loaded;
				this.mDataLoadedTipShowLeftTime = 0.7f;
				LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
				if (shownItemByItemIndex == null)
				{
					return;
				}
				this.UpdateLoadingTip(shownItemByItemIndex);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000BF564 File Offset: 0x000BD964
		private void Update()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.Loaded)
			{
				this.mDataLoadedTipShowLeftTime -= Time.deltaTime;
				if (this.mDataLoadedTipShowLeftTime <= 0f)
				{
					this.mLoadingTipStatus = LoadingTipStatus.None;
					LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
					if (shownItemByItemIndex == null)
					{
						return;
					}
					this.UpdateLoadingTip(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, -this.mLoadingTipItemHeight, 0f);
					this.mLoopListView.OnItemSizeChanged(0);
				}
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000BF608 File Offset: 0x000BDA08
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

		// Token: 0x060022BC RID: 8892 RVA: 0x000BF64C File Offset: 0x000BDA4C
		private void OnAddItemBtnClicked()
		{
			if (this.mLoopListView.ItemTotalCount < 0)
			{
				return;
			}
			int num = 0;
			if (!int.TryParse(this.mAddItemInput.text, out num))
			{
				return;
			}
			num = this.mLoopListView.ItemTotalCount + num;
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount + 1)
			{
				return;
			}
			this.mLoopListView.SetListItemCount(num, false);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000BF6BC File Offset: 0x000BDABC
		private void OnSetItemCountBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mSetCountInput.text, out num))
			{
				return;
			}
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount)
			{
				return;
			}
			num++;
			this.mLoopListView.SetListItemCount(num, false);
		}

		// Token: 0x0400224B RID: 8779
		public LoopListView2 mLoopListView;

		// Token: 0x0400224C RID: 8780
		private LoadingTipStatus mLoadingTipStatus;

		// Token: 0x0400224D RID: 8781
		private float mDataLoadedTipShowLeftTime;

		// Token: 0x0400224E RID: 8782
		private float mLoadingTipItemHeight = 100f;

		// Token: 0x0400224F RID: 8783
		private Button mScrollToButton;

		// Token: 0x04002250 RID: 8784
		private Button mAddItemButton;

		// Token: 0x04002251 RID: 8785
		private Button mSetCountButton;

		// Token: 0x04002252 RID: 8786
		private InputField mScrollToInput;

		// Token: 0x04002253 RID: 8787
		private InputField mAddItemInput;

		// Token: 0x04002254 RID: 8788
		private InputField mSetCountInput;

		// Token: 0x04002255 RID: 8789
		private Button mBackButton;
	}
}
