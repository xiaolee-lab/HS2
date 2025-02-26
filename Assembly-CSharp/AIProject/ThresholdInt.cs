using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F11 RID: 3857
	[Serializable]
	public struct ThresholdInt
	{
		// Token: 0x06007E41 RID: 32321 RVA: 0x0035BF68 File Offset: 0x0035A368
		public ThresholdInt(int minValue, int maxValue)
		{
			this.min = minValue;
			this.max = maxValue;
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06007E42 RID: 32322 RVA: 0x0035BF78 File Offset: 0x0035A378
		public int RandomValue
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(this.min, this.max + 1);
			}
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x0035BF8D File Offset: 0x0035A38D
		public bool IsRange(int value)
		{
			return value >= this.min && value <= this.max;
		}

		// Token: 0x040065E0 RID: 26080
		public int min;

		// Token: 0x040065E1 RID: 26081
		public int max;
	}
}
