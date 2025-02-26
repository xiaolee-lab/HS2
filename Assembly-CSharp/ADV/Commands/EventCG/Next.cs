using System;
using ADV.EventCG;

namespace ADV.Commands.EventCG
{
	// Token: 0x02000712 RID: 1810
	public class Next : CommandBase
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000FA6A4 File Offset: 0x000F8AA4
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No"
				};
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x000FA6B4 File Offset: 0x000F8AB4
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0"
				};
			}
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000FA6C4 File Offset: 0x000F8AC4
		public override void Do()
		{
			base.Do();
			int index = int.Parse(this.args[0]);
			base.scenario.CrossFadeStart();
			base.scenario.commandController.EventCGRoot.GetChild(0).GetComponent<Data>().Next(index, base.scenario.commandController.Characters);
		}
	}
}
