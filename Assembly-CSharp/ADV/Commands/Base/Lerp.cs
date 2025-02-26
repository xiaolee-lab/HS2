using System;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006E3 RID: 1763
	public class Lerp : CommandBase
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000F5B6F File Offset: 0x000F3F6F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Answer",
					"A",
					"B",
					"T",
					"isUnclamped"
				};
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000F5B9F File Offset: 0x000F3F9F
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Answer",
					"0",
					"0",
					"0",
					bool.FalseString
				};
			}
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000F5BCF File Offset: 0x000F3FCF
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			this.answer = this.args[0];
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000F5BE8 File Offset: 0x000F3FE8
		public override void Do()
		{
			base.Do();
			int num = 1;
			Func<float, float, float, bool, float> func = (float a, float b, float t, bool isUnclamped) => (!isUnclamped) ? Mathf.LerpUnclamped(a, b, t) : Mathf.Lerp(a, b, t);
			base.scenario.Vars[this.answer] = new ValData(func(float.Parse(this.args[num++]), float.Parse(this.args[num++]), float.Parse(this.args[num++]), bool.Parse(this.args[num++])));
		}

		// Token: 0x04002ACE RID: 10958
		private string answer;
	}
}
