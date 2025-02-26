using System;

// Token: 0x020004B3 RID: 1203
public class SysUtil
{
	// Token: 0x06001637 RID: 5687 RVA: 0x0008898E File Offset: 0x00086D8E
	public static string FormatDateAsFileNameString(DateTime dt)
	{
		return string.Format("{0:0000}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000889BE File Offset: 0x00086DBE
	public static string FormatTimeAsFileNameString(DateTime dt)
	{
		return string.Format("{0:00}-{1:00}-{2:00}", dt.Hour, dt.Minute, dt.Second);
	}
}
