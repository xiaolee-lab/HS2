using System;
using UniRx;

namespace AIProject
{
	// Token: 0x02000E96 RID: 3734
	public interface IUIFader
	{
		// Token: 0x06007830 RID: 30768
		IObservable<TimeInterval<float>[]> Open();

		// Token: 0x06007831 RID: 30769
		IObservable<TimeInterval<float>[]> Close();
	}
}
