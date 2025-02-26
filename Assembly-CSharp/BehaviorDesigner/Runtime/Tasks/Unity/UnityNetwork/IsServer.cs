using System;
using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNetwork
{
	// Token: 0x020001CC RID: 460
	public class IsServer : Conditional
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x00026C78 File Offset: 0x00025078
		public override TaskStatus OnUpdate()
		{
			return (!NetworkServer.active) ? TaskStatus.Failure : TaskStatus.Success;
		}
	}
}
