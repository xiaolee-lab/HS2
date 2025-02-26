using System;
using System.Collections.Generic;
using MessagePack;

namespace OutputLogControl
{
	// Token: 0x0200083C RID: 2108
	[MessagePackObject(true)]
	public class LogInfo
	{
		// Token: 0x060035D7 RID: 13783 RVA: 0x0013D60B File Offset: 0x0013BA0B
		public LogInfo()
		{
			this.tag = "OutputLog";
			this.version = new Version("1.0.0");
			this.dictLog = new Dictionary<string, List<LogData>>();
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x0013D639 File Offset: 0x0013BA39
		// (set) Token: 0x060035D9 RID: 13785 RVA: 0x0013D641 File Offset: 0x0013BA41
		public string tag { get; set; }

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x0013D64A File Offset: 0x0013BA4A
		// (set) Token: 0x060035DB RID: 13787 RVA: 0x0013D652 File Offset: 0x0013BA52
		public Version version { get; set; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060035DC RID: 13788 RVA: 0x0013D65B File Offset: 0x0013BA5B
		// (set) Token: 0x060035DD RID: 13789 RVA: 0x0013D663 File Offset: 0x0013BA63
		public Dictionary<string, List<LogData>> dictLog { get; set; }

		// Token: 0x04003632 RID: 13874
		public const string LogTag = "OutputLog";

		// Token: 0x04003633 RID: 13875
		private const string LogVersion = "1.0.0";
	}
}
