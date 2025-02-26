using System;
using UnityEngine;

// Token: 0x0200048F RID: 1167
public class UsMain : IDisposable
{
	// Token: 0x06001581 RID: 5505 RVA: 0x00084F78 File Offset: 0x00083378
	public UsMain(bool LogRemotely, bool LogIntoFile, bool InGameGui)
	{
		Application.runInBackground = true;
		this._logServ = new LogService(LogIntoFile, -1, true);
		this._test = new utest();
		if (LogRemotely)
		{
			this._logServ.LogTargets += this.LogTarget_Remotely;
		}
		UsNet.Instance = new UsNet();
		UsMain_NetHandlers.Instance = new UsMain_NetHandlers(UsNet.Instance.CmdExecutor);
		UsvConsole.Instance = new UsvConsole();
		GameUtil.Log("on_level loaded.", Array.Empty<object>());
		GameInterface.Instance.Init();
		this._inGameGui = InGameGui;
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x0008501C File Offset: 0x0008341C
	private void LogTarget_Remotely(object sender, LogEventArgs args)
	{
		if (UsNet.Instance != null)
		{
			UsCmd usCmd = new UsCmd();
			usCmd.WriteNetCmd(eNetCmd.SV_App_Logging);
			usCmd.WriteInt16((short)args.SeqID);
			usCmd.WriteInt32((int)args.LogType);
			usCmd.WriteStringStripped(args.Content, 1024);
			usCmd.WriteFloat(args.Time);
			UsNet.Instance.SendCommand(usCmd);
		}
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x00085088 File Offset: 0x00083488
	public void Update()
	{
		this._currentTimeInMilliseconds = DateTime.Now.Ticks / 10000L;
		if (this._currentTimeInMilliseconds - this._tickNetLast > this._tickNetInterval)
		{
			if (UsNet.Instance != null)
			{
				UsNet.Instance.Update();
			}
			this._tickNetLast = this._currentTimeInMilliseconds;
		}
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000850E7 File Offset: 0x000834E7
	public void Dispose()
	{
		UsNet.Instance.Dispose();
		this._test.Dispose();
		this._logServ.Dispose();
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x00085109 File Offset: 0x00083509
	public void OnLevelWasLoaded()
	{
		this._test.OnLevelWasLoaded();
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x00085116 File Offset: 0x00083516
	public void OnGUI()
	{
		if (this._inGameGui)
		{
			this._test.OnGUI();
		}
	}

	// Token: 0x04001896 RID: 6294
	public const int MAX_CONTENT_LEN = 1024;

	// Token: 0x04001897 RID: 6295
	private long _currentTimeInMilliseconds;

	// Token: 0x04001898 RID: 6296
	private long _tickNetLast;

	// Token: 0x04001899 RID: 6297
	private long _tickNetInterval = 200L;

	// Token: 0x0400189A RID: 6298
	private LogService _logServ;

	// Token: 0x0400189B RID: 6299
	private utest _test;

	// Token: 0x0400189C RID: 6300
	private bool _inGameGui;
}
