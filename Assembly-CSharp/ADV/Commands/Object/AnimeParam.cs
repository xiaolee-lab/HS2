using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x0200076C RID: 1900
	public class AnimeParam : CommandBase
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000FFB22 File Offset: 0x000FDF22
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Type",
					"Param1",
					"Param2"
				};
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000FFB4C File Offset: 0x000FDF4C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					AnimeParam.Type.Float.ToString(),
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000FFB94 File Offset: 0x000FDF94
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			Animator component = base.scenario.commandController.Objects[key].GetComponent<Animator>();
			string self = this.args[num++];
			switch (self.Check(true, Enum.GetNames(typeof(AnimeParam.Type))))
			{
			case 0:
				component.SetFloat(this.args[num++], float.Parse(this.args[num++]));
				break;
			case 1:
				component.SetInteger(this.args[num++], int.Parse(this.args[num++]));
				break;
			case 2:
				component.SetBool(this.args[num++], bool.Parse(this.args[num++]));
				break;
			case 3:
				component.SetTrigger(this.args[num++]);
				break;
			case 4:
				component.SetLayerWeight(int.Parse(this.args[num++]), float.Parse(this.args[num++]));
				break;
			}
		}

		// Token: 0x0200076D RID: 1901
		private enum Type
		{
			// Token: 0x04002B65 RID: 11109
			Float,
			// Token: 0x04002B66 RID: 11110
			Int,
			// Token: 0x04002B67 RID: 11111
			Bool,
			// Token: 0x04002B68 RID: 11112
			Trigger,
			// Token: 0x04002B69 RID: 11113
			Weight
		}
	}
}
