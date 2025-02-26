using System;
using MessagePack;

namespace OutputLogControl
{
	// Token: 0x0200083B RID: 2107
	[MessagePackObject(true)]
	public class LogData
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x0013D5D8 File Offset: 0x0013B9D8
		// (set) Token: 0x060035D2 RID: 13778 RVA: 0x0013D5E0 File Offset: 0x0013B9E0
		public int type { get; set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x0013D5E9 File Offset: 0x0013B9E9
		// (set) Token: 0x060035D4 RID: 13780 RVA: 0x0013D5F1 File Offset: 0x0013B9F1
		public string time { get; set; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x0013D5FA File Offset: 0x0013B9FA
		// (set) Token: 0x060035D6 RID: 13782 RVA: 0x0013D602 File Offset: 0x0013BA02
		public string msg { get; set; }
	}
}
