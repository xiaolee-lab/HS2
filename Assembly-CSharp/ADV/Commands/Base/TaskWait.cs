using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006FA RID: 1786
	public class TaskWait : CommandBase
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000F6EE4 File Offset: 0x000F52E4
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x000F6EE7 File Offset: 0x000F52E7
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000F6EEA File Offset: 0x000F52EA
		public override void Do()
		{
			base.Do();
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000F6EF2 File Offset: 0x000F52F2
		public override bool Process()
		{
			base.Process();
			return !base.scenario.isBackGroundCommandProcessing;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000F6F09 File Offset: 0x000F5309
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			if (!processEnd)
			{
				return;
			}
		}
	}
}
