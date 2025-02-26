using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D81 RID: 3457
	[TaskCategory("")]
	public class CanDressBeforeBath : AgentConditional
	{
		// Token: 0x06006C68 RID: 27752 RVA: 0x002E6372 File Offset: 0x002E4772
		public override TaskStatus OnUpdate()
		{
			if ((float)base.Agent.ChaControl.fileGameInfo.flavorState[2] > Singleton<Resources>.Instance.StatusProfile.CanDressBorder)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
