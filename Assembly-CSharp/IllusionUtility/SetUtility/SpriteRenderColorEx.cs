using System;
using UnityEngine;

namespace IllusionUtility.SetUtility
{
	// Token: 0x020011A4 RID: 4516
	public static class SpriteRenderColorEx
	{
		// Token: 0x0600948D RID: 38029 RVA: 0x003D4BA4 File Offset: 0x003D2FA4
		public static void SetColorR(this SpriteRenderer sr, float r)
		{
			Color color = new Color(r, sr.color.g, sr.color.b, sr.color.a);
			sr.color = color;
		}

		// Token: 0x0600948E RID: 38030 RVA: 0x003D4BEC File Offset: 0x003D2FEC
		public static void SetColorG(this SpriteRenderer sr, float g)
		{
			Color color = new Color(sr.color.r, g, sr.color.b, sr.color.a);
			sr.color = color;
		}

		// Token: 0x0600948F RID: 38031 RVA: 0x003D4C34 File Offset: 0x003D3034
		public static void SetColorB(this SpriteRenderer sr, float b)
		{
			Color color = new Color(sr.color.r, sr.color.g, b, sr.color.a);
			sr.color = color;
		}

		// Token: 0x06009490 RID: 38032 RVA: 0x003D4C7C File Offset: 0x003D307C
		public static void SetColorA(this SpriteRenderer sr, float a)
		{
			Color color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
			sr.color = color;
		}
	}
}
