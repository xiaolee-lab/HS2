using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D36 RID: 3382
	[TaskCategory("")]
	public class EnterTargetRange : AgentConditional
	{
		// Token: 0x06006BC5 RID: 27589 RVA: 0x002E3D38 File Offset: 0x002E2138
		public override void OnStart()
		{
			base.OnStart();
			this._arrivedDistance = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
			this._acceptableHeight = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.acceptableHeight;
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x002E3D88 File Offset: 0x002E2188
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Actor targetInSightActor = agent.TargetInSightActor;
			if (targetInSightActor == null)
			{
				agent.ChangeBehavior(Desire.ActionType.Normal);
				return TaskStatus.Failure;
			}
			Vector3 position = agent.Position;
			position.y = 0f;
			Vector3 position2 = targetInSightActor.Position;
			position2.y = 0f;
			float num = Vector3.Distance(position, position2);
			float num2 = Mathf.Abs(agent.Position.y - targetInSightActor.Position.y);
			if (targetInSightActor != null && num <= this._arrivedDistance && num2 <= this._acceptableHeight)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AB2 RID: 23218
		private float _arrivedDistance;

		// Token: 0x04005AB3 RID: 23219
		private float _acceptableHeight;
	}
}
