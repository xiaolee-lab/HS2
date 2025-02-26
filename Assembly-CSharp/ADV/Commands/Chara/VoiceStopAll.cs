using System;
using Manager;

namespace ADV.Commands.Chara
{
	// Token: 0x02000730 RID: 1840
	public class VoiceStopAll : CommandBase
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000FCACA File Offset: 0x000FAECA
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000FCACD File Offset: 0x000FAECD
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000FCAD0 File Offset: 0x000FAED0
		public override void Do()
		{
			base.Do();
			Singleton<Voice>.Instance.StopAll(true);
		}
	}
}
