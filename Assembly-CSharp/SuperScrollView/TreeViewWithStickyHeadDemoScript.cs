using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005E8 RID: 1512
	public class TreeViewWithStickyHeadDemoScript : MonoBehaviour
	{
		// Token: 0x0600230B RID: 8971 RVA: 0x000C1B68 File Offset: 0x000BFF68
		private void Start()
		{
			int treeViewItemCount = TreeViewDataSourceMgr.Get.TreeViewItemCount;
			for (int i = 0; i < treeViewItemCount; i++)
			{
				int childCount = TreeViewDataSourceMgr.Get.GetItemDataByIndex(i).ChildCount;
				this.mTreeItemCountMgr.AddTreeItem(childCount, true);
			}
			this.mLoopListView.InitListView(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mExpandAllButton = GameObject.Find("ButtonPanel/buttonGroup1/ExpandAllButton").GetComponent<Button>();
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mCollapseAllButton = GameObject.Find("ButtonPanel/buttonGroup3/CollapseAllButton").GetComponent<Button>();
			this.mScrollToInputItem = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputFieldItem").GetComponent<InputField>();
			this.mScrollToInputChild = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputFieldChild").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mExpandAllButton.onClick.AddListener(new UnityAction(this.OnExpandAllBtnClicked));
			this.mCollapseAllButton.onClick.AddListener(new UnityAction(this.OnCollapseAllBtnClicked));
			this.mStickeyHeadItemHeight = this.mStickeyHeadItem.GetComponent<RectTransform>().rect.height;
			this.mStickeyHeadItem.Init();
			this.mStickeyHeadItem.SetClickCallBack(new Action<int>(this.OnExpandClicked));
			this.mStickeyHeadItemRf = this.mStickeyHeadItem.gameObject.GetComponent<RectTransform>();
			this.mLoopListView.ScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollContentPosChanged));
			this.UpdateStickeyHeadPos();
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000C1D3E File Offset: 0x000C013E
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000C1D4C File Offset: 0x000C014C
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemCountMgr.QueryTreeItemByTotalIndex(index);
			if (treeViewItemCountData == null)
			{
				return null;
			}
			int mTreeItemIndex = treeViewItemCountData.mTreeItemIndex;
			TreeViewItemData itemDataByIndex = TreeViewDataSourceMgr.Get.GetItemDataByIndex(mTreeItemIndex);
			if (!treeViewItemCountData.IsChild(index))
			{
				LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
				ListItem12 component = loopListViewItem.GetComponent<ListItem12>();
				if (!loopListViewItem.IsInitHandlerCalled)
				{
					loopListViewItem.IsInitHandlerCalled = true;
					component.Init();
					component.SetClickCallBack(new Action<int>(this.OnExpandClicked));
				}
				loopListViewItem.UserIntData1 = mTreeItemIndex;
				loopListViewItem.UserIntData2 = 0;
				component.mText.text = itemDataByIndex.mName;
				component.SetItemData(mTreeItemIndex, treeViewItemCountData.mIsExpand);
				return loopListViewItem;
			}
			int childIndex = treeViewItemCountData.GetChildIndex(index);
			ItemData child = itemDataByIndex.GetChild(childIndex);
			if (child == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem2 = listView.NewListViewItem("ItemPrefab2");
			ListItem13 component2 = loopListViewItem2.GetComponent<ListItem13>();
			if (!loopListViewItem2.IsInitHandlerCalled)
			{
				loopListViewItem2.IsInitHandlerCalled = true;
				component2.Init();
			}
			loopListViewItem2.UserIntData1 = mTreeItemIndex;
			loopListViewItem2.UserIntData2 = childIndex;
			component2.SetItemData(child, mTreeItemIndex, childIndex);
			return loopListViewItem2;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000C1E72 File Offset: 0x000C0272
		public void OnExpandClicked(int index)
		{
			this.mTreeItemCountMgr.ToggleItemExpand(index);
			this.mLoopListView.SetListItemCount(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000C1EA4 File Offset: 0x000C02A4
		private void OnJumpBtnClicked()
		{
			int treeIndex = 0;
			int num = 0;
			if (!int.TryParse(this.mScrollToInputItem.text, out treeIndex))
			{
				return;
			}
			if (!int.TryParse(this.mScrollToInputChild.text, out num))
			{
				num = 0;
			}
			if (num < 0)
			{
				num = 0;
			}
			TreeViewItemCountData treeItem = this.mTreeItemCountMgr.GetTreeItem(treeIndex);
			if (treeItem == null)
			{
				return;
			}
			int mChildCount = treeItem.mChildCount;
			int itemIndex;
			if (!treeItem.mIsExpand || mChildCount == 0 || num == 0)
			{
				itemIndex = treeItem.mBeginIndex;
			}
			else
			{
				if (num > mChildCount)
				{
					num = mChildCount;
				}
				if (num < 1)
				{
					num = 1;
				}
				itemIndex = treeItem.mBeginIndex + num;
			}
			this.mLoopListView.MovePanelToItemIndex(itemIndex, this.mStickeyHeadItemHeight);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000C1F60 File Offset: 0x000C0360
		private void OnExpandAllBtnClicked()
		{
			int treeViewItemCount = this.mTreeItemCountMgr.TreeViewItemCount;
			for (int i = 0; i < treeViewItemCount; i++)
			{
				this.mTreeItemCountMgr.SetItemExpand(i, true);
			}
			this.mLoopListView.SetListItemCount(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000C1FBC File Offset: 0x000C03BC
		private void OnCollapseAllBtnClicked()
		{
			int treeViewItemCount = this.mTreeItemCountMgr.TreeViewItemCount;
			for (int i = 0; i < treeViewItemCount; i++)
			{
				this.mTreeItemCountMgr.SetItemExpand(i, false);
			}
			this.mLoopListView.SetListItemCount(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000C2018 File Offset: 0x000C0418
		private void UpdateStickeyHeadPos()
		{
			bool activeSelf = this.mStickeyHeadItem.gameObject.activeSelf;
			int shownItemCount = this.mLoopListView.ShownItemCount;
			if (shownItemCount == 0)
			{
				if (activeSelf)
				{
					this.mStickeyHeadItem.gameObject.SetActive(false);
				}
				return;
			}
			LoopListViewItem2 shownItemByIndex = this.mLoopListView.GetShownItemByIndex(0);
			Vector3 itemCornerPosInViewPort = this.mLoopListView.GetItemCornerPosInViewPort(shownItemByIndex, ItemCornerEnum.LeftTop);
			LoopListViewItem2 loopListViewItem = null;
			float num = itemCornerPosInViewPort.y;
			float num2 = num - shownItemByIndex.ItemSizeWithPadding;
			int num3 = -1;
			if (num <= 0f)
			{
				if (activeSelf)
				{
					this.mStickeyHeadItem.gameObject.SetActive(false);
				}
				return;
			}
			if (num2 < 0f)
			{
				loopListViewItem = shownItemByIndex;
				num3 = 0;
			}
			else
			{
				for (int i = 1; i < shownItemCount; i++)
				{
					LoopListViewItem2 shownItemByIndexWithoutCheck = this.mLoopListView.GetShownItemByIndexWithoutCheck(i);
					num = num2;
					num2 = num - shownItemByIndexWithoutCheck.ItemSizeWithPadding;
					if (num >= 0f && num2 <= 0f)
					{
						loopListViewItem = shownItemByIndexWithoutCheck;
						num3 = i;
						break;
					}
				}
			}
			if (loopListViewItem == null)
			{
				if (activeSelf)
				{
					this.mStickeyHeadItem.gameObject.SetActive(false);
				}
				return;
			}
			int userIntData = loopListViewItem.UserIntData1;
			int userIntData2 = loopListViewItem.UserIntData2;
			TreeViewItemCountData treeItem = this.mTreeItemCountMgr.GetTreeItem(userIntData);
			if (treeItem == null)
			{
				if (activeSelf)
				{
					this.mStickeyHeadItem.gameObject.SetActive(false);
				}
				return;
			}
			if (!treeItem.mIsExpand || treeItem.mChildCount == 0)
			{
				if (activeSelf)
				{
					this.mStickeyHeadItem.gameObject.SetActive(false);
				}
				return;
			}
			if (!activeSelf)
			{
				this.mStickeyHeadItem.gameObject.SetActive(true);
			}
			if (this.mStickeyHeadItem.TreeItemIndex != userIntData)
			{
				TreeViewItemData itemDataByIndex = TreeViewDataSourceMgr.Get.GetItemDataByIndex(userIntData);
				this.mStickeyHeadItem.mText.text = itemDataByIndex.mName;
				this.mStickeyHeadItem.SetItemData(userIntData, treeItem.mIsExpand);
			}
			this.mStickeyHeadItem.gameObject.transform.localPosition = Vector3.zero;
			float num4 = -num2;
			float padding = loopListViewItem.Padding;
			if (num4 - padding >= this.mStickeyHeadItemHeight)
			{
				return;
			}
			for (int j = num3 + 1; j < shownItemCount; j++)
			{
				LoopListViewItem2 shownItemByIndexWithoutCheck2 = this.mLoopListView.GetShownItemByIndexWithoutCheck(j);
				if (shownItemByIndexWithoutCheck2.UserIntData1 != userIntData)
				{
					break;
				}
				num4 += shownItemByIndexWithoutCheck2.ItemSizeWithPadding;
				padding = shownItemByIndexWithoutCheck2.Padding;
				if (num4 - padding >= this.mStickeyHeadItemHeight)
				{
					return;
				}
			}
			float y = this.mStickeyHeadItemHeight - (num4 - padding);
			this.mStickeyHeadItemRf.anchoredPosition3D = new Vector3(0f, y, 0f);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000C22E5 File Offset: 0x000C06E5
		private void OnScrollContentPosChanged(Vector2 pos)
		{
			this.UpdateStickeyHeadPos();
		}

		// Token: 0x04002296 RID: 8854
		public LoopListView2 mLoopListView;

		// Token: 0x04002297 RID: 8855
		private Button mScrollToButton;

		// Token: 0x04002298 RID: 8856
		private Button mExpandAllButton;

		// Token: 0x04002299 RID: 8857
		private Button mCollapseAllButton;

		// Token: 0x0400229A RID: 8858
		private InputField mScrollToInputItem;

		// Token: 0x0400229B RID: 8859
		private InputField mScrollToInputChild;

		// Token: 0x0400229C RID: 8860
		private Button mBackButton;

		// Token: 0x0400229D RID: 8861
		private TreeViewItemCountMgr mTreeItemCountMgr = new TreeViewItemCountMgr();

		// Token: 0x0400229E RID: 8862
		public ListItem12 mStickeyHeadItem;

		// Token: 0x0400229F RID: 8863
		private RectTransform mStickeyHeadItemRf;

		// Token: 0x040022A0 RID: 8864
		private float mStickeyHeadItemHeight = -1f;
	}
}
