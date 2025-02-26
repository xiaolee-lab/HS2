using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CAD RID: 3245
	[TaskCategory("")]
	public class Toilet : AgentStateAction
	{
		// Token: 0x06006971 RID: 26993 RVA: 0x002CE6D8 File Offset: 0x002CCAD8
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Toilet;
			base.OnStart();
			AgentProfile.PoseIDCollection poseIDTable = Singleton<Resources>.Instance.AgentProfile.PoseIDTable;
			int hpositionSubID = agent.HPositionSubID;
			if (hpositionSubID != 3)
			{
				if (hpositionSubID == 5)
				{
					agent.SurprisePoseID = new PoseKeyPair?(poseIDTable.SurpriseInToiletSquatID);
				}
			}
			else
			{
				agent.SurprisePoseID = new PoseKeyPair?(poseIDTable.SurpriseInToiletSitID);
			}
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x002CE750 File Offset: 0x002CCB50
		protected override void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Toilet);
			agent.SetDesire(desireKey, 0f);
			agent.SurprisePoseID = null;
		}
	}
}
