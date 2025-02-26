using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x0200060C RID: 1548
	public class StaggeredGridItemGroup
	{
		// Token: 0x060024CB RID: 9419 RVA: 0x000CE588 File Offset: 0x000CC988
		public void Init(LoopStaggeredGridView parent, int itemTotalCount, int groupIndex, Func<int, int, LoopStaggeredGridViewItem> onGetItemByIndex)
		{
			this.mGroupIndex = groupIndex;
			this.mParentGridView = parent;
			this.mArrangeType = this.mParentGridView.ArrangeType;
			this.mGameObject = this.mParentGridView.gameObject;
			this.mScrollRect = this.mGameObject.GetComponent<ScrollRect>();
			this.mItemPosMgr = new ItemPosMgr(this.mItemDefaultWithPaddingSize);
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mOnGetItemByIndex = onGetItemByIndex;
			this.mItemTotalCount = itemTotalCount;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
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
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000CE6D0 File Offset: 0x000CCAD0
		public List<int> ItemIndexMap
		{
			get
			{
				return this.mItemIndexMap;
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000CE6D8 File Offset: 0x000CCAD8
		public void ResetListView()
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000CE6EC File Offset: 0x000CCAEC
		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
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
			for (int i = 0; i < count; i++)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[i];
				if (loopStaggeredGridViewItem.ItemIndex == itemIndex)
				{
					return loopStaggeredGridViewItem;
				}
			}
			return null;
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000CE770 File Offset: 0x000CCB70
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

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000CE7B0 File Offset: 0x000CCBB0
		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000CE7D0 File Offset: 0x000CCBD0
		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000CE7F0 File Offset: 0x000CCBF0
		private bool IsDraging
		{
			get
			{
				return this.mParentGridView.IsDraging;
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000CE800 File Offset: 0x000CCC00
		public LoopStaggeredGridViewItem GetShownItemByIndexInGroup(int indexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (indexInGroup < this.mItemList[0].ItemIndexInGroup || indexInGroup > this.mItemList[count - 1].ItemIndexInGroup)
			{
				return null;
			}
			int index = indexInGroup - this.mItemList[0].ItemIndexInGroup;
			return this.mItemList[index];
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000CE874 File Offset: 0x000CCC74
		public int GetIndexInShownItemList(LoopStaggeredGridViewItem item)
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

		// Token: 0x060024D5 RID: 9429 RVA: 0x000CE8D0 File Offset: 0x000CCCD0
		public void RefreshAllShownItem()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.RefreshAllShownItemWithFirstIndexInGroup(this.mItemList[0].ItemIndexInGroup);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000CE908 File Offset: 0x000CCD08
		public void OnItemSizeChanged(int indexInGroup)
		{
			LoopStaggeredGridViewItem shownItemByIndexInGroup = this.GetShownItemByIndexInGroup(indexInGroup);
			if (shownItemByIndexInGroup == null)
			{
				return;
			}
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(indexInGroup, shownItemByIndexInGroup.CachedRectTransform.rect.height, shownItemByIndexInGroup.Padding);
				}
				else
				{
					this.SetItemSize(indexInGroup, shownItemByIndexInGroup.CachedRectTransform.rect.width, shownItemByIndexInGroup.Padding);
				}
			}
			this.UpdateAllShownItemsPos();
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000CE98C File Offset: 0x000CCD8C
		public void RefreshItemByIndexInGroup(int indexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (indexInGroup < this.mItemList[0].ItemIndexInGroup || indexInGroup > this.mItemList[count - 1].ItemIndexInGroup)
			{
				return;
			}
			int itemIndexInGroup = this.mItemList[0].ItemIndexInGroup;
			int index = indexInGroup - itemIndexInGroup;
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[index];
			Vector3 anchoredPosition3D = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleItemTmp(loopStaggeredGridViewItem);
			LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(indexInGroup);
			if (newItemByIndexInGroup == null)
			{
				this.RefreshAllShownItemWithFirstIndexInGroup(itemIndexInGroup);
				return;
			}
			this.mItemList[index] = newItemByIndexInGroup;
			if (this.mIsVertList)
			{
				anchoredPosition3D.x = newItemByIndexInGroup.StartPosOffset;
			}
			else
			{
				anchoredPosition3D.y = newItemByIndexInGroup.StartPosOffset;
			}
			newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
			this.OnItemSizeChanged(indexInGroup);
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000CEA88 File Offset: 0x000CCE88
		public void RefreshAllShownItemWithFirstIndexInGroup(int firstItemIndexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
			Vector3 anchoredPosition3D = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleAllItem();
			for (int i = 0; i < count; i++)
			{
				int num = firstItemIndexInGroup + i;
				LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num);
				if (newItemByIndexInGroup == null)
				{
					break;
				}
				if (this.mIsVertList)
				{
					anchoredPosition3D.x = newItemByIndexInGroup.StartPosOffset;
				}
				else
				{
					anchoredPosition3D.y = newItemByIndexInGroup.StartPosOffset;
				}
				newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				if (this.mSupportScrollBar)
				{
					if (this.mIsVertList)
					{
						this.SetItemSize(num, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					else
					{
						this.SetItemSize(num, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
				}
				this.mItemList.Add(newItemByIndexInGroup);
			}
			this.UpdateAllShownItemsPos();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000CEBB0 File Offset: 0x000CCFB0
		public void RefreshAllShownItemWithFirstIndexAndPos(int firstItemIndexInGroup, Vector3 pos)
		{
			this.RecycleAllItem();
			LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(firstItemIndexInGroup);
			if (newItemByIndexInGroup == null)
			{
				return;
			}
			if (this.mIsVertList)
			{
				pos.x = newItemByIndexInGroup.StartPosOffset;
			}
			else
			{
				pos.y = newItemByIndexInGroup.StartPosOffset;
			}
			newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = pos;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(firstItemIndexInGroup, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
				}
				else
				{
					this.SetItemSize(firstItemIndexInGroup, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
				}
			}
			this.mItemList.Add(newItemByIndexInGroup);
			this.UpdateAllShownItemsPos();
			this.mParentGridView.UpdateListViewWithDefault();
			this.ClearAllTmpRecycledItem();
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000CEC8C File Offset: 0x000CD08C
		private void SetItemSize(int itemIndex, float itemSize, float padding)
		{
			this.mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
			if (itemIndex >= this.mLastItemIndex)
			{
				this.mLastItemIndex = itemIndex;
				this.mLastItemPadding = padding;
			}
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000CECB7 File Offset: 0x000CD0B7
		private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			return this.mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000CECC7 File Offset: 0x000CD0C7
		public float GetItemPos(int itemIndex)
		{
			return this.mItemPosMgr.GetItemPos(itemIndex);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000CECD5 File Offset: 0x000CD0D5
		public Vector3 GetItemCornerPosInViewPort(LoopStaggeredGridViewItem item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
		{
			item.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			return this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[(int)corner]);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000CED04 File Offset: 0x000CD104
		public void RecycleItemTmp(LoopStaggeredGridViewItem item)
		{
			this.mParentGridView.RecycleItemTmp(item);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000CED14 File Offset: 0x000CD114
		public void RecycleAllItem()
		{
			foreach (LoopStaggeredGridViewItem item in this.mItemList)
			{
				this.RecycleItemTmp(item);
			}
			this.mItemList.Clear();
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000CED7C File Offset: 0x000CD17C
		public void ClearAllTmpRecycledItem()
		{
			this.mParentGridView.ClearAllTmpRecycledItem();
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000CED89 File Offset: 0x000CD189
		private LoopStaggeredGridViewItem GetNewItemByIndexInGroup(int indexInGroup)
		{
			return this.mParentGridView.GetNewItemByGroupAndIndex(this.mGroupIndex, indexInGroup);
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000CED9D File Offset: 0x000CD19D
		public int HadCreatedItemCount
		{
			get
			{
				return this.mItemIndexMap.Count;
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000CEDAC File Offset: 0x000CD1AC
		public void SetListItemCount(int itemCount)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			int num = this.mItemTotalCount;
			this.mItemTotalCount = itemCount;
			this.UpdateItemIndexMap(num);
			if (num < this.mItemTotalCount)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(this.HadCreatedItemCount);
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			this.RecycleAllItem();
			if (this.mItemTotalCount == 0)
			{
				this.mCurReadyMaxItemIndex = 0;
				this.mCurReadyMinItemIndex = 0;
				this.mNeedCheckNextMaxItem = false;
				this.mNeedCheckNextMinItem = false;
				this.mItemIndexMap.Clear();
				return;
			}
			if (this.mCurReadyMaxItemIndex >= this.mItemTotalCount)
			{
				this.mCurReadyMaxItemIndex = this.mItemTotalCount - 1;
			}
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000CEE88 File Offset: 0x000CD288
		private void UpdateItemIndexMap(int oldItemTotalCount)
		{
			int count = this.mItemIndexMap.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mItemTotalCount == 0)
			{
				this.mItemIndexMap.Clear();
				return;
			}
			if (this.mItemTotalCount >= oldItemTotalCount)
			{
				return;
			}
			int itemTotalCount = this.mParentGridView.ItemTotalCount;
			if (this.mItemIndexMap[count - 1] < itemTotalCount)
			{
				return;
			}
			int i = 0;
			int num = count - 1;
			int num2 = 0;
			while (i <= num)
			{
				int num3 = (i + num) / 2;
				int num4 = this.mItemIndexMap[num3];
				if (num4 == itemTotalCount)
				{
					num2 = num3;
					break;
				}
				if (num4 >= itemTotalCount)
				{
					break;
				}
				i = num3 + 1;
				num2 = i;
			}
			int num5 = 0;
			for (int j = num2; j < count; j++)
			{
				if (this.mItemIndexMap[j] >= itemTotalCount)
				{
					num5 = j;
					break;
				}
			}
			this.mItemIndexMap.RemoveRange(num5, count - num5);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000CEF8C File Offset: 0x000CD38C
		public void UpdateListViewPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.Update(false);
			}
			this.mListUpdateCheckFrameCount = this.mParentGridView.ListUpdateCheckFrameCount;
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
				if (this.mIsVertList)
				{
					flag = this.UpdateForVertListPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
				else
				{
					flag = this.UpdateForHorizontalListPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
			}
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000CF01F File Offset: 0x000CD41F
		public bool UpdateListViewPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mIsVertList)
			{
				return this.UpdateForVertListPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
			return this.UpdateForHorizontalListPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000CF044 File Offset: 0x000CD444
		public bool UpdateForVertListPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup.StartPosOffset, num3, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.IsDraging && loopStaggeredGridViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector2.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopStaggeredGridViewItem);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.IsDraging && loopStaggeredGridViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector3.y > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopStaggeredGridViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					if (vector.y - this.mViewPortRectLocalCorners[1].y < distanceForNew0)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup - 1;
						if (num4 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.height, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Insert(0, newItemByIndexInGroup2);
								float y = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.y + newItemByIndexInGroup2.CachedRectTransform.rect.height + newItemByIndexInGroup2.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup2.StartPosOffset, y, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = false;
						}
					}
					if (this.mViewPortRectLocalCorners[0].y - vector4.y < distanceForNew1)
					{
						if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num5 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
						if (num5 >= this.mItemIndexMap.Count)
						{
							return false;
						}
						if (num5 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num5);
							if (newItemByIndexInGroup3 == null)
							{
								this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
								this.mNeedCheckNextMaxItem = false;
								this.CheckIfNeedUpdateItemPos();
								return false;
							}
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num5, newItemByIndexInGroup3.CachedRectTransform.rect.height, newItemByIndexInGroup3.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup3);
							float y2 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.y - loopStaggeredGridViewItem2.CachedRectTransform.rect.height - loopStaggeredGridViewItem2.Padding;
							newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup3.StartPosOffset, y2, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num5 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num5;
							}
							return true;
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
				LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num7);
				if (newItemByIndexInGroup4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num7, newItemByIndexInGroup4.CachedRectTransform.rect.height, newItemByIndexInGroup4.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup4);
				newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup4.StartPosOffset, y3, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				loopStaggeredGridViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.IsDraging && loopStaggeredGridViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector5.y > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopStaggeredGridViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.IsDraging && loopStaggeredGridViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector8.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopStaggeredGridViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				if (this.mViewPortRectLocalCorners[0].y - vector6.y < distanceForNew0)
				{
					if (loopStaggeredGridViewItem3.ItemIndexInGroup < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = true;
					}
					int num8 = loopStaggeredGridViewItem3.ItemIndexInGroup - 1;
					if (num8 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup5 = this.GetNewItemByIndexInGroup(num8);
						if (!(newItemByIndexInGroup5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num8, newItemByIndexInGroup5.CachedRectTransform.rect.height, newItemByIndexInGroup5.Padding);
							}
							this.mItemList.Insert(0, newItemByIndexInGroup5);
							float y4 = loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D.y - newItemByIndexInGroup5.CachedRectTransform.rect.height - newItemByIndexInGroup5.Padding;
							newItemByIndexInGroup5.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup5.StartPosOffset, y4, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num8 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num8;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = false;
					}
				}
				if (vector7.y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopStaggeredGridViewItem4.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num9 = loopStaggeredGridViewItem4.ItemIndexInGroup + 1;
					if (num9 >= this.mItemIndexMap.Count)
					{
						return false;
					}
					if (num9 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup6 = this.GetNewItemByIndexInGroup(num9);
						if (newItemByIndexInGroup6 == null)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num9, newItemByIndexInGroup6.CachedRectTransform.rect.height, newItemByIndexInGroup6.Padding);
						}
						this.mItemList.Add(newItemByIndexInGroup6);
						float y5 = loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D.y + loopStaggeredGridViewItem4.CachedRectTransform.rect.height + loopStaggeredGridViewItem4.Padding;
						newItemByIndexInGroup6.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup6.StartPosOffset, y5, 0f);
						this.CheckIfNeedUpdateItemPos();
						if (num9 > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = num9;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000CFAAC File Offset: 0x000CDEAC
		public bool UpdateForVertListPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup.StartPosOffset, num3, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (this.mViewPortRectLocalCorners[0].y - vector.y < distanceForNew1)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (newItemByIndexInGroup2 == null)
							{
								this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
								this.mNeedCheckNextMaxItem = false;
								this.CheckIfNeedUpdateItemPos();
								return false;
							}
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.height, newItemByIndexInGroup2.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup2);
							float y = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.y - loopStaggeredGridViewItem.CachedRectTransform.rect.height - loopStaggeredGridViewItem.Padding;
							newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup2.StartPosOffset, y, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num4 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num4;
							}
							return true;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num5 = this.mContainerTrans.anchoredPosition3D.y;
				if (num5 > 0f)
				{
					num5 = 0f;
				}
				int num6 = 0;
				float y2 = -num5;
				if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num5, ref num6, ref y2))
				{
					return false;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num6);
				if (newItemByIndexInGroup3 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num6, newItemByIndexInGroup3.CachedRectTransform.rect.height, newItemByIndexInGroup3.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup3);
				newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup3.StartPosOffset, y2, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num7 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
					if (num7 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num7);
						if (newItemByIndexInGroup4 == null)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num7, newItemByIndexInGroup4.CachedRectTransform.rect.height, newItemByIndexInGroup4.Padding);
						}
						this.mItemList.Add(newItemByIndexInGroup4);
						float y3 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.y + loopStaggeredGridViewItem2.CachedRectTransform.rect.height + loopStaggeredGridViewItem2.Padding;
						newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup4.StartPosOffset, y3, 0f);
						this.CheckIfNeedUpdateItemPos();
						if (num7 > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = num7;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000CFFC8 File Offset: 0x000CE3C8
		public bool UpdateForHorizontalListPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(x, newItemByIndexInGroup.StartPosOffset, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.IsDraging && loopStaggeredGridViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector2.x > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopStaggeredGridViewItem);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.IsDraging && loopStaggeredGridViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector3.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopStaggeredGridViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew0)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = true;
						}
						int num3 = loopStaggeredGridViewItem.ItemIndexInGroup - 1;
						if (num3 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num3);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num3, newItemByIndexInGroup2.CachedRectTransform.rect.width, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Insert(0, newItemByIndexInGroup2);
								float x2 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.x - newItemByIndexInGroup2.CachedRectTransform.rect.width - newItemByIndexInGroup2.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(x2, newItemByIndexInGroup2.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num3 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num3;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = false;
						}
					}
					if (vector4.x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
						if (num4 >= this.mItemIndexMap.Count)
						{
							return false;
						}
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup3.CachedRectTransform.rect.width, newItemByIndexInGroup3.Padding);
								}
								this.mItemList.Add(newItemByIndexInGroup3);
								float x3 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.x + loopStaggeredGridViewItem2.CachedRectTransform.rect.width + loopStaggeredGridViewItem2.Padding;
								newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(x3, newItemByIndexInGroup3.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
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
				LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num6);
				if (newItemByIndexInGroup4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num6, newItemByIndexInGroup4.CachedRectTransform.rect.width, newItemByIndexInGroup4.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup4);
				newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(num7, newItemByIndexInGroup4.StartPosOffset, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				loopStaggeredGridViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.IsDraging && loopStaggeredGridViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector5.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopStaggeredGridViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.IsDraging && loopStaggeredGridViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector8.x > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopStaggeredGridViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				if (vector6.x - this.mViewPortRectLocalCorners[2].x < distanceForNew0)
				{
					if (loopStaggeredGridViewItem3.ItemIndexInGroup < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = true;
					}
					int num8 = loopStaggeredGridViewItem3.ItemIndexInGroup - 1;
					if (num8 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup5 = this.GetNewItemByIndexInGroup(num8);
						if (!(newItemByIndexInGroup5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num8, newItemByIndexInGroup5.CachedRectTransform.rect.width, newItemByIndexInGroup5.Padding);
							}
							this.mItemList.Insert(0, newItemByIndexInGroup5);
							float x4 = loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D.x + newItemByIndexInGroup5.CachedRectTransform.rect.width + newItemByIndexInGroup5.Padding;
							newItemByIndexInGroup5.CachedRectTransform.anchoredPosition3D = new Vector3(x4, newItemByIndexInGroup5.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num8 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num8;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = false;
					}
				}
				if (this.mViewPortRectLocalCorners[1].x - vector7.x < distanceForNew1)
				{
					if (loopStaggeredGridViewItem4.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num9 = loopStaggeredGridViewItem4.ItemIndexInGroup + 1;
					if (num9 >= this.mItemIndexMap.Count)
					{
						return false;
					}
					if (num9 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup6 = this.GetNewItemByIndexInGroup(num9);
						if (!(newItemByIndexInGroup6 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num9, newItemByIndexInGroup6.CachedRectTransform.rect.width, newItemByIndexInGroup6.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup6);
							float x5 = loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D.x - loopStaggeredGridViewItem4.CachedRectTransform.rect.width - loopStaggeredGridViewItem4.Padding;
							newItemByIndexInGroup6.CachedRectTransform.anchoredPosition3D = new Vector3(x5, newItemByIndexInGroup6.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num9 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num9;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdateItemPos();
					}
				}
			}
			return false;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000D0A30 File Offset: 0x000CEE30
		public bool UpdateForHorizontalListPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
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
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(x, newItemByIndexInGroup.StartPosOffset, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num3 = loopStaggeredGridViewItem.ItemIndexInGroup + 1;
						if (num3 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num3);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num3, newItemByIndexInGroup2.CachedRectTransform.rect.width, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Add(newItemByIndexInGroup2);
								float x2 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.x + loopStaggeredGridViewItem.CachedRectTransform.rect.width + loopStaggeredGridViewItem.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(x2, newItemByIndexInGroup2.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num3 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num3;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num4 = this.mContainerTrans.anchoredPosition3D.x;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				int num5 = 0;
				float num6 = -num4;
				if (this.mSupportScrollBar)
				{
					if (!this.GetPlusItemIndexAndPosAtGivenPos(num4, ref num5, ref num6))
					{
						return false;
					}
					num6 = -num6;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num5);
				if (newItemByIndexInGroup3 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num5, newItemByIndexInGroup3.CachedRectTransform.rect.width, newItemByIndexInGroup3.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup3);
				newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(num6, newItemByIndexInGroup3.StartPosOffset, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew1)
				{
					if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num7 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
					if (num7 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num7);
						if (!(newItemByIndexInGroup4 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num7, newItemByIndexInGroup4.CachedRectTransform.rect.width, newItemByIndexInGroup4.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup4);
							float x3 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.x - loopStaggeredGridViewItem2.CachedRectTransform.rect.width - loopStaggeredGridViewItem2.Padding;
							newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(x3, newItemByIndexInGroup4.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num7 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num7;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdateItemPos();
					}
				}
			}
			return false;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000D0F4C File Offset: 0x000CF34C
		public float GetContentPanelSize()
		{
			float num = (this.mItemPosMgr.mTotalSize <= 0f) ? 0f : (this.mItemPosMgr.mTotalSize - this.mLastItemPadding);
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000D0FA0 File Offset: 0x000CF3A0
		public float GetShownItemPosMaxValue()
		{
			if (this.mItemList.Count == 0)
			{
				return 0f;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.BottomY);
			}
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.TopY);
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.RightX);
			}
			if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.LeftX);
			}
			return 0f;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000D1044 File Offset: 0x000CF444
		public void CheckIfNeedUpdateItemPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem.TopY > 0f || (loopStaggeredGridViewItem.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem.TopY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize = this.GetContentPanelSize();
				if (-loopStaggeredGridViewItem2.BottomY > contentPanelSize || (loopStaggeredGridViewItem2.ItemIndexInGroup == this.mCurReadyMaxItemIndex && -loopStaggeredGridViewItem2.BottomY != contentPanelSize))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem3.BottomY < 0f || (loopStaggeredGridViewItem3.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem3.BottomY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize2 = this.GetContentPanelSize();
				if (loopStaggeredGridViewItem4.TopY > contentPanelSize2 || (loopStaggeredGridViewItem4.ItemIndexInGroup == this.mCurReadyMaxItemIndex && loopStaggeredGridViewItem4.TopY != contentPanelSize2))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem5 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem6 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem5.LeftX < 0f || (loopStaggeredGridViewItem5.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem5.LeftX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize3 = this.GetContentPanelSize();
				if (loopStaggeredGridViewItem6.RightX > contentPanelSize3 || (loopStaggeredGridViewItem6.ItemIndexInGroup == this.mCurReadyMaxItemIndex && loopStaggeredGridViewItem6.RightX != contentPanelSize3))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem7 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem8 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem7.RightX > 0f || (loopStaggeredGridViewItem7.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem7.RightX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize4 = this.GetContentPanelSize();
				if (-loopStaggeredGridViewItem8.LeftX > contentPanelSize4 || (loopStaggeredGridViewItem8.ItemIndexInGroup == this.mCurReadyMaxItemIndex && -loopStaggeredGridViewItem8.LeftX != contentPanelSize4))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000D131C File Offset: 0x000CF71C
		public void UpdateAllShownItemsPos()
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = 0f;
				if (this.mSupportScrollBar)
				{
					num = -this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num2 = num;
				for (int i = 0; i < count; i++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[i];
					loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D = new Vector3(loopStaggeredGridViewItem.StartPosOffset, num2, 0f);
					num2 = num2 - loopStaggeredGridViewItem.CachedRectTransform.rect.height - loopStaggeredGridViewItem.Padding;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num3 = 0f;
				if (this.mSupportScrollBar)
				{
					num3 = this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num4 = num3;
				for (int j = 0; j < count; j++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[j];
					loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D = new Vector3(loopStaggeredGridViewItem2.StartPosOffset, num4, 0f);
					num4 = num4 + loopStaggeredGridViewItem2.CachedRectTransform.rect.height + loopStaggeredGridViewItem2.Padding;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num5 = 0f;
				if (this.mSupportScrollBar)
				{
					num5 = this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num6 = num5;
				for (int k = 0; k < count; k++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[k];
					loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D = new Vector3(num6, loopStaggeredGridViewItem3.StartPosOffset, 0f);
					num6 = num6 + loopStaggeredGridViewItem3.CachedRectTransform.rect.width + loopStaggeredGridViewItem3.Padding;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num7 = 0f;
				if (this.mSupportScrollBar)
				{
					num7 = -this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float num8 = num7;
				for (int l = 0; l < count; l++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[l];
					loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D = new Vector3(num8, loopStaggeredGridViewItem4.StartPosOffset, 0f);
					num8 = num8 - loopStaggeredGridViewItem4.CachedRectTransform.rect.width - loopStaggeredGridViewItem4.Padding;
				}
			}
		}

		// Token: 0x040023E5 RID: 9189
		private LoopStaggeredGridView mParentGridView;

		// Token: 0x040023E6 RID: 9190
		private ListItemArrangeType mArrangeType;

		// Token: 0x040023E7 RID: 9191
		private List<LoopStaggeredGridViewItem> mItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x040023E8 RID: 9192
		private RectTransform mContainerTrans;

		// Token: 0x040023E9 RID: 9193
		private ScrollRect mScrollRect;

		// Token: 0x040023EA RID: 9194
		public int mGroupIndex;

		// Token: 0x040023EB RID: 9195
		private GameObject mGameObject;

		// Token: 0x040023EC RID: 9196
		private List<int> mItemIndexMap = new List<int>();

		// Token: 0x040023ED RID: 9197
		private RectTransform mScrollRectTransform;

		// Token: 0x040023EE RID: 9198
		private RectTransform mViewPortRectTransform;

		// Token: 0x040023EF RID: 9199
		private float mItemDefaultWithPaddingSize;

		// Token: 0x040023F0 RID: 9200
		private int mItemTotalCount;

		// Token: 0x040023F1 RID: 9201
		private bool mIsVertList;

		// Token: 0x040023F2 RID: 9202
		private Func<int, int, LoopStaggeredGridViewItem> mOnGetItemByIndex;

		// Token: 0x040023F3 RID: 9203
		private Vector3[] mItemWorldCorners = new Vector3[4];

		// Token: 0x040023F4 RID: 9204
		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		// Token: 0x040023F5 RID: 9205
		private int mCurReadyMinItemIndex;

		// Token: 0x040023F6 RID: 9206
		private int mCurReadyMaxItemIndex;

		// Token: 0x040023F7 RID: 9207
		private bool mNeedCheckNextMinItem = true;

		// Token: 0x040023F8 RID: 9208
		private bool mNeedCheckNextMaxItem = true;

		// Token: 0x040023F9 RID: 9209
		private ItemPosMgr mItemPosMgr;

		// Token: 0x040023FA RID: 9210
		private bool mSupportScrollBar = true;

		// Token: 0x040023FB RID: 9211
		private int mLastItemIndex;

		// Token: 0x040023FC RID: 9212
		private float mLastItemPadding;

		// Token: 0x040023FD RID: 9213
		private Vector3 mLastFrameContainerPos = Vector3.zero;

		// Token: 0x040023FE RID: 9214
		private int mListUpdateCheckFrameCount;
	}
}
