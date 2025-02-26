using System;
using AIChara;

namespace ADV.Commands.Chara
{
	// Token: 0x02000722 RID: 1826
	public class MotionWait : CommandBase
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000FB8E1 File Offset: 0x000F9CE1
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"LayerNo",
					"Time"
				};
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000FB904 File Offset: 0x000F9D04
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					"1"
				};
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000FB940 File Offset: 0x000F9D40
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			this.layerNo = int.Parse(this.args[num++]);
			this.time = float.Parse(this.args[num++]);
			this.chaCtrl = base.scenario.commandController.GetChara(no).chaCtrl;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000FB9B4 File Offset: 0x000F9DB4
		public override bool Process()
		{
			base.Process();
			return this.chaCtrl.getAnimatorStateInfo(this.layerNo).normalizedTime >= this.time;
		}

		// Token: 0x04002B1F RID: 11039
		private int layerNo;

		// Token: 0x04002B20 RID: 11040
		private float time;

		// Token: 0x04002B21 RID: 11041
		private ChaControl chaCtrl;
	}
}
