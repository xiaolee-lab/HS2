using System;

namespace ADV.Commands.Chara
{
	// Token: 0x02000723 RID: 1827
	public class MotionLayerWeight : CommandBase
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000FB9F4 File Offset: 0x000F9DF4
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"LayerNo",
					"Weight"
				};
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000FBA14 File Offset: 0x000F9E14
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					"0"
				};
			}
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000FBA50 File Offset: 0x000F9E50
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int layerIndex = int.Parse(this.args[num++]);
			float weight = float.Parse(this.args[num++]);
			base.scenario.commandController.GetChara(no).chaCtrl.animBody.SetLayerWeight(layerIndex, weight);
		}
	}
}
