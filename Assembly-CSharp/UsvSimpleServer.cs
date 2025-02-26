using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

// Token: 0x0200049B RID: 1179
public class UsvSimpleServer : IDisposable
{
	// Token: 0x060015BE RID: 5566 RVA: 0x00086074 File Offset: 0x00084474
	public UsvSimpleServer()
	{
		try
		{
			this._cmdExec.RegisterClientHandler(eNetCmd.CL_Handshake, new UsClientCmdHandler(this.NetHandle_Handshake));
			this._cmdExec.RegisterClientHandler(eNetCmd.CL_KeepAlive, new UsClientCmdHandler(this.NetHandle_KeepAlive));
			this._cmdExec.RegisterClientHandler(eNetCmd.CL_ExecCommand, new UsClientCmdHandler(this.NetHandle_ExecCommand));
			this._tcpListener = new TcpListener(IPAddress.Any, 39981);
			this._tcpListener.Start();
			this._tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.OnAcceptTcpClient), this._tcpListener);
			this.NetLog("simple server listening started at: {0}.", new object[]
			{
				39981
			});
			this._isListening = true;
		}
		catch (Exception ex)
		{
			this.NetLog(ex.ToString(), Array.Empty<object>());
			throw;
		}
	}

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x060015BF RID: 5567 RVA: 0x00086194 File Offset: 0x00084594
	// (remove) Token: 0x060015C0 RID: 5568 RVA: 0x000861CC File Offset: 0x000845CC
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event UsvClientDisconnectedHandler ClientDisconnected;

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00086202 File Offset: 0x00084602
	public UsCmdParsing CmdExecutor
	{
		get
		{
			return this._cmdExec;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0008620A File Offset: 0x0008460A
	public bool IsListening
	{
		get
		{
			return this._isListening;
		}
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x00086214 File Offset: 0x00084614
	public void Dispose()
	{
		foreach (TcpClient tcpClient in this._tcpClients.Values)
		{
			if (tcpClient != null)
			{
				this.NetLog(string.Format("Disconnecting client {0}.", tcpClient.Client.RemoteEndPoint), Array.Empty<object>());
				tcpClient.Close();
			}
		}
		this._tcpClients.Clear();
		if (this._tcpListener != null)
		{
			this._tcpListener.Stop();
			this._tcpListener = null;
			this._isListening = false;
			this.NetLog("Listening ended.", Array.Empty<object>());
		}
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000862DC File Offset: 0x000846DC
	public void Update()
	{
		foreach (KeyValuePair<string, TcpClient> keyValuePair in this._tcpClients)
		{
			TcpClient value = keyValuePair.Value;
			try
			{
				while (value != null && value.Available > 0)
				{
					byte[] array = new byte[2];
					int num = value.GetStream().Read(array, 0, array.Length);
					ushort num2 = BitConverter.ToUInt16(array, 0);
					if (num > 0 && num2 > 0)
					{
						byte[] array2 = new byte[(int)num2];
						int num3 = value.GetStream().Read(array2, 0, array2.Length);
						if (num3 == array2.Length)
						{
							this._cmdExec.ExecuteClient(keyValuePair.Key, new UsCmd(array2));
						}
						else
						{
							this.NetLog(string.Format("corrupted cmd received - len: {0}", num3), Array.Empty<object>());
						}
					}
				}
			}
			catch (Exception ex)
			{
				value.Close();
				this._toBeRemoved.Add(keyValuePair.Key);
				if (this.ClientDisconnected != null)
				{
					this.ClientDisconnected(keyValuePair.Key);
				}
			}
		}
		foreach (string key in this._toBeRemoved)
		{
			this._tcpClients.Remove(key);
		}
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000864AC File Offset: 0x000848AC
	public TcpClient FindClient(string clientID)
	{
		TcpClient tcpClient = null;
		if (!this._tcpClients.TryGetValue(clientID, out tcpClient))
		{
			this.NetLog("unknown client: {0}", new object[]
			{
				clientID
			});
			return null;
		}
		if (tcpClient == null || tcpClient.GetStream() == null)
		{
			this.NetLog("bad client: {0}", new object[]
			{
				clientID
			});
			return null;
		}
		return tcpClient;
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x00086510 File Offset: 0x00084910
	public void SendCommand(string clientID, UsCmd cmd)
	{
		TcpClient tcpClient = this.FindClient(clientID);
		if (tcpClient != null)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)cmd.WrittenLen);
			tcpClient.GetStream().Write(bytes, 0, bytes.Length);
			tcpClient.GetStream().Write(cmd.Buffer, 0, cmd.WrittenLen);
		}
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x00086560 File Offset: 0x00084960
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
			TcpClient tcpClient = tcpListener.EndAcceptTcpClient(asyncResult);
			this._tcpClients.Add(tcpClient.Client.RemoteEndPoint.ToString(), tcpClient);
			this.NetLog(string.Format("Client {0} connected.", tcpClient.Client.RemoteEndPoint), Array.Empty<object>());
		}
		catch (SocketException ex)
		{
			this.NetLog(string.Format("<color=red>Error accepting TCP connection: {0}</color>", ex.Message), Array.Empty<object>());
		}
		catch (ObjectDisposedException)
		{
		}
		catch (Exception ex2)
		{
			this.NetLog(string.Format("<color=red>An error occured: {0}</color>", ex2.Message), Array.Empty<object>());
		}
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x00086650 File Offset: 0x00084A50
	private void NetLog(string text, params object[] args)
	{
		string text2 = (args.Length <= 0) ? text : string.Format(text, args);
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x00086674 File Offset: 0x00084A74
	private void NetLogClient(string clientID, string text, params object[] args)
	{
		string text2 = (args.Length <= 0) ? text : string.Format(text, args);
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x00086698 File Offset: 0x00084A98
	private bool NetHandle_Handshake(string clientID, eNetCmd cmd, UsCmd c)
	{
		this.NetLog("executing handshake.", Array.Empty<object>());
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_HandshakeResponse);
		this.SendCommand(clientID, usCmd);
		return true;
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x000866D0 File Offset: 0x00084AD0
	private bool NetHandle_KeepAlive(string clientID, eNetCmd cmd, UsCmd c)
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_KeepAliveResponse);
		this.SendCommand(clientID, usCmd);
		return true;
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x000866F8 File Offset: 0x00084AF8
	public void RegisterHandlerClass(Type handlerClassType, object handlerInst)
	{
		foreach (MethodInfo methodInfo in handlerClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
		{
			foreach (object obj in methodInfo.GetCustomAttributes(typeof(ClientConsoleCmdHandler), false))
			{
				ClientConsoleCmdHandler clientConsoleCmdHandler = obj as ClientConsoleCmdHandler;
				if (clientConsoleCmdHandler != null)
				{
					try
					{
						Delegate @delegate = Delegate.CreateDelegate(typeof(UsvClientConsoleCmdHandler), handlerInst, methodInfo);
						if (@delegate != null)
						{
							string key = clientConsoleCmdHandler.Command.ToLower();
							this._clientConsoleCmdHandlers[key] = (UsvClientConsoleCmdHandler)@delegate;
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000867C0 File Offset: 0x00084BC0
	private bool NetHandle_ExecCommand(string clientID, eNetCmd cmd, UsCmd c)
	{
		string text = c.ReadString();
		string[] array = text.Split(Array.Empty<char>());
		if (array.Length == 0)
		{
			Log.Info("empty command received, ignored.", Array.Empty<object>());
			return false;
		}
		UsvClientConsoleCmdHandler usvClientConsoleCmdHandler;
		if (!this._clientConsoleCmdHandlers.TryGetValue(array[0].ToLower(), out usvClientConsoleCmdHandler))
		{
			Log.Info("unknown command ('{0}') received, ignored.", new object[]
			{
				text
			});
			return false;
		}
		bool flag = usvClientConsoleCmdHandler(clientID, array);
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_ExecCommandResponse);
		usCmd.WriteInt32((!flag) ? 0 : 1);
		this.SendCommand(clientID, usCmd);
		return true;
	}

	// Token: 0x040018B1 RID: 6321
	private TcpListener _tcpListener;

	// Token: 0x040018B2 RID: 6322
	private Dictionary<string, TcpClient> _tcpClients = new Dictionary<string, TcpClient>();

	// Token: 0x040018B3 RID: 6323
	private UsCmdParsing _cmdExec = new UsCmdParsing();

	// Token: 0x040018B4 RID: 6324
	private bool _isListening;

	// Token: 0x040018B5 RID: 6325
	private List<string> _toBeRemoved = new List<string>();

	// Token: 0x040018B6 RID: 6326
	private Dictionary<string, UsvClientConsoleCmdHandler> _clientConsoleCmdHandlers = new Dictionary<string, UsvClientConsoleCmdHandler>();
}
