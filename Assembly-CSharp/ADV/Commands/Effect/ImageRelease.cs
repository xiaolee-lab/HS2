using System;
using Illusion.Extensions;

namespace ADV.Commands.Effect
{
	// Token: 0x0200073D RID: 1853
	public class ImageRelease : CommandBase
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000FD4AA File Offset: 0x000FB8AA
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type"
				};
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000FD4BA File Offset: 0x000FB8BA
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					bool.TrueString
				};
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000FD4D4 File Offset: 0x000FB8D4
		public override void Do()
		{
			base.Do();
			int num = 0;
			bool isFront = this.args[num++].Compare("front", true);
			ADVFade advFade = base.scenario.advScene.AdvFade;
			advFade.ReleaseSprite(isFront);
		}
	}
}
