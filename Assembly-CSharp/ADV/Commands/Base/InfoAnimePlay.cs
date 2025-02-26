using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006E1 RID: 1761
	public class InfoAnimePlay : CommandBase
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x000F59C6 File Offset: 0x000F3DC6
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"crossFadeTime",
					"isCrossFade",
					"layerNo",
					"transitionDuration",
					"normalizedTime"
				};
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x000F59F6 File Offset: 0x000F3DF6
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000F59FC File Offset: 0x000F3DFC
		public override void Do()
		{
			base.Do();
			int num = 0;
			Info.Anime.Play play = base.scenario.info.anime.play;
			this.args.SafeProc(num++, delegate(string s)
			{
				play.crossFadeTime = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				play.isCrossFade = bool.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				play.layerNo = int.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				play.transitionDuration = float.Parse(s);
			});
			this.args.SafeProc(num++, delegate(string s)
			{
				play.normalizedTime = float.Parse(s);
			});
		}
	}
}
