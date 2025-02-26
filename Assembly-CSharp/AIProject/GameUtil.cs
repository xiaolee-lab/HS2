using System;
using Manager;

namespace AIProject
{
	// Token: 0x02000968 RID: 2408
	public static class GameUtil
	{
		// Token: 0x060042CC RID: 17100 RVA: 0x001A4F41 File Offset: 0x001A3341
		public static void OpenConfig()
		{
			if (Singleton<Game>.Instance.Config != null)
			{
				return;
			}
			Singleton<Game>.Instance.LoadConfig();
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x001A4F63 File Offset: 0x001A3363
		public static void GameEnd(bool isCheck = true)
		{
			Singleton<Scene>.Instance.GameEnd(isCheck);
		}
	}
}
