using System;
using System.Linq;
using AIChara;

namespace ADV.Commands.H
{
	// Token: 0x02000748 RID: 1864
	public class MozVisible : CommandBase
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000FDB84 File Offset: 0x000FBF84
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"isVisible"
				};
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000FDB9C File Offset: 0x000FBF9C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					bool.TrueString
				};
			}
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000FDBB4 File Offset: 0x000FBFB4
		public override void Do()
		{
			base.Do();
			int num = 0;
			string[] argToSplit = base.GetArgToSplit(num++);
			bool flag = bool.Parse(this.args[num++]);
			foreach (ChaControl chaControl in from s in argToSplit
			select base.scenario.commandController.GetChara(int.Parse(s)) into p
			select p.chaCtrl)
			{
				chaControl.hideMoz = !flag;
			}
		}
	}
}
