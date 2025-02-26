using System;
using System.Runtime.CompilerServices;

// Token: 0x0200046F RID: 1135
public class ObjectUtil
{
	// Token: 0x060014EF RID: 5359 RVA: 0x00082F28 File Offset: 0x00081328
	public static string GetHashCodeString(object obj)
	{
		return RuntimeHelpers.GetHashCode(obj).ToString("X8");
	}
}
