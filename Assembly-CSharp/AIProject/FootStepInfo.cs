using System;
using System.Collections.Generic;

namespace AIProject
{
	// Token: 0x02000C5C RID: 3164
	public class FootStepInfo
	{
		// Token: 0x060065FE RID: 26110 RVA: 0x002B6230 File Offset: 0x002B4630
		public FootStepInfo()
		{
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x002B6260 File Offset: 0x002B4660
		public FootStepInfo(FootStepInfo info)
		{
			this.Threshold = info.Threshold;
			if (this.Keys.Length != info.Keys.Length)
			{
				this.Keys = new FootStepInfo.Key[info.Keys.Length];
			}
			for (int i = 0; i < info.Keys.Length; i++)
			{
				this.Keys[i] = info.Keys[i];
			}
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x002B6300 File Offset: 0x002B4700
		public FootStepInfo(float min, float max, float[] keys)
		{
			this.Threshold = new Threshold(min, max);
			if (this.Keys.Length != keys.Length)
			{
				this.Keys = new FootStepInfo.Key[keys.Length];
			}
			for (int i = 0; i < keys.Length; i++)
			{
				this.Keys[i] = new FootStepInfo.Key(keys[i], false);
			}
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x002B6388 File Offset: 0x002B4788
		public FootStepInfo(float min, float max, List<float> keys)
		{
			this.Threshold = new Threshold(min, max);
			if (this.Keys.Length != keys.Count)
			{
				this.Keys = new FootStepInfo.Key[keys.Count];
			}
			for (int i = 0; i < keys.Count; i++)
			{
				this.Keys[i] = new FootStepInfo.Key(keys[i], false);
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06006602 RID: 26114 RVA: 0x002B641C File Offset: 0x002B481C
		// (set) Token: 0x06006603 RID: 26115 RVA: 0x002B6424 File Offset: 0x002B4824
		public Threshold Threshold { get; private set; } = default(Threshold);

		// Token: 0x0400583F RID: 22591
		public FootStepInfo.Key[] Keys = new FootStepInfo.Key[0];

		// Token: 0x02000C5D RID: 3165
		public struct Key
		{
			// Token: 0x06006604 RID: 26116 RVA: 0x002B642D File Offset: 0x002B482D
			public Key(FootStepInfo.Key key)
			{
				this.Time = key.Time;
				this.Execute = key.Execute;
			}

			// Token: 0x06006605 RID: 26117 RVA: 0x002B6449 File Offset: 0x002B4849
			public Key(float time, bool execute)
			{
				this.Time = time;
				this.Execute = execute;
			}

			// Token: 0x17001477 RID: 5239
			// (get) Token: 0x06006606 RID: 26118 RVA: 0x002B6459 File Offset: 0x002B4859
			// (set) Token: 0x06006607 RID: 26119 RVA: 0x002B6461 File Offset: 0x002B4861
			public float Time { get; set; }

			// Token: 0x17001478 RID: 5240
			// (get) Token: 0x06006608 RID: 26120 RVA: 0x002B646A File Offset: 0x002B486A
			// (set) Token: 0x06006609 RID: 26121 RVA: 0x002B6472 File Offset: 0x002B4872
			public bool Execute { get; set; }
		}
	}
}
