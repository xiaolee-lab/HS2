using System;
using UnityEngine;

// Token: 0x02001196 RID: 4502
[AttributeUsage(AttributeTargets.Field)]
public class LabelAttribute : PropertyAttribute
{
	// Token: 0x06009459 RID: 37977 RVA: 0x003D3AF5 File Offset: 0x003D1EF5
	public LabelAttribute(string label)
	{
		this.label = label;
	}

	// Token: 0x0400776B RID: 30571
	public string label;
}
