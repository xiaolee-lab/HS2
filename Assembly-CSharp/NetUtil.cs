using System;

// Token: 0x02000476 RID: 1142
public static class NetUtil
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600152A RID: 5418 RVA: 0x00083B3F File Offset: 0x00081F3F
	// (set) Token: 0x0600152B RID: 5419 RVA: 0x00083B46 File Offset: 0x00081F46
	public static NetLogHandler LogHandler { get; set; }

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600152C RID: 5420 RVA: 0x00083B4E File Offset: 0x00081F4E
	// (set) Token: 0x0600152D RID: 5421 RVA: 0x00083B55 File Offset: 0x00081F55
	public static NetLogHandler LogErrorHandler { get; set; }

	// Token: 0x0600152E RID: 5422 RVA: 0x00083B5D File Offset: 0x00081F5D
	public static void Log(string fmt, params object[] args)
	{
		if (NetUtil.LogHandler != null)
		{
			NetUtil.LogHandler(fmt, args);
		}
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x00083B75 File Offset: 0x00081F75
	public static void LogError(string fmt, params object[] args)
	{
		if (NetUtil.LogErrorHandler != null)
		{
			NetUtil.LogErrorHandler(fmt, args);
		}
	}
}
