using System;

namespace AIProject.Animal
{
	// Token: 0x02000B82 RID: 2946
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8>
	{
		// Token: 0x0600572E RID: 22318 RVA: 0x0025B0CC File Offset: 0x002594CC
		public Tuple(T1 i1, T2 i2, T3 i3, T4 i4, T5 i5, T6 i6, T7 i7, T8 i8)
		{
			this.Item1 = i1;
			this.Item2 = i2;
			this.Item3 = i3;
			this.Item4 = i4;
			this.Item5 = i5;
			this.Item6 = i6;
			this.Item7 = i7;
			this.Item8 = i8;
		}

		// Token: 0x04005088 RID: 20616
		public T1 Item1;

		// Token: 0x04005089 RID: 20617
		public T2 Item2;

		// Token: 0x0400508A RID: 20618
		public T3 Item3;

		// Token: 0x0400508B RID: 20619
		public T4 Item4;

		// Token: 0x0400508C RID: 20620
		public T5 Item5;

		// Token: 0x0400508D RID: 20621
		public T6 Item6;

		// Token: 0x0400508E RID: 20622
		public T7 Item7;

		// Token: 0x0400508F RID: 20623
		public T8 Item8;
	}
}
