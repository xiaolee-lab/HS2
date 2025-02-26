using System;
using System.Collections.Generic;

// Token: 0x0200047D RID: 1149
public class UsCmdParsing
{
	// Token: 0x0600154D RID: 5453 RVA: 0x00083EBA File Offset: 0x000822BA
	public void RegisterHandler(eNetCmd cmd, UsCmdHandler handler)
	{
		this.m_handlers[cmd] = handler;
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x00083EC9 File Offset: 0x000822C9
	public void RegisterClientHandler(eNetCmd cmd, UsClientCmdHandler handler)
	{
		this.m_clientHandlers[cmd] = handler;
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x00083ED8 File Offset: 0x000822D8
	public UsCmdExecResult Execute(UsCmd c)
	{
		UsCmdExecResult result;
		try
		{
			eNetCmd eNetCmd = c.ReadNetCmd();
			UsCmdHandler usCmdHandler;
			if (!this.m_handlers.TryGetValue(eNetCmd, out usCmdHandler))
			{
				result = UsCmdExecResult.HandlerNotFound;
			}
			else if (usCmdHandler(eNetCmd, c))
			{
				result = UsCmdExecResult.Succ;
			}
			else
			{
				result = UsCmdExecResult.Failed;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("[cmd] Execution failed. ({0})", ex.Message);
			result = UsCmdExecResult.Failed;
		}
		return result;
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x00083F4C File Offset: 0x0008234C
	public UsCmdExecResult ExecuteClient(string clientID, UsCmd c)
	{
		UsCmdExecResult result;
		try
		{
			eNetCmd eNetCmd = c.ReadNetCmd();
			UsClientCmdHandler usClientCmdHandler;
			if (!this.m_clientHandlers.TryGetValue(eNetCmd, out usClientCmdHandler))
			{
				result = UsCmdExecResult.HandlerNotFound;
			}
			else if (usClientCmdHandler(clientID, eNetCmd, c))
			{
				result = UsCmdExecResult.Succ;
			}
			else
			{
				result = UsCmdExecResult.Failed;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("[cmd] Execution failed. ({0})", ex.Message);
			result = UsCmdExecResult.Failed;
		}
		return result;
	}

	// Token: 0x0400183E RID: 6206
	private Dictionary<eNetCmd, UsCmdHandler> m_handlers = new Dictionary<eNetCmd, UsCmdHandler>();

	// Token: 0x0400183F RID: 6207
	private Dictionary<eNetCmd, UsClientCmdHandler> m_clientHandlers = new Dictionary<eNetCmd, UsClientCmdHandler>();
}
