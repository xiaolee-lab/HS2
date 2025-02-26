using System;

// Token: 0x02000487 RID: 1159
public class UsLogPacket
{
	// Token: 0x06001562 RID: 5474 RVA: 0x0008446C File Offset: 0x0008286C
	public UsLogPacket()
	{
		this.SeqID = ushort.MaxValue;
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x0008447F File Offset: 0x0008287F
	public UsLogPacket(UsCmd c)
	{
		this.SeqID = (ushort)c.ReadInt16();
		this.LogType = (UsLogType)c.ReadInt32();
		this.Content = c.ReadString();
		this.RealtimeSinceStartup = c.ReadFloat();
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000844B8 File Offset: 0x000828B8
	public UsCmd CreatePacket()
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_App_Logging);
		usCmd.WriteInt16((short)this.SeqID);
		usCmd.WriteInt32((int)this.LogType);
		usCmd.WriteStringStripped(this.Content, 1024);
		usCmd.WriteFloat(this.RealtimeSinceStartup);
		return usCmd;
	}

	// Token: 0x0400185D RID: 6237
	public const int MAX_CONTENT_LEN = 1024;

	// Token: 0x0400185E RID: 6238
	public const int MAX_CALLSTACK_LEN = 1024;

	// Token: 0x0400185F RID: 6239
	public ushort SeqID;

	// Token: 0x04001860 RID: 6240
	public UsLogType LogType;

	// Token: 0x04001861 RID: 6241
	public string Content;

	// Token: 0x04001862 RID: 6242
	public float RealtimeSinceStartup;

	// Token: 0x04001863 RID: 6243
	public string Callstack;
}
