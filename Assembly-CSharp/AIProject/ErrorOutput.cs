using System;

namespace AIProject
{
	// Token: 0x0200086E RID: 2158
	public static class ErrorOutput
	{
		// Token: 0x06003704 RID: 14084 RVA: 0x00146023 File Offset: 0x00144423
		public static void OutputError(object message, Action action = null)
		{
			if (!Debug.isDebugBuild)
			{
				return;
			}
			if (!(message is string))
			{
				if (message is Exception)
				{
				}
			}
		}
	}
}
