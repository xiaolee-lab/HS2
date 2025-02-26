using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000F93 RID: 3987
public class CircleOutline : ModifiedShadow
{
	// Token: 0x17001D1D RID: 7453
	// (get) Token: 0x06008509 RID: 34057 RVA: 0x00374448 File Offset: 0x00372848
	// (set) Token: 0x0600850A RID: 34058 RVA: 0x00374450 File Offset: 0x00372850
	public int circleCount
	{
		get
		{
			return this.m_circleCount;
		}
		set
		{
			this.m_circleCount = Mathf.Max(value, 1);
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x17001D1E RID: 7454
	// (get) Token: 0x0600850B RID: 34059 RVA: 0x0037447B File Offset: 0x0037287B
	// (set) Token: 0x0600850C RID: 34060 RVA: 0x00374483 File Offset: 0x00372883
	public int firstSample
	{
		get
		{
			return this.m_firstSample;
		}
		set
		{
			this.m_firstSample = Mathf.Max(value, 2);
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x17001D1F RID: 7455
	// (get) Token: 0x0600850D RID: 34061 RVA: 0x003744AE File Offset: 0x003728AE
	// (set) Token: 0x0600850E RID: 34062 RVA: 0x003744B6 File Offset: 0x003728B6
	public int sampleIncrement
	{
		get
		{
			return this.m_sampleIncrement;
		}
		set
		{
			this.m_sampleIncrement = Mathf.Max(value, 1);
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x0600850F RID: 34063 RVA: 0x003744E4 File Offset: 0x003728E4
	public override void ModifyVertices(List<UIVertex> verts)
	{
		if (!this.IsActive())
		{
			return;
		}
		int num = (this.m_firstSample * 2 + this.m_sampleIncrement * (this.m_circleCount - 1)) * this.m_circleCount / 2;
		int num2 = verts.Count * (num + 1);
		if (verts.Capacity < num2)
		{
			verts.Capacity = num2;
		}
		int count = verts.Count;
		int num3 = 0;
		int num4 = this.m_firstSample;
		float num5 = base.effectDistance.x / (float)this.circleCount;
		float num6 = base.effectDistance.y / (float)this.circleCount;
		for (int i = 1; i <= this.m_circleCount; i++)
		{
			float num7 = num5 * (float)i;
			float num8 = num6 * (float)i;
			float num9 = 6.2831855f / (float)num4;
			float num10 = (float)(i % 2) * num9 * 0.5f;
			for (int j = 0; j < num4; j++)
			{
				int num11 = num3 + count;
				base.ApplyShadow(verts, base.effectColor, num3, num11, num7 * Mathf.Cos(num10), num8 * Mathf.Sin(num10));
				num3 = num11;
				num10 += num9;
			}
			num4 += this.m_sampleIncrement;
		}
	}

	// Token: 0x04006BAA RID: 27562
	[SerializeField]
	private int m_circleCount = 2;

	// Token: 0x04006BAB RID: 27563
	[SerializeField]
	private int m_firstSample = 4;

	// Token: 0x04006BAC RID: 27564
	[SerializeField]
	private int m_sampleIncrement = 2;
}
