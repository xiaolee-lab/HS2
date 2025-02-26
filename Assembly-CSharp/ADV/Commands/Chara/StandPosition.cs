using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x02000726 RID: 1830
	public class StandPosition : CommandBase
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x000FBCFC File Offset: 0x000FA0FC
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Stand"
				};
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000FBD14 File Offset: 0x000FA114
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"Center"
				};
			}
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000FBD48 File Offset: 0x000FA148
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			Transform transform = base.scenario.commandController.GetChara(no).transform;
			string key = this.args[num++];
			Transform transform2 = base.scenario.commandController.characterStandNulls[key];
			transform.SetPositionAndRotation(transform2.position, transform2.rotation);
		}
	}
}
