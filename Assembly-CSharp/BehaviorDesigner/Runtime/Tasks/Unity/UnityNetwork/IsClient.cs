using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNetwork
{
	// Token: 0x020001CB RID: 459
	public class IsClient : Conditional
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00026C5D File Offset: 0x0002505D
		public override TaskStatus OnUpdate()
		{
			return (!NetworkClient.active) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}
