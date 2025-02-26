using System;
using UnityEngine;

// Token: 0x0200119A RID: 4506
public static class ScreenInfo
{
	// Token: 0x0600945D RID: 37981 RVA: 0x003D3B46 File Offset: 0x003D1F46
	public static float GetBaseScreenWidth()
	{
		return 1280f;
	}

	// Token: 0x0600945E RID: 37982 RVA: 0x003D3B4D File Offset: 0x003D1F4D
	public static float GetBaseScreenHeight()
	{
		return 720f;
	}

	// Token: 0x0600945F RID: 37983 RVA: 0x003D3B54 File Offset: 0x003D1F54
	public static Vector2 GetScreenSize()
	{
		Vector2 result = new Vector2((float)Screen.width, (float)Screen.height);
		return result;
	}

	// Token: 0x06009460 RID: 37984 RVA: 0x003D3B78 File Offset: 0x003D1F78
	public static float GetSpriteRate()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		return 1f / (3.6f * (num2 / (720f * (num / 1280f))));
	}

	// Token: 0x06009461 RID: 37985 RVA: 0x003D3BB0 File Offset: 0x003D1FB0
	public static float GetSpriteCorrectY()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		return (num2 - num / 1280f * 720f) * (2f / num2) * 0.5f;
	}

	// Token: 0x06009462 RID: 37986 RVA: 0x003D3BE8 File Offset: 0x003D1FE8
	public static float GetScreenRate()
	{
		float num = (float)Screen.width;
		return num / 1280f;
	}

	// Token: 0x06009463 RID: 37987 RVA: 0x003D3C04 File Offset: 0x003D2004
	public static float GetScreenCorrectY()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		return (num2 - num / 1280f * 720f) * 0.5f;
	}
}
