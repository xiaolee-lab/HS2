using System;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200074B RID: 1867
	public class SendCommandData : CommandBase
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000FDF9A File Offset: 0x000FC39A
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name"
				};
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000FDFAA File Offset: 0x000FC3AA
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty
				};
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000FDFBC File Offset: 0x000FC3BC
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int num = 0;
			this.name = this.args[num++];
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000FDFE4 File Offset: 0x000FC3E4
		public override void Do()
		{
			base.Do();
			foreach (CommandData commandData in base.scenario.package.commandList)
			{
				if (commandData.key == this.name)
				{
					commandData.ReceiveADV(base.scenario);
					break;
				}
			}
		}

		// Token: 0x04002B50 RID: 11088
		private string name;
	}
}
