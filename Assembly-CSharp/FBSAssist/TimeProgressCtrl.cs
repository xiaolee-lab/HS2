using System;
using UnityEngine;

namespace FBSAssist
{
	// Token: 0x020010EE RID: 4334
	public class TimeProgressCtrl
	{
		// Token: 0x06008FD2 RID: 36818 RVA: 0x003BF0E7 File Offset: 0x003BD4E7
		public TimeProgressCtrl(float ptime = 0.15f)
		{
			this.progressTime = ptime;
		}

		// Token: 0x06008FD3 RID: 36819 RVA: 0x003BF10C File Offset: 0x003BD50C
		public void End()
		{
			this.count = this.progressTime;
			this.rate = 1f;
		}

		// Token: 0x06008FD4 RID: 36820 RVA: 0x003BF125 File Offset: 0x003BD525
		public void Start()
		{
			this.count = 0f;
			this.rate = 0f;
		}

		// Token: 0x06008FD5 RID: 36821 RVA: 0x003BF140 File Offset: 0x003BD540
		public float Calculate()
		{
			this.count += Time.deltaTime;
			if (this.count < this.progressTime)
			{
				this.rate = Mathf.InverseLerp(0f, this.progressTime, this.count);
			}
			else
			{
				this.End();
			}
			return this.rate;
		}

		// Token: 0x06008FD6 RID: 36822 RVA: 0x003BF19D File Offset: 0x003BD59D
		public void SetProgressTime(float time)
		{
			this.progressTime = time;
		}

		// Token: 0x06008FD7 RID: 36823 RVA: 0x003BF1A6 File Offset: 0x003BD5A6
		public float GetProgressRate()
		{
			return this.rate;
		}

		// Token: 0x040074A5 RID: 29861
		private float count;

		// Token: 0x040074A6 RID: 29862
		private float rate = 1f;

		// Token: 0x040074A7 RID: 29863
		private float progressTime = 0.15f;
	}
}
