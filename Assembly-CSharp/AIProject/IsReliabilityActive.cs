using System;
using AIChara;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D6A RID: 3434
	[TaskCategory("")]
	public class IsReliabilityActive : AgentConditional
	{
		// Token: 0x06006C39 RID: 27705 RVA: 0x002E5D9C File Offset: 0x002E419C
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			ChaFileGameInfo fileGameInfo = agent.ChaControl.fileGameInfo;
			if (this._checkLow)
			{
				if (fileGameInfo.phase <= 1)
				{
					return TaskStatus.Success;
				}
				if (fileGameInfo.flavorState[1] <= this._border)
				{
					return TaskStatus.Success;
				}
			}
			else
			{
				if (fileGameInfo.phase <= 1)
				{
					return TaskStatus.Failure;
				}
				if (fileGameInfo.flavorState[1] >= this._border)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ADD RID: 23261
		[SerializeField]
		private bool _checkLow;

		// Token: 0x04005ADE RID: 23262
		[SerializeField]
		private int _border = 50;
	}
}
