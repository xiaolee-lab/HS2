using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIProject.Definitions
{
	// Token: 0x0200094E RID: 2382
	public static class Environment
	{
		// Token: 0x0600428C RID: 17036 RVA: 0x001A2BF4 File Offset: 0x001A0FF4
		// Note: this type is marked as 'beforefieldinit'.
		static Environment()
		{
			Dictionary<TimeZone, int> dictionary = new Dictionary<TimeZone, int>();
			dictionary[TimeZone.Morning] = 0;
			dictionary[TimeZone.Day] = 1;
			dictionary[TimeZone.Night] = 2;
			Environment.TimeZoneIDTable = new ReadOnlyDictionary<TimeZone, int>(dictionary);
		}

		// Token: 0x04003F23 RID: 16163
		public static ReadOnlyDictionary<TimeZone, int> TimeZoneIDTable;
	}
}
