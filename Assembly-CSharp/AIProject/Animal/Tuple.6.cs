using System;

namespace AIProject.Animal
{
	// Token: 0x02000B80 RID: 2944
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6>
	{
		// Token: 0x0600572C RID: 22316 RVA: 0x0025B05A File Offset: 0x0025945A
		public Tuple(T1 i1, T2 i2, T3 i3, T4 i4, T5 i5, T6 i6)
		{
			this.Item1 = i1;
			this.Item2 = i2;
			this.Item3 = i3;
			this.Item4 = i4;
			this.Item5 = i5;
			this.Item6 = i6;
		}

		// Token: 0x0400507B RID: 20603
		public T1 Item1;

		// Token: 0x0400507C RID: 20604
		public T2 Item2;

		// Token: 0x0400507D RID: 20605
		public T3 Item3;

		// Token: 0x0400507E RID: 20606
		public T4 Item4;

		// Token: 0x0400507F RID: 20607
		public T5 Item5;

		// Token: 0x04005080 RID: 20608
		public T6 Item6;
	}
}
