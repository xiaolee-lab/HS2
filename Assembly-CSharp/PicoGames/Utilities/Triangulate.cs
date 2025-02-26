using System;
using System.Collections.Generic;
using UnityEngine;

namespace PicoGames.Utilities
{
	// Token: 0x02000A77 RID: 2679
	public class Triangulate
	{
		// Token: 0x06004F60 RID: 20320 RVA: 0x001E7F30 File Offset: 0x001E6330
		public static int[] Edge(Vector3[] _shape)
		{
			Triangulate.shape = _shape;
			List<int> list = new List<int>();
			int num = Triangulate.shape.Length;
			if (num < 3)
			{
				return list.ToArray();
			}
			int[] array = new int[num];
			if (Triangulate.Area() > 0f)
			{
				for (int i = 0; i < num; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					array[j] = num - 1 - j;
				}
			}
			int k = num;
			int num2 = 2 * k;
			int num3 = 0;
			int num4 = k - 1;
			while (k > 2)
			{
				if (num2-- <= 0)
				{
					return list.ToArray();
				}
				int num5 = num4;
				if (k <= num5)
				{
					num5 = 0;
				}
				num4 = num5 + 1;
				if (k <= num4)
				{
					num4 = 0;
				}
				int num6 = num4 + 1;
				if (k <= num6)
				{
					num6 = 0;
				}
				if (Triangulate.Snip(num5, num4, num6, k, array))
				{
					int item = array[num5];
					int item2 = array[num4];
					int item3 = array[num6];
					list.Add(item);
					list.Add(item2);
					list.Add(item3);
					num3++;
					int num7 = num4;
					for (int l = num4 + 1; l < k; l++)
					{
						array[num7] = array[l];
						num7++;
					}
					k--;
					num2 = 2 * k;
				}
			}
			list.Reverse();
			return list.ToArray();
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x001E80A0 File Offset: 0x001E64A0
		private static float Area()
		{
			int num = Triangulate.shape.Length;
			float num2 = 0f;
			int num3 = num - 1;
			int i = 0;
			while (i < num)
			{
				Vector3 vector = Triangulate.shape[num3];
				Vector3 vector2 = Triangulate.shape[i];
				num2 += vector.x * vector2.y - vector2.x * vector.y;
				num3 = i++;
			}
			return num2 * 0.5f;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x001E8120 File Offset: 0x001E6520
		private static bool OverlapsPoint(Vector3 _t1, Vector3 _t2, Vector3 _t3, Vector3 _p1)
		{
			float num = _t3.x - _t2.x;
			float num2 = _t3.y - _t2.y;
			float num3 = _t1.x - _t3.x;
			float num4 = _t1.y - _t3.y;
			float num5 = _t2.x - _t1.x;
			float num6 = _t2.y - _t1.y;
			float num7 = _p1.x - _t1.x;
			float num8 = _p1.y - _t1.y;
			float num9 = _p1.x - _t2.x;
			float num10 = _p1.y - _t2.y;
			float num11 = _p1.x - _t3.x;
			float num12 = _p1.y - _t3.y;
			float num13 = num * num10 - num2 * num9;
			float num14 = num5 * num8 - num6 * num7;
			float num15 = num3 * num12 - num4 * num11;
			return num13 >= 0f && num15 >= 0f && num14 >= 0f;
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x001E8240 File Offset: 0x001E6640
		private static bool Snip(int u, int v, int w, int n, int[] V)
		{
			Vector3 t = Triangulate.shape[V[u]];
			Vector3 t2 = Triangulate.shape[V[v]];
			Vector3 t3 = Triangulate.shape[V[w]];
			if (Mathf.Epsilon > (t2.x - t.x) * (t3.y - t.y) - (t2.y - t.y) * (t3.x - t.x))
			{
				return false;
			}
			for (int i = 0; i < n; i++)
			{
				if (i != u && i != v && i != w)
				{
					Vector3 p = Triangulate.shape[V[i]];
					if (Triangulate.OverlapsPoint(t, t2, t3, p))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04004865 RID: 18533
		private static Vector3[] shape;
	}
}
