using System;
using UnityEngine;

// Token: 0x02001198 RID: 4504
[AttributeUsage(AttributeTargets.Field)]
public class RangeLabelAttribute : PropertyAttribute
{
	// Token: 0x0600945B RID: 37979 RVA: 0x003D3B21 File Offset: 0x003D1F21
	public RangeLabelAttribute(string label, float min, float max)
	{
		this.label = label;
		this.min = min;
		this.max = max;
	}

	// Token: 0x0400776F RID: 30575
	public string label;

	// Token: 0x04007770 RID: 30576
	public float min;

	// Token: 0x04007771 RID: 30577
	public float max;
}
