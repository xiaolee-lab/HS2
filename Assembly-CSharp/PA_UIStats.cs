using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004CA RID: 1226
public class PA_UIStats
{
	// Token: 0x060016A7 RID: 5799 RVA: 0x0008B208 File Offset: 0x00089608
	public void BeginFrame()
	{
		if (this._cachedFrames.Count > 0)
		{
			this._curFrame = this._cachedFrames[this._cachedFrames.Count - 1];
			this._cachedFrames.RemoveAt(this._cachedFrames.Count - 1);
		}
		else
		{
			this._curFrame = new PA_UIFrameStats();
		}
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x0008B26C File Offset: 0x0008966C
	public void EndFrame()
	{
		this._lastSecFrames.Add(this._curFrame);
		this._curFrame = null;
		float num = Time.realtimeSinceStartup - this._lastWriteTime;
		if (num >= PA_UIStatsConst.WriteInterval)
		{
			for (int i = 0; i < this._lastSecFrames.Count; i++)
			{
				this._lastSecFrames[i].Clear();
			}
			this._cachedFrames.AddRange(this._lastSecFrames);
			this._lastSecFrames.Clear();
			this._lastWriteTime = Mathf.Floor(Time.realtimeSinceStartup);
		}
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x0008B304 File Offset: 0x00089704
	private string GenerateStatsInfo()
	{
		this._accum.Clear();
		this._max.Clear();
		for (int i = 0; i < this._lastSecFrames.Count; i++)
		{
			this._accum._wtbCnt += this._lastSecFrames[i]._wtbCnt;
			this._accum._wtbU1Cnt += this._lastSecFrames[i]._wtbU1Cnt;
			this._accum._wtbNormCnt += this._lastSecFrames[i]._wtbNormCnt;
			this._accum._totalVertCount += this._lastSecFrames[i]._totalVertCount;
			this._max._wtbCnt = Mathf.Max(this._lastSecFrames[i]._wtbCnt, this._max._wtbCnt);
			this._max._wtbU1Cnt = Mathf.Max(this._lastSecFrames[i]._wtbU1Cnt, this._max._wtbU1Cnt);
			this._max._wtbNormCnt = Mathf.Max(this._lastSecFrames[i]._wtbNormCnt, this._max._wtbNormCnt);
			this._max._totalVertCount = Mathf.Max(this._lastSecFrames[i]._totalVertCount, this._max._totalVertCount);
		}
		string text = string.Format("accum: <cnt: {0} u1: {1} norm: {2}, vert:{3}>", new object[]
		{
			this._accum._wtbCnt,
			this._accum._wtbU1Cnt,
			this._accum._wtbNormCnt,
			this._accum._totalVertCount
		});
		string text2 = string.Format("max: <cnt: {0} u1: {1} norm: {2}, vert:{3}>", new object[]
		{
			this._max._wtbCnt,
			this._max._wtbU1Cnt,
			this._max._wtbNormCnt,
			this._max._totalVertCount
		});
		string empty = string.Empty;
		string empty2 = string.Empty;
		return string.Format("{0} {1} -- {2} {3}", new object[]
		{
			text,
			text2,
			empty,
			empty2
		});
	}

	// Token: 0x0400196C RID: 6508
	public static PA_UIStats Instance;

	// Token: 0x0400196D RID: 6509
	public PA_UIFrameStats _curFrame;

	// Token: 0x0400196E RID: 6510
	public List<PA_UIFrameStats> _lastSecFrames = new List<PA_UIFrameStats>(50);

	// Token: 0x0400196F RID: 6511
	public List<PA_UIFrameStats> _cachedFrames = new List<PA_UIFrameStats>(50);

	// Token: 0x04001970 RID: 6512
	private PA_UIFrameStats _accum = new PA_UIFrameStats();

	// Token: 0x04001971 RID: 6513
	private PA_UIFrameStats _max = new PA_UIFrameStats();

	// Token: 0x04001972 RID: 6514
	private float _lastWriteTime;
}
