using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CC2 RID: 3266
	public class GetEmotion : AgentAction
	{
		// Token: 0x060069D7 RID: 27095 RVA: 0x002D1304 File Offset: 0x002CF704
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			string empty = string.Empty;
			int stateHashName = Animator.StringToHash(empty);
			agent.Animation.Animator.CrossFadeInFixedTime(stateHashName, 0.1f, 0, 0.1f, 0f);
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x002D1358 File Offset: 0x002CF758
		public override TaskStatus OnUpdate()
		{
			this._elapsedTime += Time.deltaTime;
			if (this._elapsedTime < this._duration)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x002D1380 File Offset: 0x002CF780
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x040059C2 RID: 22978
		private float _duration = 5f;

		// Token: 0x040059C3 RID: 22979
		private float _elapsedTime;
	}
}
