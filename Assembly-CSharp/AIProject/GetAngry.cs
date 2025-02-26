using System;
using Manager;

namespace AIProject
{
	// Token: 0x02000CC1 RID: 3265
	public class GetAngry : AgentEmotion
	{
		// Token: 0x060069D3 RID: 27091 RVA: 0x002D1284 File Offset: 0x002CF684
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair angryID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.AngryID;
			base.PlayAnimation(angryID.postureID, angryID.poseID);
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x002D12D3 File Offset: 0x002CF6D3
		protected override void OnCompletedEmoteTask()
		{
			base.Agent.ApplySituationResultParameter(22);
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x002D12E2 File Offset: 0x002CF6E2
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}
	}
}
