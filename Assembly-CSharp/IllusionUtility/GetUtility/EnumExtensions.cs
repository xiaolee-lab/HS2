using System;

namespace IllusionUtility.GetUtility
{
	// Token: 0x02001188 RID: 4488
	public static class EnumExtensions
	{
		// Token: 0x060093F6 RID: 37878 RVA: 0x003D2994 File Offset: 0x003D0D94
		public static bool HasFlag(this Enum self, Enum flag)
		{
			return self.AND(flag) == Convert.ToUInt64(flag);
		}

		// Token: 0x060093F7 RID: 37879 RVA: 0x003D29A5 File Offset: 0x003D0DA5
		public static ulong Add(this Enum self, Enum flag)
		{
			return self.OR(flag);
		}

		// Token: 0x060093F8 RID: 37880 RVA: 0x003D29AE File Offset: 0x003D0DAE
		public static ulong Sub(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) & flag.NOT();
		}

		// Token: 0x060093F9 RID: 37881 RVA: 0x003D29BD File Offset: 0x003D0DBD
		public static ulong Get(this Enum self, Enum flag)
		{
			return self.AND(flag);
		}

		// Token: 0x060093FA RID: 37882 RVA: 0x003D29C6 File Offset: 0x003D0DC6
		public static ulong Reverse(this Enum self, Enum flag)
		{
			return self.XOR(flag);
		}

		// Token: 0x060093FB RID: 37883 RVA: 0x003D29CF File Offset: 0x003D0DCF
		public static ulong NOT(this Enum self)
		{
			return ~Convert.ToUInt64(self);
		}

		// Token: 0x060093FC RID: 37884 RVA: 0x003D29D8 File Offset: 0x003D0DD8
		public static ulong AND(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) & Convert.ToUInt64(flag);
		}

		// Token: 0x060093FD RID: 37885 RVA: 0x003D29E7 File Offset: 0x003D0DE7
		public static ulong OR(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) | Convert.ToUInt64(flag);
		}

		// Token: 0x060093FE RID: 37886 RVA: 0x003D29F6 File Offset: 0x003D0DF6
		public static ulong XOR(this Enum self, Enum flag)
		{
			return Convert.ToUInt64(self) ^ Convert.ToUInt64(flag);
		}
	}
}
