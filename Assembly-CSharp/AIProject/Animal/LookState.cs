using System;

namespace AIProject.Animal
{
	// Token: 0x02000B4B RID: 2891
	public struct LookState
	{
		// Token: 0x06005640 RID: 22080 RVA: 0x00258E1F File Offset: 0x0025721F
		public LookState(int _ptnNo, bool _waitFlag)
		{
			this.ptnNo = _ptnNo;
			this.waitFlag = _waitFlag;
		}

		// Token: 0x04004F9C RID: 20380
		public int ptnNo;

		// Token: 0x04004F9D RID: 20381
		public bool waitFlag;
	}
}
