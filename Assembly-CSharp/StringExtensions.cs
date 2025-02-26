using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000A86 RID: 2694
internal static class StringExtensions
{
	// Token: 0x06004F89 RID: 20361 RVA: 0x001E9912 File Offset: 0x001E7D12
	public static bool ContainsAll(this string str, IEnumerable<string> needles)
	{
		return str != null && needles.All(new Func<string, bool>(str.Contains));
	}

	// Token: 0x06004F8A RID: 20362 RVA: 0x001E9932 File Offset: 0x001E7D32
	public static bool ContainsAny(this string str, IEnumerable<string> needles)
	{
		return str != null && needles.Any(new Func<string, bool>(str.Contains));
	}

	// Token: 0x06004F8B RID: 20363 RVA: 0x001E9952 File Offset: 0x001E7D52
	public static bool ContainsAll(this string str, params string[] needles)
	{
		return str.ContainsAll(needles.AsEnumerable<string>());
	}

	// Token: 0x06004F8C RID: 20364 RVA: 0x001E9960 File Offset: 0x001E7D60
	public static bool ContainsAny(this string str, params string[] needles)
	{
		return str.ContainsAny(needles.AsEnumerable<string>());
	}
}
