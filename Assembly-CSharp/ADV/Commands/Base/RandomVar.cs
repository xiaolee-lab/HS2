using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006F0 RID: 1776
	public class RandomVar : CommandBase
	{
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x000F6540 File Offset: 0x000F4940
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"Variable",
					"Min",
					"Max"
				};
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000F6568 File Offset: 0x000F4968
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"int",
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000F6590 File Offset: 0x000F4990
		public override void Convert(string fileName, ref string[] args)
		{
			new VAR().Convert(fileName, ref args);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000F65A0 File Offset: 0x000F49A0
		public override void ConvertBeforeArgsProc()
		{
			base.ConvertBeforeArgsProc();
			int num = 0;
			this.type = Type.GetType(this.args[num++]);
			this.name = this.args[num++];
			if (this.type != typeof(bool))
			{
				this.min = this.args.SafeGet(num++);
				this.max = this.args.SafeGet(num++);
				this.refMinCnt = VAR.RefCheck(ref this.min);
				this.refMaxCnt = VAR.RefCheck(ref this.max);
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000F6648 File Offset: 0x000F4A48
		public override void Do()
		{
			base.Do();
			Dictionary<string, ValData> vars = base.scenario.Vars;
			VAR.RefGet(this.type, this.refMinCnt, this.min, vars).SafeProc(delegate(string s)
			{
				this.min = s;
			});
			VAR.RefGet(this.type, this.refMaxCnt, this.max, vars).SafeProc(delegate(string s)
			{
				this.max = s;
			});
			if (this.type == typeof(int))
			{
				vars[this.name] = new ValData(ValData.Convert(UnityEngine.Random.Range(int.Parse(this.min), int.Parse(this.max) + 1), this.type));
			}
			else if (this.type == typeof(float))
			{
				vars[this.name] = new ValData(ValData.Convert(UnityEngine.Random.Range(float.Parse(this.min), float.Parse(this.max)), this.type));
			}
			else if (this.type == typeof(string))
			{
				vars[this.name] = new ValData(ValData.Convert((UnityEngine.Random.Range(0, 2) != 1) ? this.max : this.min, this.type));
			}
			else if (this.type == typeof(bool))
			{
				vars[this.name] = new ValData(ValData.Convert(UnityEngine.Random.Range(0, 2) == 1, this.type));
			}
		}

		// Token: 0x04002AD9 RID: 10969
		private Type type;

		// Token: 0x04002ADA RID: 10970
		private string name;

		// Token: 0x04002ADB RID: 10971
		private string min;

		// Token: 0x04002ADC RID: 10972
		private string max;

		// Token: 0x04002ADD RID: 10973
		private int refMinCnt;

		// Token: 0x04002ADE RID: 10974
		private int refMaxCnt;
	}
}
