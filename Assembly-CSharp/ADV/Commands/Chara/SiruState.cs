using System;
using AIChara;
using Illusion.Extensions;

namespace ADV.Commands.Chara
{
	// Token: 0x0200070E RID: 1806
	public class SiruState : CommandBase
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000FA083 File Offset: 0x000F8483
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Parts",
					"State"
				};
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000FA0A4 File Offset: 0x000F84A4
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					ChaFileDefine.ClothesKind.top.ToString(),
					"0"
				};
			}
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000FA0DC File Offset: 0x000F84DC
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			string text = this.args[num++];
			int parts;
			if (!int.TryParse(text, out parts))
			{
				parts = text.Check(true, Enum.GetNames(typeof(ChaFileDefine.SiruParts)));
			}
			string text2 = this.args[num++];
			int num2;
			if (!int.TryParse(text2, out num2))
			{
				num2 = text2.Check(true, new string[]
				{
					"なし",
					"少ない",
					"多い"
				});
			}
			base.scenario.commandController.GetChara(no).chaCtrl.SetSiruFlag((ChaFileDefine.SiruParts)parts, (byte)num2);
		}
	}
}
