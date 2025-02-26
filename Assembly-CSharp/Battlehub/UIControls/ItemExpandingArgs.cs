using System;
using System.Collections;

namespace Battlehub.UIControls
{
	// Token: 0x0200008F RID: 143
	public class ItemExpandingArgs : EventArgs
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000E45A File Offset: 0x0000C85A
		public ItemExpandingArgs(object item)
		{
			this.Item = item;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000E469 File Offset: 0x0000C869
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000E471 File Offset: 0x0000C871
		public object Item { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000E47A File Offset: 0x0000C87A
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000E482 File Offset: 0x0000C882
		public IEnumerable Children { get; set; }
	}
}
