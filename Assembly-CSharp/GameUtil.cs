using System;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class GameUtil
{
	// Token: 0x060015CF RID: 5583 RVA: 0x0008686B File Offset: 0x00084C6B
	public static void Log(string format, params object[] args)
	{
		GameUtil._log = string.Format(format, args) + "\r\n" + GameUtil._log;
		GameUtil._logPosition.y = 0f;
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x00086897 File Offset: 0x00084C97
	public static float Clamp(float value, float min, float max)
	{
		return Math.Max(Math.Min(value, max), min);
	}

	// Token: 0x040018B7 RID: 6327
	public static string _log = string.Empty;

	// Token: 0x040018B8 RID: 6328
	public static Vector2 _logPosition = Vector2.zero;

	// Token: 0x040018B9 RID: 6329
	public static int FontSize = 16;
}
