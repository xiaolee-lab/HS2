using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000C91 RID: 3217
	[TaskCategory("")]
	public class DateTurn : AgentAction
	{
		// Token: 0x06006904 RID: 26884 RVA: 0x002CA734 File Offset: 0x002C8B34
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			if (agent.Partner != null)
			{
				agent.StopNavMeshAgent();
				agent.ChangeStaticNavMeshAgentAvoidance();
				PlayState.AnimStateInfo idleStateInfo = Singleton<Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
				float angleFromForward = base.Agent.Partner.Animation.GetAngleFromForward(agent.transform.forward);
				agent.Animation.PlayTurnAnimation(angleFromForward, 1f, idleStateInfo);
			}
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x002CA7B3 File Offset: 0x002C8BB3
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingTurnAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x002CA7D0 File Offset: 0x002C8BD0
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.Animation.StopTurnAnimCoroutine();
			agent.ChangeDynamicNavMeshAgentAvoidance();
		}
	}
}
