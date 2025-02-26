using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x0200060A RID: 1546
	public class LoopStaggeredGridView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000CCCF4 File Offset: 0x000CB0F4
		// (set) Token: 0x0600246E RID: 9326 RVA: 0x000CCCFC File Offset: 0x000CB0FC
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

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000CCD05 File Offset: 0x000CB105
		public List<StaggeredGridItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000CCD0D File Offset: 0x000CB10D
		public int ListUpdateCheckFrameCount
		{
			get
			{
				return this.mListUpdateCheckFrameCount;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x000CCD15 File Offset: 0x000CB115
		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000CCD1D File Offset: 0x000CB11D
		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000CCD25 File Offset: 0x000CB125
		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000CCD2D File Offset: 0x000CB12D
		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000CCD35 File Offset: 0x000CB135
		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x000CCD3D File Offset: 0x000CB13D
		public GridViewLayoutParam LayoutParam
		{
			get
			{
				return this.mLayoutParam;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x000CCD45 File Offset: 0x000CB145
		public bool IsInited
		{
			get
			{
				return this.mListViewInited;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000CCD50 File Offset: 0x000CB150
		public StaggeredGridItemGroup GetItemGroupByIndex(int index)
		{
			int count = this.mItemGroupList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemGroupList[index];
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000CCD88 File Offset: 0x000CB188
		public StaggeredGridItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (StaggeredGridItemPrefabConfData staggeredGridItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (!(staggeredGridItemPrefabConfData.mItemPrefab == null))
				{
					if (prefabName == staggeredGridItemPrefabConfData.mItemPrefab.name)
					{
						return staggeredGridItemPrefabConfData;
					}
				}
			}
			return null;
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000CCE14 File Offset: 0x000CB214
		public void InitListView(int itemTotalCount, GridViewLayoutParam layoutParam, Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> onGetItemByItemIndex, StaggeredGridViewInitParam initParam = null)
		{
			this.mLayoutParam = layoutParam;
			if (this.mLayoutParam == null)
			{
				return;
			}
			if (!this.mLayoutParam.CheckParam())
			{
				return;
			}
			if (initParam != null)
			{
				this.mDistanceForRecycle0 = initParam.mDistanceForRecycle0;
				this.mDistanceForNew0 = initParam.mDistanceForNew0;
				this.mDistanceForRecycle1 = initParam.mDistanceForRecycle1;
				this.mDistanceForNew1 = initParam.mDistanceForNew1;
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
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			this.mGroupCount = this.mLayoutParam.mColumnOrRowCount;
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
			this.AdjustPivot(this.mViewPortRectTransform);
			this.AdjustAnchor(this.mContainerTrans);
			this.AdjustContainerPivot(this.mContainerTrans);
			this.InitItemPool();
			this.mOnGetItemByItemIndex = onGetItemByItemIndex;
			if (this.mListViewInited)
			{
			}
			this.mListViewInited = true;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			this.mItemTotalCount = itemTotalCount;
			this.UpdateLayoutParamAutoValue();
			this.mItemGroupList.Clear();
			for (int i = 0; i < this.mGroupCount; i++)
			{
				StaggeredGridItemGroup staggeredGridItemGroup = new StaggeredGridItemGroup();
				staggeredGridItemGroup.Init(this, this.mItemTotalCount, i, new Func<int, int, LoopStaggeredGridViewItem>(this.GetNewItemByGroupAndIndex));
				this.mItemGroupList.Add(staggeredGridItemGroup);
			}
			this.UpdateContentSize();
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000CD088 File Offset: 0x000CB488
		public void ResetGridViewLayoutParam(int itemTotalCount, GridViewLayoutParam layoutParam)
		{
			if (!this.mListViewInited)
			{
				return;
			}
			this.mScrollRect.StopMovement();
			this.SetListItemCount(0, true);
			this.RecycleAllItem();
			this.ClearAllTmpRecycledItem();
			this.mLayoutParam = layoutParam;
			if (this.mLayoutParam == null)
			{
				return;
			}
			if (!this.mLayoutParam.CheckParam())
			{
				return;
			}
			this.mGroupCount = this.mLayoutParam.mColumnOrRowCount;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			this.mItemTotalCount = itemTotalCount;
			this.UpdateLayoutParamAutoValue();
			this.mItemGroupList.Clear();
			for (int i = 0; i < this.mGroupCount; i++)
			{
				StaggeredGridItemGroup staggeredGridItemGroup = new StaggeredGridItemGroup();
				staggeredGridItemGroup.Init(this, this.mItemTotalCount, i, new Func<int, int, LoopStaggeredGridViewItem>(this.GetNewItemByGroupAndIndex));
				this.mItemGroupList.Add(staggeredGridItemGroup);
			}
			this.UpdateContentSize();
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000CD178 File Offset: 0x000CB578
		private void UpdateLayoutParamAutoValue()
		{
			if (this.mLayoutParam.mCustomColumnOrRowOffsetArray == null)
			{
				this.mLayoutParam.mCustomColumnOrRowOffsetArray = new float[this.mGroupCount];
				float num = this.mLayoutParam.mItemWidthOrHeight * (float)this.mGroupCount;
				float num2;
				if (this.IsVertList)
				{
					num2 = (this.ViewPortWidth - this.mLayoutParam.mPadding1 - this.mLayoutParam.mPadding2 - num) / (float)(this.mGroupCount - 1);
				}
				else
				{
					num2 = (this.ViewPortHeight - this.mLayoutParam.mPadding1 - this.mLayoutParam.mPadding2 - num) / (float)(this.mGroupCount - 1);
				}
				float num3 = this.mLayoutParam.mPadding1;
				for (int i = 0; i < this.mGroupCount; i++)
				{
					if (this.IsVertList)
					{
						this.mLayoutParam.mCustomColumnOrRowOffsetArray[i] = num3;
					}
					else
					{
						this.mLayoutParam.mCustomColumnOrRowOffsetArray[i] = -num3;
					}
					num3 = num3 + this.mLayoutParam.mItemWidthOrHeight + num2;
				}
			}
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000CD28C File Offset: 0x000CB68C
		public LoopStaggeredGridViewItem NewListViewItem(string itemPrefabName)
		{
			StaggeredGridItemPool staggeredGridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out staggeredGridItemPool))
			{
				return null;
			}
			LoopStaggeredGridViewItem item = staggeredGridItemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentListView = this;
			return item;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000CD2F4 File Offset: 0x000CB6F4
		public void SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			int count = this.mItemGroupList.Count;
			this.mItemTotalCount = itemCount;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].SetListItemCount(this.mItemTotalCount);
			}
			this.UpdateContentSize();
			if (this.mItemTotalCount == 0)
			{
				this.mItemIndexDataList.Clear();
				this.ClearAllTmpRecycledItem();
				return;
			}
			int count2 = this.mItemIndexDataList.Count;
			if (count2 > this.mItemTotalCount)
			{
				this.mItemIndexDataList.RemoveRange(this.mItemTotalCount, count2 - this.mItemTotalCount);
			}
			if (resetPos)
			{
				this.MovePanelToItemIndex(0, 0f);
				return;
			}
			if (count2 > this.mItemTotalCount)
			{
				this.MovePanelToItemIndex(this.mItemTotalCount - 1, 0f);
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000CD3D0 File Offset: 0x000CB7D0
		public void MovePanelToItemIndex(int itemIndex, float offset)
		{
			this.mScrollRect.StopMovement();
			if (this.mItemTotalCount == 0 || itemIndex < 0)
			{
				return;
			}
			this.CheckAllGroupIfNeedUpdateItemPos();
			this.UpdateContentSize();
			float viewPortSize = this.ViewPortSize;
			float contentSize = this.GetContentSize();
			if (contentSize <= viewPortSize)
			{
				if (this.IsVertList)
				{
					this.SetAnchoredPositionY(this.mContainerTrans, 0f);
				}
				else
				{
					this.SetAnchoredPositionX(this.mContainerTrans, 0f);
				}
				return;
			}
			if (itemIndex >= this.mItemTotalCount)
			{
				itemIndex = this.mItemTotalCount - 1;
			}
			float itemAbsPosByItemIndex = this.GetItemAbsPosByItemIndex(itemIndex);
			if (itemAbsPosByItemIndex < 0f)
			{
				return;
			}
			if (this.IsVertList)
			{
				float num = (float)((this.mArrangeType != ListItemArrangeType.TopToBottom) ? -1 : 1);
				float num2 = itemAbsPosByItemIndex + offset;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				if (contentSize - num2 >= viewPortSize)
				{
					this.SetAnchoredPositionY(this.mContainerTrans, num * num2);
				}
				else
				{
					this.SetAnchoredPositionY(this.mContainerTrans, num * (contentSize - viewPortSize));
					this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
					this.ClearAllTmpRecycledItem();
					this.UpdateContentSize();
					contentSize = this.GetContentSize();
					if (contentSize - num2 >= viewPortSize)
					{
						this.SetAnchoredPositionY(this.mContainerTrans, num * num2);
					}
					else
					{
						this.SetAnchoredPositionY(this.mContainerTrans, num * (contentSize - viewPortSize));
					}
				}
			}
			else
			{
				float num3 = (float)((this.mArrangeType != ListItemArrangeType.RightToLeft) ? -1 : 1);
				float num4 = itemAbsPosByItemIndex + offset;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				if (contentSize - num4 >= viewPortSize)
				{
					this.SetAnchoredPositionX(this.mContainerTrans, num3 * num4);
				}
				else
				{
					this.SetAnchoredPositionX(this.mContainerTrans, num3 * (contentSize - viewPortSize));
					this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
					this.ClearAllTmpRecycledItem();
					this.UpdateContentSize();
					contentSize = this.GetContentSize();
					if (contentSize - num4 >= viewPortSize)
					{
						this.SetAnchoredPositionX(this.mContainerTrans, num3 * num4);
					}
					else
					{
						this.SetAnchoredPositionX(this.mContainerTrans, num3 * (contentSize - viewPortSize));
					}
				}
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000CD5FC File Offset: 0x000CB9FC
		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return null;
			}
			StaggeredGridItemGroup itemGroupByIndex = this.GetItemGroupByIndex(itemIndexData.mGroupIndex);
			return itemGroupByIndex.GetShownItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000CD634 File Offset: 0x000CBA34
		public void RefreshAllShownItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RefreshAllShownItem();
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000CD670 File Offset: 0x000CBA70
		public void OnItemSizeChanged(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			StaggeredGridItemGroup itemGroupByIndex = this.GetItemGroupByIndex(itemIndexData.mGroupIndex);
			itemGroupByIndex.OnItemSizeChanged(itemIndexData.mIndexInGroup);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000CD6A8 File Offset: 0x000CBAA8
		public void RefreshItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			StaggeredGridItemGroup itemGroupByIndex = this.GetItemGroupByIndex(itemIndexData.mGroupIndex);
			itemGroupByIndex.RefreshItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000CD6DD File Offset: 0x000CBADD
		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000CD708 File Offset: 0x000CBB08
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

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000CD748 File Offset: 0x000CBB48
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000CD768 File Offset: 0x000CBB68
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000CD788 File Offset: 0x000CBB88
		public void RecycleAllItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RecycleAllItem();
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000CD7C4 File Offset: 0x000CBBC4
		public void RecycleItemTmp(LoopStaggeredGridViewItem item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			StaggeredGridItemPool staggeredGridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out staggeredGridItemPool))
			{
				return;
			}
			staggeredGridItemPool.RecycleItem(item);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000CD814 File Offset: 0x000CBC14
		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000CD850 File Offset: 0x000CBC50
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

		// Token: 0x0600248C RID: 9356 RVA: 0x000CD8DC File Offset: 0x000CBCDC
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

		// Token: 0x0600248D RID: 9357 RVA: 0x000CD968 File Offset: 0x000CBD68
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

		// Token: 0x0600248E RID: 9358 RVA: 0x000CDA30 File Offset: 0x000CBE30
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

		// Token: 0x0600248F RID: 9359 RVA: 0x000CDAF8 File Offset: 0x000CBEF8
		private void InitItemPool()
		{
			foreach (StaggeredGridItemPrefabConfData staggeredGridItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (!(staggeredGridItemPrefabConfData.mItemPrefab == null))
				{
					string name = staggeredGridItemPrefabConfData.mItemPrefab.name;
					if (!this.mItemPoolDict.ContainsKey(name))
					{
						RectTransform component = staggeredGridItemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (!(component == null))
						{
							this.AdjustAnchor(component);
							this.AdjustPivot(component);
							LoopStaggeredGridViewItem component2 = staggeredGridItemPrefabConfData.mItemPrefab.GetComponent<LoopStaggeredGridViewItem>();
							if (component2 == null)
							{
								staggeredGridItemPrefabConfData.mItemPrefab.AddComponent<LoopStaggeredGridViewItem>();
							}
							StaggeredGridItemPool staggeredGridItemPool = new StaggeredGridItemPool();
							staggeredGridItemPool.Init(staggeredGridItemPrefabConfData.mItemPrefab, staggeredGridItemPrefabConfData.mPadding, staggeredGridItemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, staggeredGridItemPool);
							this.mItemPoolList.Add(staggeredGridItemPool);
						}
					}
				}
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000CDC18 File Offset: 0x000CC018
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mIsDraging = true;
			this.CacheDragPointerEventData(eventData);
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction();
			}
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000CDC4A File Offset: 0x000CC04A
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
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000CDC7C File Offset: 0x000CC07C
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

		// Token: 0x06002493 RID: 9363 RVA: 0x000CDCA8 File Offset: 0x000CC0A8
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

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000CDD14 File Offset: 0x000CC114
		public int CurMaxCreatedItemIndexCount
		{
			get
			{
				return this.mItemIndexDataList.Count;
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000CDD24 File Offset: 0x000CC124
		private void SetAnchoredPositionX(RectTransform rtf, float x)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.x = x;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000CDD48 File Offset: 0x000CC148
		private void SetAnchoredPositionY(RectTransform rtf, float y)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.y = y;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000CDD6C File Offset: 0x000CC16C
		public ItemIndexData GetItemIndexData(int itemIndex)
		{
			int count = this.mItemIndexDataList.Count;
			if (itemIndex < 0 || itemIndex >= count)
			{
				return null;
			}
			return this.mItemIndexDataList[itemIndex];
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000CDDA4 File Offset: 0x000CC1A4
		public void UpdateAllGroupShownItemsPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateAllShownItemsPos();
			}
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000CDDE0 File Offset: 0x000CC1E0
		private void CheckAllGroupIfNeedUpdateItemPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].CheckIfNeedUpdateItemPos();
			}
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000CDE1C File Offset: 0x000CC21C
		public float GetItemAbsPosByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.mItemIndexDataList.Count)
			{
				return -1f;
			}
			ItemIndexData itemIndexData = this.mItemIndexDataList[itemIndex];
			return this.mItemGroupList[itemIndexData.mGroupIndex].GetItemPos(itemIndexData.mIndexInGroup);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000CDE70 File Offset: 0x000CC270
		public LoopStaggeredGridViewItem GetNewItemByGroupAndIndex(int groupIndex, int indexInGroup)
		{
			if (indexInGroup < 0)
			{
				return null;
			}
			if (this.mItemTotalCount == 0)
			{
				return null;
			}
			List<int> itemIndexMap = this.mItemGroupList[groupIndex].ItemIndexMap;
			int count = itemIndexMap.Count;
			if (count > indexInGroup)
			{
				int num = itemIndexMap[indexInGroup];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mOnGetItemByItemIndex(this, num);
				if (loopStaggeredGridViewItem == null)
				{
					return null;
				}
				loopStaggeredGridViewItem.StartPosOffset = this.mLayoutParam.mCustomColumnOrRowOffsetArray[groupIndex];
				loopStaggeredGridViewItem.ItemIndexInGroup = indexInGroup;
				loopStaggeredGridViewItem.ItemIndex = num;
				loopStaggeredGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
				return loopStaggeredGridViewItem;
			}
			else
			{
				if (count != indexInGroup)
				{
					return null;
				}
				int count2 = this.mItemIndexDataList.Count;
				if (count2 >= this.mItemTotalCount)
				{
					return null;
				}
				int num = count2;
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mOnGetItemByItemIndex(this, num);
				if (loopStaggeredGridViewItem == null)
				{
					return null;
				}
				itemIndexMap.Add(num);
				ItemIndexData itemIndexData = new ItemIndexData();
				itemIndexData.mGroupIndex = groupIndex;
				itemIndexData.mIndexInGroup = indexInGroup;
				this.mItemIndexDataList.Add(itemIndexData);
				loopStaggeredGridViewItem.StartPosOffset = this.mLayoutParam.mCustomColumnOrRowOffsetArray[groupIndex];
				loopStaggeredGridViewItem.ItemIndexInGroup = indexInGroup;
				loopStaggeredGridViewItem.ItemIndex = num;
				loopStaggeredGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
				return loopStaggeredGridViewItem;
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000CDFA8 File Offset: 0x000CC3A8
		private int GetCurShouldAddNewItemGroupIndex()
		{
			float num = float.MaxValue;
			int count = this.mItemGroupList.Count;
			int result = 0;
			for (int i = 0; i < count; i++)
			{
				float shownItemPosMaxValue = this.mItemGroupList[i].GetShownItemPosMaxValue();
				if (shownItemPosMaxValue < num)
				{
					num = shownItemPosMaxValue;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000CDFFC File Offset: 0x000CC3FC
		public void UpdateListViewWithDefault()
		{
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.UpdateContentSize();
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000CE022 File Offset: 0x000CC422
		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			this.UpdateListViewWithDefault();
			this.ClearAllTmpRecycledItem();
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000CE050 File Offset: 0x000CC450
		public void UpdateListView(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			this.mListUpdateCheckFrameCount++;
			bool flag = true;
			int num = 0;
			int num2 = 9999;
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateListViewPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
			while (flag)
			{
				num++;
				if (num >= num2)
				{
					break;
				}
				int curShouldAddNewItemGroupIndex = this.GetCurShouldAddNewItemGroupIndex();
				flag = this.mItemGroupList[curShouldAddNewItemGroupIndex].UpdateListViewPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000CE0EC File Offset: 0x000CC4EC
		public float GetContentSize()
		{
			if (this.mIsVertList)
			{
				return this.mContainerTrans.rect.height;
			}
			return this.mContainerTrans.rect.width;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000CE12C File Offset: 0x000CC52C
		public void UpdateContentSize()
		{
			int count = this.mItemGroupList.Count;
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				float contentPanelSize = this.mItemGroupList[i].GetContentPanelSize();
				if (contentPanelSize > num)
				{
					num = contentPanelSize;
				}
			}
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height != num)
				{
					this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num);
				}
			}
			else if (this.mContainerTrans.rect.width != num)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num);
			}
		}

		// Token: 0x040023B6 RID: 9142
		private Dictionary<string, StaggeredGridItemPool> mItemPoolDict = new Dictionary<string, StaggeredGridItemPool>();

		// Token: 0x040023B7 RID: 9143
		private List<StaggeredGridItemPool> mItemPoolList = new List<StaggeredGridItemPool>();

		// Token: 0x040023B8 RID: 9144
		[SerializeField]
		private List<StaggeredGridItemPrefabConfData> mItemPrefabDataList = new List<StaggeredGridItemPrefabConfData>();

		// Token: 0x040023B9 RID: 9145
		[SerializeField]
		private ListItemArrangeType mArrangeType;

		// Token: 0x040023BA RID: 9146
		private RectTransform mContainerTrans;

		// Token: 0x040023BB RID: 9147
		private ScrollRect mScrollRect;

		// Token: 0x040023BC RID: 9148
		private int mGroupCount;

		// Token: 0x040023BD RID: 9149
		private List<StaggeredGridItemGroup> mItemGroupList = new List<StaggeredGridItemGroup>();

		// Token: 0x040023BE RID: 9150
		private List<ItemIndexData> mItemIndexDataList = new List<ItemIndexData>();

		// Token: 0x040023BF RID: 9151
		private RectTransform mScrollRectTransform;

		// Token: 0x040023C0 RID: 9152
		private RectTransform mViewPortRectTransform;

		// Token: 0x040023C1 RID: 9153
		private float mItemDefaultWithPaddingSize = 20f;

		// Token: 0x040023C2 RID: 9154
		private int mItemTotalCount;

		// Token: 0x040023C3 RID: 9155
		private bool mIsVertList;

		// Token: 0x040023C4 RID: 9156
		private Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> mOnGetItemByItemIndex;

		// Token: 0x040023C5 RID: 9157
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x040023C6 RID: 9158
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x040023C7 RID: 9159
		private float mDistanceForRecycle0 = 300f;

		// Token: 0x040023C8 RID: 9160
		private float mDistanceForNew0 = 200f;

		// Token: 0x040023C9 RID: 9161
		private float mDistanceForRecycle1 = 300f;

		// Token: 0x040023CA RID: 9162
		private float mDistanceForNew1 = 200f;

		// Token: 0x040023CB RID: 9163
		private bool mIsDraging;

		// Token: 0x040023CC RID: 9164
		private PointerEventData mPointerEventData;

		// Token: 0x040023CD RID: 9165
		public Action mOnBeginDragAction;

		// Token: 0x040023CE RID: 9166
		public Action mOnDragingAction;

		// Token: 0x040023CF RID: 9167
		public Action mOnEndDragAction;

		// Token: 0x040023D0 RID: 9168
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x040023D1 RID: 9169
		private bool mListViewInited;

		// Token: 0x040023D2 RID: 9170
		private int mListUpdateCheckFrameCount;

		// Token: 0x040023D3 RID: 9171
		private GridViewLayoutParam mLayoutParam;
	}
}
