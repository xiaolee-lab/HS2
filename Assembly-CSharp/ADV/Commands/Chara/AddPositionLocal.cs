using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200072C RID: 1836
	public class AddPositionLocal : CommandBase
	{
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000FC4B4 File Offset: 0x000FA8B4
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

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000FC4F4 File Offset: 0x000FA8F4
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

		// Token: 0x06002B9A RID: 11162 RVA: 0x000FC520 File Offset: 0x000FA920
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
			transform.localPosition += b;
			Vector3 b2;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out b2))
			{
				CommandBase.CountAddV3(this.args, ref index, ref b2);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.localEulerAngles += b2;
		}
	}
}
