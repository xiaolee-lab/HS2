using System;
using System.Diagnostics;
using System.Timers;

// Token: 0x02000474 RID: 1140
public class NetManager : IDisposable
{
	// Token: 0x06001510 RID: 5392 RVA: 0x00083690 File Offset: 0x00081A90
	public NetManager()
	{
		this._client.Connected += this.OnConnected;
		this._client.Disconnected += this.OnDisconnected;
		this._client.RegisterCmdHandler(eNetCmd.SV_HandshakeResponse, new UsCmdHandler(this.Handle_HandshakeResponse));
		this._client.RegisterCmdHandler(eNetCmd.SV_KeepAliveResponse, new UsCmdHandler(this.Handle_KeepAliveResponse));
		this._client.RegisterCmdHandler(eNetCmd.SV_ExecCommandResponse, new UsCmdHandler(this.Handle_ExecCommandResponse));
		this._guardTimer.Timeout += this.OnGuardingTimeout;
		this._tickTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
		{
			this.Tick();
		};
		this._tickTimer.AutoReset = true;
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06001511 RID: 5393 RVA: 0x000837AD File Offset: 0x00081BAD
	public bool IsConnected
	{
		get
		{
			return this._client.IsConnected;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06001512 RID: 5394 RVA: 0x000837BA File Offset: 0x00081BBA
	public string RemoteAddr
	{
		get
		{
			return this._client.RemoteAddr;
		}
	}

	// Token: 0x14000059 RID: 89
	// (add) Token: 0x06001513 RID: 5395 RVA: 0x000837C8 File Offset: 0x00081BC8
	// (remove) Token: 0x06001514 RID: 5396 RVA: 0x00083800 File Offset: 0x00081C00
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation LogicallyConnected;

	// Token: 0x1400005A RID: 90
	// (add) Token: 0x06001515 RID: 5397 RVA: 0x00083838 File Offset: 0x00081C38
	// (remove) Token: 0x06001516 RID: 5398 RVA: 0x00083870 File Offset: 0x00081C70
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation LogicallyDisconnected;

	// Token: 0x06001517 RID: 5399 RVA: 0x000838A6 File Offset: 0x00081CA6
	public void Dispose()
	{
		this._client.Dispose();
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000838B3 File Offset: 0x00081CB3
	public bool Connect(string addr)
	{
		this._client.Connect(addr, 39980);
		return true;
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000838C7 File Offset: 0x00081CC7
	public void Disconnect()
	{
		this._client.Disconnect();
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000838D4 File Offset: 0x00081CD4
	public void Send(UsCmd cmd)
	{
		this._client.SendPacket(cmd);
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000838E2 File Offset: 0x00081CE2
	public void RegisterCmdHandler(eNetCmd cmd, UsCmdHandler handler)
	{
		this._client.RegisterCmdHandler(cmd, handler);
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000838F4 File Offset: 0x00081CF4
	public void ExecuteCmd(string cmdText)
	{
		if (!this.IsConnected)
		{
			NetUtil.Log("not connected to server, command ignored.", Array.Empty<object>());
			return;
		}
		if (cmdText.Length == 0)
		{
			NetUtil.Log("the command bar is empty, try 'help' to list all supported commands.", Array.Empty<object>());
			return;
		}
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.CL_ExecCommand);
		usCmd.WriteString(cmdText);
		this.Send(usCmd);
		NetUtil.Log("command executed: [b]{0}[/b]", new object[]
		{
			cmdText
		});
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x0008396C File Offset: 0x00081D6C
	private void OnConnected(object sender, EventArgs e)
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteInt16(1001);
		usCmd.WriteInt16(0);
		usCmd.WriteInt16(1);
		usCmd.WriteInt16(0);
		this._client.SendPacket(usCmd);
		this._tickTimer.Start();
		this._guardTimer.Activate();
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000839C1 File Offset: 0x00081DC1
	private void OnDisconnected(object sender, EventArgs e)
	{
		this._tickTimer.Stop();
		this._guardTimer.Deactivate();
		SysPost.InvokeMulticast(this, this.LogicallyDisconnected);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000839E5 File Offset: 0x00081DE5
	private void OnGuardingTimeout(object sender, EventArgs e)
	{
		NetUtil.LogError("guarding timeout, closing connection...", Array.Empty<object>());
		this.Disconnect();
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000839FC File Offset: 0x00081DFC
	private bool Handle_HandshakeResponse(eNetCmd cmd, UsCmd c)
	{
		NetUtil.Log("eNetCmd.SV_HandshakeResponse received, connection validated.", Array.Empty<object>());
		SysPost.InvokeMulticast(this, this.LogicallyConnected);
		this._guardTimer.Deactivate();
		return true;
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x00083A25 File Offset: 0x00081E25
	private bool Handle_KeepAliveResponse(eNetCmd cmd, UsCmd c)
	{
		return true;
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x00083A28 File Offset: 0x00081E28
	private bool Handle_ExecCommandResponse(eNetCmd cmd, UsCmd c)
	{
		int num = c.ReadInt32();
		NetUtil.Log("command executing result: [b]{0}[/b]", new object[]
		{
			num
		});
		return true;
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x00083A58 File Offset: 0x00081E58
	private void Tick()
	{
		if (!this._client.IsConnected)
		{
			return;
		}
		this._currentTimeInMilliseconds = DateTime.Now.Ticks / 10000L;
		if (this._currentTimeInMilliseconds - this._lastKeepAlive > this.INTERVAL_KeepAlive)
		{
			UsCmd usCmd = new UsCmd();
			usCmd.WriteNetCmd(eNetCmd.CL_KeepAlive);
			this._client.SendPacket(usCmd);
			this._lastKeepAlive = this._currentTimeInMilliseconds;
		}
		if (this._currentTimeInMilliseconds - this._lastCheckingConnectionStatus > this.INTERVAL_CheckingConnectionStatus)
		{
			this._client.Tick_CheckConnectionStatus();
			this._lastCheckingConnectionStatus = this._currentTimeInMilliseconds;
		}
		if (this._currentTimeInMilliseconds - this._lastReceivingData > this.INTERVAL_ReceivingData)
		{
			this._client.Tick_ReceivingData();
			this._lastReceivingData = this._currentTimeInMilliseconds;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06001524 RID: 5412 RVA: 0x00083B2F File Offset: 0x00081F2F
	public NetClient Client
	{
		get
		{
			return this._client;
		}
	}

	// Token: 0x04001820 RID: 6176
	public static NetManager Instance;

	// Token: 0x04001823 RID: 6179
	private long INTERVAL_KeepAlive = 3000L;

	// Token: 0x04001824 RID: 6180
	private long INTERVAL_CheckingConnectionStatus = 1000L;

	// Token: 0x04001825 RID: 6181
	private long INTERVAL_ReceivingData = 200L;

	// Token: 0x04001826 RID: 6182
	private long _currentTimeInMilliseconds;

	// Token: 0x04001827 RID: 6183
	private long _lastKeepAlive;

	// Token: 0x04001828 RID: 6184
	private long _lastCheckingConnectionStatus;

	// Token: 0x04001829 RID: 6185
	private long _lastReceivingData;

	// Token: 0x0400182A RID: 6186
	private NetClient _client = new NetClient();

	// Token: 0x0400182B RID: 6187
	private NetGuardTimer _guardTimer = new NetGuardTimer();

	// Token: 0x0400182C RID: 6188
	private Timer _tickTimer = new Timer(100.0);
}
