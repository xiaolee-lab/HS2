using System;
using UnityEngine.AI;

namespace ADV.Commands.Game
{
	// Token: 0x02000746 RID: 1862
	public class NavMeshAgentSetActive : CommandBase
	{
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000FDA96 File Offset: 0x000FBE96
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"isEnabled"
				};
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000FDAA6 File Offset: 0x000FBEA6
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					bool.TrueString
				};
			}
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000FDAB6 File Offset: 0x000FBEB6
		public override void Do()
		{
			base.Do();
			this.isEnabled = bool.Parse(this.args[0]);
			base.scenario.currentChara.transform.GetComponent<NavMeshAgent>().enabled = this.isEnabled;
		}

		// Token: 0x04002B4E RID: 11086
		private bool isEnabled;
	}
}
