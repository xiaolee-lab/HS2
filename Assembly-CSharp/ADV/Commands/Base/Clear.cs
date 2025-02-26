using System;

namespace ADV.Commands.Base
{
	// Token: 0x02000705 RID: 1797
	public class Clear : CommandBase
	{
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000F9114 File Offset: 0x000F7514
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000F9117 File Offset: 0x000F7517
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000F911A File Offset: 0x000F751A
		public override void Do()
		{
			base.Do();
			base.scenario.captionSystem.Clear();
		}
	}
}
