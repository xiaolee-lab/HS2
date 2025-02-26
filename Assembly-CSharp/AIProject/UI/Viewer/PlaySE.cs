using System;
using Manager;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EC5 RID: 3781
	public class PlaySE
	{
		// Token: 0x06007BDA RID: 31706 RVA: 0x00344C17 File Offset: 0x00343017
		public PlaySE() : this(true)
		{
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x00344C20 File Offset: 0x00343020
		public PlaySE(bool use)
		{
			this.use = use;
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x06007BDC RID: 31708 RVA: 0x00344C2F File Offset: 0x0034302F
		// (set) Token: 0x06007BDD RID: 31709 RVA: 0x00344C37 File Offset: 0x00343037
		public bool use { private get; set; }

		// Token: 0x06007BDE RID: 31710 RVA: 0x00344C40 File Offset: 0x00343040
		public void Play(SoundPack.SystemSE se)
		{
			if (!this.use)
			{
				return;
			}
			Singleton<Resources>.Instance.SoundPack.Play(se);
		}
	}
}
