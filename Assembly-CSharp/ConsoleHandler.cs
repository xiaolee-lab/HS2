using System;

// Token: 0x02000494 RID: 1172
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ConsoleHandler : Attribute
{
	// Token: 0x060015A1 RID: 5537 RVA: 0x00085BED File Offset: 0x00083FED
	public ConsoleHandler(string cmd)
	{
		this.Command = cmd;
	}

	// Token: 0x040018A7 RID: 6311
	public string Command;
}
