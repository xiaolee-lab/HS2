using System;

namespace ADV.Commands.Effect
{
	// Token: 0x02000735 RID: 1845
	public class SceneFadeRegulate : CommandBase
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000FCF2F File Offset: 0x000FB32F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Flag"
				};
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000FCF3F File Offset: 0x000FB33F
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					bool.TrueString
				};
			}
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000FCF4F File Offset: 0x000FB34F
		public override void Do()
		{
			base.Do();
			base.scenario.isSceneFadeRegulate = bool.Parse(this.args[0]);
		}
	}
}
