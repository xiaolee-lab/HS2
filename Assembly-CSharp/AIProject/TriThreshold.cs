using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x020008F0 RID: 2288
	public struct TriThreshold
	{
		// Token: 0x06003EAB RID: 16043 RVA: 0x0019DEAF File Offset: 0x0019C2AF
		public TriThreshold(int s, int m, int l)
		{
			this.SThreshold = (float)s;
			this.MThreshold = (float)m;
			this.LThreshold = (float)l;
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0019DEC9 File Offset: 0x0019C2C9
		public TriThreshold(float s, float m, float l)
		{
			this.SThreshold = s;
			this.MThreshold = m;
			this.LThreshold = l;
		}

		// Token: 0x06003EAD RID: 16045 RVA: 0x0019DEE0 File Offset: 0x0019C2E0
		public int Evaluate(float t)
		{
			if (Mathf.Approximately(this.SThreshold, 0f) && Mathf.Approximately(this.MThreshold, 0f) && Mathf.Approximately(this.LThreshold, 0f))
			{
				return 0;
			}
			if (Mathf.Approximately(this.MThreshold, 0f))
			{
				return Mathf.RoundToInt(Mathf.Lerp(this.SThreshold, this.LThreshold, t));
			}
			if (t < 0.5f)
			{
				float t2 = Mathf.InverseLerp(0f, 0.5f, t);
				return (int)Mathf.Lerp(this.SThreshold, this.MThreshold, t2);
			}
			float t3 = Mathf.InverseLerp(0.5f, 1f, t);
			return (int)Mathf.Lerp(this.MThreshold, this.LThreshold, t3);
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x0019DFB0 File Offset: 0x0019C3B0
		public float EvaluateFloat(float t)
		{
			if (Mathf.Approximately(this.MThreshold, 0f) && Mathf.Approximately(this.LThreshold, 0f))
			{
				return this.SThreshold;
			}
			if (Mathf.Approximately(this.LThreshold, 0f))
			{
				return Mathf.Lerp(this.SThreshold, this.LThreshold, t);
			}
			if (t < 0.5f)
			{
				float t2 = Mathf.InverseLerp(0f, 0.5f, t);
				return Mathf.Lerp(this.SThreshold, this.MThreshold, t2);
			}
			float t3 = Mathf.InverseLerp(0.5f, 1f, t);
			return Mathf.Lerp(this.MThreshold, this.LThreshold, t3);
		}

		// Token: 0x04003BA7 RID: 15271
		public float SThreshold;

		// Token: 0x04003BA8 RID: 15272
		public float MThreshold;

		// Token: 0x04003BA9 RID: 15273
		public float LThreshold;
	}
}
