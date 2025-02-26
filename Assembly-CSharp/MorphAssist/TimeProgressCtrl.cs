using System;
using UnityEngine;

namespace MorphAssist
{
	// Token: 0x0200110F RID: 4367
	public class TimeProgressCtrl
	{
		// Token: 0x060090E5 RID: 37093 RVA: 0x003C54AB File Offset: 0x003C38AB
		public void End()
		{
			this.count = this.progressTime;
			this.rate = 1f;
		}

		// Token: 0x060090E6 RID: 37094 RVA: 0x003C54C4 File Offset: 0x003C38C4
		public void Start()
		{
			this.count = 0f;
			this.rate = 0f;
		}

		// Token: 0x060090E7 RID: 37095 RVA: 0x003C54DC File Offset: 0x003C38DC
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

		// Token: 0x060090E8 RID: 37096 RVA: 0x003C5539 File Offset: 0x003C3939
		public void SetProgressTime(float time)
		{
			this.progressTime = time;
		}

		// Token: 0x060090E9 RID: 37097 RVA: 0x003C5542 File Offset: 0x003C3942
		public float GetProgressRate()
		{
			return this.rate;
		}

		// Token: 0x04007574 RID: 30068
		private float count;

		// Token: 0x04007575 RID: 30069
		private float rate = 1f;

		// Token: 0x04007576 RID: 30070
		private float progressTime = 0.15f;
	}
}
