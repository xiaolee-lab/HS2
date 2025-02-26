using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x020008F9 RID: 2297
	public class ValueInfo
	{
		// Token: 0x06003FAA RID: 16298 RVA: 0x0019E068 File Offset: 0x0019C468
		public ValueInfo(float term)
		{
			this.Term = term;
			this._cycle = 0f;
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x0019E082 File Offset: 0x0019C482
		// (set) Token: 0x06003FAC RID: 16300 RVA: 0x0019E08A File Offset: 0x0019C48A
		public float Term { get; set; }

		// Token: 0x06003FAD RID: 16301 RVA: 0x0019E093 File Offset: 0x0019C493
		private int Add()
		{
			return this.Add(1f);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0019E0A0 File Offset: 0x0019C4A0
		private int Add(float speed)
		{
			if (this.Term <= 0f)
			{
				return 0;
			}
			float num = 1f / this.Term;
			if (this._cycle < 1f)
			{
				this._cycle += Time.deltaTime * num * speed;
				return 0;
			}
			this._cycle = 0f;
			return 1;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0019E100 File Offset: 0x0019C500
		public static int operator *(ValueInfo a, float d)
		{
			return a.Add(d);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0019E109 File Offset: 0x0019C509
		public static implicit operator int(ValueInfo a)
		{
			return a.Add();
		}

		// Token: 0x04003C22 RID: 15394
		private float _cycle;
	}
}
