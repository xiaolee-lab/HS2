using System;
using UniRx;

namespace Illusion.Game.Elements
{
	// Token: 0x020007A8 RID: 1960
	public class Single : IObservable<Unit>, IDisposable
	{
		// Token: 0x06002E6D RID: 11885 RVA: 0x00106584 File Offset: 0x00104984
		public void Done()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (!this._asyncSubject.IsCompleted)
				{
					this._asyncSubject.OnNext(Unit.Default);
					this._asyncSubject.OnCompleted();
				}
			}
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x001065F4 File Offset: 0x001049F4
		public IDisposable Subscribe(IObserver<Unit> observer)
		{
			object obj = this.lockObject;
			IDisposable result;
			lock (obj)
			{
				result = this._asyncSubject.Subscribe(observer);
			}
			return result;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x00106640 File Offset: 0x00104A40
		public void Dispose()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this._asyncSubject.Dispose();
			}
		}

		// Token: 0x04002D41 RID: 11585
		private readonly AsyncSubject<Unit> _asyncSubject = new AsyncSubject<Unit>();

		// Token: 0x04002D42 RID: 11586
		private readonly object lockObject = new object();
	}
}
