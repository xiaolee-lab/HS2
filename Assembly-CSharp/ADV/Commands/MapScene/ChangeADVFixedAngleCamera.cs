using System;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x0200075A RID: 1882
	public class ChangeADVFixedAngleCamera : CommandBase
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000FECBD File Offset: 0x000FD0BD
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"AttitudeID"
				};
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000FECD5 File Offset: 0x000FD0D5
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					"0"
				};
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000FECF0 File Offset: 0x000FD0F0
		public override void Do()
		{
			base.Do();
			int num = 0;
			CharaData chara = this.GetChara(ref num);
			if (chara == null)
			{
				return;
			}
			int attitudeID = int.Parse(this.args[num++]);
			ADV.ChangeADVFixedAngleCamera(chara.data.actor, attitudeID);
		}
	}
}
