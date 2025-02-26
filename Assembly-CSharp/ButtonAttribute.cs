using System;
using UnityEngine;

// Token: 0x02001190 RID: 4496
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public sealed class ButtonAttribute : PropertyAttribute
{
	// Token: 0x0600944D RID: 37965 RVA: 0x003D3A61 File Offset: 0x003D1E61
	public ButtonAttribute(string function, string name, params object[] parameters)
	{
		this.Function = function;
		this.Name = name;
		this.Parameters = parameters;
	}

	// Token: 0x17001F83 RID: 8067
	// (get) Token: 0x0600944E RID: 37966 RVA: 0x003D3A7E File Offset: 0x003D1E7E
	// (set) Token: 0x0600944F RID: 37967 RVA: 0x003D3A86 File Offset: 0x003D1E86
	public string Function { get; private set; }

	// Token: 0x17001F84 RID: 8068
	// (get) Token: 0x06009450 RID: 37968 RVA: 0x003D3A8F File Offset: 0x003D1E8F
	// (set) Token: 0x06009451 RID: 37969 RVA: 0x003D3A97 File Offset: 0x003D1E97
	public string Name { get; private set; }

	// Token: 0x17001F85 RID: 8069
	// (get) Token: 0x06009452 RID: 37970 RVA: 0x003D3AA0 File Offset: 0x003D1EA0
	// (set) Token: 0x06009453 RID: 37971 RVA: 0x003D3AA8 File Offset: 0x003D1EA8
	public object[] Parameters { get; private set; }
}
