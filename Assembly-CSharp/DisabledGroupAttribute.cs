using System;
using UnityEngine;

// Token: 0x02001191 RID: 4497
public class DisabledGroupAttribute : PropertyAttribute
{
	// Token: 0x06009454 RID: 37972 RVA: 0x003D3AB1 File Offset: 0x003D1EB1
	public DisabledGroupAttribute(string label)
	{
		this.label = label;
	}

	// Token: 0x04007767 RID: 30567
	public string label;
}
