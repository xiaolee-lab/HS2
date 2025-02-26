using System;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200075E RID: 1886
	public class CharaSetting : CommandBase
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000FF12F File Offset: 0x000FD52F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Active"
				};
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000FF147 File Offset: 0x000FD547
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					bool.TrueString
				};
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000FF160 File Offset: 0x000FD560
		public override void Do()
		{
			base.Do();
			int num = 0;
			CharaData chara = this.GetChara(ref num);
			if (chara == null)
			{
				return;
			}
			bool flag = bool.Parse(this.args[num++]);
			if (flag)
			{
				chara.backup.Repair();
			}
			else
			{
				chara.backup.Set();
			}
		}
	}
}
