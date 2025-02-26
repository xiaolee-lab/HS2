using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D5C RID: 3420
	[TaskCategory("")]
	public class IsHealthManager : AgentConditional
	{
		// Token: 0x06006C1D RID: 27677 RVA: 0x002E58B8 File Offset: 0x002E3CB8
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			bool flag = agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(28);
			float num = agent.AgentData.StatsTable[3];
			bool flag2 = num < Singleton<Resources>.Instance.StatusProfile.HealthyPhysicalBorder;
			if (flag && flag2)
			{
				int desireKey = Desire.GetDesireKey(Desire.Type.Break);
				agent.SetMotivation(desireKey, agent.AgentData.StatsTable[5]);
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
