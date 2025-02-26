using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

// Token: 0x0200046E RID: 1134
public class LogUtil
{
	// Token: 0x060014E8 RID: 5352 RVA: 0x00082E03 File Offset: 0x00081203
	public static string CombinePaths(params string[] paths)
	{
		if (paths == null)
		{
			throw new ArgumentNullException("paths");
		}
		if (LogUtil.<>f__mg$cache0 == null)
		{
			LogUtil.<>f__mg$cache0 = new Func<string, string, string>(Path.Combine);
		}
		return paths.Aggregate(LogUtil.<>f__mg$cache0);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x00082E39 File Offset: 0x00081239
	public static string FormatDateAsFileNameString(DateTime dt)
	{
		return string.Format("{0:0000}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x00082E69 File Offset: 0x00081269
	public static string FormatTimeAsFileNameString(DateTime dt)
	{
		return string.Format("{0:00}-{1:00}-{2:00}", dt.Hour, dt.Minute, dt.Second);
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x00082E99 File Offset: 0x00081299
	public static void PushInMemoryException(string exception)
	{
		LogUtil.InMemoryExceptions.Add(exception);
		while (LogUtil.InMemoryExceptions.Count > LogUtil.InMemoryItemMaxCount)
		{
			LogUtil.InMemoryExceptions.RemoveAt(0);
		}
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x00082ECA File Offset: 0x000812CA
	public static void PushInMemoryError(string error)
	{
		LogUtil.InMemoryErrors.Add(error);
		while (LogUtil.InMemoryErrors.Count > LogUtil.InMemoryItemMaxCount)
		{
			LogUtil.InMemoryErrors.RemoveAt(0);
		}
	}

	// Token: 0x0400180C RID: 6156
	public static bool EnableInMemoryStorage = false;

	// Token: 0x0400180D RID: 6157
	public static int InMemoryItemMaxCount = 3;

	// Token: 0x0400180E RID: 6158
	public static List<string> InMemoryExceptions = new List<string>();

	// Token: 0x0400180F RID: 6159
	public static List<string> InMemoryErrors = new List<string>();

	// Token: 0x04001810 RID: 6160
	[CompilerGenerated]
	private static Func<string, string, string> <>f__mg$cache0;
}
