using System;

namespace Illusion.Extensions
{
	// Token: 0x0200107D RID: 4221
	public static class IntExtensions
	{
		// Token: 0x06008D6B RID: 36203 RVA: 0x003B12FD File Offset: 0x003AF6FD
		public static string MinusThroughToString(this int self, string format)
		{
			return (self < 0) ? self.ToString() : self.ToString(format);
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x003B1320 File Offset: 0x003AF720
		public static int ValueRound(this int self, int add)
		{
			if (add == 0)
			{
				return self;
			}
			int num = self;
			self += add;
			if (add > 0 && self < num)
			{
				self = int.MaxValue;
			}
			else if (add < 0 && self > num)
			{
				self = int.MinValue;
			}
			return self;
		}
	}
}
