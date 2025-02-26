using System;
using UnityEngine;

namespace Exploder.Utils
{
	// Token: 0x020003CC RID: 972
	internal class Hull2D
	{
		// Token: 0x06001144 RID: 4420 RVA: 0x0006549F File Offset: 0x0006389F
		public static void Sort(Vector2[] array)
		{
			Array.Sort<Vector2>(array, delegate(Vector2 value0, Vector2 value1)
			{
				int num = value0.x.CompareTo(value1.x);
				if (num != 0)
				{
					return num;
				}
				return value0.y.CompareTo(value1.y);
			});
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000654C4 File Offset: 0x000638C4
		public static void DumpArray(Vector2[] array)
		{
			foreach (Vector2 vector in array)
			{
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000654F4 File Offset: 0x000638F4
		public static Vector2[] ChainHull2D(Vector2[] Pnts)
		{
			int num = Pnts.Length;
			int num2 = 0;
			Hull2D.Sort(Pnts);
			Vector2[] array = new Vector2[2 * num];
			for (int i = 0; i < num; i++)
			{
				while (num2 >= 2 && Hull2D.Hull2DCross(ref array[num2 - 2], ref array[num2 - 1], ref Pnts[i]) <= 0f)
				{
					num2--;
				}
				array[num2++] = Pnts[i];
			}
			int j = num - 2;
			int num3 = num2 + 1;
			while (j >= 0)
			{
				while (num2 >= num3 && Hull2D.Hull2DCross(ref array[num2 - 2], ref array[num2 - 1], ref Pnts[j]) <= 0f)
				{
					num2--;
				}
				array[num2++] = Pnts[j];
				j--;
			}
			Vector2[] array2 = new Vector2[num2];
			for (int k = 0; k < num2; k++)
			{
				array2[k] = array[k];
			}
			return array2;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0006562B File Offset: 0x00063A2B
		private static float Hull2DCross(ref Vector2 O, ref Vector2 A, ref Vector2 B)
		{
			return (A.x - O.x) * (B.y - O.y) - (A.y - O.y) * (B.x - O.x);
		}
	}
}
