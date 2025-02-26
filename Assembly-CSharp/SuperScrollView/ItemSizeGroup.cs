using System;

namespace SuperScrollView
{
	// Token: 0x020005F5 RID: 1525
	public class ItemSizeGroup
	{
		// Token: 0x06002340 RID: 9024 RVA: 0x000C313E File Offset: 0x000C153E
		public ItemSizeGroup(int index, float itemDefaultSize)
		{
			this.mGroupIndex = index;
			this.mItemDefaultSize = itemDefaultSize;
			this.Init();
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000C3164 File Offset: 0x000C1564
		public void Init()
		{
			this.mItemSizeArray = new float[100];
			if (this.mItemDefaultSize != 0f)
			{
				for (int i = 0; i < this.mItemSizeArray.Length; i++)
				{
					this.mItemSizeArray[i] = this.mItemDefaultSize;
				}
			}
			this.mItemStartPosArray = new float[100];
			this.mItemStartPosArray[0] = 0f;
			this.mItemCount = 100;
			this.mGroupSize = this.mItemDefaultSize * (float)this.mItemSizeArray.Length;
			if (this.mItemDefaultSize != 0f)
			{
				this.mDirtyBeginIndex = 0;
			}
			else
			{
				this.mDirtyBeginIndex = 100;
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000C3211 File Offset: 0x000C1611
		public float GetItemStartPos(int index)
		{
			return this.mGroupStartPos + this.mItemStartPosArray[index];
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000C3222 File Offset: 0x000C1622
		public bool IsDirty
		{
			get
			{
				return this.mDirtyBeginIndex < this.mItemCount;
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000C3234 File Offset: 0x000C1634
		public float SetItemSize(int index, float size)
		{
			if (index > this.mMaxNoZeroIndex && size > 0f)
			{
				this.mMaxNoZeroIndex = index;
			}
			float num = this.mItemSizeArray[index];
			if (num == size)
			{
				return 0f;
			}
			this.mItemSizeArray[index] = size;
			if (index < this.mDirtyBeginIndex)
			{
				this.mDirtyBeginIndex = index;
			}
			float num2 = size - num;
			this.mGroupSize += num2;
			return num2;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000C32A4 File Offset: 0x000C16A4
		public void SetItemCount(int count)
		{
			if (count < this.mMaxNoZeroIndex)
			{
				this.mMaxNoZeroIndex = count;
			}
			if (this.mItemCount == count)
			{
				return;
			}
			this.mItemCount = count;
			this.RecalcGroupSize();
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000C32D4 File Offset: 0x000C16D4
		public void RecalcGroupSize()
		{
			this.mGroupSize = 0f;
			for (int i = 0; i < this.mItemCount; i++)
			{
				this.mGroupSize += this.mItemSizeArray[i];
			}
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000C3318 File Offset: 0x000C1718
		public int GetItemIndexByPos(float pos)
		{
			if (this.mItemCount == 0)
			{
				return -1;
			}
			int i = 0;
			int num = this.mItemCount - 1;
			if (this.mItemDefaultSize == 0f)
			{
				if (this.mMaxNoZeroIndex < 0)
				{
					this.mMaxNoZeroIndex = 0;
				}
				num = this.mMaxNoZeroIndex;
			}
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				float num3 = this.mItemStartPosArray[num2];
				float num4 = num3 + this.mItemSizeArray[num2];
				if (num3 <= pos && num4 >= pos)
				{
					return num2;
				}
				if (pos > num4)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000C33B8 File Offset: 0x000C17B8
		public void UpdateAllItemStartPos()
		{
			if (this.mDirtyBeginIndex >= this.mItemCount)
			{
				return;
			}
			int num = (this.mDirtyBeginIndex >= 1) ? this.mDirtyBeginIndex : 1;
			for (int i = num; i < this.mItemCount; i++)
			{
				this.mItemStartPosArray[i] = this.mItemStartPosArray[i - 1] + this.mItemSizeArray[i - 1];
			}
			this.mDirtyBeginIndex = this.mItemCount;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000C3430 File Offset: 0x000C1830
		public void ClearOldData()
		{
			for (int i = this.mItemCount; i < 100; i++)
			{
				this.mItemSizeArray[i] = 0f;
			}
		}

		// Token: 0x040022DD RID: 8925
		public float[] mItemSizeArray;

		// Token: 0x040022DE RID: 8926
		public float[] mItemStartPosArray;

		// Token: 0x040022DF RID: 8927
		public int mItemCount;

		// Token: 0x040022E0 RID: 8928
		private int mDirtyBeginIndex = 100;

		// Token: 0x040022E1 RID: 8929
		public float mGroupSize;

		// Token: 0x040022E2 RID: 8930
		public float mGroupStartPos;

		// Token: 0x040022E3 RID: 8931
		public float mGroupEndPos;

		// Token: 0x040022E4 RID: 8932
		public int mGroupIndex;

		// Token: 0x040022E5 RID: 8933
		private float mItemDefaultSize;

		// Token: 0x040022E6 RID: 8934
		private int mMaxNoZeroIndex;
	}
}
