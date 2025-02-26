using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x0200060D RID: 1549
	public class StaggeredGridItemPool
	{
		// Token: 0x060024F0 RID: 9456 RVA: 0x000D15EC File Offset: 0x000CF9EC
		public void Init(GameObject prefabObj, float padding, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mPadding = padding;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopStaggeredGridViewItem item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000D1658 File Offset: 0x000CFA58
		public LoopStaggeredGridViewItem GetItem()
		{
			StaggeredGridItemPool.mCurItemIdCount++;
			LoopStaggeredGridViewItem loopStaggeredGridViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopStaggeredGridViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopStaggeredGridViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopStaggeredGridViewItem = this.CreateItem();
				}
				else
				{
					loopStaggeredGridViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopStaggeredGridViewItem.gameObject.SetActive(true);
				}
			}
			loopStaggeredGridViewItem.Padding = this.mPadding;
			loopStaggeredGridViewItem.ItemId = StaggeredGridItemPool.mCurItemIdCount;
			return loopStaggeredGridViewItem;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000D1720 File Offset: 0x000CFB20
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

		// Token: 0x060024F3 RID: 9459 RVA: 0x000D1774 File Offset: 0x000CFB74
		public LoopStaggeredGridViewItem CreateItem()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopStaggeredGridViewItem component2 = gameObject.GetComponent<LoopStaggeredGridViewItem>();
			component2.ItemPrefabName = this.mPrefabName;
			component2.StartPosOffset = 0f;
			return component2;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000D17EB File Offset: 0x000CFBEB
		private void RecycleItemReal(LoopStaggeredGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000D1805 File Offset: 0x000CFC05
		public void RecycleItem(LoopStaggeredGridViewItem item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000D1814 File Offset: 0x000CFC14
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

		// Token: 0x040023FF RID: 9215
		private GameObject mPrefabObj;

		// Token: 0x04002400 RID: 9216
		private string mPrefabName;

		// Token: 0x04002401 RID: 9217
		private int mInitCreateCount = 1;

		// Token: 0x04002402 RID: 9218
		private float mPadding;

		// Token: 0x04002403 RID: 9219
		private List<LoopStaggeredGridViewItem> mTmpPooledItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x04002404 RID: 9220
		private List<LoopStaggeredGridViewItem> mPooledItemList = new List<LoopStaggeredGridViewItem>();

		// Token: 0x04002405 RID: 9221
		private static int mCurItemIdCount;

		// Token: 0x04002406 RID: 9222
		private RectTransform mItemParent;
	}
}
