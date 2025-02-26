using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200081C RID: 2076
public class CoroutineAssist
{
	// Token: 0x060034E8 RID: 13544 RVA: 0x00137EB0 File Offset: 0x001362B0
	public CoroutineAssist(MonoBehaviour _mono, Func<IEnumerator> _func)
	{
		this.nowFunc = null;
		this.status = CoroutineAssist.Status.Idle;
		this.timeoutTime = 0f;
		this.enableTimeout = false;
		this.startTime = 0f;
		this.mono = _mono;
		this.func = _func;
	}

	// Token: 0x17000994 RID: 2452
	// (get) Token: 0x060034E9 RID: 13545 RVA: 0x00137EFC File Offset: 0x001362FC
	// (set) Token: 0x060034EA RID: 13546 RVA: 0x00137F04 File Offset: 0x00136304
	public CoroutineAssist.Status status { get; set; }

	// Token: 0x060034EB RID: 13547 RVA: 0x00137F0D File Offset: 0x0013630D
	public bool IsIdle()
	{
		return this.status == CoroutineAssist.Status.Idle;
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x00137F18 File Offset: 0x00136318
	public bool IsRun()
	{
		return this.status == CoroutineAssist.Status.Run;
	}

	// Token: 0x060034ED RID: 13549 RVA: 0x00137F23 File Offset: 0x00136323
	public bool IsPause()
	{
		return this.status == CoroutineAssist.Status.Pause;
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x00137F30 File Offset: 0x00136330
	public bool Start(bool _enableTimeout = false, float _timeout = 20f)
	{
		if (this.func == null)
		{
			return false;
		}
		if (this.status != CoroutineAssist.Status.Idle)
		{
			return false;
		}
		this.nowFunc = this.func();
		this.status = CoroutineAssist.Status.Run;
		if (_enableTimeout)
		{
			this.StartTimeoutCheck(_timeout);
		}
		this.mono.StartCoroutine(this.nowFunc);
		return true;
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x00137F8F File Offset: 0x0013638F
	public bool Restart()
	{
		if (this.nowFunc == null)
		{
			return false;
		}
		this.mono.StartCoroutine(this.nowFunc);
		this.status = CoroutineAssist.Status.Run;
		this.startTime = Time.realtimeSinceStartup;
		return true;
	}

	// Token: 0x060034F0 RID: 13552 RVA: 0x00137FC3 File Offset: 0x001363C3
	public void Pause()
	{
		if (this.nowFunc == null)
		{
			return;
		}
		this.mono.StopCoroutine(this.nowFunc);
		this.status = CoroutineAssist.Status.Pause;
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x00137FE9 File Offset: 0x001363E9
	public void End()
	{
		if (this.nowFunc == null)
		{
			return;
		}
		this.mono.StopCoroutine(this.nowFunc);
		this.EndStatus();
	}

	// Token: 0x060034F2 RID: 13554 RVA: 0x0013800E File Offset: 0x0013640E
	public void EndStatus()
	{
		this.nowFunc = null;
		this.status = CoroutineAssist.Status.Idle;
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x0013801E File Offset: 0x0013641E
	public void StartTimeoutCheck(float _timeout = 20f)
	{
		this.enableTimeout = true;
		this.timeoutTime = _timeout;
		this.startTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x00138039 File Offset: 0x00136439
	public void EndTimeoutCheck()
	{
		this.enableTimeout = false;
		this.timeoutTime = 0f;
		this.startTime = 0f;
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x00138058 File Offset: 0x00136458
	public bool TimeOutCheck()
	{
		if (this.status != CoroutineAssist.Status.Run)
		{
			return false;
		}
		if (!this.enableTimeout)
		{
			return false;
		}
		float num = Time.realtimeSinceStartup - this.startTime;
		if (num > this.timeoutTime)
		{
			this.enableTimeout = false;
			this.End();
			return true;
		}
		return false;
	}

	// Token: 0x04003572 RID: 13682
	private MonoBehaviour mono;

	// Token: 0x04003573 RID: 13683
	private Func<IEnumerator> func;

	// Token: 0x04003574 RID: 13684
	private IEnumerator nowFunc;

	// Token: 0x04003576 RID: 13686
	private float timeoutTime;

	// Token: 0x04003577 RID: 13687
	private bool enableTimeout;

	// Token: 0x04003578 RID: 13688
	private float startTime;

	// Token: 0x0200081D RID: 2077
	public enum Status
	{
		// Token: 0x0400357A RID: 13690
		Idle,
		// Token: 0x0400357B RID: 13691
		Run,
		// Token: 0x0400357C RID: 13692
		Pause
	}
}
