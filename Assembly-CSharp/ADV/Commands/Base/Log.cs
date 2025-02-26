using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006E8 RID: 1768
	public class Log : CommandBase
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x000F6028 File Offset: 0x000F4428
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"Msg"
				};
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000F6040 File Offset: 0x000F4440
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Log",
					string.Empty
				};
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000F6058 File Offset: 0x000F4458
		public override void Do()
		{
			base.Do();
		}
	}
}
