using System;

namespace SuperScrollView
{
	// Token: 0x020005F4 RID: 1524
	public struct RowColumnPair
	{
		// Token: 0x0600233A RID: 9018 RVA: 0x000C3088 File Offset: 0x000C1488
		public RowColumnPair(int row1, int column1)
		{
			this.mRow = row1;
			this.mColumn = column1;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000C3098 File Offset: 0x000C1498
		public bool Equals(RowColumnPair other)
		{
			return this.mRow == other.mRow && this.mColumn == other.mColumn;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000C30BE File Offset: 0x000C14BE
		public static bool operator ==(RowColumnPair a, RowColumnPair b)
		{
			return a.mRow == b.mRow && a.mColumn == b.mColumn;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000C30E6 File Offset: 0x000C14E6
		public static bool operator !=(RowColumnPair a, RowColumnPair b)
		{
			return a.mRow != b.mRow || a.mColumn != b.mColumn;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000C3111 File Offset: 0x000C1511
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000C3114 File Offset: 0x000C1514
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj is RowColumnPair && this.Equals((RowColumnPair)obj);
		}

		// Token: 0x040022DB RID: 8923
		public int mRow;

		// Token: 0x040022DC RID: 8924
		public int mColumn;
	}
}
