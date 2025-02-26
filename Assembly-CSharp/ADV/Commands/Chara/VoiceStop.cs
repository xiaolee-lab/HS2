using System;
using Manager;

namespace ADV.Commands.Chara
{
	// Token: 0x0200072F RID: 1839
	public class VoiceStop : CommandBase
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000FC9F0 File Offset: 0x000FADF0
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

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000FCA00 File Offset: 0x000FAE00
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

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000FCA2C File Offset: 0x000FAE2C
		public override void Do()
		{
			base.Do();
			CharaData chara = base.scenario.commandController.GetChara(int.Parse(this.args[0]));
			Singleton<Voice>.Instance.Stop(chara.voiceNo, chara.voiceTrans);
			base.scenario.loopVoiceList.RemoveAll((TextScenario.LoopVoicePack item) => item.voiceNo == chara.voiceNo);
		}
	}
}
