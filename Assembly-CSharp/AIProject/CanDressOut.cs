using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D82 RID: 3458
	[TaskCategory("")]
	public class CanDressOut : AgentConditional
	{
		// Token: 0x06006C6A RID: 27754 RVA: 0x002E63B0 File Offset: 0x002E47B0
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if ((float)agent.ChaControl.fileGameInfo.flavorState[2] >= Singleton<Resources>.Instance.StatusProfile.CanDressBorder && agent.AgentData.PlayedDressIn)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
