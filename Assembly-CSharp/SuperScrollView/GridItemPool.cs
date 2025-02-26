using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005F8 RID: 1528
	public class GridItemPool
	{
		// Token: 0x0600235F RID: 9055 RVA: 0x000C3CA0 File Offset: 0x000C20A0
		public void Init(GameObject prefabObj, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopGridViewItem item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000C3D04 File Offset: 0x000C2104
		public LoopGridViewItem GetItem()
		{
			GridItemPool.mCurItemIdCount++;
			LoopGridViewItem loopGridViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopGridViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopGridViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopGridViewItem = this.CreateItem();
				}
				else
				{
					loopGridViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopGridViewItem.gameObject.SetActive(true);
				}
			}
			loopGridViewItem.ItemId = GridItemPool.mCurItemIdCount;
			return loopGridViewItem;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000C3DC0 File Offset: 0x000C21C0
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

		// Token: 0x06002362 RID: 9058 RVA: 0x000C3E14 File Offset: 0x000C2214
		public LoopGridViewItem CreateItem()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopGridViewItem component2 = gameObject.GetComponent<LoopGridViewItem>();
			component2.ItemPrefabName = this.mPrefabName;
			return component2;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000C3E80 File Offset: 0x000C2280
		private void RecycleItemReal(LoopGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000C3E9A File Offset: 0x000C229A
		public void RecycleItem(LoopGridViewItem item)
		{
			item.PrevItem = null;
			item.NextItem = null;
			this.mTmpPooledItemList.Add(item);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000C3EB8 File Offset: 0x000C22B8
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

		// Token: 0x040022F1 RID: 8945
		private GameObject mPrefabObj;

		// Token: 0x040022F2 RID: 8946
		private string mPrefabName;

		// Token: 0x040022F3 RID: 8947
		private int mInitCreateCount = 1;

		// Token: 0x040022F4 RID: 8948
		private List<LoopGridViewItem> mTmpPooledItemList = new List<LoopGridViewItem>();

		// Token: 0x040022F5 RID: 8949
		private List<LoopGridViewItem> mPooledItemList = new List<LoopGridViewItem>();

		// Token: 0x040022F6 RID: 8950
		private static int mCurItemIdCount;

		// Token: 0x040022F7 RID: 8951
		private RectTransform mItemParent;
	}
}
