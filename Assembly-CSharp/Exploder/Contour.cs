using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x0200039A RID: 922
	public class Contour
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x0005B03F File Offset: 0x0005943F
		public Contour(int trianglesNum)
		{
			this.AllocateBuffers(trianglesNum);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0005B050 File Offset: 0x00059450
		public void AllocateBuffers(int trianglesNum)
		{
			if (this.lsHash == null || this.lsHash.Capacity() < trianglesNum * 2)
			{
				this.midPoints = new ArrayDictionary<Contour.MidPoint>(trianglesNum * 2);
				this.contour = new List<Dictionary<int, int>>();
				this.lsHash = new LSHash(0.001f, trianglesNum * 2);
			}
			else
			{
				this.lsHash.Clear();
				foreach (Dictionary<int, int> dictionary in this.contour)
				{
					dictionary.Clear();
				}
				this.contour.Clear();
				if (this.midPoints.Size < trianglesNum * 2)
				{
					this.midPoints = new ArrayDictionary<Contour.MidPoint>(trianglesNum * 2);
				}
				else
				{
					this.midPoints.Clear();
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0005B144 File Offset: 0x00059544
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0005B14C File Offset: 0x0005954C
		public int MidPointsCount { get; private set; }

		// Token: 0x0600104E RID: 4174 RVA: 0x0005B158 File Offset: 0x00059558
		public void AddTriangle(int triangleID, int id0, int id1, Vector3 v0, Vector3 v1)
		{
			int num;
			int num2;
			this.lsHash.Hash(v0, v1, out num, out num2);
			if (num == num2)
			{
				return;
			}
			Contour.MidPoint value;
			if (this.midPoints.TryGetValue(num, out value))
			{
				if (value.idNext == 2147483647 && value.idPrev != num2)
				{
					value.idNext = num2;
				}
				else if (value.idPrev == 2147483647 && value.idNext != num2)
				{
					value.idPrev = num2;
				}
				this.midPoints[num] = value;
			}
			else
			{
				this.midPoints.Add(num, new Contour.MidPoint
				{
					id = num,
					vertexId = id0,
					idNext = num2,
					idPrev = int.MaxValue
				});
			}
			if (this.midPoints.TryGetValue(num2, out value))
			{
				if (value.idNext == 2147483647 && value.idPrev != num)
				{
					value.idNext = num;
				}
				else if (value.idPrev == 2147483647 && value.idNext != num)
				{
					value.idPrev = num;
				}
				this.midPoints[num2] = value;
			}
			else
			{
				this.midPoints.Add(num2, new Contour.MidPoint
				{
					id = num2,
					vertexId = id1,
					idPrev = num,
					idNext = int.MaxValue
				});
			}
			this.MidPointsCount = this.midPoints.Count;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0005B2F0 File Offset: 0x000596F0
		public bool FindContours()
		{
			if (this.midPoints.Count == 0)
			{
				return false;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>(this.midPoints.Count);
			int num = this.midPoints.Count * 2;
			Contour.MidPoint firstValue = this.midPoints.GetFirstValue();
			dictionary.Add(firstValue.id, firstValue.vertexId);
			this.midPoints.Remove(firstValue.id);
			int num2 = firstValue.idNext;
			while (this.midPoints.Count > 0)
			{
				if (num2 == 2147483647)
				{
					return false;
				}
				Contour.MidPoint midPoint;
				if (!this.midPoints.TryGetValue(num2, out midPoint))
				{
					this.contour.Clear();
					return false;
				}
				dictionary.Add(midPoint.id, midPoint.vertexId);
				this.midPoints.Remove(midPoint.id);
				if (dictionary.ContainsKey(midPoint.idNext))
				{
					if (dictionary.ContainsKey(midPoint.idPrev))
					{
						this.contour.Add(new Dictionary<int, int>(dictionary));
						dictionary.Clear();
						if (this.midPoints.Count == 0)
						{
							break;
						}
						firstValue = this.midPoints.GetFirstValue();
						dictionary.Add(firstValue.id, firstValue.vertexId);
						this.midPoints.Remove(firstValue.id);
						num2 = firstValue.idNext;
						continue;
					}
					else
					{
						num2 = midPoint.idPrev;
					}
				}
				else
				{
					num2 = midPoint.idNext;
				}
				num--;
				if (num == 0)
				{
					this.contour.Clear();
					return false;
				}
			}
			return true;
		}

		// Token: 0x040011FD RID: 4605
		public List<Dictionary<int, int>> contour;

		// Token: 0x040011FE RID: 4606
		private ArrayDictionary<Contour.MidPoint> midPoints;

		// Token: 0x040011FF RID: 4607
		private LSHash lsHash;

		// Token: 0x0200039B RID: 923
		private struct MidPoint
		{
			// Token: 0x04001201 RID: 4609
			public int id;

			// Token: 0x04001202 RID: 4610
			public int vertexId;

			// Token: 0x04001203 RID: 4611
			public int idNext;

			// Token: 0x04001204 RID: 4612
			public int idPrev;
		}
	}
}
