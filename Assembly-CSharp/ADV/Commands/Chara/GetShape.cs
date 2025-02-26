using System;

namespace ADV.Commands.Chara
{
	// Token: 0x0200070A RID: 1802
	public class GetShape : CommandBase
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x000F9D1A File Offset: 0x000F811A
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Name",
					"Index"
				};
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000F9D3A File Offset: 0x000F813A
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					string.Empty,
					"0"
				};
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000F9D5A File Offset: 0x000F815A
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.name = this.args[1];
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000F9D70 File Offset: 0x000F8170
		public override void Do()
		{
			base.Do();
			this.no = int.Parse(this.args[0]);
			this.index = int.Parse(this.args[2]);
			base.scenario.Vars[this.name] = new ValData(base.scenario.commandController.GetChara(this.no).chaCtrl.GetShapeBodyValue(this.index));
		}

		// Token: 0x04002B19 RID: 11033
		private int no;

		// Token: 0x04002B1A RID: 11034
		private string name;

		// Token: 0x04002B1B RID: 11035
		private int index;
	}
}
