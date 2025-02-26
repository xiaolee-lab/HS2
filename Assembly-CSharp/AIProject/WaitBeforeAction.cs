using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D29 RID: 3369
	[TaskCategory("")]
	public class WaitBeforeAction : AgentAction
	{
		// Token: 0x06006B97 RID: 27543 RVA: 0x002E1FEC File Offset: 0x002E03EC
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			int parameterHash = Animator.StringToHash("Forward");
			agent.Animation.SetFloat(parameterHash, 0f);
		}

		// Token: 0x06006B98 RID: 27544 RVA: 0x002E202E File Offset: 0x002E042E
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x06006B99 RID: 27545 RVA: 0x002E203B File Offset: 0x002E043B
		public override TaskStatus OnUpdate()
		{
			this._elapsedTime += Time.deltaTime;
			if (this._elapsedTime > this._durationTime)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x04005A98 RID: 23192
		[SerializeField]
		private float _durationTime;

		// Token: 0x04005A99 RID: 23193
		private float _elapsedTime;
	}
}
