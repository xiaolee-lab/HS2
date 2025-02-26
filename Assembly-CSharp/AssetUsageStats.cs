using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004B9 RID: 1209
public class AssetUsageStats : IDisposable
{
	// Token: 0x06001649 RID: 5705 RVA: 0x00088ECC File Offset: 0x000872CC
	public AssetUsageStats(bool enableTracking)
	{
		this._enableTracking = enableTracking;
		if (this._enableTracking)
		{
			try
			{
				this._logDir = Path.Combine(Application.persistentDataPath, "asset_stats");
				if (!Directory.Exists(this._logDir))
				{
					Directory.CreateDirectory(this._logDir);
				}
			}
			catch (Exception ex)
			{
				this._logDir = string.Empty;
				this._enableTracking = false;
				this.LogError(ex.Message);
				this.LogError("Failed to prepare the stats dir, aborted.");
			}
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x00088FA4 File Offset: 0x000873A4
	public bool EnableTracking
	{
		get
		{
			return this._enableTracking;
		}
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x00088FAC File Offset: 0x000873AC
	private void LogInfo(string info)
	{
		UnityEngine.Debug.LogFormat("[AssetUsageStats] (info): {0} ", new object[]
		{
			info
		});
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x00088FC2 File Offset: 0x000873C2
	private void LogError(string err)
	{
		UnityEngine.Debug.LogErrorFormat("[AssetUsageStats] (err): {0} ", new object[]
		{
			err
		});
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x00088FD8 File Offset: 0x000873D8
	public bool PrepareWriter()
	{
		if (!this._enableTracking)
		{
			return false;
		}
		DateTime now = DateTime.Now;
		if (this._logWriter != null)
		{
			if (now.Hour == this._lastWriteTime.Hour && now.Minute / 10 == this._lastWriteTime.Minute / 10)
			{
				return true;
			}
			this.LogInfo("Switching file at: " + now.ToString());
			this.CloseWriter();
		}
		bool result;
		try
		{
			string text = (!Application.isEditor) ? IPManager.GetIP(ADDRESSFAM.IPv4) : "editor";
			string text2 = "000";
			string text3 = "000";
			string path = string.Format("{0}_{1}-{2}_{3}_{4}.txt", new object[]
			{
				text,
				SysUtil.FormatDateAsFileNameString(now),
				SysUtil.FormatTimeAsFileNameString(now),
				text2,
				text3
			});
			string text4 = Path.Combine(this._logDir, path);
			if (!File.Exists(text4))
			{
				File.Create(text4).Dispose();
				this.LogInfo("Creating new text successfully at: " + text4);
			}
			if (this._logWriter == null)
			{
				this._logWriter = new StreamWriter(text4, true);
				this._logWriter.AutoFlush = true;
			}
			result = true;
		}
		catch (Exception ex)
		{
			this._enableTracking = false;
			this.LogError("Creating new text failed: " + ex.Message);
			this.CloseWriter();
			result = false;
		}
		return result;
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x0008915C File Offset: 0x0008755C
	public void CloseWriter()
	{
		if (this._logWriter != null)
		{
			try
			{
				this._logWriter.Close();
			}
			catch (Exception ex)
			{
				this.LogError(ex.Message);
			}
			finally
			{
				this._logWriter = null;
				this.LogInfo("Writer closed.");
			}
		}
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000891C8 File Offset: 0x000875C8
	public void Dispose()
	{
		if (this._enableTracking && this._logWriter != null)
		{
			this.CloseWriter();
			this._enableTracking = false;
		}
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x000891F0 File Offset: 0x000875F0
	public void TrackLuaRequest(string path, int bytes, bool loadFromCache)
	{
		if (!this._enableTracking)
		{
			return;
		}
		LoadingStats.Instance.LogLua(path, bytes, loadFromCache);
		AssetRequestInfo assetRequestInfo = this.NewRequest(path);
		assetRequestInfo.requestType = ResourceRequestType.Ordinary;
		this.TrackRequestWithObject(assetRequestInfo, this._luaAsset);
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x00089232 File Offset: 0x00087632
	public void TrackSyncStartTiming()
	{
		if (!this._enableTracking)
		{
			return;
		}
		this.m_syncTimer.Reset();
		this.m_syncTimer.Start();
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x00089258 File Offset: 0x00087658
	public double TrackSyncStopTiming(string path)
	{
		if (!this._enableTracking)
		{
			return 0.0;
		}
		this.m_syncTimer.Stop();
		double num = (double)this.m_syncTimer.ElapsedTicks / 10000.0;
		LoadingStats.Instance.LogSync(path, num);
		return num;
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000892AC File Offset: 0x000876AC
	public void TrackSyncRequest(UnityEngine.Object spawned, string path)
	{
		if (!this._enableTracking)
		{
			return;
		}
		double duration = this.TrackSyncStopTiming(path);
		AssetRequestInfo assetRequestInfo = this.NewRequest(path);
		assetRequestInfo.duration = duration;
		assetRequestInfo.requestType = ResourceRequestType.Ordinary;
		this.TrackRequestWithObject(assetRequestInfo, spawned);
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x000892EC File Offset: 0x000876EC
	public void TrackResourcesDotLoad(UnityEngine.Object loaded, string path)
	{
		if (!this._enableTracking)
		{
			return;
		}
		double duration = this.TrackSyncStopTiming(path);
		AssetRequestInfo assetRequestInfo = this.NewRequest(path);
		assetRequestInfo.duration = duration;
		assetRequestInfo.requestType = ResourceRequestType.Ordinary;
		this.TrackRequestWithObject(assetRequestInfo, loaded);
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0008932B File Offset: 0x0008772B
	public void TrackAsyncRequest(object handle, string path)
	{
		if (!this._enableTracking)
		{
			return;
		}
		this.InProgressAsyncObjects[handle] = this.NewRequest(path);
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x0008934C File Offset: 0x0008774C
	public void TrackAsyncDone(object handle, UnityEngine.Object target)
	{
		if (!this._enableTracking || target == null)
		{
			return;
		}
		AssetRequestInfo assetRequestInfo;
		if (!this.InProgressAsyncObjects.TryGetValue(handle, out assetRequestInfo))
		{
			return;
		}
		assetRequestInfo.requestType = ResourceRequestType.Async;
		assetRequestInfo.duration = 0.0;
		LoadingStats.Instance.LogAsync(assetRequestInfo.resourcePath, 0.0);
		this.TrackRequestWithObject(assetRequestInfo, target);
		this.InProgressAsyncObjects.Remove(handle);
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000893CC File Offset: 0x000877CC
	public void TrackSceneLoaded(string sceneName)
	{
		if (!this._enableTracking)
		{
			return;
		}
		GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			this.TrackSyncRequest(rootGameObjects[i], sceneName + ".unity");
		}
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x0008941C File Offset: 0x0008781C
	private AssetRequestInfo NewRequest(string path)
	{
		return new AssetRequestInfo
		{
			resourcePath = path
		};
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x00089438 File Offset: 0x00087838
	private void TrackRequestWithObject(AssetRequestInfo req, UnityEngine.Object obj)
	{
		if (obj == null || !this._enableTracking || !this.PrepareWriter())
		{
			return;
		}
		if (!LoadingStats.Instance.enabled)
		{
			return;
		}
		try
		{
			req.RecordObject(obj);
			string value = req.ToString();
			if (this._logWriter != null && !string.IsNullOrEmpty(value) && req.duration >= 1.0)
			{
				this._logWriter.WriteLine(value);
			}
			this._lastWriteTime = DateTime.Now;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogErrorFormat("[ResourceTracker.TrackRequestWithObject] error: {0} \n {1} \n {2}", new object[]
			{
				ex.Message,
				(req == null) ? string.Empty : req.ToString(),
				ex.StackTrace
			});
		}
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x00089520 File Offset: 0x00087920
	private static void ThreadedUploading(object obj)
	{
		if (!(obj is AssetUsageStats))
		{
			return;
		}
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x0008953C File Offset: 0x0008793C
	private void UploadFiles()
	{
		if (AssetUsageStats.<>f__mg$cache0 == null)
		{
			AssetUsageStats.<>f__mg$cache0 = new ParameterizedThreadStart(AssetUsageStats.ThreadedUploading);
		}
		Thread thread = new Thread(AssetUsageStats.<>f__mg$cache0);
		thread.Start(this);
		thread.Join();
	}

	// Token: 0x0400190C RID: 6412
	public static AssetUsageStats Instance;

	// Token: 0x0400190D RID: 6413
	private bool _enableTracking;

	// Token: 0x0400190E RID: 6414
	private StreamWriter _logWriter;

	// Token: 0x0400190F RID: 6415
	private DateTime _lastWriteTime = DateTime.Now;

	// Token: 0x04001910 RID: 6416
	private string _logDir = string.Empty;

	// Token: 0x04001911 RID: 6417
	private GameObject _luaAsset = new GameObject("LuaAsset");

	// Token: 0x04001912 RID: 6418
	private Stopwatch m_syncTimer = new Stopwatch();

	// Token: 0x04001913 RID: 6419
	private Dictionary<object, AssetRequestInfo> InProgressAsyncObjects = new Dictionary<object, AssetRequestInfo>();

	// Token: 0x04001914 RID: 6420
	[CompilerGenerated]
	private static ParameterizedThreadStart <>f__mg$cache0;
}
