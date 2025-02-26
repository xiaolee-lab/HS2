using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA7 RID: 3495
	[TaskCategory("商人")]
	public class TalkWithAgent : MerchantAction
	{
		// Token: 0x06006D0B RID: 27915 RVA: 0x002E8004 File Offset: 0x002E6404
		public override void OnStart()
		{
			base.OnStart();
			base.Merchant.ActivateNavMeshObstacle(base.Merchant.Position);
			this.agent = (base.Merchant.CommandPartner as AgentActor);
			base.Merchant.CrossFade();
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x002E8043 File Offset: 0x002E6443
		public override TaskStatus OnUpdate()
		{
			if (this.agent == null)
			{
				return TaskStatus.Failure;
			}
			if (this.agent.LivesTalkSequence)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x002E806B File Offset: 0x002E646B
		public override void OnEnd()
		{
			if (this.agent != null && base.Merchant.CommandPartner == this.agent)
			{
				base.Merchant.CommandPartner = null;
			}
			base.OnEnd();
		}

		// Token: 0x04005B21 RID: 23329
		private AgentActor agent;
	}
}
