using System;

namespace Housing
{
	// Token: 0x020008CB RID: 2251
	public class CanhangeArgs : EventArgs
	{
		// Token: 0x06003AE5 RID: 15077 RVA: 0x0015805D File Offset: 0x0015645D
		public CanhangeArgs(bool _flag)
		{
			this.Can = _flag;
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x0015806C File Offset: 0x0015646C
		// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x00158074 File Offset: 0x00156474
		public bool Can { get; private set; }
	}
}
