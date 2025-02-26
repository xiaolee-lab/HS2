using System;

namespace AIProject.Animal
{
	// Token: 0x02000B7B RID: 2939
	[Serializable]
	public class Tuple<T1>
	{
		// Token: 0x06005727 RID: 22311 RVA: 0x0025AFC6 File Offset: 0x002593C6
		public Tuple(T1 i1)
		{
			this.Item1 = i1;
		}

		// Token: 0x0400506C RID: 20588
		public T1 Item1;
	}
}
