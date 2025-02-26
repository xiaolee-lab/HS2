using System;

namespace AIProject.Animal
{
	// Token: 0x02000B4A RID: 2890
	public struct AnimalSchedule
	{
		// Token: 0x0600563F RID: 22079 RVA: 0x00258DEA File Offset: 0x002571EA
		public AnimalSchedule(bool _enable, DateTime _start, TimeSpan _duration, bool _managing)
		{
			this.enable = _enable;
			this.name = string.Empty;
			this.start = _start;
			this.duration = _duration;
			this.elapsedTime = TimeSpan.Zero;
			this.managing = _managing;
		}

		// Token: 0x04004F96 RID: 20374
		public bool enable;

		// Token: 0x04004F97 RID: 20375
		public string name;

		// Token: 0x04004F98 RID: 20376
		public DateTime start;

		// Token: 0x04004F99 RID: 20377
		public TimeSpan duration;

		// Token: 0x04004F9A RID: 20378
		public TimeSpan elapsedTime;

		// Token: 0x04004F9B RID: 20379
		public bool managing;
	}
}
