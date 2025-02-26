using System;

namespace AIProject.Animal
{
	// Token: 0x02000B7D RID: 2941
	[Serializable]
	public class Tuple<T1, T2, T3>
	{
		// Token: 0x06005729 RID: 22313 RVA: 0x0025AFEB File Offset: 0x002593EB
		public Tuple(T1 i1, T2 i2, T3 i3)
		{
			this.Item1 = i1;
			this.Item2 = i2;
			this.Item3 = i3;
		}

		// Token: 0x0400506F RID: 20591
		public T1 Item1;

		// Token: 0x04005070 RID: 20592
		public T2 Item2;

		// Token: 0x04005071 RID: 20593
		public T3 Item3;
	}
}
