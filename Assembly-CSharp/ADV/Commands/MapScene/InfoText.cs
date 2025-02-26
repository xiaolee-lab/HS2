using System;
using AIProject;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200074A RID: 1866
	public class InfoText : CommandBase
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000FDF31 File Offset: 0x000FC331
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Text"
				};
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000FDF41 File Offset: 0x000FC341
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

		// Token: 0x06002C11 RID: 11281 RVA: 0x000FDF51 File Offset: 0x000FC351
		public override void Do()
		{
			base.Do();
			MapUIContainer.WarningMessageUI.isFadeInForOutWait = true;
			MapUIContainer.PushMessageUI(this.args[0], 0, 1, null);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000FDF74 File Offset: 0x000FC374
		public override bool Process()
		{
			base.Process();
			return false;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000FDF7E File Offset: 0x000FC37E
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			MapUIContainer.WarningMessageUI.isFadeInForOutWait = false;
		}
	}
}
