using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006F3 RID: 1779
	public class Replace : CommandBase
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x000F6941 File Offset: 0x000F4D41
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

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x000F6959 File Offset: 0x000F4D59
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000F695C File Offset: 0x000F4D5C
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
