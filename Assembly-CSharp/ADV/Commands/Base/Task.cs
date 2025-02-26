using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006F9 RID: 1785
	public class Task : CommandBase
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000F6E9C File Offset: 0x000F529C
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"isTask"
				};
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000F6EAC File Offset: 0x000F52AC
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					bool.FalseString
				};
			}
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000F6EBC File Offset: 0x000F52BC
		public override void Do()
		{
			base.Do();
			base.scenario.isBackGroundCommanding = bool.Parse(this.args[0]);
		}
	}
}
