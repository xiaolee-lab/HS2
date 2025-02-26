using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B6E RID: 2926
	public static class Vector2IntExtensions
	{
		// Token: 0x06005706 RID: 22278 RVA: 0x0025A9B8 File Offset: 0x00258DB8
		public static int RandomRange(this Vector2Int vec)
		{
			return UnityEngine.Random.Range(vec.x, vec.y + 1);
		}
	}
}
