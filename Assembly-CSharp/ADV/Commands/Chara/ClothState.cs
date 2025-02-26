using System;
using AIChara;
using Illusion.Extensions;

namespace ADV.Commands.Chara
{
	// Token: 0x0200070D RID: 1805
	public class ClothState : CommandBase
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000F9F95 File Offset: 0x000F8395
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Kind",
					"State"
				};
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000F9FB8 File Offset: 0x000F83B8
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

		// Token: 0x06002B23 RID: 11043 RVA: 0x000F9FF0 File Offset: 0x000F83F0
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			string text = this.args[num++];
			int clothesKind;
			if (!int.TryParse(text, out clothesKind))
			{
				clothesKind = text.Check(true, Enum.GetNames(typeof(ChaFileDefine.ClothesKind)));
			}
			int num2 = int.Parse(this.args[num++]);
			base.scenario.commandController.GetChara(no).chaCtrl.SetClothesState(clothesKind, (byte)num2, true);
		}
	}
}
