using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200072B RID: 1835
	public class SetPositionLocal : CommandBase
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000FC367 File Offset: 0x000FA767
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

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000FC3A8 File Offset: 0x000FA7A8
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

		// Token: 0x06002B96 RID: 11158 RVA: 0x000FC3D4 File Offset: 0x000FA7D4
		public override void Do()
		{
			base.Do();
			int index = 0;
			int no = int.Parse(this.args[index++]);
			Transform transform = base.scenario.commandController.GetChara(no).transform;
			Vector3 localPosition;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out localPosition))
			{
				localPosition = transform.localPosition;
				CommandBase.CountAddV3(this.args, ref index, ref localPosition);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.localPosition = localPosition;
			Vector3 localEulerAngles;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out localEulerAngles))
			{
				localEulerAngles = transform.localEulerAngles;
				CommandBase.CountAddV3(this.args, ref index, ref localEulerAngles);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.localEulerAngles = localEulerAngles;
		}
	}
}
