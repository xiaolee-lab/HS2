using System;
using System.Collections.Generic;
using System.Linq;
using ADV.Commands.Base;

namespace ADV.Commands.Chara
{
	// Token: 0x02000721 RID: 1825
	public class Motion : Motion
	{
		// Token: 0x06002B6E RID: 11118 RVA: 0x000FB87C File Offset: 0x000F9C7C
		public override void Do()
		{
			List<Motion.Data> list = Motion.Convert(ref this.args, base.scenario, this.ArgsLabel.Length);
			if (list.Any<Motion.Data>())
			{
				base.scenario.CrossFadeStart();
			}
			list.ForEach(delegate(Motion.Data p)
			{
				p.Play(base.scenario);
			});
		}
	}
}
