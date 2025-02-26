using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B4E RID: 2894
	public struct ParamRange
	{
		// Token: 0x0600564B RID: 22091 RVA: 0x00258EC4 File Offset: 0x002572C4
		public ParamRange(float _min, float _max, float _limit)
		{
			this.min = _min;
			this.max = _max;
			this.limit = _limit;
			this.min = Mathf.Max(Mathf.Min(_min, _max), 0f);
			this.max = Mathf.Min(Mathf.Max(_min, this.max), this.limit);
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x0600564C RID: 22092 RVA: 0x00258F1A File Offset: 0x0025731A
		public float RandomValue
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(this.min, this.max);
			}
		}

		// Token: 0x04004FA8 RID: 20392
		public float min;

		// Token: 0x04004FA9 RID: 20393
		public float max;

		// Token: 0x04004FAA RID: 20394
		public float limit;
	}
}
