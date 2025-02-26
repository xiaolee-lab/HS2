using System;
using Manager;

namespace AIProject
{
	// Token: 0x02000CF5 RID: 3317
	public class WakeUp : AgentEmotion
	{
		// Token: 0x06006ACB RID: 27339 RVA: 0x002D9A50 File Offset: 0x002D7E50
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.DeactivateNavMeshAgent();
			PoseKeyPair wakeUpID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.WakeUpID;
			base.PlayAnimation(wakeUpID.postureID, wakeUpID.poseID);
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x002D9A99 File Offset: 0x002D7E99
		public override void OnEnd()
		{
			base.OnEnd();
			base.Agent.ActivateNavMeshAgent();
		}
	}
}
