using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B6D RID: 2925
	public static class Vector2Extensions
	{
		// Token: 0x06005702 RID: 22274 RVA: 0x0025A95A File Offset: 0x00258D5A
		public static float Min(this Vector2 vec)
		{
			return Mathf.Min(vec.x, vec.y);
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x0025A96F File Offset: 0x00258D6F
		public static float Max(this Vector2 vec)
		{
			return Mathf.Max(vec.x, vec.y);
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x0025A984 File Offset: 0x00258D84
		public static float RandomRange(this Vector2 vec)
		{
			return UnityEngine.Random.Range(vec.x, vec.y);
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x0025A999 File Offset: 0x00258D99
		public static bool Range(this Vector2 vec, float value)
		{
			return vec.x <= value && value <= vec.y;
		}
	}
}
