using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006FB RID: 1787
	public class TaskEnd : CommandBase
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000F6F21 File Offset: 0x000F5321
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x000F6F24 File Offset: 0x000F5324
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000F6F27 File Offset: 0x000F5327
		public override void Do()
		{
			base.Do();
			base.scenario.BackGroundCommandProcessEnd();
		}
	}
}
