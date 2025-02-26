using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D4F RID: 3407
	[TaskCategory("")]
	public class IsBadGirl : AgentConditional
	{
		// Token: 0x06006C01 RID: 27649 RVA: 0x002E5378 File Offset: 0x002E3778
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			if (agent.ChaControl.fileGameInfo.morality < 50 && agent.AgentData.StatsTable[2] < statusProfile.ShallowSleepHungerLowBorder)
			{
				float num = UnityEngine.Random.Range(0f, 100f);
				if (num < statusProfile.ShallowSleepProb)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}
	}
}
