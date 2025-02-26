using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006DB RID: 1755
	public class Close : CommandBase
	{
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x000F5253 File Offset: 0x000F3653
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000F5256 File Offset: 0x000F3656
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000F5259 File Offset: 0x000F3659
		public override void Do()
		{
			base.Do();
			this.Proc();
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000F5268 File Offset: 0x000F3668
		private void Proc()
		{
			bool isADVActionActive = Program.isADVActionActive;
			if (base.scenario.advScene != null)
			{
				base.scenario.advScene.Release();
			}
			else
			{
				base.scenario.gameObject.SetActive(false);
			}
		}
	}
}
