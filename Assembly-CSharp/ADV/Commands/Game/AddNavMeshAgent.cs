using System;
using UnityEngine.AI;

namespace ADV.Commands.Game
{
	// Token: 0x02000745 RID: 1861
	public class AddNavMeshAgent : CommandBase
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000FDA65 File Offset: 0x000FBE65
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000FDA68 File Offset: 0x000FBE68
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000FDA6B File Offset: 0x000FBE6B
		public override void Do()
		{
			base.Do();
			base.scenario.currentChara.transform.gameObject.AddComponent<NavMeshAgent>();
		}
	}
}
