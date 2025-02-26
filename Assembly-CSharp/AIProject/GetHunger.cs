using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CC4 RID: 3268
	public class GetHunger : AgentAction
	{
		// Token: 0x060069DE RID: 27102 RVA: 0x002D1414 File Offset: 0x002CF814
		public override TaskStatus OnUpdate()
		{
			Dictionary<int, float> statsTable;
			(statsTable = base.Agent.AgentData.StatsTable)[2] = statsTable[2] - 10f;
			return TaskStatus.Success;
		}
	}
}
