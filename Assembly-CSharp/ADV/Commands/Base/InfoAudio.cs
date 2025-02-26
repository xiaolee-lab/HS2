using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006DF RID: 1759
	public class InfoAudio : CommandBase
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000F579F File Offset: 0x000F3B9F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"is2D",
					"isMoveMouth"
				};
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x000F57B7 File Offset: 0x000F3BB7
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000F57BC File Offset: 0x000F3BBC
		public override void Do()
		{
			base.Do();
			int num = 0;
			Info.Audio audio = base.scenario.info.audio;
			this.args.SafeProc(num++, delegate(string s)
			{
				audio.is2D = bool.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				audio.isNotMoveMouth = bool.Parse(s);
			});
		}
	}
}
