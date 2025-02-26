using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D3E RID: 3390
	[TaskCategory("")]
	public class ExistsLesbianSkill : AgentConditional
	{
		// Token: 0x06006BDD RID: 27613 RVA: 0x002E4C04 File Offset: 0x002E3004
		public override TaskStatus OnUpdate()
		{
			AgentProfile.HSkillIDDefines hskillIDSetting = Singleton<Resources>.Instance.AgentProfile.HSkillIDSetting;
			foreach (KeyValuePair<int, int> keyValuePair in base.Agent.ChaControl.fileGameInfo.hSkill)
			{
				if (keyValuePair.Value != -1)
				{
					if (keyValuePair.Value == hskillIDSetting.homosexualID)
					{
						return TaskStatus.Success;
					}
				}
			}
			return TaskStatus.Failure;
		}
	}
}
