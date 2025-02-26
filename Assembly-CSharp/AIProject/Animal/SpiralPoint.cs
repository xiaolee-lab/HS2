using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B93 RID: 2963
	public class SpiralPoint
	{
		// Token: 0x06005844 RID: 22596 RVA: 0x0025F445 File Offset: 0x0025D845
		public SpiralPoint(int limit)
		{
			this.Clear();
			this.Limit = limit;
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x0025F47C File Offset: 0x0025D87C
		public void Clear()
		{
			this.Vector = Vector2Int.left;
			this.Count = (this.Current = Vector2Int.zero);
			this.Counter = (this.Limit = 0);
			this.End = false;
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x0025F4C0 File Offset: 0x0025D8C0
		public Vector2Int Next()
		{
			if (this.Count.x == this.Count.y && this.Count.x == this.Counter)
			{
				this.RotaRight();
				this.Count = Vector2Int.zero;
				this.Counter++;
				if (this.Limit == this.Counter)
				{
					this.End = true;
				}
			}
			int y = this.Count.y;
			this.Current += this.Vector;
			this.Count.x = this.Count.x + Mathf.Abs(this.Vector.x);
			this.Count.y = this.Count.y + Mathf.Abs(this.Vector.y);
			if (this.Counter != y && this.Counter == this.Count.y)
			{
				this.RotaRight();
			}
			return this.Current;
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x0025F5CC File Offset: 0x0025D9CC
		private void RotaRight()
		{
			if (this.Vector.x != 0)
			{
				this.Vector.y = -this.Vector.x;
				this.Vector.x = 0;
			}
			else
			{
				this.Vector.x = this.Vector.y;
				this.Vector.y = 0;
			}
		}

		// Token: 0x04005100 RID: 20736
		public Vector2Int Vector = Vector2Int.left;

		// Token: 0x04005101 RID: 20737
		public Vector2Int Count = Vector2Int.zero;

		// Token: 0x04005102 RID: 20738
		public Vector2Int Current = Vector2Int.zero;

		// Token: 0x04005103 RID: 20739
		public int Counter;

		// Token: 0x04005104 RID: 20740
		public int Limit;

		// Token: 0x04005105 RID: 20741
		public bool End;
	}
}
