using System;
using System.Collections.Generic;

// Token: 0x02000470 RID: 1136
public class UniqueString
{
	// Token: 0x060014F1 RID: 5361 RVA: 0x00082F50 File Offset: 0x00081350
	public static string Intern(string str, bool removable = true)
	{
		if (str == null)
		{
			return null;
		}
		string text = UniqueString.IsInterned(str);
		if (text != null)
		{
			return text;
		}
		if (removable)
		{
			UniqueString.m_strings.Add(str, str);
			return str;
		}
		return string.Intern(str);
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x00082F90 File Offset: 0x00081390
	public static string IsInterned(string str)
	{
		if (str == null)
		{
			return null;
		}
		string text = string.IsInterned(str);
		if (text != null)
		{
			return text;
		}
		if (UniqueString.m_strings.TryGetValue(str, out text))
		{
			return text;
		}
		return null;
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x00082FC9 File Offset: 0x000813C9
	public static void Clear()
	{
		UniqueString.m_strings.Clear();
	}

	// Token: 0x04001811 RID: 6161
	private static Dictionary<string, string> m_strings = new Dictionary<string, string>();
}
