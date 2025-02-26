using System;

namespace Manager
{
	// Token: 0x02000925 RID: 2341
	public static class SoundEnumExtensions
	{
		// Token: 0x0600425F RID: 16991 RVA: 0x001A1972 File Offset: 0x0019FD72
		public static bool Contains(this SoundPlayer.UpdateType sour, SoundPlayer.UpdateType checkType)
		{
			return (sour | checkType) == sour && checkType != (SoundPlayer.UpdateType)0;
		}
	}
}
