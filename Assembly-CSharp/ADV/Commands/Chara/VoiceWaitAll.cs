using System;
using Manager;

namespace ADV.Commands.Chara
{
	// Token: 0x02000732 RID: 1842
	public class VoiceWaitAll : CommandBase
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000FCB85 File Offset: 0x000FAF85
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000FCB88 File Offset: 0x000FAF88
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000FCB8B File Offset: 0x000FAF8B
		public override bool Process()
		{
			base.Process();
			return !Singleton<Voice>.Instance.IsVoiceCheck();
		}
	}
}
