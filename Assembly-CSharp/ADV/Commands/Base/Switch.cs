using System;
using System.Collections.Generic;
using System.Linq;

namespace ADV.Commands.Base
{
	// Token: 0x020006F7 RID: 1783
	public class Switch : CommandBase
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000F6CDC File Offset: 0x000F50DC
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Key",
					"CaseTag"
				};
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x000F6CF4 File Offset: 0x000F50F4
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"a",
					"Case,Tag"
				};
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000F6D0C File Offset: 0x000F510C
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int count = 0;
			this.key = this.args[count++];
			this.answers = (from s in this.args.Skip(count)
			select s.Split(new char[]
			{
				','
			})).Select(delegate(string[] array)
			{
				string[] result;
				if (array.Length == 1)
				{
					string[] array2 = new string[2];
					array2[0] = "default";
					result = array2;
					array2[1] = array[0];
				}
				else
				{
					result = array;
				}
				return result;
			}).ToDictionary((string[] v) => v[0], (string[] v) => v[1]);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000F6DC8 File Offset: 0x000F51C8
		public override void Do()
		{
			base.Do();
			object o = base.scenario.Vars[this.key].o;
			string text = o.ToString();
			string jump;
			if (!this.answers.TryGetValue(text, out jump))
			{
				text = "default";
				jump = this.answers[text];
			}
			base.scenario.SearchTagJumpOrOpenFile(jump, base.localLine);
		}

		// Token: 0x04002AE3 RID: 10979
		private const string defaultKey = "default";

		// Token: 0x04002AE4 RID: 10980
		private string key;

		// Token: 0x04002AE5 RID: 10981
		private Dictionary<string, string> answers;
	}
}
