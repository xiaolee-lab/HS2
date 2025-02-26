using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A6 RID: 1190
public class RuntimeCoroutineStats
{
	// Token: 0x060015F1 RID: 5617 RVA: 0x000871F4 File Offset: 0x000855F4
	public void MarkCreation(int seq, string mangledName)
	{
		if (!this._broadcastStarted)
		{
			return;
		}
		CoroutineCreation coroutineCreation = new CoroutineCreation(seq);
		coroutineCreation.mangledName = mangledName;
		coroutineCreation.stacktrace = StackTraceUtility.ExtractStackTrace();
		coroutineCreation.curFrame = Time.frameCount;
		this._activities.Add(coroutineCreation);
		this._activeCoroutines.Add(seq);
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x0008724C File Offset: 0x0008564C
	public void MarkMoveNext(int seq, float timeConsumed)
	{
		if (!this._broadcastStarted)
		{
			return;
		}
		if (!this._activeCoroutines.Contains(seq))
		{
			return;
		}
		CoroutineExecution coroutineExecution = new CoroutineExecution(seq);
		coroutineExecution.timeConsumed = timeConsumed;
		coroutineExecution.curFrame = Time.frameCount;
		this._activities.Add(coroutineExecution);
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x0008729C File Offset: 0x0008569C
	public void MarkTermination(int seq)
	{
		if (!this._broadcastStarted)
		{
			return;
		}
		if (!this._activeCoroutines.Contains(seq))
		{
			return;
		}
		CoroutineTermination coroutineTermination = new CoroutineTermination(seq);
		coroutineTermination.curFrame = Time.frameCount;
		this._activities.Add(coroutineTermination);
		this._activeCoroutines.Remove(seq);
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x000872F4 File Offset: 0x000856F4
	public IEnumerator BroadcastCoroutine()
	{
		this._broadcastStarted = true;
		for (;;)
		{
			if (this.hasBroadcastReceivers())
			{
				this._onBroadcast(this._activities);
			}
			if (this.hasCoStatsAnalyzer2File())
			{
				this._onAnalyzer2File(this._activities);
			}
			this._activities.Clear();
			yield return new WaitForSeconds(CoroutineRuntimeTrackingConfig.BroadcastInterval);
		}
		yield break;
	}

	// Token: 0x1400005F RID: 95
	// (add) Token: 0x060015F5 RID: 5621 RVA: 0x0008730F File Offset: 0x0008570F
	// (remove) Token: 0x060015F6 RID: 5622 RVA: 0x0008733F File Offset: 0x0008573F
	public event OnCoStatsBroadcast OnBroadcast
	{
		add
		{
			this._onBroadcast = (OnCoStatsBroadcast)Delegate.Remove(this._onBroadcast, value);
			this._onBroadcast = (OnCoStatsBroadcast)Delegate.Combine(this._onBroadcast, value);
		}
		remove
		{
			this._onBroadcast = (OnCoStatsBroadcast)Delegate.Remove(this._onBroadcast, value);
		}
	}

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x060015F7 RID: 5623 RVA: 0x00087358 File Offset: 0x00085758
	// (remove) Token: 0x060015F8 RID: 5624 RVA: 0x00087388 File Offset: 0x00085788
	public event OnCoStatsAnalyzer2File OnAnalyzer2File
	{
		add
		{
			this._onAnalyzer2File = (OnCoStatsAnalyzer2File)Delegate.Remove(this._onAnalyzer2File, value);
			this._onAnalyzer2File = (OnCoStatsAnalyzer2File)Delegate.Combine(this._onAnalyzer2File, value);
		}
		remove
		{
			this._onAnalyzer2File = (OnCoStatsAnalyzer2File)Delegate.Remove(this._onAnalyzer2File, value);
		}
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x000873A1 File Offset: 0x000857A1
	private bool hasBroadcastReceivers()
	{
		return this._onBroadcast != null && this._onBroadcast.GetInvocationList().Length > 0;
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x000873C1 File Offset: 0x000857C1
	private bool hasCoStatsAnalyzer2File()
	{
		return this._onAnalyzer2File != null && this._onAnalyzer2File.GetInvocationList().Length > 0;
	}

	// Token: 0x040018CC RID: 6348
	public static RuntimeCoroutineStats Instance = new RuntimeCoroutineStats();

	// Token: 0x040018CD RID: 6349
	private OnCoStatsBroadcast _onBroadcast;

	// Token: 0x040018CE RID: 6350
	private OnCoStatsAnalyzer2File _onAnalyzer2File;

	// Token: 0x040018CF RID: 6351
	private List<CoroutineActivity> _activities = new List<CoroutineActivity>();

	// Token: 0x040018D0 RID: 6352
	private HashSet<int> _activeCoroutines = new HashSet<int>();

	// Token: 0x040018D1 RID: 6353
	private bool _broadcastStarted;
}
