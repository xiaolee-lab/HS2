using System;

namespace ADV
{
	// Token: 0x020006C0 RID: 1728
	public class ADVData : IData
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000F1995 File Offset: 0x000EFD95
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x000F199D File Offset: 0x000EFD9D
		public OpenData openData { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x000F19A6 File Offset: 0x000EFDA6
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x000F19AE File Offset: 0x000EFDAE
		public IPack pack { get; set; }
	}
}
