using System;
using AIChara;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x020007A4 RID: 1956
	[Serializable]
	public class IKMotion : AssetBundleData
	{
		// Token: 0x06002E54 RID: 11860 RVA: 0x00106192 File Offset: 0x00104592
		public IKMotion()
		{
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x001061A1 File Offset: 0x001045A1
		public IKMotion(string bundle, string asset) : base(bundle, asset)
		{
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x001061B2 File Offset: 0x001045B2
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x001061BA File Offset: 0x001045BA
		public MotionIK motionIK { get; private set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x001061C3 File Offset: 0x001045C3
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x001061CB File Offset: 0x001045CB
		public bool use { get; set; } = true;

		// Token: 0x06002E5A RID: 11866 RVA: 0x001061D4 File Offset: 0x001045D4
		public void Create(ChaControl chaCtrl, MotionIK motionIK = null, params MotionIK[] partners)
		{
			if (motionIK != null)
			{
				this.motionIK = motionIK;
			}
			else
			{
				this.motionIK = new MotionIK(chaCtrl, false, null);
				this.motionIK.SetPartners(partners);
			}
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x00106202 File Offset: 0x00104602
		public void Setting(ChaControl chaCtrl, string state)
		{
			this.Setting(chaCtrl, this.bundle, this.asset, state, false);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0010621C File Offset: 0x0010461C
		public void Setting(ChaControl chaCtrl, string bundle, string asset, string state, bool isCheck)
		{
			if (!this.use)
			{
				return;
			}
			if (isCheck && !base.Check(bundle, asset))
			{
				return;
			}
			if (this.motionIK == null)
			{
				this.Create(chaCtrl, null, Array.Empty<MotionIK>());
			}
			bool flag = false;
			if (!asset.IsNullOrEmpty())
			{
				this.asset = asset;
				if (!bundle.IsNullOrEmpty())
				{
					this.bundle = bundle;
				}
				this.motionIK.LoadData(bundle, asset, false);
				this.UnloadBundle(false, false);
				flag = true;
			}
			if (!state.IsNullOrEmpty())
			{
				flag = true;
			}
			this.motionIK.enabled = (flag && this.motionIK.GetNowState(state) != null);
			if (this.motionIK.enabled)
			{
				this.motionIK.Calc(state);
			}
		}
	}
}
