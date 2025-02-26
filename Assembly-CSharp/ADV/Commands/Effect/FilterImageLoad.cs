using System;
using Illusion.Game;
using UnityEngine.UI;

namespace ADV.Commands.Effect
{
	// Token: 0x02000738 RID: 1848
	public class FilterImageLoad : CommandBase
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000FCFF4 File Offset: 0x000FB3F4
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Bundle",
					"Asset",
					"Manifest"
				};
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000FD014 File Offset: 0x000FB414
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000FD034 File Offset: 0x000FB434
		public override void Do()
		{
			base.Do();
			int num = 0;
			string bundleName = null;
			if (this.args.SafeProc(num++, delegate(string s)
			{
				bundleName = s;
			}))
			{
				string assetName = null;
				this.args.SafeProc(num++, delegate(string s)
				{
					assetName = s;
				});
				string manifest = null;
				this.args.SafeProc(num++, delegate(string s)
				{
					manifest = s;
				});
				string bundleName2 = bundleName;
				string assetName2 = assetName;
				Image filterImage = base.scenario.FilterImage;
				bool isTexSize = false;
				string manifest2 = manifest;
				Illusion.Game.Utils.Bundle.LoadSprite(bundleName2, assetName2, filterImage, isTexSize, null, manifest2, null);
			}
			base.scenario.FilterImage.enabled = true;
		}
	}
}
