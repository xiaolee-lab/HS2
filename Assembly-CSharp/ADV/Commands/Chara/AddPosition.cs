using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200072A RID: 1834
	public class AddPosition : CommandBase
	{
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000FC214 File Offset: 0x000FA614
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"X",
					"Y",
					"Z",
					"Pitch",
					"Yaw",
					"Roll"
				};
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000FC254 File Offset: 0x000FA654
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString()
				};
			}
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000FC280 File Offset: 0x000FA680
		public override void Do()
		{
			base.Do();
			int index = 0;
			int no = int.Parse(this.args[index++]);
			Transform transform = base.scenario.commandController.GetChara(no).transform;
			Vector3 b;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out b))
			{
				CommandBase.CountAddV3(this.args, ref index, ref b);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.position += b;
			Vector3 b2;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out b2))
			{
				CommandBase.CountAddV3(this.args, ref index, ref b2);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.eulerAngles += b2;
		}
	}
}
