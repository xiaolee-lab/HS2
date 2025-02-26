using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006E2 RID: 1762
	public class Jump : CommandBase
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000F5B32 File Offset: 0x000F3F32
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Tag"
				};
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000F5B42 File Offset: 0x000F3F42
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000F5B45 File Offset: 0x000F3F45
		public override void Do()
		{
			base.Do();
			base.scenario.SearchTagJumpOrOpenFile(this.args[0], base.localLine);
		}
	}
}
