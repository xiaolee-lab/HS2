using System;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x020007A2 RID: 1954
	[Serializable]
	public abstract class BaseMotion : AssetBundleData
	{
		// Token: 0x06002E45 RID: 11845 RVA: 0x00105F87 File Offset: 0x00104387
		public BaseMotion()
		{
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x00105F8F File Offset: 0x0010438F
		public BaseMotion(string bundle, string asset) : base(bundle, asset)
		{
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x00105F99 File Offset: 0x00104399
		public BaseMotion(string bundle, string asset, string state) : base(bundle, asset)
		{
			this.state = state;
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x00105FAA File Offset: 0x001043AA
		public new bool isEmpty
		{
			get
			{
				return base.isEmpty || this.state.IsNullOrEmpty();
			}
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x00105FC5 File Offset: 0x001043C5
		public bool Check(string bundle, string asset, string state)
		{
			return (!state.IsNullOrEmpty() && this.state != state) || base.Check(bundle, asset);
		}

		// Token: 0x04002D36 RID: 11574
		public string state;
	}
}
