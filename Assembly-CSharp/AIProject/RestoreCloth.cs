using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CA4 RID: 3236
	[TaskCategory("")]
	public class RestoreCloth : AgentStateAction
	{
		// Token: 0x06006947 RID: 26951 RVA: 0x002CC5C0 File Offset: 0x002CA9C0
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.ClothChange;
			agent.AgentData.NowCoordinateFileName = null;
			base.OnStart();
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x002CC5F1 File Offset: 0x002CA9F1
		protected override void OnCompletedStateTask()
		{
			base.Agent.AgentData.IsOtherCoordinate = false;
		}
	}
}
