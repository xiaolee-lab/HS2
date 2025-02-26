using System;

namespace ADV.Commands.Game
{
	// Token: 0x02000747 RID: 1863
	public class BundleCheck : CommandBase
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000FDAF9 File Offset: 0x000FBEF9
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Variable",
					"Bundle"
				};
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000FDB11 File Offset: 0x000FBF11
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"file",
					"abdata"
				};
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000FDB2C File Offset: 0x000FBF2C
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string bundle = this.args[num++];
			base.scenario.Vars[key] = new ValData(AssetBundleCheck.IsManifestOrBundle(bundle));
		}
	}
}
