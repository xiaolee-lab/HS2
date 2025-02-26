using System;
using System.Collections;

namespace Battlehub.UIControls
{
	// Token: 0x020000A7 RID: 167
	public class VirtualizingItemExpandingArgs : EventArgs
	{
		// Token: 0x060003AC RID: 940 RVA: 0x000168BC File Offset: 0x00014CBC
		public VirtualizingItemExpandingArgs(object item)
		{
			this.Item = item;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000168CB File Offset: 0x00014CCB
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000168D3 File Offset: 0x00014CD3
		public object Item { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000168DC File Offset: 0x00014CDC
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000168E4 File Offset: 0x00014CE4
		public IEnumerable Children { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000168ED File Offset: 0x00014CED
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000168F5 File Offset: 0x00014CF5
		public IEnumerable ChildrenExpand { get; set; }
	}
}
