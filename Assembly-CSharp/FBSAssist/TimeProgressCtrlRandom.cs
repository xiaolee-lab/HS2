using System;
using UnityEngine;

namespace FBSAssist
{
	// Token: 0x020010EF RID: 4335
	public class TimeProgressCtrlRandom : TimeProgressCtrl
	{
		// Token: 0x06008FD8 RID: 36824 RVA: 0x003BF1AE File Offset: 0x003BD5AE
		public TimeProgressCtrlRandom() : base(0.15f)
		{
		}

		// Token: 0x06008FD9 RID: 36825 RVA: 0x003BF1D1 File Offset: 0x003BD5D1
		public void Init(float min, float max)
		{
			this.minTime = min;
			this.maxTime = max;
			base.SetProgressTime(UnityEngine.Random.Range(this.minTime, this.maxTime));
			base.Start();
		}

		// Token: 0x06008FDA RID: 36826 RVA: 0x003BF200 File Offset: 0x003BD600
		public new float Calculate()
		{
			float num = base.Calculate();
			if (num == 1f)
			{
				base.SetProgressTime(UnityEngine.Random.Range(this.minTime, this.maxTime));
				base.Start();
			}
			return num;
		}

		// Token: 0x06008FDB RID: 36827 RVA: 0x003BF23D File Offset: 0x003BD63D
		public float Calculate(float _minTime, float _maxTime)
		{
			this.minTime = _minTime;
			this.maxTime = _maxTime;
			return this.Calculate();
		}

		// Token: 0x040074A8 RID: 29864
		private float minTime = 0.1f;

		// Token: 0x040074A9 RID: 29865
		private float maxTime = 0.2f;
	}
}
