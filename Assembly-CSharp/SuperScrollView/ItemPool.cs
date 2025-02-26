using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x02000600 RID: 1536
	public class ItemPool
	{
		// Token: 0x060023E6 RID: 9190 RVA: 0x000C6FC8 File Offset: 0x000C53C8
		public void Init(GameObject prefabObj, float padding, float startPosOffset, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mPadding = padding;
			this.mStartPosOffset = startPosOffset;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopListViewItem2 item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000C703C File Offset: 0x000C543C
		public LoopListViewItem2 GetItem()
		{
			ItemPool.mCurItemIdCount++;
			LoopListViewItem2 loopListViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopListViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopListViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopListViewItem = this.CreateItem();
				}
				else
				{
					loopListViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopListViewItem.gameObject.SetActive(true);
				}
			}
			loopListViewItem.Padding = this.mPadding;
			loopListViewItem.ItemId = ItemPool.mCurItemIdCount;
			return loopListViewItem;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000C7104 File Offset: 0x000C5504
		public void DestroyAllItem()
		{
			this.ClearTmpRecycledItem();
			int count = this.mPooledItemList.Count;
			for (int i = 0; i < count; i++)
			{
				UnityEngine.Object.DestroyImmediate(this.mPooledItemList[i].gameObject);
			}
			this.mPooledItemList.Clear();
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000C7158 File Offset: 0x000C5558
		public LoopListViewItem2 CreateItem()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopListViewItem2 component2 = gameObject.GetComponent<LoopListViewItem2>();
			component2.ItemPrefabName = this.mPrefabName;
			component2.StartPosOffset = this.mStartPosOffset;
			return component2;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000C71D0 File Offset: 0x000C55D0
		private void RecycleItemReal(LoopListViewItem2 item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000C71EA File Offset: 0x000C55EA
		public void RecycleItem(LoopListViewItem2 item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000C71F8 File Offset: 0x000C55F8
		public void ClearTmpRecycledItem()
		{
			int count = this.mTmpPooledItemList.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.RecycleItemReal(this.mTmpPooledItemList[i]);
			}
			this.mTmpPooledItemList.Clear();
		}

		// Token: 0x0400234B RID: 9035
		private GameObject mPrefabObj;

		// Token: 0x0400234C RID: 9036
		private string mPrefabName;

		// Token: 0x0400234D RID: 9037
		private int mInitCreateCount = 1;

		// Token: 0x0400234E RID: 9038
		private float mPadding;

		// Token: 0x0400234F RID: 9039
		private float mStartPosOffset;

		// Token: 0x04002350 RID: 9040
		private List<LoopListViewItem2> mTmpPooledItemList = new List<LoopListViewItem2>();

		// Token: 0x04002351 RID: 9041
		private List<LoopListViewItem2> mPooledItemList = new List<LoopListViewItem2>();

		// Token: 0x04002352 RID: 9042
		private static int mCurItemIdCount;

		// Token: 0x04002353 RID: 9043
		private RectTransform mItemParent;
	}
}
