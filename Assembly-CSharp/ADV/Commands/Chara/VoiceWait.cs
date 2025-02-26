using System;
using Manager;

namespace ADV.Commands.Chara
{
	// Token: 0x02000731 RID: 1841
	public class VoiceWait : CommandBase
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000FCAEB File Offset: 0x000FAEEB
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No"
				};
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000FCAFC File Offset: 0x000FAEFC
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString()
				};
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000FCB25 File Offset: 0x000FAF25
		public override void Do()
		{
			base.Do();
			this.chara = base.scenario.commandController.GetChara(int.Parse(this.args[0]));
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000FCB50 File Offset: 0x000FAF50
		public override bool Process()
		{
			base.Process();
			return !Singleton<Voice>.Instance.IsVoiceCheck(this.chara.voiceNo, this.chara.voiceTrans, true);
		}

		// Token: 0x04002B2C RID: 11052
		private CharaData chara;
	}
}
