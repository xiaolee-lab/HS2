using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006E0 RID: 1760
	public class InfoAudioEco : CommandBase
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000F585D File Offset: 0x000F3C5D
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Use",
					"Delay",
					"DecayRatio",
					"WetMix",
					"DryMix"
				};
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000F588D File Offset: 0x000F3C8D
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000F5890 File Offset: 0x000F3C90
		public override void Do()
		{
			base.Do();
			int num = 0;
			Info.Audio.Eco eco = base.scenario.info.audio.eco;
			this.args.SafeProc(num++, delegate(string s)
			{
				eco.use = bool.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				eco.delay = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				eco.decayRatio = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				eco.wetMix = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				eco.dryMix = float.Parse(s);
			});
		}
	}
}
