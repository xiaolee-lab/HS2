using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class LogService : IDisposable
{
	// Token: 0x060014D6 RID: 5334 RVA: 0x000826C4 File Offset: 0x00080AC4
	public LogService(bool logIntoFile, int oldLogsKeptDays, bool useMemBuf)
	{
		this.RegisterCallback();
		this._useMemBuf = useMemBuf;
		this._memBuf = new LogBuffer(LogService.UserDefinedMemBufSize);
		if (logIntoFile)
		{
			try
			{
				DateTime now = DateTime.Now;
				string text = LogUtil.CombinePaths(new string[]
				{
					Application.persistentDataPath,
					"log",
					LogUtil.FormatDateAsFileNameString(now)
				});
				Directory.CreateDirectory(text);
				string text2 = Path.Combine(text, string.Concat(new object[]
				{
					LogUtil.FormatDateAsFileNameString(now),
					'_',
					LogUtil.FormatTimeAsFileNameString(now),
					".txt"
				}));
				this._logWriter = new FileInfo(text2).CreateText();
				this._logWriter.AutoFlush = true;
				this._logPath = text2;
				Log.Info("'Log Into File' enabled, file opened successfully. ('{0}')", new object[]
				{
					this._logPath
				});
				LogService.LastLogFile = this._logPath;
			}
			catch (Exception ex)
			{
				Log.Info("'Log Into File' enabled but failed to open file.", Array.Empty<object>());
				Log.Exception(ex);
			}
		}
		else
		{
			Log.Info("'Log Into File' disabled.", Array.Empty<object>());
			LogService.LastLogFile = string.Empty;
		}
		if (oldLogsKeptDays > 0)
		{
			try
			{
				this.CleanupLogsOlderThan(oldLogsKeptDays);
			}
			catch (Exception ex2)
			{
				Log.Exception(ex2);
				Log.Error("Cleaning up logs ({0}) failed.", new object[]
				{
					oldLogsKeptDays
				});
			}
		}
	}

	// Token: 0x14000055 RID: 85
	// (add) Token: 0x060014D7 RID: 5335 RVA: 0x00082848 File Offset: 0x00080C48
	// (remove) Token: 0x060014D8 RID: 5336 RVA: 0x00082880 File Offset: 0x00080C80
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event LogTargetHandler LogTargets;

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060014D9 RID: 5337 RVA: 0x000828B6 File Offset: 0x00080CB6
	// (set) Token: 0x060014DA RID: 5338 RVA: 0x000828BE File Offset: 0x00080CBE
	public bool UseMemBuf
	{
		get
		{
			return this._useMemBuf;
		}
		set
		{
			this._useMemBuf = value;
			this.FlushLogWriting();
		}
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000828CD File Offset: 0x00080CCD
	public void Dispose()
	{
		this.FlushLogWriting();
		Log.Info("destroying log service...", Array.Empty<object>());
		if (this._logWriter != null)
		{
			this._logWriter.Close();
		}
		this._disposed = true;
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x00082904 File Offset: 0x00080D04
	public void WriteLog(string content, LogType type)
	{
		if (LogUtil.EnableInMemoryStorage)
		{
			if (type != LogType.Error)
			{
				if (type == LogType.Exception)
				{
					LogUtil.PushInMemoryException(content);
				}
			}
			else
			{
				LogUtil.PushInMemoryError(content);
			}
		}
		if (this._useMemBuf)
		{
			if (type == LogType.Error || Encoding.Default.GetByteCount(content) > this._memBuf.BufSize)
			{
				this.FlushMemBuffer();
				if (this._logWriter != null)
				{
					this._logWriter.Write(content);
				}
			}
			else if (!this._memBuf.Receive(content))
			{
				this.FlushMemBuffer();
				this._memBuf.Receive(content);
			}
		}
		else if (this._logWriter != null)
		{
			this._logWriter.Write(content);
		}
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x000829D6 File Offset: 0x00080DD6
	public void FlushLogWriting()
	{
		this.FlushFoldedMessage();
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000829E0 File Offset: 0x00080DE0
	private void CleanupLogsOlderThan(int days)
	{
		DateTime dt = DateTime.Now.Subtract(TimeSpan.FromDays((double)days));
		string strB = LogUtil.FormatDateAsFileNameString(dt);
		DirectoryInfo directoryInfo = new DirectoryInfo(LogUtil.CombinePaths(new string[]
		{
			Application.persistentDataPath,
			"log"
		}));
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		List<string> list = new List<string>();
		foreach (DirectoryInfo directoryInfo2 in directories)
		{
			if (string.CompareOrdinal(directoryInfo2.Name, strB) <= 0)
			{
				list.Add(directoryInfo2.FullName);
			}
		}
		foreach (string text in list)
		{
			Directory.Delete(text, true);
			Log.Info("[ Log Cleanup ]: {0}", new object[]
			{
				text
			});
		}
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00082AE4 File Offset: 0x00080EE4
	private void RegisterCallback()
	{
		Application.logMessageReceivedThreaded += this.OnLogReceived;
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x00082AF7 File Offset: 0x00080EF7
	private void WriteTrace(string content)
	{
		this.OnLogReceived(content, string.Empty, LogType.Log);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x00082B08 File Offset: 0x00080F08
	private void OnLogReceived(string condition, string stackTrace, LogType type)
	{
		if (this._disposed)
		{
			throw new Exception(string.Format("LogService used after being disposed. (content:{0})", condition));
		}
		if (this._reentranceGuard)
		{
			throw new Exception(string.Format("LogService Reentrance occurred. (content:{0})", condition));
		}
		this._reentranceGuard = true;
		this._seqID += 1;
		switch (type)
		{
		case LogType.Error:
			this._errorCount++;
			break;
		case LogType.Assert:
			this._assertCount++;
			break;
		case LogType.Exception:
			this._exceptionCount++;
			break;
		}
		try
		{
			if (type == LogType.Exception)
			{
				condition = string.Format("{0}\r\n  {1}", condition, stackTrace.Replace("\n", "\r\n  "));
			}
			if (condition == this._lastWrittenContent)
			{
				this._foldedCount++;
			}
			else
			{
				this.FlushFoldedMessage();
				this.WriteLog(string.Format("{0:0.00} {1}: {2}\r\n", Time.realtimeSinceStartup, type, condition), type);
				this._lastWrittenContent = condition;
				this._lastWrittenType = type;
			}
			if (this.LogTargets != null)
			{
				foreach (LogTargetHandler logTargetHandler in this.LogTargets.GetInvocationList())
				{
					ISynchronizeInvoke synchronizeInvoke = logTargetHandler.Target as ISynchronizeInvoke;
					LogEventArgs logEventArgs = new LogEventArgs((int)this._seqID, type, condition, stackTrace, Time.realtimeSinceStartup);
					if (synchronizeInvoke != null && synchronizeInvoke.InvokeRequired)
					{
						synchronizeInvoke.Invoke(logTargetHandler, new object[]
						{
							this,
							logEventArgs
						});
					}
					else
					{
						logTargetHandler(this, logEventArgs);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
		}
		finally
		{
			this._reentranceGuard = false;
		}
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x00082D18 File Offset: 0x00081118
	private void FlushMemBuffer()
	{
		if (!this._useMemBuf)
		{
			return;
		}
		if (this._logWriter != null && this._memBuf.BufWrittenBytes > 0)
		{
			this._logWriter.Write(Encoding.Default.GetString(this._memBuf.Buf, 0, this._memBuf.BufWrittenBytes));
		}
		this._memBuf.Clear();
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x00082D84 File Offset: 0x00081184
	private void FlushFoldedMessage()
	{
		this.FlushMemBuffer();
		if (this._foldedCount > 0)
		{
			if (this._logWriter != null)
			{
				this._logWriter.Write(string.Format("{0:0.00} {1}: --<< folded {2} messages >>--\r\n", Time.realtimeSinceStartup, this._lastWrittenType, this._foldedCount));
			}
			this._foldedCount = 0;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00082DEA File Offset: 0x000811EA
	// (set) Token: 0x060014E5 RID: 5349 RVA: 0x00082DF1 File Offset: 0x000811F1
	public static string LastLogFile { get; set; }

	// Token: 0x040017FD RID: 6141
	public static int UserDefinedMemBufSize;

	// Token: 0x040017FE RID: 6142
	private string _logPath;

	// Token: 0x040017FF RID: 6143
	private StreamWriter _logWriter;

	// Token: 0x04001800 RID: 6144
	private ushort _seqID;

	// Token: 0x04001801 RID: 6145
	private int _assertCount;

	// Token: 0x04001802 RID: 6146
	private int _errorCount;

	// Token: 0x04001803 RID: 6147
	private int _exceptionCount;

	// Token: 0x04001804 RID: 6148
	private bool _disposed;

	// Token: 0x04001805 RID: 6149
	private bool _useMemBuf = true;

	// Token: 0x04001806 RID: 6150
	private LogBuffer _memBuf;

	// Token: 0x04001807 RID: 6151
	private string _lastWrittenContent;

	// Token: 0x04001808 RID: 6152
	private LogType _lastWrittenType;

	// Token: 0x04001809 RID: 6153
	private int _foldedCount;

	// Token: 0x0400180A RID: 6154
	private bool _reentranceGuard;
}
