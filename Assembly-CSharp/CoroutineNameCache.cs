using System;
using System.Collections.Generic;

// Token: 0x020004A8 RID: 1192
public class CoroutineNameCache
{
	// Token: 0x060015FF RID: 5631 RVA: 0x00087510 File Offset: 0x00085910
	public static string Mangle(string rawName)
	{
		string text;
		if (CoroutineNameCache._mangledNames.TryGetValue(rawName, out text))
		{
			return text;
		}
		text = rawName.Replace('<', '{').Replace('>', '}');
		CoroutineNameCache._mangledNames[rawName] = text;
		return text;
	}

	// Token: 0x040018D4 RID: 6356
	private static Dictionary<string, string> _mangledNames = new Dictionary<string, string>();
}
