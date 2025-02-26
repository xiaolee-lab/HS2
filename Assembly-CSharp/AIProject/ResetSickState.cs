using System;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D0D RID: 3341
	[TaskCategory("")]
	public class ResetSickState : AgentAction
	{
		// Token: 0x06006B1E RID: 27422 RVA: 0x002DC5F4 File Offset: 0x002DA9F4
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Sickness sickState = agent.AgentData.SickState;
			if (sickState.ID == 0)
			{
				agent.AgentData.ColdLockInfo.Lock = true;
				if (sickState.Enabled)
				{
					agent.SetStatus(0, 50f);
				}
			}
			sickState.ID = -1;
			return TaskStatus.Success;
		}
	}
}
