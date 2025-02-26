using System;

namespace AIProject
{
	// Token: 0x02000900 RID: 2304
	public struct ActionInfo
	{
		// Token: 0x06003FE3 RID: 16355 RVA: 0x0019E55C File Offset: 0x0019C95C
		public ActionInfo(bool hasAction_, int randCount)
		{
			this.hasAction = hasAction_;
			this.randomCount = randCount;
		}

		// Token: 0x04003C3F RID: 15423
		public readonly bool hasAction;

		// Token: 0x04003C40 RID: 15424
		public readonly int randomCount;
	}
}
