using System;
using UnityEngine;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x020007A6 RID: 1958
	[Serializable]
	public class MotionOverride : Motion
	{
		// Token: 0x06002E64 RID: 11876 RVA: 0x001063EF File Offset: 0x001047EF
		public MotionOverride()
		{
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x00106402 File Offset: 0x00104802
		public MotionOverride(string bundle, string asset) : base(bundle, asset)
		{
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x00106417 File Offset: 0x00104817
		public MotionOverride(string bundle, string asset, string state) : base(bundle, asset, state)
		{
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0010642D File Offset: 0x0010482D
		public override bool Setting(Animator animator)
		{
			return this.Setting(animator, this.bundle, this.asset, this.overrideMotion.bundle, this.overrideMotion.asset, this.state, false);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x00106460 File Offset: 0x00104860
		public bool Setting(Animator animator, string bundle, string asset, string overrideBundle, string overrideAsset, string state, bool isCheck)
		{
			bool flag = this.Setting(animator, bundle, asset, state, isCheck);
			if ((flag || !isCheck || this.overrideMotion.Check(overrideBundle, overrideAsset)) && !overrideAsset.IsNullOrEmpty())
			{
				this.overrideMotion.asset = overrideAsset;
				if (!overrideBundle.IsNullOrEmpty())
				{
					this.overrideMotion.bundle = overrideBundle;
				}
				this.UnloadBundle(false, false);
				this.overrideMotion.UnloadBundle(false, false);
				animator.runtimeAnimatorController = Utils.Animator.SetupAnimatorOverrideController(animator.runtimeAnimatorController, this.overrideMotion.GetAsset<RuntimeAnimatorController>());
				flag = true;
			}
			return flag;
		}

		// Token: 0x04002D3F RID: 11583
		public AssetBundleData overrideMotion = new AssetBundleData();
	}
}
