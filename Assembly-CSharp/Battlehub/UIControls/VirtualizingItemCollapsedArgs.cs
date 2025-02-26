using System;

namespace Battlehub.UIControls
{
	// Token: 0x020000A8 RID: 168
	public class VirtualizingItemCollapsedArgs : EventArgs
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x000168FE File Offset: 0x00014CFE
		public VirtualizingItemCollapsedArgs(object item)
		{
			this.Item = item;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001690D File Offset: 0x00014D0D
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00016915 File Offset: 0x00014D15
		public object Item { get; private set; }
	}
}
