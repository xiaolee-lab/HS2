using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F10 RID: 3856
	[Serializable]
	public struct Threshold
	{
		// Token: 0x06007E3D RID: 32317 RVA: 0x0035BF14 File Offset: 0x0035A314
		public Threshold(float minValue, float maxValue)
		{
			this.min = minValue;
			this.max = maxValue;
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06007E3E RID: 32318 RVA: 0x0035BF24 File Offset: 0x0035A324
		public float RandomValue
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(this.min, this.max);
			}
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x0035BF37 File Offset: 0x0035A337
		public float Lerp(float t)
		{
			return Mathf.Lerp(this.min, this.max, t);
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x0035BF4B File Offset: 0x0035A34B
		public bool IsRange(float value)
		{
			return value >= this.min && value <= this.max;
		}

		// Token: 0x040065DE RID: 26078
		public float min;

		// Token: 0x040065DF RID: 26079
		public float max;
	}
}
