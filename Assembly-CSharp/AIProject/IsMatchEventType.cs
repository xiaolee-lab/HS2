using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D60 RID: 3424
	[TaskCategory("")]
	public class IsMatchEventType : AgentConditional
	{
		// Token: 0x06006C25 RID: 27685 RVA: 0x002E59E8 File Offset: 0x002E3DE8
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Partner != null)
			{
				if (!agent.TargetInSightActionPoint.AgentDateEventType.Contains(this._targetKey) || agent.EventKey != this._targetKey)
				{
					return TaskStatus.Failure;
				}
			}
			else if (!agent.TargetInSightActionPoint.AgentEventType.Contains(this._targetKey) || agent.EventKey != this._targetKey)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005AD7 RID: 23255
		[SerializeField]
		private EventType _targetKey;
	}
}
