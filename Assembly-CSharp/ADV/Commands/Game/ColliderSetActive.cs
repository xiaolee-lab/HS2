using System;
using UnityEngine;

namespace ADV.Commands.Game
{
	// Token: 0x02000744 RID: 1860
	public class ColliderSetActive : CommandBase
	{
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000FDA02 File Offset: 0x000FBE02
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

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000FDA12 File Offset: 0x000FBE12
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

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000FDA22 File Offset: 0x000FBE22
		public override void Do()
		{
			base.Do();
			this.isEnabled = bool.Parse(this.args[0]);
			base.scenario.currentChara.transform.GetComponent<Collider>().enabled = this.isEnabled;
		}

		// Token: 0x04002B4D RID: 11085
		private bool isEnabled;
	}
}
