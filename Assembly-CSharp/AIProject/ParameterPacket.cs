using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x020008EE RID: 2286
	public class ParameterPacket
	{
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x0019DE52 File Offset: 0x0019C252
		// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x0019DE5A File Offset: 0x0019C25A
		public float Probability { get; set; }

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003EA2 RID: 16034 RVA: 0x0019DE63 File Offset: 0x0019C263
		// (set) Token: 0x06003EA3 RID: 16035 RVA: 0x0019DE6B File Offset: 0x0019C26B
		public Dictionary<int, TriThreshold> Parameters { get; private set; } = new Dictionary<int, TriThreshold>();
	}
}
