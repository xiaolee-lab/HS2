using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D27 RID: 3367
	[TaskCategory("")]
	public class Turn : AgentAction
	{
		// Token: 0x06006B8F RID: 27535 RVA: 0x002E1D80 File Offset: 0x002E0180
		public override void OnStart()
		{
			base.OnStart();
			if (base.Agent.CommandPartner != null)
			{
				base.Agent.StopNavMeshAgent();
				PlayState.AnimStateInfo idleStateInfo = Singleton<Resources>.Instance.DefinePack.AnimatorState.IdleStateInfo;
				base.Agent.Animation.PlayTurnAnimation(base.Agent.CommandPartner.Position, 1f, idleStateInfo, false);
			}
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x002E1DF0 File Offset: 0x002E01F0
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingTurnAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x002E1E0A File Offset: 0x002E020A
		public override void OnEnd()
		{
			base.Agent.Animation.StopTurnAnimCoroutine();
		}
	}
}
