using System;
using UnityEngine;

namespace PlayfulSystems
{
	// Token: 0x0200064F RID: 1615
	public static class Utils
	{
		// Token: 0x0600264E RID: 9806 RVA: 0x000DA17C File Offset: 0x000D857C
		public static float EaseSinInOut(float lerp, float start, float change)
		{
			return -change / 2f * (Mathf.Cos(3.1415927f * lerp) - 1f) + start;
		}
	}
}
