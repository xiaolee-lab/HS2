using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200116B RID: 4459
public static class Debug
{
	// Token: 0x17001F7A RID: 8058
	// (get) Token: 0x0600932D RID: 37677 RVA: 0x003D0796 File Offset: 0x003CEB96
	public static bool isDebugBuild
	{
		[CompilerGenerated]
		get
		{
			return UnityEngine.Debug.isDebugBuild;
		}
	}

	// Token: 0x0600932E RID: 37678 RVA: 0x003D079D File Offset: 0x003CEB9D
	[Conditional("GAME_DEBUG")]
	public static void Break()
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.Break();
		}
	}

	// Token: 0x0600932F RID: 37679 RVA: 0x003D07AE File Offset: 0x003CEBAE
	[Conditional("GAME_DEBUG")]
	public static void Log(object message)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.Log(message);
		}
	}

	// Token: 0x06009330 RID: 37680 RVA: 0x003D07C0 File Offset: 0x003CEBC0
	[Conditional("GAME_DEBUG")]
	public static void Log(object message, UnityEngine.Object context)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.Log(message, context);
		}
	}

	// Token: 0x06009331 RID: 37681 RVA: 0x003D07D3 File Offset: 0x003CEBD3
	[Conditional("GAME_DEBUG")]
	public static void LogFormat(string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogFormat(format, args);
		}
	}

	// Token: 0x06009332 RID: 37682 RVA: 0x003D07E6 File Offset: 0x003CEBE6
	[Conditional("GAME_DEBUG")]
	public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogFormat(context, format, args);
		}
	}

	// Token: 0x06009333 RID: 37683 RVA: 0x003D07FA File Offset: 0x003CEBFA
	[Conditional("GAME_DEBUG")]
	public static void LogWarning(object message)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogWarning(message);
		}
	}

	// Token: 0x06009334 RID: 37684 RVA: 0x003D080C File Offset: 0x003CEC0C
	[Conditional("GAME_DEBUG")]
	public static void LogWarning(object message, UnityEngine.Object context)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogWarning(message, context);
		}
	}

	// Token: 0x06009335 RID: 37685 RVA: 0x003D081F File Offset: 0x003CEC1F
	[Conditional("GAME_DEBUG")]
	public static void LogWarningFormat(string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogWarningFormat(format, args);
		}
	}

	// Token: 0x06009336 RID: 37686 RVA: 0x003D0832 File Offset: 0x003CEC32
	[Conditional("GAME_DEBUG")]
	public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogWarningFormat(context, format, args);
		}
	}

	// Token: 0x06009337 RID: 37687 RVA: 0x003D0846 File Offset: 0x003CEC46
	[Conditional("GAME_DEBUG")]
	public static void LogError(object message)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogError(message);
		}
	}

	// Token: 0x06009338 RID: 37688 RVA: 0x003D0858 File Offset: 0x003CEC58
	[Conditional("GAME_DEBUG")]
	public static void LogError(object message, UnityEngine.Object context)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogError(message, context);
		}
	}

	// Token: 0x06009339 RID: 37689 RVA: 0x003D086B File Offset: 0x003CEC6B
	[Conditional("GAME_DEBUG")]
	public static void LogErrorFormat(string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogErrorFormat(format, args);
		}
	}

	// Token: 0x0600933A RID: 37690 RVA: 0x003D087E File Offset: 0x003CEC7E
	[Conditional("GAME_DEBUG")]
	public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogErrorFormat(context, format, args);
		}
	}

	// Token: 0x0600933B RID: 37691 RVA: 0x003D0892 File Offset: 0x003CEC92
	[Conditional("GAME_DEBUG")]
	public static void LogException(Exception exception)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogException(exception);
		}
	}

	// Token: 0x0600933C RID: 37692 RVA: 0x003D08A4 File Offset: 0x003CECA4
	[Conditional("GAME_DEBUG")]
	public static void LogException(Exception exception, UnityEngine.Object context)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.LogException(exception, context);
		}
	}

	// Token: 0x0600933D RID: 37693 RVA: 0x003D08B7 File Offset: 0x003CECB7
	[Conditional("GAME_DEBUG")]
	public static void Assert(bool condition)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x0600933E RID: 37694 RVA: 0x003D08C3 File Offset: 0x003CECC3
	[Conditional("GAME_DEBUG")]
	public static void Assert(bool condition, object message)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x0600933F RID: 37695 RVA: 0x003D08CF File Offset: 0x003CECCF
	[Conditional("GAME_DEBUG")]
	public static void Assert(bool condition, object message, UnityEngine.Object context)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009340 RID: 37696 RVA: 0x003D08DB File Offset: 0x003CECDB
	[Conditional("GAME_DEBUG")]
	public static void AssertFormat(bool condition, string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009341 RID: 37697 RVA: 0x003D08E7 File Offset: 0x003CECE7
	[Conditional("GAME_DEBUG")]
	public static void AssertFormat(bool condition, UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009342 RID: 37698 RVA: 0x003D08F3 File Offset: 0x003CECF3
	[Conditional("GAME_DEBUG")]
	public static void LogAssertion(Exception exception)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009343 RID: 37699 RVA: 0x003D08FF File Offset: 0x003CECFF
	[Conditional("GAME_DEBUG")]
	public static void LogAssertion(bool condition, UnityEngine.Object context)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009344 RID: 37700 RVA: 0x003D090B File Offset: 0x003CED0B
	[Conditional("GAME_DEBUG")]
	public static void LogAssertionFormat(string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009345 RID: 37701 RVA: 0x003D0917 File Offset: 0x003CED17
	[Conditional("GAME_DEBUG")]
	public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.isDebugBuild)
		{
		}
	}

	// Token: 0x06009346 RID: 37702 RVA: 0x003D0923 File Offset: 0x003CED23
	[Conditional("GAME_DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawLine(start, end);
		}
	}

	// Token: 0x06009347 RID: 37703 RVA: 0x003D0936 File Offset: 0x003CED36
	[Conditional("GAME_DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawLine(start, end, color);
		}
	}

	// Token: 0x06009348 RID: 37704 RVA: 0x003D094A File Offset: 0x003CED4A
	[Conditional("GAME_DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawLine(start, end, color, duration);
		}
	}

	// Token: 0x06009349 RID: 37705 RVA: 0x003D095F File Offset: 0x003CED5F
	[Conditional("GAME_DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
		}
	}

	// Token: 0x0600934A RID: 37706 RVA: 0x003D0976 File Offset: 0x003CED76
	[Conditional("GAME_DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawRay(start, dir);
		}
	}

	// Token: 0x0600934B RID: 37707 RVA: 0x003D0989 File Offset: 0x003CED89
	[Conditional("GAME_DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawRay(start, dir, color);
		}
	}

	// Token: 0x0600934C RID: 37708 RVA: 0x003D099D File Offset: 0x003CED9D
	[Conditional("GAME_DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawRay(start, dir, color, duration);
		}
	}

	// Token: 0x0600934D RID: 37709 RVA: 0x003D09B2 File Offset: 0x003CEDB2
	[Conditional("GAME_DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest)
	{
		if (global::Debug.isDebugBuild)
		{
			UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
		}
	}

	// Token: 0x04007707 RID: 30471
	private const string DEBUG_MODE = "GAME_DEBUG";
}
