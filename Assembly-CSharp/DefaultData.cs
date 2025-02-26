using System;

// Token: 0x020011AA RID: 4522
public static class DefaultData
{
	// Token: 0x060094A3 RID: 38051 RVA: 0x003D4EC7 File Offset: 0x003D32C7
	public static string Create(string name)
	{
		return DefaultData.fileDat.Create(name);
	}

	// Token: 0x17001F8A RID: 8074
	// (get) Token: 0x060094A4 RID: 38052 RVA: 0x003D4ED4 File Offset: 0x003D32D4
	public static string Path
	{
		get
		{
			return DefaultData.fileDat.Path;
		}
	}

	// Token: 0x04007791 RID: 30609
	private static FileData fileDat = new FileData("DefaultData");
}
