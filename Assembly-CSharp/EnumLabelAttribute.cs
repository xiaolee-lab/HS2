using System;
using UnityEngine;

// Token: 0x02001194 RID: 4500
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumLabelAttribute : PropertyAttribute
{
	// Token: 0x06009457 RID: 37975 RVA: 0x003D3ADE File Offset: 0x003D1EDE
	public EnumLabelAttribute(string label)
	{
		this.label = label;
	}

	// Token: 0x0400776A RID: 30570
	public string label;
}
