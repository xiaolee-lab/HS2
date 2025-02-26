using System;
using UnityEngine;

// Token: 0x02001192 RID: 4498
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public sealed class EnumFlags2Attribute : PropertyAttribute
{
	// Token: 0x06009455 RID: 37973 RVA: 0x003D3AC0 File Offset: 0x003D1EC0
	public EnumFlags2Attribute(string label, int _oneline = -1)
	{
		this.label = label;
		this.line = _oneline;
	}

	// Token: 0x04007768 RID: 30568
	public string label;

	// Token: 0x04007769 RID: 30569
	public int line;
}
