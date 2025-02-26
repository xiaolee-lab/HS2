using System;

namespace AIProject.Animal
{
	// Token: 0x02000B81 RID: 2945
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7>
	{
		// Token: 0x0600572D RID: 22317 RVA: 0x0025B08F File Offset: 0x0025948F
		public Tuple(T1 i1, T2 i2, T3 i3, T4 i4, T5 i5, T6 i6, T7 i7)
		{
			this.Item1 = i1;
			this.Item2 = i2;
			this.Item3 = i3;
			this.Item4 = i4;
			this.Item5 = i5;
			this.Item6 = i6;
			this.Item7 = i7;
		}

		// Token: 0x04005081 RID: 20609
		public T1 Item1;

		// Token: 0x04005082 RID: 20610
		public T2 Item2;

		// Token: 0x04005083 RID: 20611
		public T3 Item3;

		// Token: 0x04005084 RID: 20612
		public T4 Item4;

		// Token: 0x04005085 RID: 20613
		public T5 Item5;

		// Token: 0x04005086 RID: 20614
		public T6 Item6;

		// Token: 0x04005087 RID: 20615
		public T7 Item7;
	}
}
