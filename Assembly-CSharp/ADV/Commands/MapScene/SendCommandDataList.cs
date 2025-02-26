using System;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200074C RID: 1868
	public class SendCommandDataList : CommandBase
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000FE078 File Offset: 0x000FC478
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000FE07B File Offset: 0x000FC47B
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000FE080 File Offset: 0x000FC480
		public override void Do()
		{
			base.Do();
			foreach (CommandData commandData in base.scenario.package.commandList)
			{
				commandData.ReceiveADV(base.scenario);
			}
		}
	}
}
