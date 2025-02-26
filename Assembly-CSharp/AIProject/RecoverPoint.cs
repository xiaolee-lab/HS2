using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CE0 RID: 3296
	[TaskCategory("")]
	public class RecoverPoint : AgentAction
	{
		// Token: 0x06006A7A RID: 27258 RVA: 0x002D5D80 File Offset: 0x002D4180
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.ActivateNavMeshAgent();
			agent.Animation.EndStates();
			agent.SetActiveOnEquipedItem(true);
			return TaskStatus.Success;
		}
	}
}
