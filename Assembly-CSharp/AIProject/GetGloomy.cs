using System;
using Manager;

namespace AIProject
{
	// Token: 0x02000CC3 RID: 3267
	public class GetGloomy : AgentEmotion
	{
		// Token: 0x060069DB RID: 27099 RVA: 0x002D1398 File Offset: 0x002CF798
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair groomyID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.GroomyID;
			base.PlayAnimation(groomyID.postureID, groomyID.poseID);
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x002D13E8 File Offset: 0x002CF7E8
		protected override void OnCompletedEmoteTask()
		{
			AgentActor agent = base.Agent;
			agent.ApplySituationResultParameter(21);
			agent.ChangeDynamicNavMeshAgentAvoidance();
		}
	}
}
