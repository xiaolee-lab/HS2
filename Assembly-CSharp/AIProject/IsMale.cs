using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D8E RID: 3470
	[TaskCategory("")]
	public class IsMale : AgentConditional
	{
		// Token: 0x06006C82 RID: 27778 RVA: 0x002E65A4 File Offset: 0x002E49A4
		public override TaskStatus OnUpdate()
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
			if (player.ChaControl.sex == 1 && !player.ChaControl.fileParam.futanari)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}
	}
}
