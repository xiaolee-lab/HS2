using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x02000603 RID: 1539
	public class LoopListView2 : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000C73D1 File Offset: 0x000C57D1
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000C73D9 File Offset: 0x000C57D9
		public ListItemArrangeType ArrangeType
		{
			get
			{
				return this.mArrangeType;
			}
			set
			{
				this.mArrangeType = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000C73E2 File Offset: 0x000C57E2
		public bool IsInit
		{
			get
			{
				return this.mListViewInited;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000C73EA File Offset: 0x000C57EA
		public List<ItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000C73F2 File Offset: 0x000C57F2
		public List<LoopListViewItem2> ItemList
		{
			get
			{
				return this.mItemList;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000C73FA File Offset: 0x000C57FA
		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000C7402 File Offset: 0x000C5802
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000C740A File Offset: 0x000C580A
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000C7412 File Offset: 0x000C5812
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000C741A File Offset: 0x000C581A
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x000C7422 File Offset: 0x000C5822
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x000C742A File Offset: 0x000C582A
		public bool ItemSnapEnable
		{
			get
			{
				return this.mItemSnapEnable;
			}
			set
			{
				this.mItemSnapEnable = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x000C7433 File Offset: 0x000C5833
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x000C743B File Offset: 0x000C583B
		public bool SupportScrollBar
		{
			get
			{
				return this.mSupportScrollBar;
			}
			set
			{
				this.mSupportScrollBar = value;
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000C7444 File Offset: 0x000C5844
		public ItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (ItemPrefabConfData itemPrefabConfData in this.mItemPrefabDataList)
			{
				if (!(itemPrefabConfData.mItemPrefab == null))
				{
					if (prefabName == itemPrefabConfData.mItemPrefab.name)
					{
						return itemPrefabConfData;
					}
				}
			}
			return null;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000C74D0 File Offset: 0x000C58D0
		public void OnItemPrefabChanged(string prefabName)
		{
			ItemPrefabConfData itemPrefabConfData = this.GetItemPrefabConfData(prefabName);
			if (itemPrefabConfData == null)
			{
				return;
			}
			ItemPool itemPool = null;
			if (!this.mItemPoolDict.TryGetValue(prefabName, out itemPool))
			{
				return;
			}
			int num = -1;
			Vector3 pos = Vector3.zero;
			if (this.mItemList.Count > 0)
			{
				num = this.mItemList[0].ItemIndex;
				pos = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
			}
			this.RecycleAllItem();
			this.ClearAllTmpRecycledItem();
			itemPool.DestroyAllItem();
			itemPool.Init(itemPrefabConfData.mItemPrefab, itemPrefabConfData.mPadding, itemPrefabConfData.mStartPosOffset, itemPrefabConfData.mInitCreateCount, this.mContainerTrans);
			if (num >= 0)
			{
				this.RefreshAllShownItemWithFirstIndexAndPos(num, pos);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000C758C File Offset: 0x000C598C
		public void InitListView(int itemTotalCount, Func<LoopListView2, int, LoopListViewItem2> onGetItemByIndex, LoopListViewInitParam initParam = null)
		{
			if (initParam != null)
			{
				this.mDistanceForRecycle0 = initParam.mDistanceForRecycle0;
				this.mDistanceForNew0 = initParam.mDistanceForNew0;
				this.mDistanceForRecycle1 = initParam.mDistanceForRecycle1;
				this.mDistanceForNew1 = initParam.mDistanceForNew1;
				this.mSmoothDumpRate = initParam.mSmoothDumpRate;
				this.mSnapFinishThreshold = initParam.mSnapFinishThreshold;
				this.mSnapVecThreshold = initParam.mSnapVecThreshold;
				this.mItemDefaultWithPaddingSize = initParam.mItemDefaultWithPaddingSize;
			}
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				return;
			}
			if (this.mDistanceForRecycle0 <= this.mDistanceForNew0)
			{
			}
			if (this.mDistanceForRecycle1 <= this.mDistanceForNew1)
			{
			}
			this.mCurSnapData.Clear();
			this.mItemPosMgr = new ItemPosMgr(this.mItemDefaultWithPaddingSize);
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			if (this.mScrollRect.horizontalScrollbarVisibility != ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport || this.mScrollRect.horizontalScrollbar != null)
			{
			}
			if (this.mScrollRect.verticalScrollbarVisibility != ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport || this.mScrollRect.verticalScrollbar != null)
			{
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mScrollRect.horizontal = !this.mIsVertList;
			this.mScrollRect.vertical = this.mIsVertList;
			this.SetScrollbarListener();
			this.AdjustPivot(this.mViewPortRectTransform);
			this.AdjustAnchor(this.mContainerTrans);
			this.AdjustContainerPivot(this.mContainerTrans);
			this.InitItemPool();
			this.mOnGetItemByIndex = onGetItemByIndex;
			if (this.mListViewInited)
			{
			}
			this.mListViewInited = true;
			this.ResetListView(true);
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemTotalCount;
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			this.mCurReadyMaxItemIndex = 0;
			this.mCurReadyMinItemIndex = 0;
			this.mLeftSnapUpdateExtraCount = 1;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
			this.UpdateContentSize();
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000C780C File Offset: 0x000C5C0C
		public void InitListViewAndSize(int itemTotalCount, Func<LoopListView2, int, LoopListViewItem2> onGetItemByIndex)
		{
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				return;
			}
			if (this.mDistanceForRecycle0 <= this.mDistanceForNew0)
			{
			}
			if (this.mDistanceForRecycle1 <= this.mDistanceForNew1)
			{
			}
			this.mCurSnapData.Clear();
			this.mItemPosMgr = new ItemPosMgr(this.mItemDefaultWithPaddingSize);
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			if (this.mScrollRect.horizontalScrollbarVisibility != ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport || this.mScrollRect.horizontalScrollbar != null)
			{
			}
			if (this.mScrollRect.verticalScrollbarVisibility != ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport || this.mScrollRect.verticalScrollbar != null)
			{
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mScrollRect.horizontal = !this.mIsVertList;
			this.mScrollRect.vertical = this.mIsVertList;
			this.SetScrollbarListener();
			this.AdjustPivot(this.mViewPortRectTransform);
			this.AdjustAnchor(this.mContainerTrans);
			this.AdjustContainerPivot(this.mContainerTrans);
			this.InitItemPool();
			this.mOnGetItemByIndex = onGetItemByIndex;
			if (this.mListViewInited)
			{
			}
			this.mListViewInited = true;
			this.ResetListView(true);
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemTotalCount;
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			this.mCurReadyMaxItemIndex = 0;
			this.mCurReadyMinItemIndex = 0;
			this.mLeftSnapUpdateExtraCount = 1;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
			this.MovePanelToItemIndex(0, 0f);
			this.SetSize(this.mItemTotalCount);
			this.UpdateContentSize();
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000C7A40 File Offset: 0x000C5E40
		private void SetScrollbarListener()
		{
			this.mScrollBarClickEventListener = null;
			Scrollbar scrollbar = null;
			if (this.mIsVertList && this.mScrollRect.verticalScrollbar != null)
			{
				scrollbar = this.mScrollRect.verticalScrollbar;
			}
			if (!this.mIsVertList && this.mScrollRect.horizontalScrollbar != null)
			{
				scrollbar = this.mScrollRect.horizontalScrollbar;
			}
			if (scrollbar == null)
			{
				return;
			}
			ClickEventListener clickEventListener = ClickEventListener.Get(scrollbar.gameObject);
			this.mScrollBarClickEventListener = clickEventListener;
			clickEventListener.SetPointerUpHandler(new Action<GameObject>(this.OnPointerUpInScrollBar));
			clickEventListener.SetPointerDownHandler(new Action<GameObject>(this.OnPointerDownInScrollBar));
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000C7AF4 File Offset: 0x000C5EF4
		private void OnPointerDownInScrollBar(GameObject obj)
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000C7B01 File Offset: 0x000C5F01
		private void OnPointerUpInScrollBar(GameObject obj)
		{
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000C7B09 File Offset: 0x000C5F09
		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000C7B38 File Offset: 0x000C5F38
		public bool SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return false;
			}
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemCount;
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			if (this.mItemTotalCount == 0)
			{
				this.mCurReadyMaxItemIndex = 0;
				this.mCurReadyMinItemIndex = 0;
				this.mNeedCheckNextMaxItem = false;
				this.mNeedCheckNextMinItem = false;
				this.RecycleAllItem();
				this.ClearAllTmpRecycledItem();
				this.UpdateContentSize();
				return false;
			}
			if (this.mCurReadyMaxItemIndex >= this.mItemTotalCount)
			{
				this.mCurReadyMaxItemIndex = this.mItemTotalCount - 1;
			}
			this.mLeftSnapUpdateExtraCount = 1;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
			if (resetPos)
			{
				this.MovePanelToItemIndex(0, 0f);
				return false;
			}
			if (this.mItemList.Count == 0)
			{
				this.MovePanelToItemIndex(0, 0f);
				return false;
			}
			int num = this.mItemTotalCount - 1;
			int itemIndex = this.mItemList[this.mItemList.Count - 1].ItemIndex;
			if (itemIndex <= num)
			{
				this.UpdateContentSize();
				this.UpdateAllShownItemsPos();
				return false;
			}
			this.MovePanelToItemIndex(num, 0f);
			return true;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000C7C90 File Offset: 0x000C6090
		public void ReSetListItemCount(int itemCount)
		{
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemCount;
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			if (this.mItemTotalCount == 0)
			{
				this.mCurReadyMaxItemIndex = 0;
				this.mCurReadyMinItemIndex = 0;
				this.mNeedCheckNextMaxItem = false;
				this.mNeedCheckNextMinItem = false;
				this.RecycleAllItem();
				this.ClearAllTmpRecycledItem();
				this.UpdateContentSize();
				return;
			}
			if (this.mCurReadyMaxItemIndex >= this.mItemTotalCount)
			{
				this.mCurReadyMaxItemIndex = this.mItemTotalCount - 1;
			}
			this.mLeftSnapUpdateExtraCount = 1;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
			this.MovePanelToItemIndex(0, 0f);
			this.SetSize(this.mItemTotalCount);
			this.UpdateContentSize();
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000C7D7C File Offset: 0x000C617C
		private void SetSize(int loopCount)
		{
			LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			if (this.mSupportScrollBar)
			{
				for (int i = 0; i < loopCount; i++)
				{
					if (this.mIsVertList)
					{
						this.SetItemSize(i, shownItemByItemIndex.CachedRectTransform.rect.height, shownItemByItemIndex.Padding);
					}
					else
					{
						this.SetItemSize(i, shownItemByItemIndex.CachedRectTransform.rect.width, shownItemByItemIndex.Padding);
					}
				}
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000C7E0C File Offset: 0x000C620C
		public LoopListViewItem2 GetShownItemByItemIndex(int itemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (itemIndex < this.mItemList[0].ItemIndex || itemIndex > this.mItemList[count - 1].ItemIndex)
			{
				return null;
			}
			int index = itemIndex - this.mItemList[0].ItemIndex;
			return this.mItemList[index];
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x000C7E7F File Offset: 0x000C627F
		public int ShownItemCount
		{
			get
			{
				return this.mItemList.Count;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000C7E8C File Offset: 0x000C628C
		public float ViewPortSize
		{
			get
			{
				if (this.mIsVertList)
				{
					return this.mViewPortRectTransform.rect.height;
				}
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x000C7ECC File Offset: 0x000C62CC
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000C7EEC File Offset: 0x000C62EC
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000C7F0C File Offset: 0x000C630C
		public LoopListViewItem2 GetShownItemByIndex(int index)
		{
			int count = this.mItemList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemList[index];
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000C7F41 File Offset: 0x000C6341
		public LoopListViewItem2 GetShownItemByIndexWithoutCheck(int index)
		{
			return this.mItemList[index];
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000C7F50 File Offset: 0x000C6350
		public int GetIndexInShownItemList(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return -1;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return -1;
			}
			for (int i = 0; i < count; i++)
			{
				if (this.mItemList[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000C7FAC File Offset: 0x000C63AC
		public void DoActionForEachShownItem(Action<LoopListViewItem2, object> action, object param)
		{
			if (action == null)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				action(this.mItemList[i], param);
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000C7FF8 File Offset: 0x000C63F8
		public LoopListViewItem2 NewListViewItem(string itemPrefabName)
		{
			ItemPool itemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out itemPool))
			{
				return null;
			}
			LoopListViewItem2 item = itemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentListView = this;
			return item;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000C8060 File Offset: 0x000C6460
		public void OnItemSizeChanged(int itemIndex)
		{
			LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(itemIndex);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(itemIndex, shownItemByItemIndex.CachedRectTransform.rect.height, shownItemByItemIndex.Padding);
				}
				else
				{
					this.SetItemSize(itemIndex, shownItemByItemIndex.CachedRectTransform.rect.width, shownItemByItemIndex.Padding);
				}
			}
			this.UpdateContentSize();
			this.UpdateAllShownItemsPos();
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000C80EC File Offset: 0x000C64EC
		public void RefreshItemByItemIndex(int itemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (itemIndex < this.mItemList[0].ItemIndex || itemIndex > this.mItemList[count - 1].ItemIndex)
			{
				return;
			}
			int itemIndex2 = this.mItemList[0].ItemIndex;
			int index = itemIndex - itemIndex2;
			LoopListViewItem2 loopListViewItem = this.mItemList[index];
			Vector3 anchoredPosition3D = loopListViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleItemTmp(loopListViewItem);
			LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(itemIndex);
			if (newItemByIndex == null)
			{
				this.RefreshAllShownItemWithFirstIndex(itemIndex2);
				return;
			}
			this.mItemList[index] = newItemByIndex;
			if (this.mIsVertList)
			{
				anchoredPosition3D.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				anchoredPosition3D.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
			this.OnItemSizeChanged(itemIndex);
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000C81E7 File Offset: 0x000C65E7
		public void FinishSnapImmediately()
		{
			this.UpdateSnapMove(true, false);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000C81F4 File Offset: 0x000C65F4
		public void MovePanelToItemIndex(int itemIndex, float offset)
		{
			this.mScrollRect.StopMovement();
			this.mCurSnapData.Clear();
			if (this.mItemTotalCount == 0)
			{
				return;
			}
			if (itemIndex < 0 && this.mItemTotalCount > 0)
			{
				return;
			}
			if (this.mItemTotalCount > 0 && itemIndex >= this.mItemTotalCount)
			{
				itemIndex = this.mItemTotalCount - 1;
			}
			if (offset < 0f)
			{
				offset = 0f;
			}
			Vector3 zero = Vector3.zero;
			float viewPortSize = this.ViewPortSize;
			if (offset > viewPortSize)
			{
				offset = viewPortSize;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = this.mContainerTrans.anchoredPosition3D.y;
				if (num < 0f)
				{
					num = 0f;
				}
				zero.y = -num - offset;
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num2 = this.mContainerTrans.anchoredPosition3D.y;
				if (num2 > 0f)
				{
					num2 = 0f;
				}
				zero.y = -num2 + offset;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num3 = this.mContainerTrans.anchoredPosition3D.x;
				if (num3 > 0f)
				{
					num3 = 0f;
				}
				zero.x = -num3 + offset;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num4 = this.mContainerTrans.anchoredPosition3D.x;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				zero.x = -num4 - offset;
			}
			this.RecycleAllItem();
			LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(itemIndex);
			if (newItemByIndex == null)
			{
				this.ClearAllTmpRecycledItem();
				return;
			}
			if (this.mIsVertList)
			{
				zero.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				zero.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = zero;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(itemIndex, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
				}
				else
				{
					this.SetItemSize(itemIndex, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
				}
			}
			this.mItemList.Add(newItemByIndex);
			this.UpdateContentSize();
			this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
			this.AdjustPanelPos();
			this.ClearAllTmpRecycledItem();
			this.ForceSnapUpdateCheck();
			this.UpdateSnapMove(false, true);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000C8490 File Offset: 0x000C6890
		public void RefreshAllShownItem()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.RefreshAllShownItemWithFirstIndex(this.mItemList[0].ItemIndex);
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000C84C8 File Offset: 0x000C68C8
		public void RefreshAllShownItemWithFirstIndex(int firstItemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			LoopListViewItem2 loopListViewItem = this.mItemList[0];
			Vector3 anchoredPosition3D = loopListViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleAllItem();
			for (int i = 0; i < count; i++)
			{
				int num = firstItemIndex + i;
				LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(num);
				if (newItemByIndex == null)
				{
					break;
				}
				if (this.mIsVertList)
				{
					anchoredPosition3D.x = newItemByIndex.StartPosOffset;
				}
				else
				{
					anchoredPosition3D.y = newItemByIndex.StartPosOffset;
				}
				newItemByIndex.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				if (this.mSupportScrollBar)
				{
					if (this.mIsVertList)
					{
						this.SetItemSize(num, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
					}
					else
					{
						this.SetItemSize(num, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
					}
				}
				this.mItemList.Add(newItemByIndex);
			}
			this.UpdateContentSize();
			this.UpdateAllShownItemsPos();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000C85F8 File Offset: 0x000C69F8
		public void RefreshAllShownItemWithFirstIndexAndPos(int firstItemIndex, Vector3 pos)
		{
			this.RecycleAllItem();
			LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(firstItemIndex);
			if (newItemByIndex == null)
			{
				return;
			}
			if (this.mIsVertList)
			{
				pos.x = newItemByIndex.StartPosOffset;
			}
			else
			{
				pos.y = newItemByIndex.StartPosOffset;
			}
			newItemByIndex.CachedRectTransform.anchoredPosition3D = pos;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(firstItemIndex, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
				}
				else
				{
					this.SetItemSize(firstItemIndex, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
				}
			}
			this.mItemList.Add(newItemByIndex);
			this.UpdateContentSize();
			this.UpdateAllShownItemsPos();
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000C86F0 File Offset: 0x000C6AF0
		private void RecycleItemTmp(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			ItemPool itemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out itemPool))
			{
				return;
			}
			itemPool.RecycleItem(item);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000C8740 File Offset: 0x000C6B40
		private void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000C877C File Offset: 0x000C6B7C
		private void RecycleAllItem()
		{
			foreach (LoopListViewItem2 item in this.mItemList)
			{
				this.RecycleItemTmp(item);
			}
			this.mItemList.Clear();
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000C87E4 File Offset: 0x000C6BE4
		private void AdjustContainerPivot(RectTransform rtf)
		{
			Vector2 pivot = rtf.pivot;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				pivot.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				pivot.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				pivot.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				pivot.x = 1f;
			}
			rtf.pivot = pivot;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000C8870 File Offset: 0x000C6C70
		private void AdjustPivot(RectTransform rtf)
		{
			Vector2 pivot = rtf.pivot;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				pivot.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				pivot.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				pivot.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				pivot.x = 1f;
			}
			rtf.pivot = pivot;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000C88FC File Offset: 0x000C6CFC
		private void AdjustContainerAnchor(RectTransform rtf)
		{
			Vector2 anchorMin = rtf.anchorMin;
			Vector2 anchorMax = rtf.anchorMax;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				anchorMin.y = 0f;
				anchorMax.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				anchorMin.y = 1f;
				anchorMax.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				anchorMin.x = 0f;
				anchorMax.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				anchorMin.x = 1f;
				anchorMax.x = 1f;
			}
			rtf.anchorMin = anchorMin;
			rtf.anchorMax = anchorMax;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000C89C4 File Offset: 0x000C6DC4
		private void AdjustAnchor(RectTransform rtf)
		{
			Vector2 anchorMin = rtf.anchorMin;
			Vector2 anchorMax = rtf.anchorMax;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				anchorMin.y = 0f;
				anchorMax.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				anchorMin.y = 1f;
				anchorMax.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				anchorMin.x = 0f;
				anchorMax.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				anchorMin.x = 1f;
				anchorMax.x = 1f;
			}
			rtf.anchorMin = anchorMin;
			rtf.anchorMax = anchorMax;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000C8A8C File Offset: 0x000C6E8C
		private void InitItemPool()
		{
			foreach (ItemPrefabConfData itemPrefabConfData in this.mItemPrefabDataList)
			{
				if (!(itemPrefabConfData.mItemPrefab == null))
				{
					string name = itemPrefabConfData.mItemPrefab.name;
					if (!this.mItemPoolDict.ContainsKey(name))
					{
						RectTransform component = itemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (!(component == null))
						{
							this.AdjustAnchor(component);
							this.AdjustPivot(component);
							LoopListViewItem2 component2 = itemPrefabConfData.mItemPrefab.GetComponent<LoopListViewItem2>();
							if (component2 == null)
							{
								itemPrefabConfData.mItemPrefab.AddComponent<LoopListViewItem2>();
							}
							ItemPool itemPool = new ItemPool();
							itemPool.Init(itemPrefabConfData.mItemPrefab, itemPrefabConfData.mPadding, itemPrefabConfData.mStartPosOffset, itemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, itemPool);
							this.mItemPoolList.Add(itemPool);
						}
					}
				}
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000C8BB4 File Offset: 0x000C6FB4
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mIsDraging = true;
			this.CacheDragPointerEventData(eventData);
			this.mCurSnapData.Clear();
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction();
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000C8BF1 File Offset: 0x000C6FF1
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mIsDraging = false;
			this.mPointerEventData = null;
			if (this.mOnEndDragAction != null)
			{
				this.mOnEndDragAction();
			}
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000C8C29 File Offset: 0x000C7029
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.CacheDragPointerEventData(eventData);
			if (this.mOnDragingAction != null)
			{
				this.mOnDragingAction();
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000C8C54 File Offset: 0x000C7054
		private void CacheDragPointerEventData(PointerEventData eventData)
		{
			if (this.mPointerEventData == null)
			{
				this.mPointerEventData = new PointerEventData(EventSystem.current);
			}
			this.mPointerEventData.button = eventData.button;
			this.mPointerEventData.position = eventData.position;
			this.mPointerEventData.pointerPressRaycast = eventData.pointerPressRaycast;
			this.mPointerEventData.pointerCurrentRaycast = eventData.pointerCurrentRaycast;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000C8CC0 File Offset: 0x000C70C0
		private LoopListViewItem2 GetNewItemByIndex(int index)
		{
			if (this.mSupportScrollBar && index < 0)
			{
				return null;
			}
			if (this.mItemTotalCount > 0 && index >= this.mItemTotalCount)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = this.mOnGetItemByIndex(this, index);
			if (loopListViewItem == null)
			{
				return null;
			}
			loopListViewItem.ItemIndex = index;
			loopListViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
			return loopListViewItem;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000C8D2B File Offset: 0x000C712B
		private void SetItemSize(int itemIndex, float itemSize, float padding)
		{
			this.mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
			if (itemIndex >= this.mLastItemIndex)
			{
				this.mLastItemIndex = itemIndex;
				this.mLastItemPadding = padding;
			}
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000C8D56 File Offset: 0x000C7156
		private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			return this.mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000C8D66 File Offset: 0x000C7166
		private float GetItemPos(int itemIndex)
		{
			return this.mItemPosMgr.GetItemPos(itemIndex);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000C8D74 File Offset: 0x000C7174
		public Vector3 GetItemCornerPosInViewPort(LoopListViewItem2 item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
		{
			item.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			return this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[(int)corner]);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000C8DA4 File Offset: 0x000C71A4
		private void AdjustPanelPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.UpdateAllShownItemsPos();
			float viewPortSize = this.ViewPortSize;
			float contentPanelSize = this.GetContentPanelSize();
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y < this.mViewPortRectLocalCorners[1].y)
				{
					Vector3 anchoredPosition3D2 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D2.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D2;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				float num = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y - this.mViewPortRectLocalCorners[0].y;
				if (num > 0f)
				{
					Vector3 anchoredPosition3D3 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D3.y -= num;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D3;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D4 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D4.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D4;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				loopListViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y > this.mViewPortRectLocalCorners[0].y)
				{
					Vector3 anchoredPosition3D5 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D5.y = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D5;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(this.mItemList[0].StartPosOffset, 0f, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				float num2 = this.mViewPortRectLocalCorners[1].y - vector.y;
				if (num2 > 0f)
				{
					Vector3 anchoredPosition3D6 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D6.y += num2;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D6;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D7 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D7.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D7;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem5 = this.mItemList[0];
				loopListViewItem5.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x > this.mViewPortRectLocalCorners[1].x)
				{
					Vector3 anchoredPosition3D8 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D8.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D8;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem6 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem6.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				float num3 = this.mViewPortRectLocalCorners[2].x - vector2.x;
				if (num3 > 0f)
				{
					Vector3 anchoredPosition3D9 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D9.x += num3;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D9;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				if (contentPanelSize <= viewPortSize)
				{
					Vector3 anchoredPosition3D10 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D10.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D10;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem7 = this.mItemList[0];
				loopListViewItem7.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x < this.mViewPortRectLocalCorners[2].x)
				{
					Vector3 anchoredPosition3D11 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D11.x = 0f;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D11;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mItemList[0].StartPosOffset, 0f);
					this.UpdateAllShownItemsPos();
					return;
				}
				LoopListViewItem2 loopListViewItem8 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem8.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x - this.mViewPortRectLocalCorners[1].x;
				if (num4 > 0f)
				{
					Vector3 anchoredPosition3D12 = this.mItemList[0].CachedRectTransform.anchoredPosition3D;
					anchoredPosition3D12.x -= num4;
					this.mItemList[0].CachedRectTransform.anchoredPosition3D = anchoredPosition3D12;
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000C9598 File Offset: 0x000C7998
		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			if (this.mNeedAdjustVec)
			{
				this.mNeedAdjustVec = false;
				if (this.mIsVertList)
				{
					if (this.mScrollRect.velocity.y * this.mAdjustedVec.y > 0f)
					{
						this.mScrollRect.velocity = this.mAdjustedVec;
					}
				}
				else if (this.mScrollRect.velocity.x * this.mAdjustedVec.x > 0f)
				{
					this.mScrollRect.velocity = this.mAdjustedVec;
				}
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.Update(false);
			}
			this.UpdateSnapMove(false, false);
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.ClearAllTmpRecycledItem();
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000C969B File Offset: 0x000C7A9B
		private void UpdateSnapMove(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			if (this.mIsVertList)
			{
				this.UpdateSnapVertical(immediate, forceSendEvent);
			}
			else
			{
				this.UpdateSnapHorizontal(immediate, forceSendEvent);
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000C96CC File Offset: 0x000C7ACC
		public void UpdateAllShownItemSnapData()
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			LoopListViewItem2 loopListViewItem = this.mItemList[0];
			loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = -(1f - this.mViewPortSnapPivot.y) * this.mViewPortRectTransform.rect.height;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y;
				float num3 = num2 - loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.y);
				for (int i = 0; i < count; i++)
				{
					this.mItemList[i].DistanceWithViewPortSnapCenter = num - num4;
					if (i + 1 < count)
					{
						num2 = num3;
						num3 -= this.mItemList[i + 1].ItemSizeWithPadding;
						num4 = num2 - this.mItemList[i + 1].ItemSize * (1f - this.mItemSnapPivot.y);
					}
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num = this.mViewPortSnapPivot.y * this.mViewPortRectTransform.rect.height;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y;
				float num3 = num2 + loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 + loopListViewItem.ItemSize * this.mItemSnapPivot.y;
				for (int j = 0; j < count; j++)
				{
					this.mItemList[j].DistanceWithViewPortSnapCenter = num - num4;
					if (j + 1 < count)
					{
						num2 = num3;
						num3 += this.mItemList[j + 1].ItemSizeWithPadding;
						num4 = num2 + this.mItemList[j + 1].ItemSize * this.mItemSnapPivot.y;
					}
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num = -(1f - this.mViewPortSnapPivot.x) * this.mViewPortRectTransform.rect.width;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x;
				float num3 = num2 - loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.x);
				for (int k = 0; k < count; k++)
				{
					this.mItemList[k].DistanceWithViewPortSnapCenter = num - num4;
					if (k + 1 < count)
					{
						num2 = num3;
						num3 -= this.mItemList[k + 1].ItemSizeWithPadding;
						num4 = num2 - this.mItemList[k + 1].ItemSize * (1f - this.mItemSnapPivot.x);
					}
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num = this.mViewPortSnapPivot.x * this.mViewPortRectTransform.rect.width;
				float num2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x;
				float num3 = num2 + loopListViewItem.ItemSizeWithPadding;
				float num4 = num2 + loopListViewItem.ItemSize * this.mItemSnapPivot.x;
				for (int l = 0; l < count; l++)
				{
					this.mItemList[l].DistanceWithViewPortSnapCenter = num - num4;
					if (l + 1 < count)
					{
						num2 = num3;
						num3 += this.mItemList[l + 1].ItemSizeWithPadding;
						num4 = num2 + this.mItemList[l + 1].ItemSize * this.mItemSnapPivot.x;
					}
				}
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000C9B20 File Offset: 0x000C7F20
		private void UpdateSnapVertical(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			bool flag = anchoredPosition3D.y != this.mLastSnapCheckPos.y;
			this.mLastSnapCheckPos = anchoredPosition3D;
			if (!flag && this.mLeftSnapUpdateExtraCount > 0)
			{
				this.mLeftSnapUpdateExtraCount--;
				flag = true;
			}
			if (flag)
			{
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				int num = -1;
				float num2 = float.MaxValue;
				if (this.mArrangeType == ListItemArrangeType.TopToBottom)
				{
					float num3 = -(1f - this.mViewPortSnapPivot.y) * this.mViewPortRectTransform.rect.height;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y;
					float num5 = num4 - loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.y);
					for (int i = 0; i < count; i++)
					{
						float f = num3 - num6;
						float num7 = Mathf.Abs(f);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = i;
						if (i + 1 < count)
						{
							num4 = num5;
							num5 -= this.mItemList[i + 1].ItemSizeWithPadding;
							num6 = num4 - this.mItemList[i + 1].ItemSize * (1f - this.mItemSnapPivot.y);
						}
					}
				}
				else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
				{
					float num3 = this.mViewPortSnapPivot.y * this.mViewPortRectTransform.rect.height;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]).y;
					float num5 = num4 + loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 + loopListViewItem.ItemSize * this.mItemSnapPivot.y;
					for (int j = 0; j < count; j++)
					{
						float f = num3 - num6;
						float num7 = Mathf.Abs(f);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = j;
						if (j + 1 < count)
						{
							num4 = num5;
							num5 += this.mItemList[j + 1].ItemSizeWithPadding;
							num6 = num4 + this.mItemList[j + 1].ItemSize * this.mItemSnapPivot.y;
						}
					}
				}
				if (num >= 0)
				{
					int num8 = this.mCurSnapNearestItemIndex;
					this.mCurSnapNearestItemIndex = this.mItemList[num].ItemIndex;
					if ((forceSendEvent || this.mItemList[num].ItemIndex != num8) && this.mOnSnapNearestChanged != null)
					{
						this.mOnSnapNearestChanged(this, this.mItemList[num]);
					}
				}
				else
				{
					this.mCurSnapNearestItemIndex = -1;
				}
			}
			if (!this.CanSnap())
			{
				this.ClearSnapData();
				return;
			}
			float num9 = Mathf.Abs(this.mScrollRect.velocity.y);
			this.UpdateCurSnapData();
			if (this.mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
			{
				return;
			}
			if (num9 > 0f)
			{
				this.mScrollRect.StopMovement();
			}
			float mCurSnapVal = this.mCurSnapData.mCurSnapVal;
			this.mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, ref this.mSmoothDumpVel, this.mSmoothDumpRate);
			float num10 = this.mCurSnapData.mCurSnapVal - mCurSnapVal;
			if (immediate || Mathf.Abs(this.mCurSnapData.mTargetSnapVal - this.mCurSnapData.mCurSnapVal) < this.mSnapFinishThreshold)
			{
				anchoredPosition3D.y = anchoredPosition3D.y + this.mCurSnapData.mTargetSnapVal - mCurSnapVal;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
				if (this.mOnSnapItemFinished != null)
				{
					LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(this.mCurSnapNearestItemIndex);
					if (shownItemByItemIndex != null)
					{
						this.mOnSnapItemFinished(this, shownItemByItemIndex);
					}
				}
			}
			else
			{
				anchoredPosition3D.y += num10;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float max = this.mViewPortRectLocalCorners[0].y + this.mContainerTrans.rect.height;
				anchoredPosition3D.y = Mathf.Clamp(anchoredPosition3D.y, 0f, max);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float min = this.mViewPortRectLocalCorners[1].y - this.mContainerTrans.rect.height;
				anchoredPosition3D.y = Mathf.Clamp(anchoredPosition3D.y, min, 0f);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000CA08C File Offset: 0x000C848C
		private void UpdateCurSnapData()
		{
			if (this.mItemList.Count == 0)
			{
				this.mCurSnapData.Clear();
				return;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoveFinish)
			{
				if (this.mCurSnapData.mSnapTargetIndex == this.mCurSnapNearestItemIndex)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoving)
			{
				if (this.mCurSnapData.mSnapTargetIndex == this.mCurSnapNearestItemIndex || this.mCurSnapData.mIsForceSnapTo)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.NoTargetSet)
			{
				LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(this.mCurSnapNearestItemIndex);
				if (shownItemByItemIndex == null)
				{
					return;
				}
				this.mCurSnapData.mSnapTargetIndex = this.mCurSnapNearestItemIndex;
				this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
				this.mCurSnapData.mIsForceSnapTo = false;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.TargetHasSet)
			{
				LoopListViewItem2 shownItemByItemIndex2 = this.GetShownItemByItemIndex(this.mCurSnapData.mSnapTargetIndex);
				if (shownItemByItemIndex2 == null)
				{
					this.mCurSnapData.Clear();
					return;
				}
				this.UpdateAllShownItemSnapData();
				this.mCurSnapData.mTargetSnapVal = shownItemByItemIndex2.DistanceWithViewPortSnapCenter;
				this.mCurSnapData.mCurSnapVal = 0f;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000CA1F0 File Offset: 0x000C85F0
		public void ClearSnapData()
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000CA1FD File Offset: 0x000C85FD
		public void SetSnapTargetItemIndex(int itemIndex)
		{
			this.mCurSnapData.mSnapTargetIndex = itemIndex;
			this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
			this.mCurSnapData.mIsForceSnapTo = true;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x000CA223 File Offset: 0x000C8623
		public int CurSnapNearestItemIndex
		{
			get
			{
				return this.mCurSnapNearestItemIndex;
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000CA22B File Offset: 0x000C862B
		public void ForceSnapUpdateCheck()
		{
			if (this.mLeftSnapUpdateExtraCount <= 0)
			{
				this.mLeftSnapUpdateExtraCount = 1;
			}
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000CA240 File Offset: 0x000C8640
		private void UpdateSnapHorizontal(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			bool flag = anchoredPosition3D.x != this.mLastSnapCheckPos.x;
			this.mLastSnapCheckPos = anchoredPosition3D;
			if (!flag && this.mLeftSnapUpdateExtraCount > 0)
			{
				this.mLeftSnapUpdateExtraCount--;
				flag = true;
			}
			if (flag)
			{
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				int num = -1;
				float num2 = float.MaxValue;
				if (this.mArrangeType == ListItemArrangeType.RightToLeft)
				{
					float num3 = -(1f - this.mViewPortSnapPivot.x) * this.mViewPortRectTransform.rect.width;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x;
					float num5 = num4 - loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 - loopListViewItem.ItemSize * (1f - this.mItemSnapPivot.x);
					for (int i = 0; i < count; i++)
					{
						float f = num3 - num6;
						float num7 = Mathf.Abs(f);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = i;
						if (i + 1 < count)
						{
							num4 = num5;
							num5 -= this.mItemList[i + 1].ItemSizeWithPadding;
							num6 = num4 - this.mItemList[i + 1].ItemSize * (1f - this.mItemSnapPivot.x);
						}
					}
				}
				else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
				{
					float num3 = this.mViewPortSnapPivot.x * this.mViewPortRectTransform.rect.width;
					float num4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).x;
					float num5 = num4 + loopListViewItem.ItemSizeWithPadding;
					float num6 = num4 + loopListViewItem.ItemSize * this.mItemSnapPivot.x;
					for (int j = 0; j < count; j++)
					{
						float f = num3 - num6;
						float num7 = Mathf.Abs(f);
						if (num7 >= num2)
						{
							break;
						}
						num2 = num7;
						num = j;
						if (j + 1 < count)
						{
							num4 = num5;
							num5 += this.mItemList[j + 1].ItemSizeWithPadding;
							num6 = num4 + this.mItemList[j + 1].ItemSize * this.mItemSnapPivot.x;
						}
					}
				}
				if (num >= 0)
				{
					int num8 = this.mCurSnapNearestItemIndex;
					this.mCurSnapNearestItemIndex = this.mItemList[num].ItemIndex;
					if ((forceSendEvent || this.mItemList[num].ItemIndex != num8) && this.mOnSnapNearestChanged != null)
					{
						this.mOnSnapNearestChanged(this, this.mItemList[num]);
					}
				}
				else
				{
					this.mCurSnapNearestItemIndex = -1;
				}
			}
			if (!this.CanSnap())
			{
				this.ClearSnapData();
				return;
			}
			float num9 = Mathf.Abs(this.mScrollRect.velocity.x);
			this.UpdateCurSnapData();
			if (this.mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
			{
				return;
			}
			if (num9 > 0f)
			{
				this.mScrollRect.StopMovement();
			}
			float mCurSnapVal = this.mCurSnapData.mCurSnapVal;
			this.mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, ref this.mSmoothDumpVel, this.mSmoothDumpRate);
			float num10 = this.mCurSnapData.mCurSnapVal - mCurSnapVal;
			if (immediate || Mathf.Abs(this.mCurSnapData.mTargetSnapVal - this.mCurSnapData.mCurSnapVal) < this.mSnapFinishThreshold)
			{
				anchoredPosition3D.x = anchoredPosition3D.x + this.mCurSnapData.mTargetSnapVal - mCurSnapVal;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
				if (this.mOnSnapItemFinished != null)
				{
					LoopListViewItem2 shownItemByItemIndex = this.GetShownItemByItemIndex(this.mCurSnapNearestItemIndex);
					if (shownItemByItemIndex != null)
					{
						this.mOnSnapItemFinished(this, shownItemByItemIndex);
					}
				}
			}
			else
			{
				anchoredPosition3D.x += num10;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float min = this.mViewPortRectLocalCorners[2].x - this.mContainerTrans.rect.width;
				anchoredPosition3D.x = Mathf.Clamp(anchoredPosition3D.x, min, 0f);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float max = this.mViewPortRectLocalCorners[1].x + this.mContainerTrans.rect.width;
				anchoredPosition3D.x = Mathf.Clamp(anchoredPosition3D.x, 0f, max);
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000CA7AC File Offset: 0x000C8BAC
		private bool CanSnap()
		{
			if (this.mIsDraging)
			{
				return false;
			}
			if (this.mScrollBarClickEventListener != null && this.mScrollBarClickEventListener.IsPressd)
			{
				return false;
			}
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height <= this.ViewPortHeight)
				{
					return false;
				}
			}
			else if (this.mContainerTrans.rect.width <= this.ViewPortWidth)
			{
				return false;
			}
			float num;
			if (this.mIsVertList)
			{
				num = Mathf.Abs(this.mScrollRect.velocity.y);
			}
			else
			{
				num = Mathf.Abs(this.mScrollRect.velocity.x);
			}
			if (num > this.mSnapVecThreshold)
			{
				return false;
			}
			if (num < 2f)
			{
				return true;
			}
			float num2 = 3f;
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num3 = this.mViewPortRectLocalCorners[2].x - this.mContainerTrans.rect.width;
				if (anchoredPosition3D.x < num3 - num2 || anchoredPosition3D.x > num2)
				{
					return false;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num4 = this.mViewPortRectLocalCorners[1].x + this.mContainerTrans.rect.width;
				if (anchoredPosition3D.x > num4 + num2 || anchoredPosition3D.x < -num2)
				{
					return false;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num5 = this.mViewPortRectLocalCorners[0].y + this.mContainerTrans.rect.height;
				if (anchoredPosition3D.y > num5 + num2 || anchoredPosition3D.y < -num2)
				{
					return false;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num6 = this.mViewPortRectLocalCorners[1].y - this.mContainerTrans.rect.height;
				if (anchoredPosition3D.y < num6 - num2 || anchoredPosition3D.y > num2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000CAA18 File Offset: 0x000C8E18
		public void UpdateListView(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			this.mListUpdateCheckFrameCount++;
			if (this.mIsVertList)
			{
				bool flag = true;
				int num = 0;
				int num2 = 9999;
				while (flag)
				{
					num++;
					if (num >= num2)
					{
						break;
					}
					flag = this.UpdateForVertList(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
			}
			else
			{
				bool flag2 = true;
				int num3 = 0;
				int num4 = 9999;
				while (flag2)
				{
					num3++;
					if (num3 >= num4)
					{
						break;
					}
					flag2 = this.UpdateForHorizontalList(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
			}
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000CAAAC File Offset: 0x000C8EAC
		private bool UpdateForVertList(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.y;
					if (num < 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float num3 = -num;
					if (this.mSupportScrollBar)
					{
						if (!this.GetPlusItemIndexAndPosAtGivenPos(num, ref num2, ref num3))
						{
							return false;
						}
						num3 = -num3;
					}
					LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(num2);
					if (newItemByIndex == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndex.CachedRectTransform.rect.height, newItemByIndex.Padding);
					}
					this.mItemList.Add(newItemByIndex);
					newItemByIndex.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex.StartPosOffset, num3, 0f);
					this.UpdateContentSize();
					return true;
				}
				else
				{
					LoopListViewItem2 loopListViewItem = this.mItemList[0];
					loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.mIsDraging && loopListViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector2.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopListViewItem);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopListViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.mIsDraging && loopListViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector3.y > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopListViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					if (this.mViewPortRectLocalCorners[0].y - vector4.y < distanceForNew1)
					{
						if (loopListViewItem2.ItemIndex > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopListViewItem2.ItemIndex + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopListViewItem2 newItemByIndex2 = this.GetNewItemByIndex(num4);
							if (!(newItemByIndex2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndex2.CachedRectTransform.rect.height, newItemByIndex2.Padding);
								}
								this.mItemList.Add(newItemByIndex2);
								float y = loopListViewItem2.CachedRectTransform.anchoredPosition3D.y - loopListViewItem2.CachedRectTransform.rect.height - loopListViewItem2.Padding;
								newItemByIndex2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex2.StartPosOffset, y, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num4 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdataItemPos();
						}
					}
					if (vector.y - this.mViewPortRectLocalCorners[1].y < distanceForNew0)
					{
						if (loopListViewItem.ItemIndex < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = true;
						}
						int num5 = loopListViewItem.ItemIndex - 1;
						if (num5 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopListViewItem2 newItemByIndex3 = this.GetNewItemByIndex(num5);
							if (!(newItemByIndex3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num5, newItemByIndex3.CachedRectTransform.rect.height, newItemByIndex3.Padding);
								}
								this.mItemList.Insert(0, newItemByIndex3);
								float y2 = loopListViewItem.CachedRectTransform.anchoredPosition3D.y + newItemByIndex3.CachedRectTransform.rect.height + newItemByIndex3.Padding;
								newItemByIndex3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex3.StartPosOffset, y2, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num5 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num5;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = false;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num6 = this.mContainerTrans.anchoredPosition3D.y;
				if (num6 > 0f)
				{
					num6 = 0f;
				}
				int num7 = 0;
				float y3 = -num6;
				if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num6, ref num7, ref y3))
				{
					return false;
				}
				LoopListViewItem2 newItemByIndex4 = this.GetNewItemByIndex(num7);
				if (newItemByIndex4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num7, newItemByIndex4.CachedRectTransform.rect.height, newItemByIndex4.Padding);
				}
				this.mItemList.Add(newItemByIndex4);
				newItemByIndex4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex4.StartPosOffset, y3, 0f);
				this.UpdateContentSize();
				return true;
			}
			else
			{
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				loopListViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.mIsDraging && loopListViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector5.y > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopListViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.mIsDraging && loopListViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector8.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopListViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				if (vector7.y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopListViewItem4.ItemIndex > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
						this.mNeedCheckNextMaxItem = true;
					}
					int num8 = loopListViewItem4.ItemIndex + 1;
					if (num8 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopListViewItem2 newItemByIndex5 = this.GetNewItemByIndex(num8);
						if (!(newItemByIndex5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num8, newItemByIndex5.CachedRectTransform.rect.height, newItemByIndex5.Padding);
							}
							this.mItemList.Add(newItemByIndex5);
							float y4 = loopListViewItem4.CachedRectTransform.anchoredPosition3D.y + loopListViewItem4.CachedRectTransform.rect.height + loopListViewItem4.Padding;
							newItemByIndex5.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex5.StartPosOffset, y4, 0f);
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
							if (num8 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num8;
							}
							return true;
						}
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdataItemPos();
					}
				}
				if (this.mViewPortRectLocalCorners[0].y - vector6.y < distanceForNew0)
				{
					if (loopListViewItem3.ItemIndex < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
						this.mNeedCheckNextMinItem = true;
					}
					int num9 = loopListViewItem3.ItemIndex - 1;
					if (num9 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopListViewItem2 newItemByIndex6 = this.GetNewItemByIndex(num9);
						if (newItemByIndex6 == null)
						{
							this.mNeedCheckNextMinItem = false;
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num9, newItemByIndex6.CachedRectTransform.rect.height, newItemByIndex6.Padding);
						}
						this.mItemList.Insert(0, newItemByIndex6);
						float y5 = loopListViewItem3.CachedRectTransform.anchoredPosition3D.y - newItemByIndex6.CachedRectTransform.rect.height - newItemByIndex6.Padding;
						newItemByIndex6.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndex6.StartPosOffset, y5, 0f);
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
						if (num9 < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = num9;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000CB508 File Offset: 0x000C9908
		private bool UpdateForHorizontalList(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.x;
					if (num > 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float x = -num;
					if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num, ref num2, ref x))
					{
						return false;
					}
					LoopListViewItem2 newItemByIndex = this.GetNewItemByIndex(num2);
					if (newItemByIndex == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndex.CachedRectTransform.rect.width, newItemByIndex.Padding);
					}
					this.mItemList.Add(newItemByIndex);
					newItemByIndex.CachedRectTransform.anchoredPosition3D = new Vector3(x, newItemByIndex.StartPosOffset, 0f);
					this.UpdateContentSize();
					return true;
				}
				else
				{
					LoopListViewItem2 loopListViewItem = this.mItemList[0];
					loopListViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.mIsDraging && loopListViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector2.x > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopListViewItem);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopListViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.mIsDraging && loopListViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector3.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopListViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
						}
						return true;
					}
					if (vector4.x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopListViewItem2.ItemIndex > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = true;
						}
						int num3 = loopListViewItem2.ItemIndex + 1;
						if (num3 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopListViewItem2 newItemByIndex2 = this.GetNewItemByIndex(num3);
							if (!(newItemByIndex2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num3, newItemByIndex2.CachedRectTransform.rect.width, newItemByIndex2.Padding);
								}
								this.mItemList.Add(newItemByIndex2);
								float x2 = loopListViewItem2.CachedRectTransform.anchoredPosition3D.x + loopListViewItem2.CachedRectTransform.rect.width + loopListViewItem2.Padding;
								newItemByIndex2.CachedRectTransform.anchoredPosition3D = new Vector3(x2, newItemByIndex2.StartPosOffset, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num3 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num3;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopListViewItem2.ItemIndex;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdataItemPos();
						}
					}
					if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew0)
					{
						if (loopListViewItem.ItemIndex < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = true;
						}
						int num4 = loopListViewItem.ItemIndex - 1;
						if (num4 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopListViewItem2 newItemByIndex3 = this.GetNewItemByIndex(num4);
							if (!(newItemByIndex3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndex3.CachedRectTransform.rect.width, newItemByIndex3.Padding);
								}
								this.mItemList.Insert(0, newItemByIndex3);
								float x3 = loopListViewItem.CachedRectTransform.anchoredPosition3D.x - newItemByIndex3.CachedRectTransform.rect.width - newItemByIndex3.Padding;
								newItemByIndex3.CachedRectTransform.anchoredPosition3D = new Vector3(x3, newItemByIndex3.StartPosOffset, 0f);
								this.UpdateContentSize();
								this.CheckIfNeedUpdataItemPos();
								if (num4 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopListViewItem.ItemIndex;
							this.mNeedCheckNextMinItem = false;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num5 = this.mContainerTrans.anchoredPosition3D.x;
				if (num5 < 0f)
				{
					num5 = 0f;
				}
				int num6 = 0;
				float num7 = -num5;
				if (this.mSupportScrollBar)
				{
					if (!this.GetPlusItemIndexAndPosAtGivenPos(num5, ref num6, ref num7))
					{
						return false;
					}
					num7 = -num7;
				}
				LoopListViewItem2 newItemByIndex4 = this.GetNewItemByIndex(num6);
				if (newItemByIndex4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num6, newItemByIndex4.CachedRectTransform.rect.width, newItemByIndex4.Padding);
				}
				this.mItemList.Add(newItemByIndex4);
				newItemByIndex4.CachedRectTransform.anchoredPosition3D = new Vector3(num7, newItemByIndex4.StartPosOffset, 0f);
				this.UpdateContentSize();
				return true;
			}
			else
			{
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				loopListViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.mIsDraging && loopListViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector5.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopListViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopListViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.mIsDraging && loopListViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector8.x > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopListViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.UpdateContentSize();
						this.CheckIfNeedUpdataItemPos();
					}
					return true;
				}
				if (this.mViewPortRectLocalCorners[1].x - vector7.x < distanceForNew1)
				{
					if (loopListViewItem4.ItemIndex > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
						this.mNeedCheckNextMaxItem = true;
					}
					int num8 = loopListViewItem4.ItemIndex + 1;
					if (num8 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopListViewItem2 newItemByIndex5 = this.GetNewItemByIndex(num8);
						if (!(newItemByIndex5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num8, newItemByIndex5.CachedRectTransform.rect.width, newItemByIndex5.Padding);
							}
							this.mItemList.Add(newItemByIndex5);
							float x4 = loopListViewItem4.CachedRectTransform.anchoredPosition3D.x - loopListViewItem4.CachedRectTransform.rect.width - loopListViewItem4.Padding;
							newItemByIndex5.CachedRectTransform.anchoredPosition3D = new Vector3(x4, newItemByIndex5.StartPosOffset, 0f);
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
							if (num8 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num8;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopListViewItem4.ItemIndex;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdataItemPos();
					}
				}
				if (vector6.x - this.mViewPortRectLocalCorners[2].x < distanceForNew0)
				{
					if (loopListViewItem3.ItemIndex < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
						this.mNeedCheckNextMinItem = true;
					}
					int num9 = loopListViewItem3.ItemIndex - 1;
					if (num9 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopListViewItem2 newItemByIndex6 = this.GetNewItemByIndex(num9);
						if (!(newItemByIndex6 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num9, newItemByIndex6.CachedRectTransform.rect.width, newItemByIndex6.Padding);
							}
							this.mItemList.Insert(0, newItemByIndex6);
							float x5 = loopListViewItem3.CachedRectTransform.anchoredPosition3D.x + newItemByIndex6.CachedRectTransform.rect.width + newItemByIndex6.Padding;
							newItemByIndex6.CachedRectTransform.anchoredPosition3D = new Vector3(x5, newItemByIndex6.StartPosOffset, 0f);
							this.UpdateContentSize();
							this.CheckIfNeedUpdataItemPos();
							if (num9 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num9;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopListViewItem3.ItemIndex;
						this.mNeedCheckNextMinItem = false;
					}
				}
			}
			return false;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000CBF7C File Offset: 0x000CA37C
		private float GetContentPanelSize()
		{
			if (this.mSupportScrollBar)
			{
				float num = (this.mItemPosMgr.mTotalSize <= 0f) ? 0f : (this.mItemPosMgr.mTotalSize - this.mLastItemPadding);
				if (num < 0f)
				{
					num = 0f;
				}
				return num;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return 0f;
			}
			if (count == 1)
			{
				return this.mItemList[0].ItemSize;
			}
			if (count == 2)
			{
				return this.mItemList[0].ItemSizeWithPadding + this.mItemList[1].ItemSize;
			}
			float num2 = 0f;
			for (int i = 0; i < count - 1; i++)
			{
				num2 += this.mItemList[i].ItemSizeWithPadding;
			}
			return num2 + this.mItemList[count - 1].ItemSize;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000CC07C File Offset: 0x000CA47C
		private void CheckIfNeedUpdataItemPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				LoopListViewItem2 loopListViewItem = this.mItemList[0];
				LoopListViewItem2 loopListViewItem2 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize = this.GetContentPanelSize();
				if (loopListViewItem.TopY > 0f || (loopListViewItem.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem.TopY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (-loopListViewItem2.BottomY > contentPanelSize || (loopListViewItem2.ItemIndex == this.mCurReadyMaxItemIndex && -loopListViewItem2.BottomY != contentPanelSize))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				LoopListViewItem2 loopListViewItem3 = this.mItemList[0];
				LoopListViewItem2 loopListViewItem4 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize2 = this.GetContentPanelSize();
				if (loopListViewItem3.BottomY < 0f || (loopListViewItem3.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem3.BottomY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (loopListViewItem4.TopY > contentPanelSize2 || (loopListViewItem4.ItemIndex == this.mCurReadyMaxItemIndex && loopListViewItem4.TopY != contentPanelSize2))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				LoopListViewItem2 loopListViewItem5 = this.mItemList[0];
				LoopListViewItem2 loopListViewItem6 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize3 = this.GetContentPanelSize();
				if (loopListViewItem5.LeftX < 0f || (loopListViewItem5.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem5.LeftX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (loopListViewItem6.RightX > contentPanelSize3 || (loopListViewItem6.ItemIndex == this.mCurReadyMaxItemIndex && loopListViewItem6.RightX != contentPanelSize3))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				LoopListViewItem2 loopListViewItem7 = this.mItemList[0];
				LoopListViewItem2 loopListViewItem8 = this.mItemList[this.mItemList.Count - 1];
				float contentPanelSize4 = this.GetContentPanelSize();
				if (loopListViewItem7.RightX > 0f || (loopListViewItem7.ItemIndex == this.mCurReadyMinItemIndex && loopListViewItem7.RightX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				if (-loopListViewItem8.LeftX > contentPanelSize4 || (loopListViewItem8.ItemIndex == this.mCurReadyMaxItemIndex && -loopListViewItem8.LeftX != contentPanelSize4))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000CC354 File Offset: 0x000CA754
		private void UpdateAllShownItemsPos()
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			this.mAdjustedVec = (this.mContainerTrans.anchoredPosition3D - this.mLastFrameContainerPos) / Time.deltaTime;
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = 0f;
				if (this.mSupportScrollBar)
				{
					num = -this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float y = this.mItemList[0].CachedRectTransform.anchoredPosition3D.y;
				float num2 = num - y;
				float num3 = num;
				for (int i = 0; i < count; i++)
				{
					LoopListViewItem2 loopListViewItem = this.mItemList[i];
					loopListViewItem.CachedRectTransform.anchoredPosition3D = new Vector3(loopListViewItem.StartPosOffset, num3, 0f);
					num3 = num3 - loopListViewItem.CachedRectTransform.rect.height - loopListViewItem.Padding;
				}
				if (num2 != 0f)
				{
					Vector2 v = this.mContainerTrans.anchoredPosition3D;
					v.y -= num2;
					this.mContainerTrans.anchoredPosition3D = v;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num4 = 0f;
				if (this.mSupportScrollBar)
				{
					num4 = this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float y2 = this.mItemList[0].CachedRectTransform.anchoredPosition3D.y;
				float num5 = num4 - y2;
				float num6 = num4;
				for (int j = 0; j < count; j++)
				{
					LoopListViewItem2 loopListViewItem2 = this.mItemList[j];
					loopListViewItem2.CachedRectTransform.anchoredPosition3D = new Vector3(loopListViewItem2.StartPosOffset, num6, 0f);
					num6 = num6 + loopListViewItem2.CachedRectTransform.rect.height + loopListViewItem2.Padding;
				}
				if (num5 != 0f)
				{
					Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D.y -= num5;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num7 = 0f;
				if (this.mSupportScrollBar)
				{
					num7 = this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float x = this.mItemList[0].CachedRectTransform.anchoredPosition3D.x;
				float num8 = num7 - x;
				float num9 = num7;
				for (int k = 0; k < count; k++)
				{
					LoopListViewItem2 loopListViewItem3 = this.mItemList[k];
					loopListViewItem3.CachedRectTransform.anchoredPosition3D = new Vector3(num9, loopListViewItem3.StartPosOffset, 0f);
					num9 = num9 + loopListViewItem3.CachedRectTransform.rect.width + loopListViewItem3.Padding;
				}
				if (num8 != 0f)
				{
					Vector3 anchoredPosition3D2 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D2.x -= num8;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D2;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num10 = 0f;
				if (this.mSupportScrollBar)
				{
					num10 = -this.GetItemPos(this.mItemList[0].ItemIndex);
				}
				float x2 = this.mItemList[0].CachedRectTransform.anchoredPosition3D.x;
				float num11 = num10 - x2;
				float num12 = num10;
				for (int l = 0; l < count; l++)
				{
					LoopListViewItem2 loopListViewItem4 = this.mItemList[l];
					loopListViewItem4.CachedRectTransform.anchoredPosition3D = new Vector3(num12, loopListViewItem4.StartPosOffset, 0f);
					num12 = num12 - loopListViewItem4.CachedRectTransform.rect.width - loopListViewItem4.Padding;
				}
				if (num11 != 0f)
				{
					Vector3 anchoredPosition3D3 = this.mContainerTrans.anchoredPosition3D;
					anchoredPosition3D3.x -= num11;
					this.mContainerTrans.anchoredPosition3D = anchoredPosition3D3;
				}
			}
			if (this.mIsDraging)
			{
				this.mScrollRect.OnBeginDrag(this.mPointerEventData);
				this.mScrollRect.Rebuild(CanvasUpdate.PostLayout);
				this.mScrollRect.velocity = this.mAdjustedVec;
				this.mNeedAdjustVec = true;
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000CC7F4 File Offset: 0x000CABF4
		private void UpdateContentSize()
		{
			float contentPanelSize = this.GetContentPanelSize();
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height != contentPanelSize)
				{
					this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentPanelSize);
				}
			}
			else if (this.mContainerTrans.rect.width != contentPanelSize)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, contentPanelSize);
			}
		}

		// Token: 0x04002360 RID: 9056
		private Dictionary<string, ItemPool> mItemPoolDict = new Dictionary<string, ItemPool>();

		// Token: 0x04002361 RID: 9057
		private List<ItemPool> mItemPoolList = new List<ItemPool>();

		// Token: 0x04002362 RID: 9058
		[SerializeField]
		private List<ItemPrefabConfData> mItemPrefabDataList = new List<ItemPrefabConfData>();

		// Token: 0x04002363 RID: 9059
		[SerializeField]
		private ListItemArrangeType mArrangeType;

		// Token: 0x04002364 RID: 9060
		private List<LoopListViewItem2> mItemList = new List<LoopListViewItem2>();

		// Token: 0x04002365 RID: 9061
		private RectTransform mContainerTrans;

		// Token: 0x04002366 RID: 9062
		private ScrollRect mScrollRect;

		// Token: 0x04002367 RID: 9063
		private RectTransform mScrollRectTransform;

		// Token: 0x04002368 RID: 9064
		private RectTransform mViewPortRectTransform;

		// Token: 0x04002369 RID: 9065
		private float mItemDefaultWithPaddingSize = 20f;

		// Token: 0x0400236A RID: 9066
		private int mItemTotalCount;

		// Token: 0x0400236B RID: 9067
		private bool mIsVertList;

		// Token: 0x0400236C RID: 9068
		private Func<LoopListView2, int, LoopListViewItem2> mOnGetItemByIndex;

		// Token: 0x0400236D RID: 9069
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x0400236E RID: 9070
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x0400236F RID: 9071
		private int mCurReadyMinItemIndex;

		// Token: 0x04002370 RID: 9072
		private int mCurReadyMaxItemIndex;

		// Token: 0x04002371 RID: 9073
		private bool mNeedCheckNextMinItem = true;

		// Token: 0x04002372 RID: 9074
		private bool mNeedCheckNextMaxItem = true;

		// Token: 0x04002373 RID: 9075
		private ItemPosMgr mItemPosMgr;

		// Token: 0x04002374 RID: 9076
		private float mDistanceForRecycle0 = 300f;

		// Token: 0x04002375 RID: 9077
		private float mDistanceForNew0 = 200f;

		// Token: 0x04002376 RID: 9078
		private float mDistanceForRecycle1 = 300f;

		// Token: 0x04002377 RID: 9079
		private float mDistanceForNew1 = 200f;

		// Token: 0x04002378 RID: 9080
		[SerializeField]
		private bool mSupportScrollBar = true;

		// Token: 0x04002379 RID: 9081
		private bool mIsDraging;

		// Token: 0x0400237A RID: 9082
		private PointerEventData mPointerEventData;

		// Token: 0x0400237B RID: 9083
		public Action mOnBeginDragAction;

		// Token: 0x0400237C RID: 9084
		public Action mOnDragingAction;

		// Token: 0x0400237D RID: 9085
		public Action mOnEndDragAction;

		// Token: 0x0400237E RID: 9086
		private int mLastItemIndex;

		// Token: 0x0400237F RID: 9087
		private float mLastItemPadding;

		// Token: 0x04002380 RID: 9088
		private float mSmoothDumpVel;

		// Token: 0x04002381 RID: 9089
		private float mSmoothDumpRate = 0.3f;

		// Token: 0x04002382 RID: 9090
		private float mSnapFinishThreshold = 0.1f;

		// Token: 0x04002383 RID: 9091
		private float mSnapVecThreshold = 145f;

		// Token: 0x04002384 RID: 9092
		[SerializeField]
		private bool mItemSnapEnable;

		// Token: 0x04002385 RID: 9093
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x04002386 RID: 9094
		public Action<LoopListView2, LoopListViewItem2> mOnSnapItemFinished;

		// Token: 0x04002387 RID: 9095
		public Action<LoopListView2, LoopListViewItem2> mOnSnapNearestChanged;

		// Token: 0x04002388 RID: 9096
		private int mCurSnapNearestItemIndex = -1;

		// Token: 0x04002389 RID: 9097
		private Vector2 mAdjustedVec;

		// Token: 0x0400238A RID: 9098
		private bool mNeedAdjustVec;

		// Token: 0x0400238B RID: 9099
		private int mLeftSnapUpdateExtraCount = 1;

		// Token: 0x0400238C RID: 9100
		[SerializeField]
		private Vector2 mViewPortSnapPivot = Vector2.zero;

		// Token: 0x0400238D RID: 9101
		[SerializeField]
		private Vector2 mItemSnapPivot = Vector2.zero;

		// Token: 0x0400238E RID: 9102
		private ClickEventListener mScrollBarClickEventListener;

		// Token: 0x0400238F RID: 9103
		private LoopListView2.SnapData mCurSnapData = new LoopListView2.SnapData();

		// Token: 0x04002390 RID: 9104
		private Vector3 mLastSnapCheckPos = Vector3.zero;

		// Token: 0x04002391 RID: 9105
		private bool mListViewInited;

		// Token: 0x04002392 RID: 9106
		private int mListUpdateCheckFrameCount;

		// Token: 0x02000604 RID: 1540
		private class SnapData
		{
			// Token: 0x06002441 RID: 9281 RVA: 0x000CC86C File Offset: 0x000CAC6C
			public void Clear()
			{
				this.mSnapStatus = SnapStatus.NoTargetSet;
				this.mIsForceSnapTo = false;
			}

			// Token: 0x04002393 RID: 9107
			public SnapStatus mSnapStatus;

			// Token: 0x04002394 RID: 9108
			public int mSnapTargetIndex;

			// Token: 0x04002395 RID: 9109
			public float mTargetSnapVal;

			// Token: 0x04002396 RID: 9110
			public float mCurSnapVal;

			// Token: 0x04002397 RID: 9111
			public bool mIsForceSnapTo;
		}
	}
}
