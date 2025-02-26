using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005E1 RID: 1505
	public class ResponsiveGridViewDemoScript2 : MonoBehaviour
	{
		// Token: 0x060022C9 RID: 8905 RVA: 0x000BFD4C File Offset: 0x000BE14C
		private void Start()
		{
			this.mLoopListView.InitListView(this.GetMaxRowCount() + 2, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mDragChangSizeScript.mOnDragEndAction = new Action(this.OnViewPortSizeChanged);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.OnViewPortSizeChanged();
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000BFE3B File Offset: 0x000BE23B
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000BFE48 File Offset: 0x000BE248
		private void UpdateItemPrefab()
		{
			ItemPrefabConfData itemPrefabConfData = this.mLoopListView.GetItemPrefabConfData("ItemPrefab2");
			GameObject mItemPrefab = itemPrefabConfData.mItemPrefab;
			RectTransform component = mItemPrefab.GetComponent<RectTransform>();
			ListItem6 component2 = mItemPrefab.GetComponent<ListItem6>();
			float viewPortWidth = this.mLoopListView.ViewPortWidth;
			int count = component2.mItemList.Count;
			GameObject gameObject = component2.mItemList[0].gameObject;
			RectTransform component3 = gameObject.GetComponent<RectTransform>();
			float width = component3.rect.width;
			int num = Mathf.FloorToInt(viewPortWidth / width);
			if (num == 0)
			{
				num = 1;
			}
			this.mItemCountPerRow = num;
			float num2 = (viewPortWidth - width * (float)num) / (float)(num + 1);
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, viewPortWidth);
			if (num > count)
			{
				int num3 = num - count;
				for (int i = 0; i < num3; i++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, component);
					RectTransform component4 = gameObject2.GetComponent<RectTransform>();
					component4.localScale = Vector3.one;
					component4.anchoredPosition3D = Vector3.zero;
					component4.rotation = Quaternion.identity;
					ListItem5 component5 = gameObject2.GetComponent<ListItem5>();
					component2.mItemList.Add(component5);
				}
			}
			else if (num < count)
			{
				int num4 = count - num;
				for (int j = 0; j < num4; j++)
				{
					ListItem5 listItem = component2.mItemList[component2.mItemList.Count - 1];
					component2.mItemList.RemoveAt(component2.mItemList.Count - 1);
					UnityEngine.Object.DestroyImmediate(listItem.gameObject);
				}
			}
			float num5 = num2;
			for (int k = 0; k < component2.mItemList.Count; k++)
			{
				GameObject gameObject3 = component2.mItemList[k].gameObject;
				gameObject3.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(num5, 0f, 0f);
				num5 = num5 + width + num2;
			}
			this.mLoopListView.OnItemPrefabChanged("ItemPrefab2");
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000C0065 File Offset: 0x000BE465
		private void OnViewPortSizeChanged()
		{
			this.UpdateItemPrefab();
			this.mLoopListView.SetListItemCount(this.GetMaxRowCount() + 2, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000C0090 File Offset: 0x000BE490
		private int GetMaxRowCount()
		{
			int num = DataSourceMgr.Get.TotalItemCount / this.mItemCountPerRow;
			if (DataSourceMgr.Get.TotalItemCount % this.mItemCountPerRow > 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000C00CC File Offset: 0x000BE4CC
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int row)
		{
			if (row < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (row == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				this.UpdateLoadingTip1(loopListViewItem);
				return loopListViewItem;
			}
			if (row == this.GetMaxRowCount() + 1)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab1");
				this.UpdateLoadingTip2(loopListViewItem);
				return loopListViewItem;
			}
			int num = row - 1;
			loopListViewItem = listView.NewListViewItem("ItemPrefab2");
			ListItem6 component = loopListViewItem.GetComponent<ListItem6>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < this.mItemCountPerRow; i++)
			{
				int num2 = num * this.mItemCountPerRow + i;
				if (num2 >= DataSourceMgr.Get.TotalItemCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num2);
					if (itemDataByIndex != null)
					{
						component.mItemList[i].gameObject.SetActive(true);
						component.mItemList[i].SetItemData(itemDataByIndex, num2);
					}
					else
					{
						component.mItemList[i].gameObject.SetActive(false);
					}
				}
			}
			return loopListViewItem;
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000C0200 File Offset: 0x000BE600
		private void UpdateLoadingTip1(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.mLoopListView.ViewPortWidth);
			ListItem17 component = item.GetComponent<ListItem17>();
			if (component == null)
			{
				return;
			}
			if (this.mLoadingTipStatus1 == LoadingTipStatus.None)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitContinureDrag)
			{
				component.mRoot1.SetActive(true);
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Refresh";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitLoad)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.Loaded)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(false);
				component.mText.text = "Refreshed Success";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000C03DC File Offset: 0x000BE7DC
		private void OnDraging()
		{
			this.OnDraging1();
			this.OnDraging2();
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000C03EA File Offset: 0x000BE7EA
		private void OnEndDrag()
		{
			this.OnEndDrag1();
			this.OnEndDrag2();
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000C03F8 File Offset: 0x000BE7F8
		private void OnDraging1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 != LoadingTipStatus.None && this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease && this.mLoadingTipStatus1 != LoadingTipStatus.WaitContinureDrag)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			ScrollRect scrollRect = this.mLoopListView.ScrollRect;
			Vector3 anchoredPosition3D = scrollRect.content.anchoredPosition3D;
			if (anchoredPosition3D.y >= 0f)
			{
				if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitContinureDrag)
				{
					this.mLoadingTipStatus1 = LoadingTipStatus.None;
					this.UpdateLoadingTip1(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
				}
			}
			else if (anchoredPosition3D.y < 0f && anchoredPosition3D.y > -this.mLoadingTipItemHeight1)
			{
				if (this.mLoadingTipStatus1 == LoadingTipStatus.None || this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
				{
					this.mLoadingTipStatus1 = LoadingTipStatus.WaitContinureDrag;
					this.UpdateLoadingTip1(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
				}
			}
			else if (anchoredPosition3D.y <= -this.mLoadingTipItemHeight1 && this.mLoadingTipStatus1 == LoadingTipStatus.WaitContinureDrag)
			{
				this.mLoadingTipStatus1 = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mLoadingTipItemHeight1, 0f);
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000C0578 File Offset: 0x000BE978
		private void OnEndDrag1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
			{
				this.mLoadingTipStatus1 = LoadingTipStatus.WaitLoad;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				DataSourceMgr.Get.RequestRefreshDataList(new Action(this.OnDataSourceRefreshFinished));
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000C05F4 File Offset: 0x000BE9F4
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

		// Token: 0x060022D5 RID: 8917 RVA: 0x000C065C File Offset: 0x000BEA5C
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

		// Token: 0x060022D6 RID: 8918 RVA: 0x000C0700 File Offset: 0x000BEB00
		private void UpdateLoadingTip2(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.mLoopListView.ViewPortWidth);
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

		// Token: 0x060022D7 RID: 8919 RVA: 0x000C081C File Offset: 0x000BEC1C
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(this.GetMaxRowCount() + 1);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex2 = this.mLoopListView.GetShownItemByItemIndex(this.GetMaxRowCount());
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

		// Token: 0x060022D8 RID: 8920 RVA: 0x000C08FC File Offset: 0x000BECFC
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(this.GetMaxRowCount() + 1);
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

		// Token: 0x060022D9 RID: 8921 RVA: 0x000C099C File Offset: 0x000BED9C
		private void OnDataSourceLoadMoreFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus2 = LoadingTipStatus.None;
				this.mLoopListView.SetListItemCount(this.GetMaxRowCount() + 2, false);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000C09F0 File Offset: 0x000BEDF0
		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			if (num < 0)
			{
				num = 0;
			}
			num++;
			int num2 = num / this.mItemCountPerRow;
			if (num % this.mItemCountPerRow > 0)
			{
				num2++;
			}
			if (num2 > 0)
			{
				num2--;
			}
			num2++;
			this.mLoopListView.MovePanelToItemIndex(num2, 0f);
		}

		// Token: 0x04002261 RID: 8801
		public LoopListView2 mLoopListView;

		// Token: 0x04002262 RID: 8802
		private LoadingTipStatus mLoadingTipStatus1;

		// Token: 0x04002263 RID: 8803
		private LoadingTipStatus mLoadingTipStatus2;

		// Token: 0x04002264 RID: 8804
		private float mDataLoadedTipShowLeftTime;

		// Token: 0x04002265 RID: 8805
		private float mLoadingTipItemHeight1 = 100f;

		// Token: 0x04002266 RID: 8806
		private float mLoadingTipItemHeight2 = 100f;

		// Token: 0x04002267 RID: 8807
		private int mLoadMoreCount = 20;

		// Token: 0x04002268 RID: 8808
		private Button mScrollToButton;

		// Token: 0x04002269 RID: 8809
		private InputField mScrollToInput;

		// Token: 0x0400226A RID: 8810
		private Button mBackButton;

		// Token: 0x0400226B RID: 8811
		private int mItemCountPerRow = 3;

		// Token: 0x0400226C RID: 8812
		public DragChangSizeScript mDragChangSizeScript;
	}
}
