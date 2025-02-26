using System;

namespace AIProject.Animal
{
	// Token: 0x02000B7E RID: 2942
	[Serializable]
	public class Tuple<T1, T2, T3, T4>
	{
		// Token: 0x0600572A RID: 22314 RVA: 0x0025B008 File Offset: 0x00259408
		public Tuple(T1 i1, T2 i2, T3 i3, T4 i4)
		{
			this.Item1 = i1;
			this.Item2 = i2;
			this.Item3 = i3;
			this.Item4 = i4;
		}

		// Token: 0x04005072 RID: 20594
		public T1 Item1;

		// Token: 0x04005073 RID: 20595
		public T2 Item2;

		// Token: 0x04005074 RID: 20596
		public T3 Item3;

		// Token: 0x04005075 RID: 20597
		public T4 Item4;
	}
}
