using System;

namespace AIProject.Animal.Resources
{
	// Token: 0x02000B8F RID: 2959
	public class AnimalActionInfo
	{
		// Token: 0x0600582E RID: 22574 RVA: 0x0025F160 File Offset: 0x0025D560
		public AnimalActionInfo()
		{
			this.timeInfo = default(AnimalActionInfo.TimeInfo);
			this.timeInfo.Disable();
		}

		// Token: 0x040050F4 RID: 20724
		public AnimalActionInfo.TimeInfo timeInfo;

		// Token: 0x02000B90 RID: 2960
		public struct TimeInfo
		{
			// Token: 0x0600582F RID: 22575 RVA: 0x0025F18D File Offset: 0x0025D58D
			public TimeInfo(bool _manageTimeEnable, int _min, int _max)
			{
				this.manageTimeEnable = _manageTimeEnable;
				this.min = _min;
				this.max = _max;
			}

			// Token: 0x17001053 RID: 4179
			// (get) Token: 0x06005830 RID: 22576 RVA: 0x0025F1A4 File Offset: 0x0025D5A4
			// (set) Token: 0x06005831 RID: 22577 RVA: 0x0025F1AC File Offset: 0x0025D5AC
			public bool manageTimeEnable { get; private set; }

			// Token: 0x17001054 RID: 4180
			// (get) Token: 0x06005832 RID: 22578 RVA: 0x0025F1B5 File Offset: 0x0025D5B5
			// (set) Token: 0x06005833 RID: 22579 RVA: 0x0025F1BD File Offset: 0x0025D5BD
			public int min { get; private set; }

			// Token: 0x17001055 RID: 4181
			// (get) Token: 0x06005834 RID: 22580 RVA: 0x0025F1C6 File Offset: 0x0025D5C6
			// (set) Token: 0x06005835 RID: 22581 RVA: 0x0025F1CE File Offset: 0x0025D5CE
			public int max { get; private set; }

			// Token: 0x06005836 RID: 22582 RVA: 0x0025F1D7 File Offset: 0x0025D5D7
			public void Disable()
			{
				this.manageTimeEnable = false;
			}
		}
	}
}
