using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000763 RID: 1891
	public class Position : CommandBase
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000FF514 File Offset: 0x000FD914
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Type",
					"X",
					"Y",
					"Z"
				};
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000FF544 File Offset: 0x000FD944
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					Position.Type.Add.ToString(),
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000FF58C File Offset: 0x000FD98C
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string self = this.args[num++];
			int num2 = self.Check(true, Enum.GetNames(typeof(Position.Type)));
			Vector3 zero = Vector3.zero;
			CommandBase.CountAddV3(this.args, ref num, ref zero);
			Position.Type type = (Position.Type)num2;
			if (type != Position.Type.Add)
			{
				if (type == Position.Type.Set)
				{
					base.scenario.commandController.Objects[key].transform.position = zero;
				}
			}
			else
			{
				base.scenario.commandController.Objects[key].transform.position += zero;
			}
		}

		// Token: 0x02000764 RID: 1892
		private enum Type
		{
			// Token: 0x04002B59 RID: 11097
			Add,
			// Token: 0x04002B5A RID: 11098
			Set
		}
	}
}
