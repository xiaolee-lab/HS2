using System;

namespace AIProject.Animal
{
	// Token: 0x02000B7F RID: 2943
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5>
	{
		// Token: 0x0600572B RID: 22315 RVA: 0x0025B02D File Offset: 0x0025942D
		public Tuple(T1 i1, T2 i2, T3 i3, T4 i4, T5 i5)
		{
			this.Item1 = i1;
			this.Item2 = i2;
			this.Item3 = i3;
			this.Item4 = i4;
			this.Item5 = i5;
		}

		// Token: 0x04005076 RID: 20598
		public T1 Item1;

		// Token: 0x04005077 RID: 20599
		public T2 Item2;

		// Token: 0x04005078 RID: 20600
		public T3 Item3;

		// Token: 0x04005079 RID: 20601
		public T4 Item4;

		// Token: 0x0400507A RID: 20602
		public T5 Item5;
	}
}
