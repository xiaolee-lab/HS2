using System;
using System.Collections.Generic;

// Token: 0x02000478 RID: 1144
public class UsCmdIOError : Exception
{
	// Token: 0x06001530 RID: 5424 RVA: 0x00083B8D File Offset: 0x00081F8D
	public UsCmdIOError(UsCmdIOErrorCode code) : base("[UsCmdIOError] " + UsCmdIOError.InfoLut[code])
	{
		this.ErrorCode = code;
	}

	// Token: 0x04001833 RID: 6195
	private static Dictionary<UsCmdIOErrorCode, string> InfoLut = new Dictionary<UsCmdIOErrorCode, string>
	{
		{
			UsCmdIOErrorCode.ReadOverflow,
			"Not enough space for reading."
		},
		{
			UsCmdIOErrorCode.WriteOverflow,
			"Not enough space for writing."
		},
		{
			UsCmdIOErrorCode.TypeMismatched,
			"Reading/writing a string as a primitive."
		}
	};

	// Token: 0x04001834 RID: 6196
	public UsCmdIOErrorCode ErrorCode;
}
