using System;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class LogEventArgs : EventArgs
{
	// Token: 0x060014D1 RID: 5329 RVA: 0x00082674 File Offset: 0x00080A74
	public LogEventArgs(int seqID, LogType type, string content, string stacktrace, float time)
	{
		this.SeqID = seqID;
		this.LogType = type;
		this.Content = content;
		this.Stacktrace = stacktrace;
		this.Time = time;
	}

	// Token: 0x040017F7 RID: 6135
	public int SeqID;

	// Token: 0x040017F8 RID: 6136
	public LogType LogType;

	// Token: 0x040017F9 RID: 6137
	public string Content = string.Empty;

	// Token: 0x040017FA RID: 6138
	public string Stacktrace = string.Empty;

	// Token: 0x040017FB RID: 6139
	public float Time;
}
