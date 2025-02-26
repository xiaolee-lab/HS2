using System;

namespace ADV.Commands.Chara
{
	// Token: 0x0200070B RID: 1803
	public class SetShape : CommandBase
	{
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000F9DF7 File Offset: 0x000F81F7
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Index",
					"Value"
				};
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000F9E17 File Offset: 0x000F8217
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					"0",
					"0.5"
				};
			}
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000F9E38 File Offset: 0x000F8238
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int index = int.Parse(this.args[num++]);
			float value = float.Parse(this.args[num++]);
			base.scenario.commandController.GetChara(no).chaCtrl.SetShapeBodyValue(index, value);
		}
	}
}
