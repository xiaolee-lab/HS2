using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D18 RID: 3352
	[TaskCategory("")]
	public class SetPlayerToTargetInSight : AgentAction
	{
		// Token: 0x06006B5D RID: 27485 RVA: 0x002E03C7 File Offset: 0x002DE7C7
		public override TaskStatus OnUpdate()
		{
			base.Agent.TargetInSightActor = Singleton<Map>.Instance.Player;
			return TaskStatus.Success;
		}
	}
}
