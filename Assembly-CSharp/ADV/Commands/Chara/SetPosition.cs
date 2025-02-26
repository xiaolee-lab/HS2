using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x02000729 RID: 1833
	public class SetPosition : CommandBase
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000FC0C5 File Offset: 0x000FA4C5
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

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000FC108 File Offset: 0x000FA508
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

		// Token: 0x06002B8E RID: 11150 RVA: 0x000FC134 File Offset: 0x000FA534
		public override void Do()
		{
			base.Do();
			int index = 0;
			int no = int.Parse(this.args[index++]);
			Transform transform = base.scenario.commandController.GetChara(no).transform;
			Vector3 position;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out position))
			{
				position = transform.position;
				CommandBase.CountAddV3(this.args, ref index, ref position);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.position = position;
			Vector3 eulerAngles;
			if (!base.scenario.commandController.GetV3Dic(this.args.SafeGet(index), out eulerAngles))
			{
				eulerAngles = transform.eulerAngles;
				CommandBase.CountAddV3(this.args, ref index, ref eulerAngles);
			}
			else
			{
				CommandBase.CountAddV3(ref index);
			}
			transform.eulerAngles = eulerAngles;
		}
	}
}
