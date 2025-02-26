using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Exploder
{
	// Token: 0x020003C7 RID: 967
	public static class Profiler
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x00064DC0 File Offset: 0x000631C0
		public static void Start(string key)
		{
			Stopwatch stopwatch = null;
			if (Profiler.timeSegments.TryGetValue(key, out stopwatch))
			{
				stopwatch.Reset();
				stopwatch.Start();
			}
			else
			{
				stopwatch = new Stopwatch();
				stopwatch.Start();
				Profiler.timeSegments.Add(key, stopwatch);
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00064E0A File Offset: 0x0006320A
		public static void End(string key)
		{
			Profiler.timeSegments[key].Stop();
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00064E1C File Offset: 0x0006321C
		public static string[] PrintResults()
		{
			string[] array = new string[Profiler.timeSegments.Count];
			int num = 0;
			foreach (KeyValuePair<string, Stopwatch> keyValuePair in Profiler.timeSegments)
			{
				array[num++] = keyValuePair.Key + " " + keyValuePair.Value.ElapsedMilliseconds.ToString() + " [ms]";
			}
			return array;
		}

		// Token: 0x04001317 RID: 4887
		private static readonly Dictionary<string, Stopwatch> timeSegments = new Dictionary<string, Stopwatch>();
	}
}
