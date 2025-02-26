using System;

namespace SuperScrollView
{
	// Token: 0x020005F7 RID: 1527
	public class GridItemGroup
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000C38B2 File Offset: 0x000C1CB2
		public int Count
		{
			get
			{
				return this.mCount;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000C38BA File Offset: 0x000C1CBA
		public LoopGridViewItem First
		{
			get
			{
				return this.mFirst;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000C38C2 File Offset: 0x000C1CC2
		public LoopGridViewItem Last
		{
			get
			{
				return this.mLast;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000C38CA File Offset: 0x000C1CCA
		// (set) Token: 0x06002355 RID: 9045 RVA: 0x000C38D2 File Offset: 0x000C1CD2
		public int GroupIndex
		{
			get
			{
				return this.mGroupIndex;
			}
			set
			{
				this.mGroupIndex = value;
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000C38DC File Offset: 0x000C1CDC
		public LoopGridViewItem GetItemByColumn(int column)
		{
			LoopGridViewItem nextItem = this.mFirst;
			while (nextItem != null)
			{
				if (nextItem.Column == column)
				{
					return nextItem;
				}
				nextItem = nextItem.NextItem;
			}
			return null;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000C3918 File Offset: 0x000C1D18
		public LoopGridViewItem GetItemByRow(int row)
		{
			LoopGridViewItem nextItem = this.mFirst;
			while (nextItem != null)
			{
				if (nextItem.Row == row)
				{
					return nextItem;
				}
				nextItem = nextItem.NextItem;
			}
			return null;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000C3954 File Offset: 0x000C1D54
		public void ReplaceItem(LoopGridViewItem curItem, LoopGridViewItem newItem)
		{
			newItem.PrevItem = curItem.PrevItem;
			newItem.NextItem = curItem.NextItem;
			if (newItem.PrevItem != null)
			{
				newItem.PrevItem.NextItem = newItem;
			}
			if (newItem.NextItem != null)
			{
				newItem.NextItem.PrevItem = newItem;
			}
			if (this.mFirst == curItem)
			{
				this.mFirst = newItem;
			}
			if (this.mLast == curItem)
			{
				this.mLast = newItem;
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000C39E4 File Offset: 0x000C1DE4
		public void AddFirst(LoopGridViewItem newItem)
		{
			newItem.PrevItem = null;
			newItem.NextItem = null;
			if (this.mFirst == null)
			{
				this.mFirst = newItem;
				this.mLast = newItem;
				this.mFirst.PrevItem = null;
				this.mFirst.NextItem = null;
				this.mCount++;
			}
			else
			{
				this.mFirst.PrevItem = newItem;
				newItem.PrevItem = null;
				newItem.NextItem = this.mFirst;
				this.mFirst = newItem;
				this.mCount++;
			}
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000C3A80 File Offset: 0x000C1E80
		public void AddLast(LoopGridViewItem newItem)
		{
			newItem.PrevItem = null;
			newItem.NextItem = null;
			if (this.mFirst == null)
			{
				this.mFirst = newItem;
				this.mLast = newItem;
				this.mFirst.PrevItem = null;
				this.mFirst.NextItem = null;
				this.mCount++;
			}
			else
			{
				this.mLast.NextItem = newItem;
				newItem.PrevItem = this.mLast;
				newItem.NextItem = null;
				this.mLast = newItem;
				this.mCount++;
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000C3B1C File Offset: 0x000C1F1C
		public LoopGridViewItem RemoveFirst()
		{
			LoopGridViewItem result = this.mFirst;
			if (this.mFirst == null)
			{
				return result;
			}
			if (this.mFirst == this.mLast)
			{
				this.mFirst = null;
				this.mLast = null;
				this.mCount--;
				return result;
			}
			this.mFirst = this.mFirst.NextItem;
			this.mFirst.PrevItem = null;
			this.mCount--;
			return result;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000C3BA4 File Offset: 0x000C1FA4
		public LoopGridViewItem RemoveLast()
		{
			LoopGridViewItem result = this.mLast;
			if (this.mFirst == null)
			{
				return result;
			}
			if (this.mFirst == this.mLast)
			{
				this.mFirst = null;
				this.mLast = null;
				this.mCount--;
				return result;
			}
			this.mLast = this.mLast.PrevItem;
			this.mLast.NextItem = null;
			this.mCount--;
			return result;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000C3C2C File Offset: 0x000C202C
		public void Clear()
		{
			LoopGridViewItem nextItem = this.mFirst;
			while (nextItem != null)
			{
				nextItem.PrevItem = null;
				nextItem.NextItem = null;
				nextItem = nextItem.NextItem;
			}
			this.mFirst = null;
			this.mLast = null;
			this.mCount = 0;
		}

		// Token: 0x040022ED RID: 8941
		private int mCount;

		// Token: 0x040022EE RID: 8942
		private int mGroupIndex = -1;

		// Token: 0x040022EF RID: 8943
		private LoopGridViewItem mFirst;

		// Token: 0x040022F0 RID: 8944
		private LoopGridViewItem mLast;
	}
}
