using System;
using Illusion.Extensions;
using Manager;
using UnityEngine;

namespace ADV.Commands.Effect
{
	// Token: 0x02000734 RID: 1844
	public class SceneFade : CommandBase
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000FCD4C File Offset: 0x000FB14C
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Fade",
					"Time",
					"Color",
					"Bundle",
					"Asset"
				};
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000FCD7C File Offset: 0x000FB17C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"in",
					string.Empty,
					string.Empty,
					string.Empty,
					"none"
				};
			}
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000FCDAC File Offset: 0x000FB1AC
		public override void Do()
		{
			base.Do();
			int num = 0;
			this.fade = Singleton<Scene>.Instance.sceneFade;
			this.fade._Fade = ((!this.args[num++].Compare("in", true)) ? SimpleFade.Fade.Out : SimpleFade.Fade.In);
			this.bkFadeTime = this.fade._Time;
			this.args.SafeProc(num++, delegate(string s)
			{
				this.fade._Time = float.Parse(s);
			});
			string self = this.args[num++];
			if (!self.IsNullOrEmpty())
			{
				this.fade._Color = self.GetColor();
			}
			string self2 = this.args[num++];
			if (!self2.IsNullOrEmpty())
			{
				this.fade._Texture = AssetBundleManager.LoadAsset(self2, this.args[num++], typeof(Texture2D), null).GetAsset<Texture2D>();
			}
			this.fade.Init();
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000FCEA6 File Offset: 0x000FB2A6
		public override bool Process()
		{
			base.Process();
			return this.fade.IsEnd;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000FCEBC File Offset: 0x000FB2BC
		public override void Result(bool processEnd)
		{
			if (this.fade._Fade == SimpleFade.Fade.Out && !this.assetBundleName.IsNullOrEmpty())
			{
				AssetBundleManager.UnloadAssetBundle(this.assetBundleName, true, null, false);
			}
			this.fade._Time = this.bkFadeTime;
			this.fade.ForceEnd();
		}

		// Token: 0x04002B34 RID: 11060
		private SimpleFade fade;

		// Token: 0x04002B35 RID: 11061
		private string assetBundleName;

		// Token: 0x04002B36 RID: 11062
		private float bkFadeTime;
	}
}
