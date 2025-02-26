using System;
using UnityEngine;

// Token: 0x02000A70 RID: 2672
public class MinAttribute : PropertyAttribute
{
	// Token: 0x06004F15 RID: 20245 RVA: 0x001E5E43 File Offset: 0x001E4243
	public MinAttribute(float _min = 0f)
	{
		this.min = _min;
	}

	// Token: 0x06004F16 RID: 20246 RVA: 0x001E5E52 File Offset: 0x001E4252
	public float GetValue(float _value)
	{
		return Mathf.Max(this.min, _value);
	}

	// Token: 0x06004F17 RID: 20247 RVA: 0x001E5E60 File Offset: 0x001E4260
	public int GetValue(int _value)
	{
		return (int)Mathf.Max(this.min, (float)_value);
	}

	// Token: 0x04004833 RID: 18483
	private float min;
}
