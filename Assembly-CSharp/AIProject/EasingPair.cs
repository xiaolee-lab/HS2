using System;

namespace AIProject
{
	// Token: 0x02000E7B RID: 3707
	[Serializable]
	public struct EasingPair
	{
		// Token: 0x06007650 RID: 30288 RVA: 0x0032198E File Offset: 0x0031FD8E
		public EasingPair(MotionType inType, MotionType outType)
		{
			this.@in = inType;
			this.@out = outType;
		}

		// Token: 0x0400603B RID: 24635
		public MotionType @in;

		// Token: 0x0400603C RID: 24636
		public MotionType @out;
	}
}
