using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA3 RID: 3491
	[TaskCategory("商人")]
	public class HWithAgent : MerchantAction
	{
		// Token: 0x06006CFC RID: 27900 RVA: 0x002E7CDC File Offset: 0x002E60DC
		public override void OnStart()
		{
			base.OnStart();
			base.Merchant.DeactivateNavMeshElement();
			this._agent = (base.Merchant.Partner as AgentActor);
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x002E7D05 File Offset: 0x002E6105
		public override TaskStatus OnUpdate()
		{
			return (!(this._agent != null)) ? TaskStatus.Failure : TaskStatus.Running;
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x002E7D20 File Offset: 0x002E6120
		public override void OnBehaviorComplete()
		{
			AgentActor agentActor = base.Merchant.Partner as AgentActor;
			if (agentActor != null && agentActor == this._agent)
			{
				Desire.ActionType mode = agentActor.Mode;
				if (mode != Desire.ActionType.EndTaskLesbianH && mode != Desire.ActionType.EndTaskLesbianMerchantH)
				{
					base.Merchant.Partner = null;
				}
			}
			base.OnBehaviorComplete();
		}

		// Token: 0x04005B1D RID: 23325
		private AgentActor _agent;
	}
}
