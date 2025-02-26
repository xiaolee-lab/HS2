using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005AE RID: 1454
	public class DataSourceMgr : MonoBehaviour
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000B9AAB File Offset: 0x000B7EAB
		public static DataSourceMgr Get
		{
			get
			{
				if (DataSourceMgr.instance == null)
				{
					DataSourceMgr.instance = UnityEngine.Object.FindObjectOfType<DataSourceMgr>();
				}
				return DataSourceMgr.instance;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000B9ACC File Offset: 0x000B7ECC
		private void Awake()
		{
			this.Init();
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000B9AD4 File Offset: 0x000B7ED4
		public void Init()
		{
			this.DoRefreshDataSource();
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000B9ADC File Offset: 0x000B7EDC
		public ItemData GetItemDataByIndex(int index)
		{
			if (index < 0 || index >= this.mItemDataList.Count)
			{
				return null;
			}
			return this.mItemDataList[index];
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000B9B04 File Offset: 0x000B7F04
		public ItemData GetItemDataById(int itemId)
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.mItemDataList[i].mId == itemId)
				{
					return this.mItemDataList[i];
				}
			}
			return null;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x000B9B54 File Offset: 0x000B7F54
		public int TotalItemCount
		{
			get
			{
				return this.mItemDataList.Count;
			}
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000B9B61 File Offset: 0x000B7F61
		public void RequestRefreshDataList(Action onReflushFinished)
		{
			this.mDataRefreshLeftTime = 1f;
			this.mOnRefreshFinished = onReflushFinished;
			this.mIsWaittingRefreshData = true;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000B9B7C File Offset: 0x000B7F7C
		public void RequestLoadMoreDataList(int loadCount, Action onLoadMoreFinished)
		{
			this.mLoadMoreCount = loadCount;
			this.mDataLoadLeftTime = 1f;
			this.mOnLoadMoreFinished = onLoadMoreFinished;
			this.mIsWaitLoadingMoreData = true;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000B9BA0 File Offset: 0x000B7FA0
		public void Update()
		{
			if (this.mIsWaittingRefreshData)
			{
				this.mDataRefreshLeftTime -= Time.deltaTime;
				if (this.mDataRefreshLeftTime <= 0f)
				{
					this.mIsWaittingRefreshData = false;
					this.DoRefreshDataSource();
					if (this.mOnRefreshFinished != null)
					{
						this.mOnRefreshFinished();
					}
				}
			}
			if (this.mIsWaitLoadingMoreData)
			{
				this.mDataLoadLeftTime -= Time.deltaTime;
				if (this.mDataLoadLeftTime <= 0f)
				{
					this.mIsWaitLoadingMoreData = false;
					this.DoLoadMoreDataSource();
					if (this.mOnLoadMoreFinished != null)
					{
						this.mOnLoadMoreFinished();
					}
				}
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000B9C4D File Offset: 0x000B804D
		public void SetDataTotalCount(int count)
		{
			this.mTotalDataCount = count;
			this.DoRefreshDataSource();
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000B9C5C File Offset: 0x000B805C
		public void ExchangeData(int index1, int index2)
		{
			ItemData value = this.mItemDataList[index1];
			ItemData value2 = this.mItemDataList[index2];
			this.mItemDataList[index1] = value2;
			this.mItemDataList[index2] = value;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000B9C9D File Offset: 0x000B809D
		public void RemoveData(int index)
		{
			this.mItemDataList.RemoveAt(index);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000B9CAB File Offset: 0x000B80AB
		public void InsertData(int index, ItemData data)
		{
			this.mItemDataList.Insert(index, data);
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000B9CBC File Offset: 0x000B80BC
		private void DoRefreshDataSource()
		{
			this.mItemDataList.Clear();
			for (int i = 0; i < this.mTotalDataCount; i++)
			{
				ItemData itemData = new ItemData();
				itemData.mId = i;
				itemData.mName = "Item" + i;
				itemData.mDesc = "Item Desc For Item " + i;
				itemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
				itemData.mStarCount = UnityEngine.Random.Range(0, 6);
				itemData.mFileSize = UnityEngine.Random.Range(20, 999);
				itemData.mChecked = false;
				itemData.mIsExpand = false;
				this.mItemDataList.Add(itemData);
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000B9D78 File Offset: 0x000B8178
		private void DoLoadMoreDataSource()
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < this.mLoadMoreCount; i++)
			{
				int num = i + count;
				ItemData itemData = new ItemData();
				itemData.mId = num;
				itemData.mName = "Item" + num;
				itemData.mDesc = "Item Desc For Item " + num;
				itemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
				itemData.mStarCount = UnityEngine.Random.Range(0, 6);
				itemData.mFileSize = UnityEngine.Random.Range(20, 999);
				itemData.mChecked = false;
				itemData.mIsExpand = false;
				this.mItemDataList.Add(itemData);
			}
			this.mTotalDataCount = this.mItemDataList.Count;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000B9E48 File Offset: 0x000B8248
		public void CheckAllItem()
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemDataList[i].mChecked = true;
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000B9E88 File Offset: 0x000B8288
		public void UnCheckAllItem()
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemDataList[i].mChecked = false;
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000B9EC8 File Offset: 0x000B82C8
		public bool DeleteAllCheckedItem()
		{
			int count = this.mItemDataList.Count;
			this.mItemDataList.RemoveAll((ItemData it) => it.mChecked);
			return count != this.mItemDataList.Count;
		}

		// Token: 0x0400213C RID: 8508
		private List<ItemData> mItemDataList = new List<ItemData>();

		// Token: 0x0400213D RID: 8509
		private Action mOnRefreshFinished;

		// Token: 0x0400213E RID: 8510
		private Action mOnLoadMoreFinished;

		// Token: 0x0400213F RID: 8511
		private int mLoadMoreCount = 20;

		// Token: 0x04002140 RID: 8512
		private float mDataLoadLeftTime;

		// Token: 0x04002141 RID: 8513
		private float mDataRefreshLeftTime;

		// Token: 0x04002142 RID: 8514
		private bool mIsWaittingRefreshData;

		// Token: 0x04002143 RID: 8515
		private bool mIsWaitLoadingMoreData;

		// Token: 0x04002144 RID: 8516
		public int mTotalDataCount = 10000;

		// Token: 0x04002145 RID: 8517
		private static DataSourceMgr instance;
	}
}
