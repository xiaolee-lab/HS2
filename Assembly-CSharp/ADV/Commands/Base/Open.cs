using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006ED RID: 1773
	public class Open : CommandBase
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000F6259 File Offset: 0x000F4659
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Bundle",
					"Asset",
					"isAdd",
					"isClearCheck",
					"isNext"
				};
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000F6289 File Offset: 0x000F4689
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					bool.FalseString,
					bool.TrueString,
					bool.TrueString
				};
			}
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000F62BC File Offset: 0x000F46BC
		public override void Do()
		{
			base.Do();
			int num = 0;
			string text = this.args[num++];
			string text2 = this.args[num++];
			bool flag = bool.Parse(this.args[num++]);
			bool isClearCheck = bool.Parse(this.args[num++]);
			bool isNext = bool.Parse(this.args[num++]);
			if (text.IsNullOrEmpty())
			{
				text = base.scenario.LoadBundleName;
			}
			if (!AssetBundleCheck.IsFile(text, string.Empty))
			{
				text = Program.ScenarioBundle(text);
			}
			base.scenario.Vars["BundleFile"] = new ValData(text);
			base.scenario.Vars["AssetFile"] = new ValData(text2);
			base.scenario.LoadFile(text, text2, !flag, isClearCheck, isNext);
		}
	}
}
