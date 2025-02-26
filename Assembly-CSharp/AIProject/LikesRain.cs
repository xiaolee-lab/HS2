using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D72 RID: 3442
	[TaskCategory("")]
	public class LikesRain : AgentConditional
	{
		// Token: 0x06006C4A RID: 27722 RVA: 0x002E6000 File Offset: 0x002E4400
		public override TaskStatus OnUpdate()
		{
			foreach (KeyValuePair<int, int> keyValuePair in base.Agent.ChaControl.fileGameInfo.normalSkill)
			{
				if (keyValuePair.Value == 0)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}
	}
}
