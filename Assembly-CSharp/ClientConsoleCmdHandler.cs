using System;

// Token: 0x0200049A RID: 1178
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ClientConsoleCmdHandler : Attribute
{
	// Token: 0x060015BD RID: 5565 RVA: 0x00086065 File Offset: 0x00084465
	public ClientConsoleCmdHandler(string cmd)
	{
		this.Command = cmd;
	}

	// Token: 0x040018AF RID: 6319
	public string Command;
}
