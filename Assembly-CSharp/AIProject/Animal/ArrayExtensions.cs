using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B74 RID: 2932
	public static class ArrayExtensions
	{
		// Token: 0x0600570C RID: 22284 RVA: 0x0025AAD8 File Offset: 0x00258ED8
		public static T Rand<T>(this T[] source)
		{
			return (!source.IsNullOrEmpty<T>()) ? source[UnityEngine.Random.Range(0, source.Length)] : default(T);
		}
	}
}
