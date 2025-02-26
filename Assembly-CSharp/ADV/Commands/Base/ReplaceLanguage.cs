using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006F4 RID: 1780
	public class ReplaceLanguage : CommandBase
	{
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000F69DC File Offset: 0x000F4DDC
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Src",
					"Dst"
				};
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000F69F4 File Offset: 0x000F4DF4
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000F69F8 File Offset: 0x000F4DF8
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string dst = string.Empty;
			this.args.SafeProc(num++, delegate(string s)
			{
				dst = s;
			});
			base.scenario.Replaces[key] = dst;
		}
	}
}
