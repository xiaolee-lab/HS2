using System;
using System.Collections.Generic;
using MessagePack;

namespace AIChara
{
	// Token: 0x020007E7 RID: 2023
	[MessagePackObject(true)]
	public class BlockHeader
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x0012FB46 File Offset: 0x0012DF46
		public BlockHeader()
		{
			this.lstInfo = new List<BlockHeader.Info>();
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x0012FB59 File Offset: 0x0012DF59
		// (set) Token: 0x0600320D RID: 12813 RVA: 0x0012FB61 File Offset: 0x0012DF61
		public List<BlockHeader.Info> lstInfo { get; set; }

		// Token: 0x0600320E RID: 12814 RVA: 0x0012FB6C File Offset: 0x0012DF6C
		public BlockHeader.Info SearchInfo(string name)
		{
			return this.lstInfo.Find((BlockHeader.Info n) => n.name == name);
		}

		// Token: 0x020007E8 RID: 2024
		[MessagePackObject(true)]
		public class Info
		{
			// Token: 0x0600320F RID: 12815 RVA: 0x0012FB9D File Offset: 0x0012DF9D
			public Info()
			{
				this.name = string.Empty;
				this.version = string.Empty;
				this.pos = 0L;
				this.size = 0L;
			}

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x06003210 RID: 12816 RVA: 0x0012FBCB File Offset: 0x0012DFCB
			// (set) Token: 0x06003211 RID: 12817 RVA: 0x0012FBD3 File Offset: 0x0012DFD3
			public string name { get; set; }

			// Token: 0x17000886 RID: 2182
			// (get) Token: 0x06003212 RID: 12818 RVA: 0x0012FBDC File Offset: 0x0012DFDC
			// (set) Token: 0x06003213 RID: 12819 RVA: 0x0012FBE4 File Offset: 0x0012DFE4
			public string version { get; set; }

			// Token: 0x17000887 RID: 2183
			// (get) Token: 0x06003214 RID: 12820 RVA: 0x0012FBED File Offset: 0x0012DFED
			// (set) Token: 0x06003215 RID: 12821 RVA: 0x0012FBF5 File Offset: 0x0012DFF5
			public long pos { get; set; }

			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x06003216 RID: 12822 RVA: 0x0012FBFE File Offset: 0x0012DFFE
			// (set) Token: 0x06003217 RID: 12823 RVA: 0x0012FC06 File Offset: 0x0012E006
			public long size { get; set; }
		}
	}
}
