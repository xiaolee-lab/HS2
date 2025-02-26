using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D65 RID: 3429
	[TaskCategory("")]
	public class IsPlayerFree : AgentConditional
	{
		// Token: 0x06006C2F RID: 27695 RVA: 0x002E5C68 File Offset: 0x002E4068
		public override TaskStatus OnUpdate()
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
			if (player.IsNeutralCommand)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
