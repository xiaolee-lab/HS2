using System;
using System.Collections.Generic;
using PrimitivesPro.ThirdParty.P2T;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003AC RID: 940
	public class Polygon
	{
		// Token: 0x0600109D RID: 4253 RVA: 0x0005FE85 File Offset: 0x0005E285
		public Polygon(Vector2[] pnts)
		{
			this.Points = pnts;
			this.Area = this.GetArea();
			this.holes = new List<Polygon>();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0005FEAC File Offset: 0x0005E2AC
		public float GetArea()
		{
			this.Min.x = float.MaxValue;
			this.Min.y = float.MaxValue;
			this.Max.x = float.MinValue;
			this.Max.y = float.MinValue;
			int num = this.Points.Length;
			float num2 = 0f;
			int num3 = num - 1;
			int i = 0;
			while (i < num)
			{
				Vector2 vector = this.Points[num3];
				Vector2 vector2 = this.Points[i];
				num2 += vector.x * vector2.y - vector2.x * vector.y;
				if (vector.x < this.Min.x)
				{
					this.Min.x = vector.x;
				}
				if (vector.y < this.Min.y)
				{
					this.Min.y = vector.y;
				}
				if (vector.x > this.Max.x)
				{
					this.Max.x = vector.x;
				}
				if (vector.y > this.Max.y)
				{
					this.Max.y = vector.y;
				}
				num3 = i++;
			}
			return num2 * 0.5f;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00060014 File Offset: 0x0005E414
		public bool IsPointInside(Vector3 p)
		{
			int num = this.Points.Length;
			Vector2 vector = this.Points[num - 1];
			bool flag = vector.y >= p.y;
			Vector2 vector2 = Vector2.zero;
			bool flag2 = false;
			for (int i = 0; i < num; i++)
			{
				vector2 = this.Points[i];
				bool flag3 = vector2.y >= p.y;
				if (flag != flag3 && (vector2.y - p.y) * (vector.x - vector2.x) >= (vector2.x - p.x) * (vector.y - vector2.y) == flag3)
				{
					flag2 = !flag2;
				}
				flag = flag3;
				vector = vector2;
			}
			return flag2;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000600F9 File Offset: 0x0005E4F9
		public bool IsPolygonInside(Polygon polygon)
		{
			return this.Area > polygon.Area && this.IsPointInside(polygon.Points[0]);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0006012A File Offset: 0x0005E52A
		public void AddHole(Polygon polygon)
		{
			this.holes.Add(polygon);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00060138 File Offset: 0x0005E538
		public bool Triangulate(Array<int> indicesArray)
		{
			if (this.holes.Count != 0)
			{
				List<PolygonPoint> list = new List<PolygonPoint>(this.Points.Length);
				foreach (Vector2 vector in this.Points)
				{
					list.Add(new PolygonPoint((double)vector.x, (double)vector.y));
				}
				Polygon polygon = new Polygon(list);
				foreach (Polygon polygon2 in this.holes)
				{
					List<PolygonPoint> list2 = new List<PolygonPoint>(polygon2.Points.Length);
					foreach (Vector2 vector2 in polygon2.Points)
					{
						list2.Add(new PolygonPoint((double)vector2.x, (double)vector2.y));
					}
					polygon.AddHole(new Polygon(list2));
				}
				try
				{
					P2T.Triangulate(polygon);
				}
				catch (Exception ex)
				{
					return false;
				}
				int count = polygon.Triangles.Count;
				indicesArray.Initialize(count * 3);
				this.Points = new Vector2[count * 3];
				int num = 0;
				this.Min.x = float.MaxValue;
				this.Min.y = float.MaxValue;
				this.Max.x = float.MinValue;
				this.Max.y = float.MinValue;
				for (int k = 0; k < count; k++)
				{
					indicesArray.Add(num);
					indicesArray.Add(num + 1);
					indicesArray.Add(num + 2);
					this.Points[num + 2].x = (float)polygon.Triangles[k].Points._0.X;
					this.Points[num + 2].y = (float)polygon.Triangles[k].Points._0.Y;
					this.Points[num + 1].x = (float)polygon.Triangles[k].Points._1.X;
					this.Points[num + 1].y = (float)polygon.Triangles[k].Points._1.Y;
					this.Points[num].x = (float)polygon.Triangles[k].Points._2.X;
					this.Points[num].y = (float)polygon.Triangles[k].Points._2.Y;
					for (int l = 0; l < 3; l++)
					{
						if (this.Points[num + l].x < this.Min.x)
						{
							this.Min.x = this.Points[num + l].x;
						}
						if (this.Points[num + l].y < this.Min.y)
						{
							this.Min.y = this.Points[num + l].y;
						}
						if (this.Points[num + l].x > this.Max.x)
						{
							this.Max.x = this.Points[num + l].x;
						}
						if (this.Points[num + l].y > this.Max.y)
						{
							this.Max.y = this.Points[num + l].y;
						}
					}
					num += 3;
				}
				return true;
			}
			indicesArray.Initialize(this.Points.Length * 3);
			int num2 = this.Points.Length;
			if (num2 < 3)
			{
				return true;
			}
			int[] array = new int[num2];
			if (this.Area > 0f)
			{
				for (int m = 0; m < num2; m++)
				{
					array[m] = m;
				}
			}
			else
			{
				for (int n = 0; n < num2; n++)
				{
					array[n] = num2 - 1 - n;
				}
			}
			int num3 = num2;
			int num4 = 2 * num3;
			int num5 = 0;
			int num6 = num3 - 1;
			while (num3 > 2)
			{
				if (num4-- <= 0)
				{
					return true;
				}
				int num7 = num6;
				if (num3 <= num7)
				{
					num7 = 0;
				}
				num6 = num7 + 1;
				if (num3 <= num6)
				{
					num6 = 0;
				}
				int num8 = num6 + 1;
				if (num3 <= num8)
				{
					num8 = 0;
				}
				if (this.Snip(num7, num6, num8, num3, array))
				{
					int data = array[num7];
					int data2 = array[num6];
					int data3 = array[num8];
					indicesArray.Add(data);
					indicesArray.Add(data2);
					indicesArray.Add(data3);
					num5++;
					int num9 = num6;
					for (int num10 = num6 + 1; num10 < num3; num10++)
					{
						array[num9] = array[num10];
						num9++;
					}
					num3--;
					num4 = 2 * num3;
				}
			}
			indicesArray.Reverse();
			return true;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000606E0 File Offset: 0x0005EAE0
		private bool Snip(int u, int v, int w, int n, int[] V)
		{
			Vector2 vector = this.Points[V[u]];
			Vector2 vector2 = this.Points[V[v]];
			Vector2 vector3 = this.Points[V[w]];
			if (Mathf.Epsilon > (vector2.x - vector.x) * (vector3.y - vector.y) - (vector2.y - vector.y) * (vector3.x - vector.x))
			{
				return false;
			}
			for (int i = 0; i < n; i++)
			{
				if (i != u && i != v && i != w)
				{
					Vector2 vector4 = this.Points[V[i]];
					float num = vector3.x - vector2.x;
					float num2 = vector3.y - vector2.y;
					float num3 = vector.x - vector3.x;
					float num4 = vector.y - vector3.y;
					float num5 = vector2.x - vector.x;
					float num6 = vector2.y - vector.y;
					float num7 = vector4.x - vector.x;
					float num8 = vector4.y - vector.y;
					float num9 = vector4.x - vector2.x;
					float num10 = vector4.y - vector2.y;
					float num11 = vector4.x - vector3.x;
					float num12 = vector4.y - vector3.y;
					float num13 = num * num10 - num2 * num9;
					float num14 = num5 * num8 - num6 * num7;
					float num15 = num3 * num12 - num4 * num11;
					bool flag = num13 >= 0f && num15 >= 0f && num14 >= 0f;
					if (flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000608E0 File Offset: 0x0005ECE0
		private bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
		{
			float num = C.x - B.x;
			float num2 = C.y - B.y;
			float num3 = A.x - C.x;
			float num4 = A.y - C.y;
			float num5 = B.x - A.x;
			float num6 = B.y - A.y;
			float num7 = P.x - A.x;
			float num8 = P.y - A.y;
			float num9 = P.x - B.x;
			float num10 = P.y - B.y;
			float num11 = P.x - C.x;
			float num12 = P.y - C.y;
			float num13 = num * num10 - num2 * num9;
			float num14 = num5 * num8 - num6 * num7;
			float num15 = num3 * num12 - num4 * num11;
			return num13 >= 0f && num15 >= 0f && num14 >= 0f;
		}

		// Token: 0x04001282 RID: 4738
		public Vector2[] Points;

		// Token: 0x04001283 RID: 4739
		public readonly float Area;

		// Token: 0x04001284 RID: 4740
		public Vector2 Min;

		// Token: 0x04001285 RID: 4741
		public Vector2 Max;

		// Token: 0x04001286 RID: 4742
		private readonly List<Polygon> holes;
	}
}
