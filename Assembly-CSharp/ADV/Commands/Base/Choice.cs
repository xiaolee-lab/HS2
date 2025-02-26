using System;
using System.Linq;
using AIProject.UI;
using Manager;

namespace ADV.Commands.Base
{
	// Token: 0x020006D9 RID: 1753
	public class Choice : CommandBase
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x000F4F91 File Offset: 0x000F3391
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Visible",
					"Case"
				};
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x000F4FA9 File Offset: 0x000F33A9
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					bool.TrueString,
					"text,tag"
				};
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000F4FC4 File Offset: 0x000F33C4
		public override void Do()
		{
			base.Do();
			int count = 0;
			if (!bool.Parse(this.args[count++]))
			{
				base.scenario.captionSystem.Clear();
			}
			CommCommandList.CommandInfo[] options = (from arg in this.args.Skip(count)
			where !this.args.IsNullOrEmpty<string>()
			select arg.Split(new char[]
			{
				','
			}) into ss
			where ss.Length >= 2
			select new Choice.ChoiceData(base.scenario.ReplaceText(ss[0]), base.scenario.ReplaceVars(ss[1])) into choice
			select new CommCommandList.CommandInfo(choice.text, null, delegate(int x)
			{
				this.choice = choice;
			})).ToArray<CommCommandList.CommandInfo>();
			this.labelTitleBK = base.scenario.ChoiceON(string.Empty, options);
			base.scenario.isSkip = (base.scenario.isSkip && Config.GameData.ChoicesSkip);
			base.scenario.isAuto = (base.scenario.isAuto && Config.GameData.ChoicesAuto);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000F50EF File Offset: 0x000F34EF
		public override bool Process()
		{
			base.Process();
			return this.choice != null;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000F5104 File Offset: 0x000F3504
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			if (!processEnd)
			{
				return;
			}
			base.scenario.ChoiceOFF(this.labelTitleBK);
			base.scenario.TextLogCall(new Text.Data(new string[]
			{
				string.Empty,
				this.choice.text
			}), null);
			base.scenario.SearchTagJumpOrOpenFile(this.choice.jump, base.localLine);
			base.scenario.captions.CanvasGroupON();
		}

		// Token: 0x04002ABD RID: 10941
		private Choice.ChoiceData choice;

		// Token: 0x04002ABE RID: 10942
		private string labelTitleBK = string.Empty;

		// Token: 0x020006DA RID: 1754
		public class ChoiceData
		{
			// Token: 0x060029F4 RID: 10740 RVA: 0x000F521A File Offset: 0x000F361A
			public ChoiceData(string text, string jump)
			{
				this.text = text;
				this.jump = jump;
			}

			// Token: 0x04002AC1 RID: 10945
			public readonly string text;

			// Token: 0x04002AC2 RID: 10946
			public readonly string jump;
		}
	}
}
