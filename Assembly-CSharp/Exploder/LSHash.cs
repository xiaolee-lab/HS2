using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003A4 RID: 932
	public class LSHash
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x0005C55B File Offset: 0x0005A95B
		public LSHash(float bucketSize, int allocSize)
		{
			this.bucketSize2 = bucketSize * bucketSize;
			this.buckets = new Vector3[allocSize];
			this.count = 0;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0005C57F File Offset: 0x0005A97F
		public int Capacity()
		{
			return this.buckets.Length;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0005C58C File Offset: 0x0005A98C
		public void Clear()
		{
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = Vector3.zero;
			}
			this.count = 0;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0005C5D0 File Offset: 0x0005A9D0
		public int Hash(Vector3 p)
		{
			for (int i = 0; i < this.count; i++)
			{
				Vector3 vector = this.buckets[i];
				float num = p.x - vector.x;
				float num2 = p.y - vector.y;
				float num3 = p.z - vector.z;
				float num4 = num * num + num2 * num2 + num3 * num3;
				if (num4 < this.bucketSize2)
				{
					return i;
				}
			}
			if (this.count >= this.buckets.Length)
			{
				return this.count - 1;
			}
			this.buckets[this.count++] = p;
			return this.count - 1;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0005C69D File Offset: 0x0005AA9D
		public void Hash(Vector3 p0, Vector3 p1, out int hash0, out int hash1)
		{
			hash0 = this.Hash(p0);
			hash1 = this.Hash(p1);
		}

		// Token: 0x04001249 RID: 4681
		private readonly Vector3[] buckets;

		// Token: 0x0400124A RID: 4682
		private readonly float bucketSize2;

		// Token: 0x0400124B RID: 4683
		private int count;
	}
}
