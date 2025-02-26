using System;

// Token: 0x020011A9 RID: 4521
public static class UserData
{
	// Token: 0x060094A0 RID: 38048 RVA: 0x003D4E9D File Offset: 0x003D329D
	public static string Create(string name)
	{
		return UserData.fileDat.Create(name);
	}

	// Token: 0x17001F89 RID: 8073
	// (get) Token: 0x060094A1 RID: 38049 RVA: 0x003D4EAA File Offset: 0x003D32AA
	public static string Path
	{
		get
		{
			return UserData.fileDat.Path;
		}
	}

	// Token: 0x04007790 RID: 30608
	private static FileData fileDat = new FileData("UserData");
}
