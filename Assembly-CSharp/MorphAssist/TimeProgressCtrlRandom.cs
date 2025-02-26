using System;
using UnityEngine;

namespace MorphAssist
{
	// Token: 0x02001110 RID: 4368
	public class TimeProgressCtrlRandom : TimeProgressCtrl
	{
		// Token: 0x060090EB RID: 37099 RVA: 0x003C5568 File Offset: 0x003C3968
		public void Init(float min, float max)
		{
			this.minTime = min;
			this.maxTime = max;
			base.SetProgressTime(UnityEngine.Random.Range(this.minTime, this.maxTime));
			base.Start();
		}

		// Token: 0x060090EC RID: 37100 RVA: 0x003C5598 File Offset: 0x003C3998
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

		// Token: 0x04007577 RID: 30071
		private float minTime = 0.1f;

		// Token: 0x04007578 RID: 30072
		private float maxTime = 0.2f;
	}
}
