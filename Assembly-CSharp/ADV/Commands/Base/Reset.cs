using System;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006F5 RID: 1781
	public class Reset : CommandBase
	{
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000F6A78 File Offset: 0x000F4E78
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"Pos",
					"Ang"
				};
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x000F6A98 File Offset: 0x000F4E98
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000F6AB8 File Offset: 0x000F4EB8
		public override void Do()
		{
			base.Do();
			int num = 0;
			int num2 = int.Parse(this.args[num++]);
			int num3 = 0;
			Vector3 zero = Vector3.zero;
			CommandBase.CountAddV3(base.GetArgToSplit(num++), ref num3, ref zero);
			Vector3 zero2 = Vector3.zero;
			num3 = 0;
			CommandBase.CountAddV3(base.GetArgToSplit(num++), ref num3, ref zero2);
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					base.scenario.commandController.CharaRoot.SetPositionAndRotation(zero, Quaternion.Euler(zero2));
				}
			}
			else
			{
				base.scenario.commandController.BaseRoot.SetPositionAndRotation(zero, Quaternion.Euler(zero2));
			}
		}
	}
}
