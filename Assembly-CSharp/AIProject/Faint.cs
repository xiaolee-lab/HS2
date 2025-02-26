using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D00 RID: 3328
	[TaskCategory("")]
	public class Faint : AgentAction
	{
		// Token: 0x06006AF4 RID: 27380 RVA: 0x002DB518 File Offset: 0x002D9918
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Immobility;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair faintID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.FaintID;
			PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[faintID.postureID][faintID.poseID];
			agent.Animation.LoadEventKeyTable(faintID.postureID, faintID.poseID);
			this._layer = playState.Layer;
			this._inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
			agent.Animation.InitializeStates(playState);
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(this._inEnableFade, this._inFadeSecond, playState.MainStateInfo.FadeOutTime, this._layer);
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x002DB612 File Offset: 0x002D9A12
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x002DB62C File Offset: 0x002D9A2C
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
		}

		// Token: 0x04005A48 RID: 23112
		private int _layer = -1;

		// Token: 0x04005A49 RID: 23113
		protected bool _inEnableFade;

		// Token: 0x04005A4A RID: 23114
		protected float _inFadeSecond;

		// Token: 0x04005A4B RID: 23115
		protected bool _outEnableFade;

		// Token: 0x04005A4C RID: 23116
		protected float _outFadeSecond;
	}
}
