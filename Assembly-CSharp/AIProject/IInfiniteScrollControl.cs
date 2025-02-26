using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F9A RID: 3994
	public interface IInfiniteScrollControl
	{
		// Token: 0x0600853B RID: 34107
		void OnPostSetupOption();

		// Token: 0x0600853C RID: 34108
		void OnUpdateOption(int count, GameObject obj);
	}
}
