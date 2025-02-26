using System;

namespace AIProject
{
	// Token: 0x020008FA RID: 2298
	public struct DateTimeThreshold
	{
		// Token: 0x06003FB1 RID: 16305 RVA: 0x0019E111 File Offset: 0x0019C511
		public DateTimeThreshold(DateTime start, DateTime end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0019E124 File Offset: 0x0019C524
		public bool Contains(DateTime time)
		{
			if (this.end > this.start)
			{
				return time > this.start && time < this.end;
			}
			if (time > this.start && time > this.end)
			{
				return time > this.start && time < new DateTime(1, 1, 1, 24, 0, 0);
			}
			return time > new DateTime(1, 1, 1, 0, 0, 0) && time < this.end;
		}

		// Token: 0x04003C23 RID: 15395
		public DateTime start;

		// Token: 0x04003C24 RID: 15396
		public DateTime end;
	}
}
