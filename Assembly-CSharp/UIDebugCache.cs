using System;
using System.Collections.Generic;

// Token: 0x020004C4 RID: 1220
public class UIDebugCache
{
	// Token: 0x06001689 RID: 5769 RVA: 0x0008A5FC File Offset: 0x000889FC
	public static string GetName(int instID)
	{
		return (!UIDebugCache.s_nameLut.ContainsKey(instID)) ? string.Empty : UIDebugCache.s_nameLut[instID];
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x0008A623 File Offset: 0x00088A23
	public static string GetParentName(int instID)
	{
		return (!UIDebugCache.s_parentNameLut.ContainsKey(instID)) ? string.Empty : UIDebugCache.s_parentNameLut[instID];
	}

	// Token: 0x0400194E RID: 6478
	public static Dictionary<int, string> s_nameLut = new Dictionary<int, string>();

	// Token: 0x0400194F RID: 6479
	public static Dictionary<int, string> s_parentNameLut = new Dictionary<int, string>();
}
