using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D84 RID: 3460
	[TaskCategory("")]
	public class CanGirlsAction : AgentConditional
	{
		// Token: 0x06006C6E RID: 27758 RVA: 0x002E6428 File Offset: 0x002E4828
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.CanGirlsAction)
			{
				float num = UnityEngine.Random.Range(0f, 100f);
				if (num <= Singleton<Manager.Resources>.Instance.StatusProfile.GirlsActionProb)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}
	}
}
