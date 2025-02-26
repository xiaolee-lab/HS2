using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000765 RID: 1893
	public class Rotation : CommandBase
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000FF660 File Offset: 0x000FDA60
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Type",
					"Pitch",
					"Yaw",
					"Roll"
				};
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000FF690 File Offset: 0x000FDA90
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					Rotation.Type.Add.ToString(),
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000FF6D8 File Offset: 0x000FDAD8
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string self = this.args[num++];
			int num2 = self.Check(true, Enum.GetNames(typeof(Rotation.Type)));
			Vector3 zero = Vector3.zero;
			CommandBase.CountAddV3(this.args, ref num, ref zero);
			Rotation.Type type = (Rotation.Type)num2;
			if (type != Rotation.Type.Add)
			{
				if (type == Rotation.Type.Set)
				{
					base.scenario.commandController.Objects[key].transform.eulerAngles = zero;
				}
			}
			else
			{
				base.scenario.commandController.Objects[key].transform.eulerAngles += zero;
			}
		}

		// Token: 0x02000766 RID: 1894
		private enum Type
		{
			// Token: 0x04002B5C RID: 11100
			Add,
			// Token: 0x04002B5D RID: 11101
			Set
		}
	}
}
