using System;
using System.Text;

// Token: 0x0200046A RID: 1130
public class LogBuffer
{
	// Token: 0x060014CE RID: 5326 RVA: 0x000825E0 File Offset: 0x000809E0
	public LogBuffer(int userDefinedSize = 0)
	{
		if (userDefinedSize != 0)
		{
			this.BufSize = userDefinedSize;
		}
		this.Buf = new byte[this.BufSize];
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x00082614 File Offset: 0x00080A14
	public bool Receive(string content)
	{
		byte[] bytes = Encoding.Default.GetBytes(content);
		if (this.BufWrittenBytes + bytes.Length > this.BufSize)
		{
			return false;
		}
		Buffer.BlockCopy(bytes, 0, this.Buf, this.BufWrittenBytes, bytes.Length);
		this.BufWrittenBytes += bytes.Length;
		return true;
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x0008266B File Offset: 0x00080A6B
	public void Clear()
	{
		this.BufWrittenBytes = 0;
	}

	// Token: 0x040017F2 RID: 6130
	public const int KB = 1024;

	// Token: 0x040017F3 RID: 6131
	public const int InternalBufSize = 16384;

	// Token: 0x040017F4 RID: 6132
	public byte[] Buf;

	// Token: 0x040017F5 RID: 6133
	public int BufSize = 16384;

	// Token: 0x040017F6 RID: 6134
	public int BufWrittenBytes;
}
