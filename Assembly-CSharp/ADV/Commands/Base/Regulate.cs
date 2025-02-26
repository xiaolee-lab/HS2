using System;
using Illusion;
using Illusion.Extensions;

namespace ADV.Commands.Base
{
	// Token: 0x020006F1 RID: 1777
	public class Regulate : CommandBase
	{
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000F682B File Offset: 0x000F4C2B
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"Regulate"
				};
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000F6844 File Offset: 0x000F4C44
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"Set",
					Regulate.Control.Next.ToString()
				};
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000F6874 File Offset: 0x000F4C74
		public override void Do()
		{
			base.Do();
			int num = 0;
			string self = this.args[num++];
			int num2 = self.Check(true, Illusion.Utils.Enum<Regulate.Type>.Names);
			string self2 = this.args[num++];
			int index = self2.Check(true, Illusion.Utils.Enum<Regulate.Control>.Names);
			Array values = Illusion.Utils.Enum<Regulate.Control>.Values;
			Regulate.Control regulate = (Regulate.Control)values.GetValue(index);
			Regulate.Type type = (Regulate.Type)num2;
			if (type != Regulate.Type.Set)
			{
				if (type != Regulate.Type.Add)
				{
					if (type == Regulate.Type.Sub)
					{
						base.scenario.regulate.SubRegulate(regulate);
					}
				}
				else
				{
					base.scenario.regulate.AddRegulate(regulate);
				}
			}
			else
			{
				base.scenario.regulate.SetRegulate(regulate);
			}
		}

		// Token: 0x020006F2 RID: 1778
		private enum Type
		{
			// Token: 0x04002AE0 RID: 10976
			Set,
			// Token: 0x04002AE1 RID: 10977
			Add,
			// Token: 0x04002AE2 RID: 10978
			Sub
		}
	}
}
