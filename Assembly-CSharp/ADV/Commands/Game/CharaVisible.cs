using System;
using AIChara;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Game
{
	// Token: 0x02000740 RID: 1856
	public class CharaVisible : CommandBase
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000FD67D File Offset: 0x000FBA7D
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Target",
					"isActive",
					"Stand"
				};
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000FD6A8 File Offset: 0x000FBAA8
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					CharaVisible.Target.All.ToString(),
					bool.TrueString,
					string.Empty
				};
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000FD6F4 File Offset: 0x000FBAF4
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			ChaControl chaCtrl = chara.chaCtrl;
			string self = this.args[num++];
			int num2 = self.Check(true, Enum.GetNames(typeof(CharaVisible.Target)));
			bool flag = bool.Parse(this.args[num++]);
			this.args.SafeProc(num++, delegate(string findName)
			{
				Transform transform = this.scenario.commandController.characterStandNulls[findName];
				chara.transform.SetPositionAndRotation(transform.position, transform.rotation);
			});
			ChaFileStatus fileStatus = chaCtrl.fileStatus;
			switch (num2)
			{
			case 0:
				chaCtrl.visibleAll = flag;
				break;
			case 1:
				fileStatus.visibleHeadAlways = flag;
				break;
			case 2:
				fileStatus.visibleBodyAlways = flag;
				break;
			case 3:
				fileStatus.visibleSonAlways = flag;
				break;
			case 4:
				fileStatus.visibleGomu = flag;
				break;
			}
		}

		// Token: 0x02000741 RID: 1857
		private enum Target
		{
			// Token: 0x04002B48 RID: 11080
			All,
			// Token: 0x04002B49 RID: 11081
			Head,
			// Token: 0x04002B4A RID: 11082
			Body,
			// Token: 0x04002B4B RID: 11083
			Son,
			// Token: 0x04002B4C RID: 11084
			Gomu
		}
	}
}
