using System;
using System.Collections;
using System.Diagnostics;

// Token: 0x020004A9 RID: 1193
public class TrackedCoroutine : IEnumerator
{
	// Token: 0x06001601 RID: 5633 RVA: 0x00087560 File Offset: 0x00085960
	public TrackedCoroutine(IEnumerator routine)
	{
		this._routine = routine;
		this._mangledName = CoroutineNameCache.Mangle(this._routine.GetType().ToString());
		this._seqID = TrackedCoroutine._seqNext++;
		RuntimeCoroutineStats.Instance.MarkCreation(this._seqID, this._mangledName);
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06001602 RID: 5634 RVA: 0x000875BE File Offset: 0x000859BE
	object IEnumerator.Current
	{
		get
		{
			return this._routine.Current;
		}
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x000875CC File Offset: 0x000859CC
	public bool MoveNext()
	{
		if (TrackedCoroutine._stopWatch == null)
		{
			TrackedCoroutine._stopWatch = Stopwatch.StartNew();
		}
		TrackedCoroutine._stopWatch.Reset();
		TrackedCoroutine._stopWatch.Start();
		bool flag = this._routine.MoveNext();
		TrackedCoroutine._stopWatch.Stop();
		float timeConsumed = (float)((double)TrackedCoroutine._stopWatch.ElapsedTicks / (double)Stopwatch.Frequency);
		RuntimeCoroutineStats.Instance.MarkMoveNext(this._seqID, timeConsumed);
		if (!flag)
		{
			RuntimeCoroutineStats.Instance.MarkTermination(this._seqID);
		}
		return flag;
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x00087653 File Offset: 0x00085A53
	public void Reset()
	{
		this._routine.Reset();
	}

	// Token: 0x040018D5 RID: 6357
	private int _seqID;

	// Token: 0x040018D6 RID: 6358
	private IEnumerator _routine;

	// Token: 0x040018D7 RID: 6359
	private string _mangledName;

	// Token: 0x040018D8 RID: 6360
	private static Stopwatch _stopWatch;

	// Token: 0x040018D9 RID: 6361
	private static int _seqNext;
}
