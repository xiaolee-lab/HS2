using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000FD0 RID: 4048
public class Outline8 : ModifiedShadow
{
	// Token: 0x06008699 RID: 34457 RVA: 0x003838A0 File Offset: 0x00381CA0
	public override void ModifyVertices(List<UIVertex> verts)
	{
		if (!this.IsActive())
		{
			return;
		}
		int num = verts.Count * 9;
		if (verts.Capacity < num)
		{
			verts.Capacity = num;
		}
		int count = verts.Count;
		int num2 = 0;
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				if (i != 0 || j != 0)
				{
					int num3 = num2 + count;
					base.ApplyShadow(verts, base.effectColor, num2, num3, base.effectDistance.x * (float)i, base.effectDistance.y * (float)j);
					num2 = num3;
				}
			}
		}
	}
}
