using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CC6 RID: 3270
	[TaskCategory("")]
	public class GetStomachache : AgentAction
	{
		// Token: 0x060069E4 RID: 27108 RVA: 0x002D162E File Offset: 0x002CFA2E
		public override TaskStatus OnUpdate()
		{
			base.Agent.AgentData.SickState.ID = 1;
			return TaskStatus.Success;
		}
	}
}
