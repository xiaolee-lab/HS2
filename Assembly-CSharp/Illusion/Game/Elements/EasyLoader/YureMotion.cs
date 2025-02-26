using System;
using AIChara;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x020007A5 RID: 1957
	[Serializable]
	public class YureMotion : AssetBundleData
	{
		// Token: 0x06002E5D RID: 11869 RVA: 0x001062F2 File Offset: 0x001046F2
		public YureMotion()
		{
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x001062FA File Offset: 0x001046FA
		public YureMotion(string bundle, string asset) : base(bundle, asset)
		{
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x00106304 File Offset: 0x00104704
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x0010630C File Offset: 0x0010470C
		public YureCtrlEx yureCtrl { get; private set; }

		// Token: 0x06002E61 RID: 11873 RVA: 0x00106315 File Offset: 0x00104715
		public void Create(ChaControl chaCtrl, YureCtrlEx yureCtrl = null)
		{
			if (yureCtrl != null)
			{
				this.yureCtrl = yureCtrl;
			}
			else
			{
				this.yureCtrl = new YureCtrlEx();
				this.yureCtrl.Init(chaCtrl);
			}
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x00106341 File Offset: 0x00104741
		public void Setting(ChaControl chaCtrl, string state)
		{
			this.Setting(chaCtrl, this.bundle, this.asset, state, false);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x00106358 File Offset: 0x00104758
		public void Setting(ChaControl chaCtrl, string bundle, string asset, string state, bool isCheck)
		{
			if (isCheck && !base.Check(bundle, asset))
			{
				return;
			}
			bool flag = false;
			if (this.yureCtrl == null)
			{
				this.Create(chaCtrl, null);
			}
			if (!asset.IsNullOrEmpty())
			{
				this.asset = asset;
				if (!bundle.IsNullOrEmpty())
				{
					this.bundle = bundle;
				}
				this.yureCtrl.Load(this.bundle, this.asset);
				flag = true;
			}
			if (!state.IsNullOrEmpty())
			{
				flag = true;
			}
			if (flag)
			{
				this.yureCtrl.Proc(state);
			}
		}
	}
}
