using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB9 RID: 3257
	[TaskCategory("")]
	public class Commun : AgentAction
	{
		// Token: 0x060069B0 RID: 27056 RVA: 0x002D0180 File Offset: 0x002CE580
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Commun;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			Transform trfTarg = player.FovTargetPointTable[Actor.FovBodyPart.Head];
			agent.ChaControl.ChangeLookEyesTarget(1, trfTarg, 0.5f, 0f, 1f, 2f);
			agent.ChaControl.ChangeLookEyesPtn(1);
			agent.ChaControl.ChangeLookNeckTarget(1, trfTarg, 0.5f, 0f, 1f, 0.8f);
			agent.ChaControl.ChangeLookNeckPtn(1, 1f);
			agent.MotivationInEncounter = agent.AgentData.StatsTable[5];
			agent.UpdateMotivation = true;
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x002D0244 File Offset: 0x002CE644
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.MotivationInEncounter <= 0f)
			{
				return TaskStatus.Success;
			}
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			if (agent.ReleasableCommand && agent.IsFarPlayer)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x002D0290 File Offset: 0x002CE690
		public override void OnEnd()
		{
			base.OnEnd();
			AgentActor agent = base.Agent;
			agent.ChaControl.ChangeLookEyesPtn(0);
			agent.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			agent.ChaControl.ChangeLookNeckPtn(3, 1f);
			agent.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			agent.ChangeDynamicNavMeshAgentAvoidance();
			agent.UpdateMotivation = false;
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x002D0318 File Offset: 0x002CE718
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				this._updatedMotivation = paused;
				agent.UpdateMotivation = false;
			}
			else
			{
				agent.UpdateMotivation = this._updatedMotivation;
			}
		}

		// Token: 0x040059B5 RID: 22965
		private bool _updatedMotivation;
	}
}
