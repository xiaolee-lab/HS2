using System;
using System.Diagnostics;
using System.Timers;

// Token: 0x02000473 RID: 1139
public class NetGuardTimer
{
	// Token: 0x14000058 RID: 88
	// (add) Token: 0x0600150B RID: 5387 RVA: 0x000835BC File Offset: 0x000819BC
	// (remove) Token: 0x0600150C RID: 5388 RVA: 0x000835F4 File Offset: 0x000819F4
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SysPost.StdMulticastDelegation Timeout;

	// Token: 0x0600150D RID: 5389 RVA: 0x0008362A File Offset: 0x00081A2A
	public void Activate()
	{
		this._timer = new Timer(3000.0);
		this._timer.Elapsed += this.OnTimeout;
		this._timer.Start();
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x00083662 File Offset: 0x00081A62
	public void Deactivate()
	{
		if (this._timer != null)
		{
			this._timer.Stop();
			this._timer = null;
		}
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x00083681 File Offset: 0x00081A81
	private void OnTimeout(object sender, ElapsedEventArgs e)
	{
		SysPost.InvokeMulticast(this, this.Timeout);
	}

	// Token: 0x0400181D RID: 6173
	public const int TimeoutInMilliseconds = 3000;

	// Token: 0x0400181E RID: 6174
	private Timer _timer;
}
