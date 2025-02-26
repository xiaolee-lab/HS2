using System;
using Illusion.Extensions;

namespace ADV.Commands.Effect
{
	// Token: 0x0200073C RID: 1852
	public class ImageLoad : CommandBase
	{
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000FD3F0 File Offset: 0x000FB7F0
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Bundle",
					"Asset",
					"Type"
				};
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000FD410 File Offset: 0x000FB810
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					string.Empty,
					bool.TrueString
				};
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000FD438 File Offset: 0x000FB838
		public override void Do()
		{
			base.Do();
			int num = 0;
			string bundleName = this.args[num++];
			string assetName = this.args[num++];
			string self = this.args[num++];
			bool isFront = !self.Compare("back", true);
			ADVFade advFade = base.scenario.advScene.AdvFade;
			advFade.LoadSprite(isFront, bundleName, assetName);
		}
	}
}
