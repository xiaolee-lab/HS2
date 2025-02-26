using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CC5 RID: 3269
	[TaskCategory("")]
	public class GetHurt : AgentAction
	{
		// Token: 0x060069E0 RID: 27104 RVA: 0x002D1458 File Offset: 0x002CF858
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			if (agent.AgentData.SickState.ID == -1)
			{
				return;
			}
			agent.StateType = State.Type.Immobility;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair standHurtID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.StandHurtID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[standHurtID.postureID][standHurtID.poseID];
			agent.Animation.LoadEventKeyTable(standHurtID.postureID, standHurtID.poseID);
			this._layer = playState.Layer;
			this._inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
			agent.Animation.InitializeStates(playState);
			agent.Animation.PlayInAnimation(this._inEnableFade, this._inFadeSecond, playState.MainStateInfo.FadeOutTime, this._layer);
		}

		// Token: 0x060069E1 RID: 27105 RVA: 0x002D1560 File Offset: 0x002CF960
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.AgentData.SickState.ID == -1)
			{
				return TaskStatus.Success;
			}
			if (agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			agent.AgentData.SickState.ID = 4;
			int num = UnityEngine.Random.Range(0, (int)TimeSpan.FromDays(2.0).TotalHours);
			agent.AgentData.SickState.Duration = agent.AgentData.SickState.ElapsedTime + TimeSpan.FromDays(2.0) + TimeSpan.FromHours((double)num);
			return TaskStatus.Success;
		}

		// Token: 0x060069E2 RID: 27106 RVA: 0x002D160D File Offset: 0x002CFA0D
		public override void OnEnd()
		{
			base.Agent.StateType = State.Type.Normal;
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x040059C4 RID: 22980
		private int _layer = -1;

		// Token: 0x040059C5 RID: 22981
		private bool _inEnableFade;

		// Token: 0x040059C6 RID: 22982
		private float _inFadeSecond;
	}
}
