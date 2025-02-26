using System;

namespace AIProject
{
	// Token: 0x020008EF RID: 2287
	public class FoodParameterPacket : ParameterPacket
	{
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x0019DE7C File Offset: 0x0019C27C
		// (set) Token: 0x06003EA6 RID: 16038 RVA: 0x0019DE84 File Offset: 0x0019C284
		public Threshold SatiationAscentThreshold { get; set; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x0019DE8D File Offset: 0x0019C28D
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x0019DE95 File Offset: 0x0019C295
		public Threshold SatiationDescentThreshold { get; set; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x0019DE9E File Offset: 0x0019C29E
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x0019DEA6 File Offset: 0x0019C2A6
		public float StomachacheRate { get; set; }
	}
}
