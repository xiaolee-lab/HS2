using System;
using System.Collections.Generic;
using System.Linq;

namespace ADV
{
	// Token: 0x020006C5 RID: 1733
	public static class ParameterList
	{
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x000F1B64 File Offset: 0x000EFF64
		private static List<SceneParameter> list { get; } = new List<SceneParameter>();

		// Token: 0x0600290F RID: 10511 RVA: 0x000F1B6B File Offset: 0x000EFF6B
		public static void Add(SceneParameter param)
		{
			ParameterList.list.Add(param);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000F1B78 File Offset: 0x000EFF78
		public static void Remove(IData data)
		{
			ParameterList.list.RemoveAll((SceneParameter p) => p.data == null || p.data == data);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000F1BA9 File Offset: 0x000EFFA9
		public static void Init()
		{
			SceneParameter sceneParameter = ParameterList.list.LastOrDefault<SceneParameter>();
			if (sceneParameter != null)
			{
				sceneParameter.Init();
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000F1BC3 File Offset: 0x000EFFC3
		public static void Release()
		{
			SceneParameter sceneParameter = ParameterList.list.LastOrDefault<SceneParameter>();
			if (sceneParameter != null)
			{
				sceneParameter.Release();
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000F1BDD File Offset: 0x000EFFDD
		public static void WaitEndProc()
		{
			SceneParameter sceneParameter = ParameterList.list.LastOrDefault<SceneParameter>();
			if (sceneParameter != null)
			{
				sceneParameter.WaitEndProc();
			}
		}
	}
}
