using System;
using Manager;
using UnityEngine;

namespace Studio.SceneAssist
{
	// Token: 0x0200129A RID: 4762
	public class Assist
	{
		// Token: 0x06009D6A RID: 40298 RVA: 0x00404BB4 File Offset: 0x00402FB4
		public static Transform PlayDecisionSE()
		{
			return Singleton<Sound>.Instance.Play(Sound.Type.SystemSE, Assist.AssetBundleSystemSE, "sse_00_02", 0f, 0f, true, true, -1, true);
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x00404BE4 File Offset: 0x00402FE4
		public static Transform PlayCancelSE()
		{
			return Singleton<Sound>.Instance.Play(Sound.Type.SystemSE, Assist.AssetBundleSystemSE, "sse_00_04", 0f, 0f, true, true, -1, true);
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06009D6C RID: 40300 RVA: 0x00404C14 File Offset: 0x00403014
		public static string AssetBundleSystemSE
		{
			get
			{
				return "sound/data/systemse.unity3d";
			}
		}
	}
}
