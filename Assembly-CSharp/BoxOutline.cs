using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000F92 RID: 3986
public class BoxOutline : ModifiedShadow
{
	// Token: 0x17001D1B RID: 7451
	// (get) Token: 0x06008503 RID: 34051 RVA: 0x003742C4 File Offset: 0x003726C4
	// (set) Token: 0x06008504 RID: 34052 RVA: 0x003742CC File Offset: 0x003726CC
	public int halfSampleCountX
	{
		get
		{
			return this.m_halfSampleCountX;
		}
		set
		{
			this.m_halfSampleCountX = Mathf.Clamp(value, 1, 20);
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x17001D1C RID: 7452
	// (get) Token: 0x06008505 RID: 34053 RVA: 0x003742F9 File Offset: 0x003726F9
	// (set) Token: 0x06008506 RID: 34054 RVA: 0x00374301 File Offset: 0x00372701
	public int halfSampleCountY
	{
		get
		{
			return this.m_halfSampleCountY;
		}
		set
		{
			this.m_halfSampleCountY = Mathf.Clamp(value, 1, 20);
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x06008507 RID: 34055 RVA: 0x00374330 File Offset: 0x00372730
	public override void ModifyVertices(List<UIVertex> verts)
	{
		if (!this.IsActive())
		{
			return;
		}
		int num = verts.Count * (this.m_halfSampleCountX * 2 + 1) * (this.m_halfSampleCountY * 2 + 1);
		if (verts.Capacity < num)
		{
			verts.Capacity = num;
		}
		int count = verts.Count;
		int num2 = 0;
		float num3 = base.effectDistance.x / (float)this.m_halfSampleCountX;
		float num4 = base.effectDistance.y / (float)this.m_halfSampleCountY;
		for (int i = -this.m_halfSampleCountX; i <= this.m_halfSampleCountX; i++)
		{
			for (int j = -this.m_halfSampleCountY; j <= this.m_halfSampleCountY; j++)
			{
				if (i != 0 || j != 0)
				{
					int num5 = num2 + count;
					base.ApplyShadow(verts, base.effectColor, num2, num5, num3 * (float)i, num4 * (float)j);
					num2 = num5;
				}
			}
		}
	}

	// Token: 0x04006BA7 RID: 27559
	private const int maxHalfSampleCount = 20;

	// Token: 0x04006BA8 RID: 27560
	[SerializeField]
	[Range(1f, 20f)]
	private int m_halfSampleCountX = 1;

	// Token: 0x04006BA9 RID: 27561
	[SerializeField]
	[Range(1f, 20f)]
	private int m_halfSampleCountY = 1;
}
