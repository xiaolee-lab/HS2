using System;
using System.Net;
using System.Net.Sockets;

// Token: 0x02000491 RID: 1169
public class UsNet : IDisposable
{
	// Token: 0x06001590 RID: 5520 RVA: 0x00085658 File Offset: 0x00083A58
	public UsNet()
	{
		try
		{
			this._tcpListener = new TcpListener(IPAddress.Any, 39980);
			this._tcpListener.Start();
			this._tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.OnAcceptTcpClient), this._tcpListener);
			this.AddToLog("usmooth listening started at: {0}.", new object[]
			{
				39980
			});
			this._isListening = true;
		}
		catch (Exception ex)
		{
			this.AddToLog(ex.ToString(), Array.Empty<object>());
			throw;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06001591 RID: 5521 RVA: 0x00085710 File Offset: 0x00083B10
	public UsCmdParsing CmdExecutor
	{
		get
		{
			return this._cmdExec;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06001592 RID: 5522 RVA: 0x00085718 File Offset: 0x00083B18
	public bool IsListening
	{
		get
		{
			return this._isListening;
		}
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x00085720 File Offset: 0x00083B20
	~UsNet()
	{
		this.FreeResources();
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x00085750 File Offset: 0x00083B50
	public void Dispose()
	{
		this.CloseTcpClient();
		if (this._tcpListener != null)
		{
			this.FreeResources();
			this.AddToLog("Listening canceled.", Array.Empty<object>());
		}
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x00085779 File Offset: 0x00083B79
	private void FreeResources()
	{
		if (this._tcpListener != null)
		{
			this._tcpListener.Stop();
			this._tcpListener = null;
			this._isListening = false;
		}
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000857A0 File Offset: 0x00083BA0
	private void CloseTcpClient()
	{
		if (this._tcpClient != null)
		{
			this.AddToLog(string.Format("Disconnecting client {0}.", this._tcpClient.Client.RemoteEndPoint), Array.Empty<object>());
			this._tcpClient.Close();
			this._tcpClient = null;
		}
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000857F0 File Offset: 0x00083BF0
	public void Update()
	{
		if (this._tcpClient == null)
		{
			return;
		}
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
					if (num3 == array2.Length)
					{
						this._cmdExec.Execute(new UsCmd(array2));
					}
					else
					{
						this.AddToLog(string.Format("corrupted cmd received - len: {0}", num3), Array.Empty<object>());
					}
				}
			}
		}
		catch (Exception ex)
		{
			this.CloseTcpClient();
		}
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x000858D0 File Offset: 0x00083CD0
	public void SendCommand(UsCmd cmd)
	{
		if (this._tcpClient == null || this._tcpClient.GetStream() == null)
		{
			return;
		}
		object netLocker = this._netLocker;
		lock (netLocker)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)cmd.WrittenLen);
			this._tcpClient.GetStream().Write(bytes, 0, bytes.Length);
			this._tcpClient.GetStream().Write(cmd.Buffer, 0, cmd.WrittenLen);
		}
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x00085968 File Offset: 0x00083D68
	private void OnAcceptTcpClient(IAsyncResult asyncResult)
	{
		TcpListener tcpListener = (TcpListener)asyncResult.AsyncState;
		if (tcpListener == null)
		{
			return;
		}
		tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.OnAcceptTcpClient), tcpListener);
		try
		{
			this._tcpClient = tcpListener.EndAcceptTcpClient(asyncResult);
			this.AddToLog(string.Format("Client {0} connected.", this._tcpClient.Client.RemoteEndPoint), Array.Empty<object>());
		}
		catch (SocketException ex)
		{
			this.AddToLog(string.Format("<color=red>Error accepting TCP connection: {0}</color>", ex.Message), Array.Empty<object>());
		}
		catch (ObjectDisposedException)
		{
		}
		catch (Exception ex2)
		{
			this.AddToLog(string.Format("<color=red>An error occured: {0}</color>", ex2.Message), Array.Empty<object>());
		}
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x00085A44 File Offset: 0x00083E44
	private void AddToLog(string text, params object[] args)
	{
		string text2 = (args.Length <= 0) ? text : string.Format(text, args);
		if (this._tcpClient != null)
		{
		}
	}

	// Token: 0x0400189F RID: 6303
	public static UsNet Instance;

	// Token: 0x040018A0 RID: 6304
	private TcpListener _tcpListener;

	// Token: 0x040018A1 RID: 6305
	private TcpClient _tcpClient;

	// Token: 0x040018A2 RID: 6306
	private readonly object _netLocker = new object();

	// Token: 0x040018A3 RID: 6307
	private UsCmdParsing _cmdExec = new UsCmdParsing();

	// Token: 0x040018A4 RID: 6308
	private bool _isListening;
}
