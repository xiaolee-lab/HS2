using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000C98 RID: 3224
	[TaskCategory("")]
	public class Bath : AgentStateAction
	{
		// Token: 0x06006918 RID: 26904 RVA: 0x002CB29C File Offset: 0x002C969C
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Bath;
			base.OnStart();
			AgentProfile.PoseIDCollection poseIDTable = Singleton<Resources>.Instance.AgentProfile.PoseIDTable;
			int hpositionSubID = agent.HPositionSubID;
			if (hpositionSubID != 2 && hpositionSubID != 13)
			{
				if (hpositionSubID == 8)
				{
					agent.SurprisePoseID = new PoseKeyPair?(poseIDTable.SurpriseInBathStandID);
				}
			}
			else
			{
				agent.SurprisePoseID = new PoseKeyPair?(poseIDTable.SurpriseInBathSitID);
			}
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x002CB31C File Offset: 0x002C971C
		protected override void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Bath);
			agent.SetDesire(desireKey, 0f);
			agent.SurprisePoseID = null;
			if (!agent.AgentData.PlayedDressIn)
			{
				agent.NextPoint = null;
				agent.AgentData.Wetness = 100f;
			}
		}
	}
}
