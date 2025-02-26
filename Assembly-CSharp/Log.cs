using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public static class Log
{
	// Token: 0x060014C4 RID: 5316 RVA: 0x00082520 File Offset: 0x00080920
	public static void Info(object msg, params object[] args)
	{
		if (Log.LogLevel >= LogLevel.Info)
		{
		}
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x0008252D File Offset: 0x0008092D
	public static void InfoEx(object msg, UnityEngine.Object context)
	{
		if (Log.LogLevel >= LogLevel.Info)
		{
		}
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x0008253A File Offset: 0x0008093A
	public static void Warning(object msg, params object[] args)
	{
		if (Log.LogLevel >= LogLevel.Warning)
		{
		}
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x00082547 File Offset: 0x00080947
	public static void Error(object msg, params object[] args)
	{
		if (Log.LogLevel >= LogLevel.Error)
		{
		}
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x00082554 File Offset: 0x00080954
	public static void Exception(Exception ex)
	{
		if (Log.LogLevel >= LogLevel.Error)
		{
		}
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x00082561 File Offset: 0x00080961
	public static void Assert(bool condition)
	{
		if (Log.LogLevel >= LogLevel.Error)
		{
			Log.Assert(condition, string.Empty, true);
		}
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x0008257A File Offset: 0x0008097A
	public static void Assert(bool condition, string assertString)
	{
		if (Log.LogLevel >= LogLevel.Error)
		{
			Log.Assert(condition, assertString, false);
		}
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x0008258F File Offset: 0x0008098F
	public static void Assert(bool condition, string assertString, bool pauseOnFail)
	{
		if (condition || Log.LogLevel < LogLevel.Error || pauseOnFail)
		{
		}
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000825A8 File Offset: 0x000809A8
	private static object _format(object msg, params object[] args)
	{
		string text = msg as string;
		if (args.Length == 0 || string.IsNullOrEmpty(text))
		{
			return msg;
		}
		return string.Format(text, args);
	}

	// Token: 0x040017F1 RID: 6129
	public static LogLevel LogLevel = LogLevel.Info;
}
