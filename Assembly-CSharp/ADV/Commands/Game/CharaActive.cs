using System;

namespace ADV.Commands.Game
{
	// Token: 0x0200073F RID: 1855
	public class CharaActive : CommandBase
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000FD5B5 File Offset: 0x000FB9B5
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"isActive",
					"Stand"
				};
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000FD5D8 File Offset: 0x000FB9D8
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					bool.TrueString,
					string.Empty
				};
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000FD614 File Offset: 0x000FBA14
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			bool isVisible = bool.Parse(this.args[num++]);
			if (!false)
			{
				chara.data.isVisible = isVisible;
			}
		}
	}
}
