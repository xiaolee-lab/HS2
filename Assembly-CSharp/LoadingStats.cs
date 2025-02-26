using System;
using System.Text;
using AClockworkBerry;
using UnityEngine;

// Token: 0x020004BA RID: 1210
public class LoadingStats : MonoBehaviour
{
	// Token: 0x0600165D RID: 5725 RVA: 0x00089591 File Offset: 0x00087991
	private void Awake()
	{
		ScreenLogger.Instance.enabled = false;
		ScreenLogger.Instance.LogMessages = false;
		ScreenLogger.Instance.LogWarnings = false;
		ScreenLogger.Instance.LogErrors = false;
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000895BF File Offset: 0x000879BF
	private void OnEnable()
	{
		ScreenLogger.Instance.enabled = true;
		ScreenLogger.Instance.Clear();
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000895D6 File Offset: 0x000879D6
	private void OnDisable()
	{
		if (ScreenLogger.Instance != null)
		{
			ScreenLogger.Instance.enabled = false;
			ScreenLogger.Instance.Clear();
		}
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x00089600 File Offset: 0x00087A00
	public void LogLua(string path, int sizeInBytes, bool loadFromCache)
	{
		this.m_strBuilder.Length = 0;
		this.m_strBuilder.AppendFormat("{0:0.00} ({1:0.00}kb) {2} {3}", new object[]
		{
			Time.time,
			(double)sizeInBytes / 1024.0,
			(!loadFromCache) ? "#LuaIO" : "#LuaCache",
			path
		});
		ScreenLogger.Instance.EnqueueDirectly(this.m_strBuilder.ToString(), LogType.Log);
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x00089684 File Offset: 0x00087A84
	public void LogSync(string path, double duration)
	{
		this.m_strBuilder.Length = 0;
		this.m_strBuilder.AppendFormat("{0:0.00} ({1:0.00}ms) #Sync {2}", Time.time, duration, path);
		ScreenLogger.Instance.EnqueueDirectly(this.m_strBuilder.ToString(), LogType.Log);
		ScreenLogger.Instance.SyncTopN.TryAdd(duration, path);
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000896E8 File Offset: 0x00087AE8
	public void LogAsync(string path, double duration)
	{
		this.m_strBuilder.Length = 0;
		this.m_strBuilder.AppendFormat("{0:0.00} ({1:0.00}ms) #Async {2}", Time.time, duration, path);
		ScreenLogger.Instance.EnqueueDirectly(this.m_strBuilder.ToString(), LogType.Warning);
		ScreenLogger.Instance.AsyncTopN.TryAdd(duration, path);
	}

	// Token: 0x04001915 RID: 6421
	public static LoadingStats Instance;

	// Token: 0x04001916 RID: 6422
	private StringBuilder m_strBuilder = new StringBuilder(256);
}
