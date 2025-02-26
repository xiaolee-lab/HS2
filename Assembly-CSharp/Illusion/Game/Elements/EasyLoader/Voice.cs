using System;
using AIChara;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x020007A7 RID: 1959
	[Serializable]
	public class Voice : AssetBundleData
	{
		// Token: 0x06002E6A RID: 11882 RVA: 0x0010650C File Offset: 0x0010490C
		public virtual void Setting(ChaControl chaCtrl, int personality, string bundle, string asset)
		{
			bool flag = false;
			if (!asset.IsNullOrEmpty())
			{
				this.asset = asset;
				if (!bundle.IsNullOrEmpty())
				{
					this.bundle = bundle;
				}
				flag = true;
			}
			if (flag)
			{
			}
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x00106549 File Offset: 0x00104949
		public virtual void Setting(ChaControl chaCtrl)
		{
			this.Setting(chaCtrl, this.personality, this.bundle, this.asset);
		}

		// Token: 0x04002D40 RID: 11584
		public int personality;
	}
}
