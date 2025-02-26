using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005FC RID: 1532
	public class LoopGridView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000C403D File Offset: 0x000C243D
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x000C4045 File Offset: 0x000C2445
		public GridItemArrangeType ArrangeType
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

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000C404E File Offset: 0x000C244E
		public List<GridViewItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000C4056 File Offset: 0x000C2456
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000C405E File Offset: 0x000C245E
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000C4068 File Offset: 0x000C2468
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000C4088 File Offset: 0x000C2488
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000C40A8 File Offset: 0x000C24A8
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x000C40B0 File Offset: 0x000C24B0
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000C40B8 File Offset: 0x000C24B8
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x000C40C0 File Offset: 0x000C24C0
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000C40C9 File Offset: 0x000C24C9
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x000C40D1 File Offset: 0x000C24D1
		public Vector2 ItemSize
		{
			get
			{
				return this.mItemSize;
			}
			set
			{
				this.SetItemSize(value);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000C40DA File Offset: 0x000C24DA
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x000C40E2 File Offset: 0x000C24E2
		public Vector2 ItemPadding
		{
			get
			{
				return this.mItemPadding;
			}
			set
			{
				this.SetItemPadding(value);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000C40EB File Offset: 0x000C24EB
		public Vector2 ItemSizeWithPadding
		{
			get
			{
				return this.mItemSizeWithPadding;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000C40F3 File Offset: 0x000C24F3
		// (set) Token: 0x0600237D RID: 9085 RVA: 0x000C40FB File Offset: 0x000C24FB
		public RectOffset Padding
		{
			get
			{
				return this.mPadding;
			}
			set
			{
				this.SetPadding(value);
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000C4104 File Offset: 0x000C2504
		public GridViewItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (GridViewItemPrefabConfData gridViewItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (!(gridViewItemPrefabConfData.mItemPrefab == null))
				{
					if (prefabName == gridViewItemPrefabConfData.mItemPrefab.name)
					{
						return gridViewItemPrefabConfData;
					}
				}
			}
			return null;
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000C4190 File Offset: 0x000C2590
		public void InitGridView(int itemTotalCount, Func<LoopGridView, int, int, int, LoopGridViewItem> onGetItemByRowColumn, LoopGridViewSettingParam settingParam = null, LoopGridViewInitParam initParam = null)
		{
			if (this.mListViewInited)
			{
				return;
			}
			this.mListViewInited = true;
			if (itemTotalCount < 0)
			{
				itemTotalCount = 0;
			}
			if (settingParam != null)
			{
				this.UpdateFromSettingParam(settingParam);
			}
			if (initParam != null)
			{
				this.mSmoothDumpRate = initParam.mSmoothDumpRate;
				this.mSnapFinishThreshold = initParam.mSnapFinishThreshold;
				this.mSnapVecThreshold = initParam.mSnapVecThreshold;
			}
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				return;
			}
			this.mCurSnapData.Clear();
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
			this.SetScrollbarListener();
			this.AdjustViewPortPivot();
			this.AdjustContainerAnchorAndPivot();
			this.InitItemPool();
			this.mOnGetItemByRowColumn = onGetItemByRowColumn;
			this.mNeedCheckContentPosLeftCount = 4;
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemTotalCount;
			this.UpdateAllGridSetting();
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000C4300 File Offset: 0x000C2700
		public void SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount < 0)
			{
				return;
			}
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			this.mCurSnapData.Clear();
			this.mItemTotalCount = itemCount;
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			this.ForceToCheckContentPos();
			if (this.mItemTotalCount == 0)
			{
				this.RecycleAllItem();
				this.ClearAllTmpRecycledItem();
				return;
			}
			this.VaildAndSetContainerPos();
			this.UpdateGridViewContent();
			this.ClearAllTmpRecycledItem();
			if (resetPos)
			{
				this.MovePanelToItemByRowColumn(0, 0, 0f, 0f);
				return;
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000C438C File Offset: 0x000C278C
		public LoopGridViewItem NewListViewItem(string itemPrefabName)
		{
			GridItemPool gridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out gridItemPool))
			{
				return null;
			}
			LoopGridViewItem item = gridItemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentGridView = this;
			return item;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000C43F4 File Offset: 0x000C27F4
		public void RefreshItemByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.ItemTotalCount)
			{
				return;
			}
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			RowColumnPair rowColumnByItemIndex = this.GetRowColumnByItemIndex(itemIndex);
			this.RefreshItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000C4444 File Offset: 0x000C2844
		public void RefreshItemByRowColumn(int row, int column)
		{
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				GridItemGroup shownGroup = this.GetShownGroup(row);
				if (shownGroup == null)
				{
					return;
				}
				LoopGridViewItem itemByColumn = shownGroup.GetItemByColumn(column);
				if (itemByColumn == null)
				{
					return;
				}
				LoopGridViewItem newItemByRowColumn = this.GetNewItemByRowColumn(row, column);
				if (newItemByRowColumn == null)
				{
					return;
				}
				Vector3 anchoredPosition3D = itemByColumn.CachedRectTransform.anchoredPosition3D;
				shownGroup.ReplaceItem(itemByColumn, newItemByRowColumn);
				this.RecycleItemTmp(itemByColumn);
				newItemByRowColumn.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				this.ClearAllTmpRecycledItem();
			}
			else
			{
				GridItemGroup shownGroup2 = this.GetShownGroup(column);
				if (shownGroup2 == null)
				{
					return;
				}
				LoopGridViewItem itemByRow = shownGroup2.GetItemByRow(row);
				if (itemByRow == null)
				{
					return;
				}
				LoopGridViewItem newItemByRowColumn2 = this.GetNewItemByRowColumn(row, column);
				if (newItemByRowColumn2 == null)
				{
					return;
				}
				Vector3 anchoredPosition3D2 = itemByRow.CachedRectTransform.anchoredPosition3D;
				shownGroup2.ReplaceItem(itemByRow, newItemByRowColumn2);
				this.RecycleItemTmp(itemByRow);
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = anchoredPosition3D2;
				this.ClearAllTmpRecycledItem();
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000C4553 File Offset: 0x000C2953
		public void ClearSnapData()
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000C4560 File Offset: 0x000C2960
		public void SetSnapTargetItemRowColumn(int row, int column)
		{
			if (row < 0)
			{
				row = 0;
			}
			if (column < 0)
			{
				column = 0;
			}
			this.mCurSnapData.mSnapTarget.mRow = row;
			this.mCurSnapData.mSnapTarget.mColumn = column;
			this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
			this.mCurSnapData.mIsForceSnapTo = true;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000C45BB File Offset: 0x000C29BB
		public RowColumnPair CurSnapNearestItemRowColumn
		{
			get
			{
				return this.mCurSnapNearestItemRowColumn;
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000C45C3 File Offset: 0x000C29C3
		public void ForceSnapUpdateCheck()
		{
			if (this.mLeftSnapUpdateExtraCount <= 0)
			{
				this.mLeftSnapUpdateExtraCount = 1;
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000C45D8 File Offset: 0x000C29D8
		public void ForceToCheckContentPos()
		{
			if (this.mNeedCheckContentPosLeftCount <= 0)
			{
				this.mNeedCheckContentPosLeftCount = 1;
			}
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000C45F0 File Offset: 0x000C29F0
		public void MovePanelToItemByIndex(int itemIndex, float offsetX = 0f, float offsetY = 0f)
		{
			if (this.ItemTotalCount == 0)
			{
				return;
			}
			if (itemIndex >= this.ItemTotalCount)
			{
				itemIndex = this.ItemTotalCount - 1;
			}
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
			RowColumnPair rowColumnByItemIndex = this.GetRowColumnByItemIndex(itemIndex);
			this.MovePanelToItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn, offsetX, offsetY);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000C4648 File Offset: 0x000C2A48
		public void MovePanelToItemByRowColumn(int row, int column, float offsetX = 0f, float offsetY = 0f)
		{
			this.mScrollRect.StopMovement();
			this.mCurSnapData.Clear();
			if (this.mItemTotalCount == 0)
			{
				return;
			}
			Vector2 itemPos = this.GetItemPos(row, column);
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			if (this.mScrollRect.horizontal)
			{
				float num = Mathf.Max(this.ContainerTrans.rect.width - this.ViewPortWidth, 0f);
				if (num > 0f)
				{
					float num2 = -itemPos.x + offsetX;
					num2 = Mathf.Min(Mathf.Abs(num2), num) * Mathf.Sign(num2);
					anchoredPosition3D.x = num2;
				}
			}
			if (this.mScrollRect.vertical)
			{
				float num3 = Mathf.Max(this.ContainerTrans.rect.height - this.ViewPortHeight, 0f);
				if (num3 > 0f)
				{
					float num4 = -itemPos.y + offsetY;
					num4 = Mathf.Min(Mathf.Abs(num4), num3) * Mathf.Sign(num4);
					anchoredPosition3D.y = num4;
				}
			}
			if (anchoredPosition3D != this.mContainerTrans.anchoredPosition3D)
			{
				this.mContainerTrans.anchoredPosition3D = anchoredPosition3D;
			}
			this.VaildAndSetContainerPos();
			this.ForceToCheckContentPos();
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000C4798 File Offset: 0x000C2B98
		public void RefreshAllShownItem()
		{
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.ForceToCheckContentPos();
			this.RecycleAllItem();
			this.UpdateGridViewContent();
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000C47CA File Offset: 0x000C2BCA
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mCurSnapData.Clear();
			this.mIsDraging = true;
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction(eventData);
			}
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000C4801 File Offset: 0x000C2C01
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mIsDraging = false;
			this.ForceSnapUpdateCheck();
			if (this.mOnEndDragAction != null)
			{
				this.mOnEndDragAction(eventData);
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000C4833 File Offset: 0x000C2C33
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.mOnDragingAction != null)
			{
				this.mOnDragingAction(eventData);
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000C4858 File Offset: 0x000C2C58
		public int GetItemIndexByRowColumn(int row, int column)
		{
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				return row * this.mFixedRowOrColumnCount + column;
			}
			return column * this.mFixedRowOrColumnCount + row;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000C487C File Offset: 0x000C2C7C
		public RowColumnPair GetRowColumnByItemIndex(int itemIndex)
		{
			if (itemIndex < 0)
			{
				itemIndex = 0;
			}
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				int row = itemIndex / this.mFixedRowOrColumnCount;
				int column = itemIndex % this.mFixedRowOrColumnCount;
				return new RowColumnPair(row, column);
			}
			int column2 = itemIndex / this.mFixedRowOrColumnCount;
			int row2 = itemIndex % this.mFixedRowOrColumnCount;
			return new RowColumnPair(row2, column2);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000C48D4 File Offset: 0x000C2CD4
		public Vector2 GetItemAbsPos(int row, int column)
		{
			float x = this.mStartPadding.x + (float)column * this.mItemSizeWithPadding.x;
			float y = this.mStartPadding.y + (float)row * this.mItemSizeWithPadding.y;
			return new Vector2(x, y);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000C4920 File Offset: 0x000C2D20
		public Vector2 GetItemPos(int row, int column)
		{
			Vector2 itemAbsPos = this.GetItemAbsPos(row, column);
			float x = itemAbsPos.x;
			float y = itemAbsPos.y;
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				return new Vector2(x, -y);
			}
			if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				return new Vector2(x, y);
			}
			if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				return new Vector2(-x, -y);
			}
			if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				return new Vector2(-x, y);
			}
			return Vector2.zero;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000C49A0 File Offset: 0x000C2DA0
		public LoopGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.ItemTotalCount)
			{
				return null;
			}
			if (this.mItemGroupList.Count == 0)
			{
				return null;
			}
			RowColumnPair rowColumnByItemIndex = this.GetRowColumnByItemIndex(itemIndex);
			return this.GetShownItemByRowColumn(rowColumnByItemIndex.mRow, rowColumnByItemIndex.mColumn);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000C49F0 File Offset: 0x000C2DF0
		public LoopGridViewItem GetShownItemByRowColumn(int row, int column)
		{
			if (this.mItemGroupList.Count == 0)
			{
				return null;
			}
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				GridItemGroup shownGroup = this.GetShownGroup(row);
				if (shownGroup == null)
				{
					return null;
				}
				return shownGroup.GetItemByColumn(column);
			}
			else
			{
				GridItemGroup shownGroup2 = this.GetShownGroup(column);
				if (shownGroup2 == null)
				{
					return null;
				}
				return shownGroup2.GetItemByRow(row);
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000C4A49 File Offset: 0x000C2E49
		public void UpdateAllGridSetting()
		{
			this.UpdateStartEndPadding();
			this.UpdateItemSize();
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000C4A70 File Offset: 0x000C2E70
		public void SetGridFixedGroupCount(GridFixedType fixedType, int count)
		{
			if (this.mGridFixedType == fixedType && this.mFixedRowOrColumnCount == count)
			{
				return;
			}
			this.mGridFixedType = fixedType;
			this.mFixedRowOrColumnCount = count;
			this.UpdateColumnRowCount();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000C4AD4 File Offset: 0x000C2ED4
		public void SetItemSize(Vector2 newSize)
		{
			if (newSize == this.mItemSize)
			{
				return;
			}
			this.mItemSize = newSize;
			this.UpdateItemSize();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000C4B2C File Offset: 0x000C2F2C
		public void SetItemPadding(Vector2 newPadding)
		{
			if (newPadding == this.mItemPadding)
			{
				return;
			}
			this.mItemPadding = newPadding;
			this.UpdateItemSize();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000C4B84 File Offset: 0x000C2F84
		public void SetPadding(RectOffset newPadding)
		{
			if (newPadding == this.mPadding)
			{
				return;
			}
			this.mPadding = newPadding;
			this.UpdateStartEndPadding();
			this.UpdateContentSize();
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			this.RecycleAllItem();
			this.ForceSnapUpdateCheck();
			this.ForceToCheckContentPos();
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000C4BD4 File Offset: 0x000C2FD4
		public void UpdateContentSize()
		{
			float num = this.mStartPadding.x + (float)this.mColumnCount * this.mItemSizeWithPadding.x - this.mItemPadding.x + this.mEndPadding.x;
			float num2 = this.mStartPadding.y + (float)this.mRowCount * this.mItemSizeWithPadding.y - this.mItemPadding.y + this.mEndPadding.y;
			if (this.mContainerTrans.rect.height != num2)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			}
			if (this.mContainerTrans.rect.width != num)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num);
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000C4CA0 File Offset: 0x000C30A0
		public void VaildAndSetContainerPos()
		{
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			this.mContainerTrans.anchoredPosition3D = this.GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000C4CE0 File Offset: 0x000C30E0
		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000C4D1C File Offset: 0x000C311C
		public void RecycleAllItem()
		{
			foreach (GridItemGroup group in this.mItemGroupList)
			{
				this.RecycleItemGroupTmp(group);
			}
			this.mItemGroupList.Clear();
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000C4D84 File Offset: 0x000C3184
		public void UpdateGridViewContent()
		{
			this.mListUpdateCheckFrameCount++;
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemGroupList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return;
			}
			this.UpdateCurFrameItemRangeData();
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				int count = this.mItemGroupList.Count;
				int mMinRow = this.mCurFrameItemRangeData.mMinRow;
				int mMaxRow = this.mCurFrameItemRangeData.mMaxRow;
				for (int i = count - 1; i >= 0; i--)
				{
					GridItemGroup gridItemGroup = this.mItemGroupList[i];
					if (gridItemGroup.GroupIndex < mMinRow || gridItemGroup.GroupIndex > mMaxRow)
					{
						this.RecycleItemGroupTmp(gridItemGroup);
						this.mItemGroupList.RemoveAt(i);
					}
				}
				if (this.mItemGroupList.Count == 0)
				{
					GridItemGroup item = this.CreateItemGroup(mMinRow);
					this.mItemGroupList.Add(item);
				}
				while (this.mItemGroupList[0].GroupIndex > mMinRow)
				{
					GridItemGroup item2 = this.CreateItemGroup(this.mItemGroupList[0].GroupIndex - 1);
					this.mItemGroupList.Insert(0, item2);
				}
				while (this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex < mMaxRow)
				{
					GridItemGroup item3 = this.CreateItemGroup(this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex + 1);
					this.mItemGroupList.Add(item3);
				}
				int count2 = this.mItemGroupList.Count;
				for (int j = 0; j < count2; j++)
				{
					this.UpdateRowItemGroupForRecycleAndNew(this.mItemGroupList[j]);
				}
			}
			else
			{
				int count3 = this.mItemGroupList.Count;
				int mMinColumn = this.mCurFrameItemRangeData.mMinColumn;
				int mMaxColumn = this.mCurFrameItemRangeData.mMaxColumn;
				for (int k = count3 - 1; k >= 0; k--)
				{
					GridItemGroup gridItemGroup2 = this.mItemGroupList[k];
					if (gridItemGroup2.GroupIndex < mMinColumn || gridItemGroup2.GroupIndex > mMaxColumn)
					{
						this.RecycleItemGroupTmp(gridItemGroup2);
						this.mItemGroupList.RemoveAt(k);
					}
				}
				if (this.mItemGroupList.Count == 0)
				{
					GridItemGroup item4 = this.CreateItemGroup(mMinColumn);
					this.mItemGroupList.Add(item4);
				}
				while (this.mItemGroupList[0].GroupIndex > mMinColumn)
				{
					GridItemGroup item5 = this.CreateItemGroup(this.mItemGroupList[0].GroupIndex - 1);
					this.mItemGroupList.Insert(0, item5);
				}
				while (this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex < mMaxColumn)
				{
					GridItemGroup item6 = this.CreateItemGroup(this.mItemGroupList[this.mItemGroupList.Count - 1].GroupIndex + 1);
					this.mItemGroupList.Add(item6);
				}
				int count4 = this.mItemGroupList.Count;
				for (int l = 0; l < count4; l++)
				{
					this.UpdateColumnItemGroupForRecycleAndNew(this.mItemGroupList[l]);
				}
			}
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000C50CC File Offset: 0x000C34CC
		public void UpdateStartEndPadding()
		{
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				this.mStartPadding.x = (float)this.mPadding.left;
				this.mStartPadding.y = (float)this.mPadding.top;
				this.mEndPadding.x = (float)this.mPadding.right;
				this.mEndPadding.y = (float)this.mPadding.bottom;
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				this.mStartPadding.x = (float)this.mPadding.left;
				this.mStartPadding.y = (float)this.mPadding.bottom;
				this.mEndPadding.x = (float)this.mPadding.right;
				this.mEndPadding.y = (float)this.mPadding.top;
			}
			else if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				this.mStartPadding.x = (float)this.mPadding.right;
				this.mStartPadding.y = (float)this.mPadding.top;
				this.mEndPadding.x = (float)this.mPadding.left;
				this.mEndPadding.y = (float)this.mPadding.bottom;
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				this.mStartPadding.x = (float)this.mPadding.right;
				this.mStartPadding.y = (float)this.mPadding.bottom;
				this.mEndPadding.x = (float)this.mPadding.left;
				this.mEndPadding.y = (float)this.mPadding.top;
			}
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000C5288 File Offset: 0x000C3688
		public void UpdateItemSize()
		{
			if (this.mItemSize.x > 0f && this.mItemSize.y > 0f)
			{
				this.mItemSizeWithPadding = this.mItemSize + this.mItemPadding;
				return;
			}
			if (this.mItemPrefabDataList.Count != 0)
			{
				GameObject mItemPrefab = this.mItemPrefabDataList[0].mItemPrefab;
				if (!(mItemPrefab == null))
				{
					RectTransform component = mItemPrefab.GetComponent<RectTransform>();
					if (!(component == null))
					{
						this.mItemSize = component.rect.size;
						this.mItemSizeWithPadding = this.mItemSize + this.mItemPadding;
					}
				}
			}
			if (this.mItemSize.x <= 0f || this.mItemSize.y <= 0f)
			{
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000C537C File Offset: 0x000C377C
		public void UpdateColumnRowCount()
		{
			if (this.mGridFixedType == GridFixedType.ColumnCountFixed)
			{
				this.mColumnCount = this.mFixedRowOrColumnCount;
				this.mRowCount = this.mItemTotalCount / this.mColumnCount;
				if (this.mItemTotalCount % this.mColumnCount > 0)
				{
					this.mRowCount++;
				}
				if (this.mItemTotalCount <= this.mColumnCount)
				{
					this.mColumnCount = this.mItemTotalCount;
				}
			}
			else
			{
				this.mRowCount = this.mFixedRowOrColumnCount;
				this.mColumnCount = this.mItemTotalCount / this.mRowCount;
				if (this.mItemTotalCount % this.mRowCount > 0)
				{
					this.mColumnCount++;
				}
				if (this.mItemTotalCount <= this.mRowCount)
				{
					this.mRowCount = this.mItemTotalCount;
				}
			}
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x000C5454 File Offset: 0x000C3854
		private bool IsContainerTransCanMove()
		{
			return this.mItemTotalCount != 0 && ((this.mScrollRect.horizontal && this.ContainerTrans.rect.width > this.ViewPortWidth) || (this.mScrollRect.vertical && this.ContainerTrans.rect.height > this.ViewPortHeight));
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000C54D0 File Offset: 0x000C38D0
		private void RecycleItemGroupTmp(GridItemGroup group)
		{
			if (group == null)
			{
				return;
			}
			while (group.First != null)
			{
				LoopGridViewItem item = group.RemoveFirst();
				this.RecycleItemTmp(item);
			}
			group.Clear();
			this.RecycleOneItemGroupObj(group);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000C5518 File Offset: 0x000C3918
		private void RecycleItemTmp(LoopGridViewItem item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			GridItemPool gridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out gridItemPool))
			{
				return;
			}
			gridItemPool.RecycleItem(item);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000C5568 File Offset: 0x000C3968
		private void AdjustViewPortPivot()
		{
			RectTransform rectTransform = this.mViewPortRectTransform;
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				rectTransform.pivot = new Vector2(0f, 1f);
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				rectTransform.pivot = new Vector2(0f, 0f);
			}
			else if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				rectTransform.pivot = new Vector2(1f, 1f);
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				rectTransform.pivot = new Vector2(1f, 0f);
			}
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000C5610 File Offset: 0x000C3A10
		private void AdjustContainerAnchorAndPivot()
		{
			RectTransform containerTrans = this.ContainerTrans;
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				containerTrans.anchorMin = new Vector2(0f, 1f);
				containerTrans.anchorMax = new Vector2(0f, 1f);
				containerTrans.pivot = new Vector2(0f, 1f);
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				containerTrans.anchorMin = new Vector2(0f, 0f);
				containerTrans.anchorMax = new Vector2(0f, 0f);
				containerTrans.pivot = new Vector2(0f, 0f);
			}
			else if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				containerTrans.anchorMin = new Vector2(1f, 1f);
				containerTrans.anchorMax = new Vector2(1f, 1f);
				containerTrans.pivot = new Vector2(1f, 1f);
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				containerTrans.anchorMin = new Vector2(1f, 0f);
				containerTrans.anchorMax = new Vector2(1f, 0f);
				containerTrans.pivot = new Vector2(1f, 0f);
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000C5760 File Offset: 0x000C3B60
		private void AdjustItemAnchorAndPivot(RectTransform rtf)
		{
			if (this.ArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				rtf.anchorMin = new Vector2(0f, 1f);
				rtf.anchorMax = new Vector2(0f, 1f);
				rtf.pivot = new Vector2(0f, 1f);
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				rtf.anchorMin = new Vector2(0f, 0f);
				rtf.anchorMax = new Vector2(0f, 0f);
				rtf.pivot = new Vector2(0f, 0f);
			}
			else if (this.ArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				rtf.anchorMin = new Vector2(1f, 1f);
				rtf.anchorMax = new Vector2(1f, 1f);
				rtf.pivot = new Vector2(1f, 1f);
			}
			else if (this.ArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				rtf.anchorMin = new Vector2(1f, 0f);
				rtf.anchorMax = new Vector2(1f, 0f);
				rtf.pivot = new Vector2(1f, 0f);
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000C58A8 File Offset: 0x000C3CA8
		private void InitItemPool()
		{
			foreach (GridViewItemPrefabConfData gridViewItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (!(gridViewItemPrefabConfData.mItemPrefab == null))
				{
					string name = gridViewItemPrefabConfData.mItemPrefab.name;
					if (!this.mItemPoolDict.ContainsKey(name))
					{
						RectTransform component = gridViewItemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (!(component == null))
						{
							this.AdjustItemAnchorAndPivot(component);
							LoopGridViewItem component2 = gridViewItemPrefabConfData.mItemPrefab.GetComponent<LoopGridViewItem>();
							if (component2 == null)
							{
								gridViewItemPrefabConfData.mItemPrefab.AddComponent<LoopGridViewItem>();
							}
							GridItemPool gridItemPool = new GridItemPool();
							gridItemPool.Init(gridViewItemPrefabConfData.mItemPrefab, gridViewItemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, gridItemPool);
							this.mItemPoolList.Add(gridItemPool);
						}
					}
				}
			}
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000C59BC File Offset: 0x000C3DBC
		private LoopGridViewItem GetNewItemByRowColumn(int row, int column)
		{
			int itemIndexByRowColumn = this.GetItemIndexByRowColumn(row, column);
			if (itemIndexByRowColumn < 0 || itemIndexByRowColumn >= this.ItemTotalCount)
			{
				return null;
			}
			LoopGridViewItem loopGridViewItem = this.mOnGetItemByRowColumn(this, itemIndexByRowColumn, row, column);
			if (loopGridViewItem == null)
			{
				return null;
			}
			loopGridViewItem.NextItem = null;
			loopGridViewItem.PrevItem = null;
			loopGridViewItem.Row = row;
			loopGridViewItem.Column = column;
			loopGridViewItem.ItemIndex = itemIndexByRowColumn;
			loopGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
			return loopGridViewItem;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000C5A38 File Offset: 0x000C3E38
		private RowColumnPair GetCeilItemRowColumnAtGivenAbsPos(float ax, float ay)
		{
			ax = Mathf.Abs(ax);
			ay = Mathf.Abs(ay);
			int num = Mathf.CeilToInt((ay - this.mStartPadding.y) / this.mItemSizeWithPadding.y) - 1;
			int num2 = Mathf.CeilToInt((ax - this.mStartPadding.x) / this.mItemSizeWithPadding.x) - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num >= this.mRowCount)
			{
				num = this.mRowCount - 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 >= this.mColumnCount)
			{
				num2 = this.mColumnCount - 1;
			}
			return new RowColumnPair(num, num2);
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000C5ADA File Offset: 0x000C3EDA
		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			this.UpdateSnapMove(false, false);
			this.UpdateGridViewContent();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000C5AFC File Offset: 0x000C3EFC
		private GridItemGroup CreateItemGroup(int groupIndex)
		{
			GridItemGroup oneItemGroupObj = this.GetOneItemGroupObj();
			oneItemGroupObj.GroupIndex = groupIndex;
			return oneItemGroupObj;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000C5B18 File Offset: 0x000C3F18
		private Vector2 GetContainerMovedDistance()
		{
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			return new Vector2(Mathf.Abs(containerVaildPos.x), Mathf.Abs(containerVaildPos.y));
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000C5B70 File Offset: 0x000C3F70
		private Vector2 GetContainerVaildPos(float curX, float curY)
		{
			float num = Mathf.Max(this.ContainerTrans.rect.width - this.ViewPortWidth, 0f);
			float num2 = Mathf.Max(this.ContainerTrans.rect.height - this.ViewPortHeight, 0f);
			if (this.mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				curX = Mathf.Clamp(curX, -num, 0f);
				curY = Mathf.Clamp(curY, 0f, num2);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				curX = Mathf.Clamp(curX, -num, 0f);
				curY = Mathf.Clamp(curY, -num2, 0f);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				curX = Mathf.Clamp(curX, 0f, num);
				curY = Mathf.Clamp(curY, -num2, 0f);
			}
			else if (this.mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				curX = Mathf.Clamp(curX, 0f, num);
				curY = Mathf.Clamp(curY, 0f, num2);
			}
			return new Vector2(curX, curY);
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x000C5C80 File Offset: 0x000C4080
		private void UpdateCurFrameItemRangeData()
		{
			Vector2 containerMovedDistance = this.GetContainerMovedDistance();
			if (this.mNeedCheckContentPosLeftCount <= 0 && this.mCurFrameItemRangeData.mCheckedPosition == containerMovedDistance)
			{
				return;
			}
			if (this.mNeedCheckContentPosLeftCount > 0)
			{
				this.mNeedCheckContentPosLeftCount--;
			}
			float num = containerMovedDistance.x - this.mItemRecycleDistance.x;
			float num2 = containerMovedDistance.y - this.mItemRecycleDistance.y;
			if (num < 0f)
			{
				num = 0f;
			}
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			RowColumnPair ceilItemRowColumnAtGivenAbsPos = this.GetCeilItemRowColumnAtGivenAbsPos(num, num2);
			this.mCurFrameItemRangeData.mMinColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			this.mCurFrameItemRangeData.mMinRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			num = containerMovedDistance.x + this.mItemRecycleDistance.x + this.ViewPortWidth;
			num2 = containerMovedDistance.y + this.mItemRecycleDistance.y + this.ViewPortHeight;
			ceilItemRowColumnAtGivenAbsPos = this.GetCeilItemRowColumnAtGivenAbsPos(num, num2);
			this.mCurFrameItemRangeData.mMaxColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			this.mCurFrameItemRangeData.mMaxRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			this.mCurFrameItemRangeData.mCheckedPosition = containerMovedDistance;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000C5DB8 File Offset: 0x000C41B8
		private void UpdateRowItemGroupForRecycleAndNew(GridItemGroup group)
		{
			int mMinColumn = this.mCurFrameItemRangeData.mMinColumn;
			int mMaxColumn = this.mCurFrameItemRangeData.mMaxColumn;
			int groupIndex = group.GroupIndex;
			while (group.First != null && group.First.Column < mMinColumn)
			{
				this.RecycleItemTmp(group.RemoveFirst());
			}
			while (group.Last != null && group.Last.Column > mMaxColumn)
			{
				this.RecycleItemTmp(group.RemoveLast());
			}
			if (group.First == null)
			{
				LoopGridViewItem newItemByRowColumn = this.GetNewItemByRowColumn(groupIndex, mMinColumn);
				if (newItemByRowColumn == null)
				{
					return;
				}
				newItemByRowColumn.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn.Row, newItemByRowColumn.Column);
				group.AddFirst(newItemByRowColumn);
			}
			while (group.First.Column > mMinColumn)
			{
				LoopGridViewItem newItemByRowColumn2 = this.GetNewItemByRowColumn(groupIndex, group.First.Column - 1);
				if (newItemByRowColumn2 == null)
				{
					return;
				}
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn2.Row, newItemByRowColumn2.Column);
				group.AddFirst(newItemByRowColumn2);
			}
			while (group.Last.Column < mMaxColumn)
			{
				LoopGridViewItem newItemByRowColumn3 = this.GetNewItemByRowColumn(groupIndex, group.Last.Column + 1);
				if (newItemByRowColumn3 == null)
				{
					return;
				}
				newItemByRowColumn3.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn3.Row, newItemByRowColumn3.Column);
				group.AddLast(newItemByRowColumn3);
			}
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000C5F68 File Offset: 0x000C4368
		private void UpdateColumnItemGroupForRecycleAndNew(GridItemGroup group)
		{
			int mMinRow = this.mCurFrameItemRangeData.mMinRow;
			int mMaxRow = this.mCurFrameItemRangeData.mMaxRow;
			int groupIndex = group.GroupIndex;
			while (group.First != null && group.First.Row < mMinRow)
			{
				this.RecycleItemTmp(group.RemoveFirst());
			}
			while (group.Last != null && group.Last.Row > mMaxRow)
			{
				this.RecycleItemTmp(group.RemoveLast());
			}
			if (group.First == null)
			{
				LoopGridViewItem newItemByRowColumn = this.GetNewItemByRowColumn(mMinRow, groupIndex);
				if (newItemByRowColumn == null)
				{
					return;
				}
				newItemByRowColumn.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn.Row, newItemByRowColumn.Column);
				group.AddFirst(newItemByRowColumn);
			}
			while (group.First.Row > mMinRow)
			{
				LoopGridViewItem newItemByRowColumn2 = this.GetNewItemByRowColumn(group.First.Row - 1, groupIndex);
				if (newItemByRowColumn2 == null)
				{
					return;
				}
				newItemByRowColumn2.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn2.Row, newItemByRowColumn2.Column);
				group.AddFirst(newItemByRowColumn2);
			}
			while (group.Last.Row < mMaxRow)
			{
				LoopGridViewItem newItemByRowColumn3 = this.GetNewItemByRowColumn(group.Last.Row + 1, groupIndex);
				if (newItemByRowColumn3 == null)
				{
					return;
				}
				newItemByRowColumn3.CachedRectTransform.anchoredPosition3D = this.GetItemPos(newItemByRowColumn3.Row, newItemByRowColumn3.Column);
				group.AddLast(newItemByRowColumn3);
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000C6118 File Offset: 0x000C4518
		private void SetScrollbarListener()
		{
			if (!this.ItemSnapEnable)
			{
				return;
			}
			this.mScrollBarClickEventListener1 = null;
			this.mScrollBarClickEventListener2 = null;
			Scrollbar scrollbar = null;
			Scrollbar scrollbar2 = null;
			if (this.mScrollRect.vertical && this.mScrollRect.verticalScrollbar != null)
			{
				scrollbar = this.mScrollRect.verticalScrollbar;
			}
			if (this.mScrollRect.horizontal && this.mScrollRect.horizontalScrollbar != null)
			{
				scrollbar2 = this.mScrollRect.horizontalScrollbar;
			}
			if (scrollbar != null)
			{
				ClickEventListener clickEventListener = ClickEventListener.Get(scrollbar.gameObject);
				this.mScrollBarClickEventListener1 = clickEventListener;
				clickEventListener.SetPointerUpHandler(new Action<GameObject>(this.OnPointerUpInScrollBar));
				clickEventListener.SetPointerDownHandler(new Action<GameObject>(this.OnPointerDownInScrollBar));
			}
			if (scrollbar2 != null)
			{
				ClickEventListener clickEventListener2 = ClickEventListener.Get(scrollbar2.gameObject);
				this.mScrollBarClickEventListener2 = clickEventListener2;
				clickEventListener2.SetPointerUpHandler(new Action<GameObject>(this.OnPointerUpInScrollBar));
				clickEventListener2.SetPointerDownHandler(new Action<GameObject>(this.OnPointerDownInScrollBar));
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000C622D File Offset: 0x000C462D
		private void OnPointerDownInScrollBar(GameObject obj)
		{
			this.mCurSnapData.Clear();
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000C623A File Offset: 0x000C463A
		private void OnPointerUpInScrollBar(GameObject obj)
		{
			this.ForceSnapUpdateCheck();
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000C6244 File Offset: 0x000C4644
		private RowColumnPair FindNearestItemWithLocalPos(float x, float y)
		{
			Vector2 b = new Vector2(x, y);
			RowColumnPair ceilItemRowColumnAtGivenAbsPos = this.GetCeilItemRowColumnAtGivenAbsPos(b.x, b.y);
			int mRow = ceilItemRowColumnAtGivenAbsPos.mRow;
			int mColumn = ceilItemRowColumnAtGivenAbsPos.mColumn;
			RowColumnPair result = new RowColumnPair(-1, -1);
			Vector2 a = Vector2.zero;
			float num = float.MaxValue;
			for (int i = mRow - 1; i <= mRow + 1; i++)
			{
				for (int j = mColumn - 1; j <= mColumn + 1; j++)
				{
					if (i >= 0 && i < this.mRowCount && j >= 0 && j < this.mColumnCount)
					{
						a = this.GetItemSnapPivotLocalPos(i, j);
						float sqrMagnitude = (a - b).sqrMagnitude;
						if (sqrMagnitude < num)
						{
							num = sqrMagnitude;
							result.mRow = i;
							result.mColumn = j;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000C633C File Offset: 0x000C473C
		private Vector2 GetItemSnapPivotLocalPos(int row, int column)
		{
			Vector2 itemAbsPos = this.GetItemAbsPos(row, column);
			if (this.mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				float x = itemAbsPos.x + this.mItemSize.x * this.mItemSnapPivot.x;
				float y = -itemAbsPos.y - this.mItemSize.y * (1f - this.mItemSnapPivot.y);
				return new Vector2(x, y);
			}
			if (this.mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				float x2 = itemAbsPos.x + this.mItemSize.x * this.mItemSnapPivot.x;
				float y2 = itemAbsPos.y + this.mItemSize.y * this.mItemSnapPivot.y;
				return new Vector2(x2, y2);
			}
			if (this.mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				float x3 = -itemAbsPos.x - this.mItemSize.x * (1f - this.mItemSnapPivot.x);
				float y3 = -itemAbsPos.y - this.mItemSize.y * (1f - this.mItemSnapPivot.y);
				return new Vector2(x3, y3);
			}
			if (this.mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				float x4 = -itemAbsPos.x - this.mItemSize.x * (1f - this.mItemSnapPivot.x);
				float y4 = itemAbsPos.y + this.mItemSize.y * this.mItemSnapPivot.y;
				return new Vector2(x4, y4);
			}
			return Vector2.zero;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000C64CC File Offset: 0x000C48CC
		private Vector2 GetViewPortSnapPivotLocalPos(Vector2 pos)
		{
			float x = 0f;
			float y = 0f;
			if (this.mArrangeType == GridItemArrangeType.TopLeftToBottomRight)
			{
				x = -pos.x + this.ViewPortWidth * this.mViewPortSnapPivot.x;
				y = -pos.y - this.ViewPortHeight * (1f - this.mViewPortSnapPivot.y);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomLeftToTopRight)
			{
				x = -pos.x + this.ViewPortWidth * this.mViewPortSnapPivot.x;
				y = -pos.y + this.ViewPortHeight * this.mViewPortSnapPivot.y;
			}
			else if (this.mArrangeType == GridItemArrangeType.TopRightToBottomLeft)
			{
				x = -pos.x - this.ViewPortWidth * (1f - this.mViewPortSnapPivot.x);
				y = -pos.y - this.ViewPortHeight * (1f - this.mViewPortSnapPivot.y);
			}
			else if (this.mArrangeType == GridItemArrangeType.BottomRightToTopLeft)
			{
				x = -pos.x - this.ViewPortWidth * (1f - this.mViewPortSnapPivot.x);
				y = -pos.y + this.ViewPortHeight * this.mViewPortSnapPivot.y;
			}
			return new Vector2(x, y);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000C6624 File Offset: 0x000C4A24
		private void UpdateNearestSnapItem(bool forceSendEvent)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			if (this.mItemGroupList.Count == 0)
			{
				return;
			}
			if (!this.IsContainerTransCanMove())
			{
				return;
			}
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			bool flag = containerVaildPos.y != this.mLastSnapCheckPos.y || containerVaildPos.x != this.mLastSnapCheckPos.x;
			this.mLastSnapCheckPos = containerVaildPos;
			if (!flag && this.mLeftSnapUpdateExtraCount > 0)
			{
				this.mLeftSnapUpdateExtraCount--;
				flag = true;
			}
			if (flag)
			{
				RowColumnPair rowColumnPair = new RowColumnPair(-1, -1);
				Vector2 viewPortSnapPivotLocalPos = this.GetViewPortSnapPivotLocalPos(containerVaildPos);
				rowColumnPair = this.FindNearestItemWithLocalPos(viewPortSnapPivotLocalPos.x, viewPortSnapPivotLocalPos.y);
				if (rowColumnPair.mRow >= 0)
				{
					RowColumnPair a = this.mCurSnapNearestItemRowColumn;
					this.mCurSnapNearestItemRowColumn = rowColumnPair;
					if ((forceSendEvent || a != this.mCurSnapNearestItemRowColumn) && this.mOnSnapNearestChanged != null)
					{
						this.mOnSnapNearestChanged(this);
					}
				}
				else
				{
					this.mCurSnapNearestItemRowColumn.mRow = -1;
					this.mCurSnapNearestItemRowColumn.mColumn = -1;
				}
			}
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000C6784 File Offset: 0x000C4B84
		private void UpdateFromSettingParam(LoopGridViewSettingParam param)
		{
			if (param == null)
			{
				return;
			}
			if (param.mItemSize != null)
			{
				this.mItemSize = (Vector2)param.mItemSize;
			}
			if (param.mItemPadding != null)
			{
				this.mItemPadding = (Vector2)param.mItemPadding;
			}
			if (param.mPadding != null)
			{
				this.mPadding = (RectOffset)param.mPadding;
			}
			if (param.mGridFixedType != null)
			{
				this.mGridFixedType = (GridFixedType)param.mGridFixedType;
			}
			if (param.mFixedRowOrColumnCount != null)
			{
				this.mFixedRowOrColumnCount = (int)param.mFixedRowOrColumnCount;
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000C6824 File Offset: 0x000C4C24
		public void FinishSnapImmediately()
		{
			this.UpdateSnapMove(true, false);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000C6830 File Offset: 0x000C4C30
		private void UpdateSnapMove(bool immediate = false, bool forceSendEvent = false)
		{
			if (!this.mItemSnapEnable)
			{
				return;
			}
			this.UpdateNearestSnapItem(false);
			Vector2 a = this.mContainerTrans.anchoredPosition3D;
			if (!this.CanSnap())
			{
				this.ClearSnapData();
				return;
			}
			this.UpdateCurSnapData();
			if (this.mCurSnapData.mSnapStatus != SnapStatus.SnapMoving)
			{
				return;
			}
			float num = Mathf.Abs(this.mScrollRect.velocity.x) + Mathf.Abs(this.mScrollRect.velocity.y);
			if (num > 0f)
			{
				this.mScrollRect.StopMovement();
			}
			float mCurSnapVal = this.mCurSnapData.mCurSnapVal;
			this.mCurSnapData.mCurSnapVal = Mathf.SmoothDamp(this.mCurSnapData.mCurSnapVal, this.mCurSnapData.mTargetSnapVal, ref this.mSmoothDumpVel, this.mSmoothDumpRate);
			float d = this.mCurSnapData.mCurSnapVal - mCurSnapVal;
			if (immediate || Mathf.Abs(this.mCurSnapData.mTargetSnapVal - this.mCurSnapData.mCurSnapVal) < this.mSnapFinishThreshold)
			{
				a += (this.mCurSnapData.mTargetSnapVal - mCurSnapVal) * this.mCurSnapData.mSnapNeedMoveDir;
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoveFinish;
				if (this.mOnSnapItemFinished != null)
				{
					LoopGridViewItem shownItemByRowColumn = this.GetShownItemByRowColumn(this.mCurSnapNearestItemRowColumn.mRow, this.mCurSnapNearestItemRowColumn.mColumn);
					if (shownItemByRowColumn != null)
					{
						this.mOnSnapItemFinished(this, shownItemByRowColumn);
					}
				}
			}
			else
			{
				a += d * this.mCurSnapData.mSnapNeedMoveDir;
			}
			this.mContainerTrans.anchoredPosition3D = this.GetContainerVaildPos(a.x, a.y);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000C6A08 File Offset: 0x000C4E08
		private GridItemGroup GetShownGroup(int groupIndex)
		{
			if (groupIndex < 0)
			{
				return null;
			}
			int count = this.mItemGroupList.Count;
			if (count == 0)
			{
				return null;
			}
			if (groupIndex < this.mItemGroupList[0].GroupIndex || groupIndex > this.mItemGroupList[count - 1].GroupIndex)
			{
				return null;
			}
			int index = groupIndex - this.mItemGroupList[0].GroupIndex;
			return this.mItemGroupList[index];
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000C6A84 File Offset: 0x000C4E84
		private void FillCurSnapData(int row, int column)
		{
			Vector2 itemSnapPivotLocalPos = this.GetItemSnapPivotLocalPos(row, column);
			Vector2 containerVaildPos = this.GetContainerVaildPos(this.ContainerTrans.anchoredPosition3D.x, this.ContainerTrans.anchoredPosition3D.y);
			Vector2 viewPortSnapPivotLocalPos = this.GetViewPortSnapPivotLocalPos(containerVaildPos);
			Vector2 vector = viewPortSnapPivotLocalPos - itemSnapPivotLocalPos;
			if (!this.mScrollRect.horizontal)
			{
				vector.x = 0f;
			}
			if (!this.mScrollRect.vertical)
			{
				vector.y = 0f;
			}
			this.mCurSnapData.mTargetSnapVal = vector.magnitude;
			this.mCurSnapData.mCurSnapVal = 0f;
			this.mCurSnapData.mSnapNeedMoveDir = vector.normalized;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000C6B48 File Offset: 0x000C4F48
		private void UpdateCurSnapData()
		{
			if (this.mItemGroupList.Count == 0)
			{
				this.mCurSnapData.Clear();
				return;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoveFinish)
			{
				if (this.mCurSnapData.mSnapTarget == this.mCurSnapNearestItemRowColumn)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.SnapMoving)
			{
				if (this.mCurSnapData.mSnapTarget == this.mCurSnapNearestItemRowColumn || this.mCurSnapData.mIsForceSnapTo)
				{
					return;
				}
				this.mCurSnapData.mSnapStatus = SnapStatus.NoTargetSet;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.NoTargetSet)
			{
				LoopGridViewItem shownItemByRowColumn = this.GetShownItemByRowColumn(this.mCurSnapNearestItemRowColumn.mRow, this.mCurSnapNearestItemRowColumn.mColumn);
				if (shownItemByRowColumn == null)
				{
					return;
				}
				this.mCurSnapData.mSnapTarget = this.mCurSnapNearestItemRowColumn;
				this.mCurSnapData.mSnapStatus = SnapStatus.TargetHasSet;
				this.mCurSnapData.mIsForceSnapTo = false;
			}
			if (this.mCurSnapData.mSnapStatus == SnapStatus.TargetHasSet)
			{
				LoopGridViewItem shownItemByRowColumn2 = this.GetShownItemByRowColumn(this.mCurSnapData.mSnapTarget.mRow, this.mCurSnapData.mSnapTarget.mColumn);
				if (shownItemByRowColumn2 == null)
				{
					this.mCurSnapData.Clear();
					return;
				}
				this.FillCurSnapData(shownItemByRowColumn2.Row, shownItemByRowColumn2.Column);
				this.mCurSnapData.mSnapStatus = SnapStatus.SnapMoving;
			}
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000C6CC8 File Offset: 0x000C50C8
		private bool CanSnap()
		{
			if (this.mIsDraging)
			{
				return false;
			}
			if (this.mScrollBarClickEventListener1 != null && this.mScrollBarClickEventListener1.IsPressd)
			{
				return false;
			}
			if (this.mScrollBarClickEventListener2 != null && this.mScrollBarClickEventListener2.IsPressd)
			{
				return false;
			}
			if (!this.IsContainerTransCanMove())
			{
				return false;
			}
			if (Mathf.Abs(this.mScrollRect.velocity.x) > this.mSnapVecThreshold)
			{
				return false;
			}
			if (Mathf.Abs(this.mScrollRect.velocity.y) > this.mSnapVecThreshold)
			{
				return false;
			}
			Vector3 anchoredPosition3D = this.mContainerTrans.anchoredPosition3D;
			Vector2 containerVaildPos = this.GetContainerVaildPos(anchoredPosition3D.x, anchoredPosition3D.y);
			return Mathf.Abs(anchoredPosition3D.x - containerVaildPos.x) <= 3f && Mathf.Abs(anchoredPosition3D.y - containerVaildPos.y) <= 3f;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000C6DE4 File Offset: 0x000C51E4
		private GridItemGroup GetOneItemGroupObj()
		{
			int count = this.mItemGroupObjPool.Count;
			if (count == 0)
			{
				return new GridItemGroup();
			}
			GridItemGroup result = this.mItemGroupObjPool[count - 1];
			this.mItemGroupObjPool.RemoveAt(count - 1);
			return result;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000C6E27 File Offset: 0x000C5227
		private void RecycleOneItemGroupObj(GridItemGroup obj)
		{
			this.mItemGroupObjPool.Add(obj);
		}

		// Token: 0x04002302 RID: 8962
		private Dictionary<string, GridItemPool> mItemPoolDict = new Dictionary<string, GridItemPool>();

		// Token: 0x04002303 RID: 8963
		private List<GridItemPool> mItemPoolList = new List<GridItemPool>();

		// Token: 0x04002304 RID: 8964
		[SerializeField]
		private List<GridViewItemPrefabConfData> mItemPrefabDataList = new List<GridViewItemPrefabConfData>();

		// Token: 0x04002305 RID: 8965
		[SerializeField]
		private GridItemArrangeType mArrangeType;

		// Token: 0x04002306 RID: 8966
		private RectTransform mContainerTrans;

		// Token: 0x04002307 RID: 8967
		private ScrollRect mScrollRect;

		// Token: 0x04002308 RID: 8968
		private RectTransform mScrollRectTransform;

		// Token: 0x04002309 RID: 8969
		private RectTransform mViewPortRectTransform;

		// Token: 0x0400230A RID: 8970
		private int mItemTotalCount;

		// Token: 0x0400230B RID: 8971
		[SerializeField]
		private int mFixedRowOrColumnCount;

		// Token: 0x0400230C RID: 8972
		[SerializeField]
		private RectOffset mPadding = new RectOffset();

		// Token: 0x0400230D RID: 8973
		[SerializeField]
		private Vector2 mItemPadding = Vector2.zero;

		// Token: 0x0400230E RID: 8974
		[SerializeField]
		private Vector2 mItemSize = Vector2.zero;

		// Token: 0x0400230F RID: 8975
		[SerializeField]
		private Vector2 mItemRecycleDistance = new Vector2(50f, 50f);

		// Token: 0x04002310 RID: 8976
		private Vector2 mItemSizeWithPadding = Vector2.zero;

		// Token: 0x04002311 RID: 8977
		private Vector2 mStartPadding;

		// Token: 0x04002312 RID: 8978
		private Vector2 mEndPadding;

		// Token: 0x04002313 RID: 8979
		private Func<LoopGridView, int, int, int, LoopGridViewItem> mOnGetItemByRowColumn;

		// Token: 0x04002314 RID: 8980
		private List<GridItemGroup> mItemGroupObjPool = new List<GridItemGroup>();

		// Token: 0x04002315 RID: 8981
		private List<GridItemGroup> mItemGroupList = new List<GridItemGroup>();

		// Token: 0x04002316 RID: 8982
		private bool mIsDraging;

		// Token: 0x04002317 RID: 8983
		private int mRowCount;

		// Token: 0x04002318 RID: 8984
		private int mColumnCount;

		// Token: 0x04002319 RID: 8985
		public Action<PointerEventData> mOnBeginDragAction;

		// Token: 0x0400231A RID: 8986
		public Action<PointerEventData> mOnDragingAction;

		// Token: 0x0400231B RID: 8987
		public Action<PointerEventData> mOnEndDragAction;

		// Token: 0x0400231C RID: 8988
		private float mSmoothDumpVel;

		// Token: 0x0400231D RID: 8989
		private float mSmoothDumpRate = 0.3f;

		// Token: 0x0400231E RID: 8990
		private float mSnapFinishThreshold = 0.1f;

		// Token: 0x0400231F RID: 8991
		private float mSnapVecThreshold = 145f;

		// Token: 0x04002320 RID: 8992
		[SerializeField]
		private bool mItemSnapEnable;

		// Token: 0x04002321 RID: 8993
		[SerializeField]
		private GridFixedType mGridFixedType;

		// Token: 0x04002322 RID: 8994
		public Action<LoopGridView, LoopGridViewItem> mOnSnapItemFinished;

		// Token: 0x04002323 RID: 8995
		public Action<LoopGridView> mOnSnapNearestChanged;

		// Token: 0x04002324 RID: 8996
		private int mLeftSnapUpdateExtraCount = 1;

		// Token: 0x04002325 RID: 8997
		[SerializeField]
		private Vector2 mViewPortSnapPivot = Vector2.zero;

		// Token: 0x04002326 RID: 8998
		[SerializeField]
		private Vector2 mItemSnapPivot = Vector2.zero;

		// Token: 0x04002327 RID: 8999
		private LoopGridView.SnapData mCurSnapData = new LoopGridView.SnapData();

		// Token: 0x04002328 RID: 9000
		private Vector3 mLastSnapCheckPos = Vector3.zero;

		// Token: 0x04002329 RID: 9001
		private bool mListViewInited;

		// Token: 0x0400232A RID: 9002
		private int mListUpdateCheckFrameCount;

		// Token: 0x0400232B RID: 9003
		private LoopGridView.ItemRangeData mCurFrameItemRangeData = new LoopGridView.ItemRangeData();

		// Token: 0x0400232C RID: 9004
		private int mNeedCheckContentPosLeftCount = 1;

		// Token: 0x0400232D RID: 9005
		private ClickEventListener mScrollBarClickEventListener1;

		// Token: 0x0400232E RID: 9006
		private ClickEventListener mScrollBarClickEventListener2;

		// Token: 0x0400232F RID: 9007
		private RowColumnPair mCurSnapNearestItemRowColumn;

		// Token: 0x020005FD RID: 1533
		private class SnapData
		{
			// Token: 0x060023C3 RID: 9155 RVA: 0x000C6E3D File Offset: 0x000C523D
			public void Clear()
			{
				this.mSnapStatus = SnapStatus.NoTargetSet;
				this.mIsForceSnapTo = false;
			}

			// Token: 0x04002330 RID: 9008
			public SnapStatus mSnapStatus;

			// Token: 0x04002331 RID: 9009
			public RowColumnPair mSnapTarget;

			// Token: 0x04002332 RID: 9010
			public Vector2 mSnapNeedMoveDir;

			// Token: 0x04002333 RID: 9011
			public float mTargetSnapVal;

			// Token: 0x04002334 RID: 9012
			public float mCurSnapVal;

			// Token: 0x04002335 RID: 9013
			public bool mIsForceSnapTo;
		}

		// Token: 0x020005FE RID: 1534
		private class ItemRangeData
		{
			// Token: 0x04002336 RID: 9014
			public int mMaxRow;

			// Token: 0x04002337 RID: 9015
			public int mMinRow;

			// Token: 0x04002338 RID: 9016
			public int mMaxColumn;

			// Token: 0x04002339 RID: 9017
			public int mMinColumn;

			// Token: 0x0400233A RID: 9018
			public Vector2 mCheckedPosition;
		}
	}
}
