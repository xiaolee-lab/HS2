using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000767 RID: 1895
	public class Scale : CommandBase
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000FF7AC File Offset: 0x000FDBAC
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

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000FF7DC File Offset: 0x000FDBDC
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					Scale.Type.Set.ToString(),
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000FF824 File Offset: 0x000FDC24
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string self = this.args[num++];
			int num2 = self.Check(true, Enum.GetNames(typeof(Scale.Type)));
			Vector3 zero = Vector3.zero;
			CommandBase.CountAddV3(this.args, ref num, ref zero);
			Scale.Type type = (Scale.Type)num2;
			if (type != Scale.Type.Add)
			{
				if (type == Scale.Type.Set)
				{
					base.scenario.commandController.Objects[key].transform.localScale = zero;
				}
			}
			else
			{
				base.scenario.commandController.Objects[key].transform.localScale += zero;
			}
		}

		// Token: 0x02000768 RID: 1896
		private enum Type
		{
			// Token: 0x04002B5F RID: 11103
			Add,
			// Token: 0x04002B60 RID: 11104
			Set
		}
	}
}
