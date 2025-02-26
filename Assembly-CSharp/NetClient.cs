using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

// Token: 0x02000472 RID: 1138
public class NetClient : IDisposable
{
	// Token: 0x14000056 RID: 86
	// (add) Token: 0x060014FB RID: 5371 RVA: 0x000830E0 File Offset: 0x000814E0
	// (remove) Token: 0x060014FC RID: 5372 RVA: 0x00083118 File Offset: 0x00081518
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation Connected;

	// Token: 0x14000057 RID: 87
	// (add) Token: 0x060014FD RID: 5373 RVA: 0x00083150 File Offset: 0x00081550
	// (remove) Token: 0x060014FE RID: 5374 RVA: 0x00083188 File Offset: 0x00081588
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation Disconnected;

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060014FF RID: 5375 RVA: 0x000831BE File Offset: 0x000815BE
	public bool IsConnected
	{
		get
		{
			return this._tcpClient != null;
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06001500 RID: 5376 RVA: 0x000831CC File Offset: 0x000815CC
	public string RemoteAddr
	{
		get
		{
			return (!this.IsConnected) ? string.Empty : this._tcpClient.Client.RemoteEndPoint.ToString();
		}
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000831F8 File Offset: 0x000815F8
	public void Connect(string host, int port)
	{
		this._host = host;
		this._port = port;
		this._tcpClient = new TcpClient();
		this._tcpClient.BeginConnect(this._host, this._port, new AsyncCallback(this.OnConnect), this._tcpClient);
		NetUtil.Log("connecting to [u]{0}:{1}[/u]...", new object[]
		{
			host,
			port
		});
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x00083268 File Offset: 0x00081668
	public void Disconnect()
	{
		if (this._tcpClient != null)
		{
			this._tcpClient.Close();
			this._tcpClient = null;
			this._host = string.Empty;
			this._port = 0;
			NetUtil.Log("connection closed.", Array.Empty<object>());
			SysPost.InvokeMulticast(this, this.Disconnected);
		}
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000832BF File Offset: 0x000816BF
	public void RegisterCmdHandler(eNetCmd cmd, UsCmdHandler handler)
	{
		this._cmdParser.RegisterHandler(cmd, handler);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x000832D0 File Offset: 0x000816D0
	public void Tick_CheckConnectionStatus()
	{
		try
		{
			if (!this._tcpClient.Connected)
			{
				NetUtil.Log("disconnection detected. (_tcpClient.Connected == false).", Array.Empty<object>());
				throw new Exception();
			}
			if (this._tcpClient.Client.Poll(0, SelectMode.SelectRead))
			{
				byte[] buffer = new byte[1];
				if (this._tcpClient.Client.Receive(buffer, SocketFlags.Peek) == 0)
				{
					NetUtil.Log("disconnection detected. (failed to read by Poll/Receive).", Array.Empty<object>());
					throw new IOException();
				}
			}
		}
		catch (Exception ex)
		{
			this.DisconnectOnError("disconnection detected while checking connection status.", ex);
		}
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x00083374 File Offset: 0x00081774
	public void Tick_ReceivingData()
	{
		try
		{
			while (this._tcpClient.Available > 0)
			{
				byte[] array = new byte[2];
				int num = this._tcpClient.GetStream().Read(array, 0, array.Length);
				ushort num2 = BitConverter.ToUInt16(array, 0);
				if (num > 0 && num2 > 0)
				{
					byte[] array2 = new byte[(int)num2];
					int num3 = this._tcpClient.GetStream().Read(array2, 0, array2.Length);
					UsCmd c = new UsCmd(array2);
					UsCmdExecResult usCmdExecResult = this._cmdParser.Execute(c);
					if (usCmdExecResult != UsCmdExecResult.Succ)
					{
						if (usCmdExecResult != UsCmdExecResult.Failed)
						{
							if (usCmdExecResult == UsCmdExecResult.HandlerNotFound)
							{
								NetUtil.Log("net unknown cmd: {0}.", new object[]
								{
									new UsCmd(array2).ReadNetCmd()
								});
							}
						}
						else
						{
							NetUtil.Log("net cmd execution failed: {0}.", new object[]
							{
								new UsCmd(array2).ReadNetCmd()
							});
						}
					}
					num3++;
				}
			}
		}
		catch (Exception ex)
		{
			this.DisconnectOnError("error detected while receiving data.", ex);
		}
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x000834A0 File Offset: 0x000818A0
	public void Dispose()
	{
		this.Disconnect();
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x000834A8 File Offset: 0x000818A8
	public void SendPacket(UsCmd cmd)
	{
		try
		{
			byte[] bytes = BitConverter.GetBytes((ushort)cmd.WrittenLen);
			this._tcpClient.GetStream().Write(bytes, 0, bytes.Length);
			this._tcpClient.GetStream().Write(cmd.Buffer, 0, cmd.WrittenLen);
		}
		catch (Exception ex)
		{
			this.DisconnectOnError("error detected while sending data.", ex);
		}
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0008351C File Offset: 0x0008191C
	private void OnConnect(IAsyncResult asyncResult)
	{
		TcpClient tcpClient = (TcpClient)asyncResult.AsyncState;
		try
		{
			if (!tcpClient.Connected)
			{
				throw new Exception();
			}
			NetUtil.Log("connected successfully.", Array.Empty<object>());
			SysPost.InvokeMulticast(this, this.Connected);
		}
		catch (Exception ex)
		{
			this.DisconnectOnError("connection failed while handling OnConnect().", ex);
		}
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x00083590 File Offset: 0x00081990
	private void DisconnectOnError(string info, Exception ex)
	{
		NetUtil.Log(info, Array.Empty<object>());
		NetUtil.Log(ex.ToString(), Array.Empty<object>());
		this.Disconnect();
	}

	// Token: 0x04001819 RID: 6169
	private string _host = string.Empty;

	// Token: 0x0400181A RID: 6170
	private int _port;

	// Token: 0x0400181B RID: 6171
	private TcpClient _tcpClient;

	// Token: 0x0400181C RID: 6172
	private UsCmdParsing _cmdParser = new UsCmdParsing();
}
