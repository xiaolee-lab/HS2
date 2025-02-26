using System;
using UnityEngine;

// Token: 0x02001197 RID: 4503
[AttributeUsage(AttributeTargets.Field)]
public class RangeIntLabelAttribute : PropertyAttribute
{
	// Token: 0x0600945A RID: 37978 RVA: 0x003D3B04 File Offset: 0x003D1F04
	public RangeIntLabelAttribute(string label, int min, int max)
	{
		this.label = label;
		this.min = min;
		this.max = max;
	}

	// Token: 0x0400776C RID: 30572
	public string label;

	// Token: 0x0400776D RID: 30573
	public int min;

	// Token: 0x0400776E RID: 30574
	public int max;
}
