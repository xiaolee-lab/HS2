using System;

// Token: 0x020011AB RID: 4523
public static class DebugData
{
	// Token: 0x060094A6 RID: 38054 RVA: 0x003D4EF1 File Offset: 0x003D32F1
	public static string Create(string name)
	{
		return DebugData.fileDat.Create(name);
	}

	// Token: 0x17001F8B RID: 8075
	// (get) Token: 0x060094A7 RID: 38055 RVA: 0x003D4EFE File Offset: 0x003D32FE
	public static string Path
	{
		get
		{
			return DebugData.fileDat.Path;
		}
	}

	// Token: 0x04007792 RID: 30610
	private static FileData fileDat = new FileData("!Debug");
}
