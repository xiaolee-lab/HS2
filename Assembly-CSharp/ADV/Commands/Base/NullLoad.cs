using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006E9 RID: 1769
	public class NullLoad : CommandBase
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x000F6068 File Offset: 0x000F4468
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Version",
					"Name"
				};
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x000F6080 File Offset: 0x000F4480
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					string.Empty
				};
			}
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000F6098 File Offset: 0x000F4498
		public override void Do()
		{
			base.Do();
		}
	}
}
