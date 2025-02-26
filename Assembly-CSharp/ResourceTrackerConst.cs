using System;
using System.IO;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
public class ResourceTrackerConst
{
	// Token: 0x06001630 RID: 5680 RVA: 0x0008883C File Offset: 0x00086C3C
	public static string FormatBytes(int bytes)
	{
		if (bytes < 0)
		{
			return "error bytes";
		}
		if (bytes < 1024)
		{
			return bytes + "b";
		}
		if (bytes < 1048576)
		{
			return bytes / 1024 + "kb";
		}
		return bytes / 1024 / 1024 + "mb";
	}

	// Token: 0x040018F5 RID: 6389
	public static string shaderPropertyNameJsonPath = Path.Combine(Path.Combine(Application.persistentDataPath, "TestTools"), "ShaderPropertyNameRecord.json");

	// Token: 0x040018F6 RID: 6390
	public static char shaderPropertyNameJsonToken = '$';
}
