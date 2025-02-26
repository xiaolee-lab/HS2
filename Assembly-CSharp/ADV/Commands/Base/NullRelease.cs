using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006EA RID: 1770
	public class NullRelease : CommandBase
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002A3E RID: 10814 RVA: 0x000F60A8 File Offset: 0x000F44A8
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000F60AB File Offset: 0x000F44AB
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000F60AE File Offset: 0x000F44AE
		public override void Do()
		{
			base.Do();
			base.scenario.commandController.ReleaseNull();
		}
	}
}
