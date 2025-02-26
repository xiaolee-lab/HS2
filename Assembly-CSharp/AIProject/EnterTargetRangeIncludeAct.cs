using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D37 RID: 3383
	[TaskCategory("")]
	public class EnterTargetRangeIncludeAct : AgentConditional
	{
		// Token: 0x06006BC8 RID: 27592 RVA: 0x002E3E40 File Offset: 0x002E2240
		public override void OnStart()
		{
			base.OnStart();
			this._arrivedDistance = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistanceIncludeAct;
			this._acceptableHeight = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.acceptableHeightIncludeAct;
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x002E3E90 File Offset: 0x002E2290
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			Actor targetInSightActor = agent.TargetInSightActor;
			Vector3 position = agent.Position;
			position.y = 0f;
			Vector3 position2 = targetInSightActor.Position;
			Vector3 b = position2;
			b.y = 0f;
			float num = Vector3.Distance(position, b);
			float num2 = Mathf.Abs(agent.Position.y - targetInSightActor.Position.y);
			if (targetInSightActor != null && num <= this._arrivedDistance && num2 <= this._acceptableHeight)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AB4 RID: 23220
		private float _arrivedDistance;

		// Token: 0x04005AB5 RID: 23221
		private float _acceptableHeight;
	}
}
