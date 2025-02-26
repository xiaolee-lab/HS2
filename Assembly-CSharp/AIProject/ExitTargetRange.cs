using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D3F RID: 3391
	[TaskCategory("")]
	public class ExitTargetRange : AgentConditional
	{
		// Token: 0x06006BDF RID: 27615 RVA: 0x002E4CB0 File Offset: 0x002E30B0
		public override void OnStart()
		{
			base.OnStart();
			this._distance = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.leaveDistance;
		}

		// Token: 0x06006BE0 RID: 27616 RVA: 0x002E4CE0 File Offset: 0x002E30E0
		public override TaskStatus OnUpdate()
		{
			Actor targetInSightActor = base.Agent.TargetInSightActor;
			if (targetInSightActor != null && Vector3.Distance(base.Agent.Position, targetInSightActor.Position) > this._distance)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ABE RID: 23230
		private float _distance;
	}
}
