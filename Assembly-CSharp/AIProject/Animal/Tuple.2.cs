using System;

namespace AIProject.Animal
{
	// Token: 0x02000B7C RID: 2940
	[Serializable]
	public class Tuple<T1, T2>
	{
		// Token: 0x06005728 RID: 22312 RVA: 0x0025AFD5 File Offset: 0x002593D5
		public Tuple(T1 i1, T2 i2)
		{
			this.Item1 = i1;
			this.Item2 = i2;
		}

		// Token: 0x0400506D RID: 20589
		public T1 Item1;

		// Token: 0x0400506E RID: 20590
		public T2 Item2;
	}
}
