using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000C8F RID: 3215
	[TaskCategory("")]
	public class Collapse : AgentAction
	{
		// Token: 0x060068FD RID: 26877 RVA: 0x002CA550 File Offset: 0x002C8950
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.StateType = State.Type.Collapse;
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			PoseKeyPair collapseID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.CollapseID;
			PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[collapseID.postureID][collapseID.poseID];
			agent.Animation.LoadEventKeyTable(collapseID.postureID, collapseID.poseID);
			this._layer = playState.Layer;
			this._inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
			base.Agent.Animation.InitializeStates(playState);
			base.Agent.Animation.PlayInAnimation(this._inEnableFade, this._inFadeSecond, playState.MainStateInfo.FadeOutTime, this._layer);
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x002CA64A File Offset: 0x002C8A4A
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x002CA664 File Offset: 0x002C8A64
		public override void OnEnd()
		{
		}

		// Token: 0x04005984 RID: 22916
		private int _layer = -1;

		// Token: 0x04005985 RID: 22917
		protected bool _inEnableFade;

		// Token: 0x04005986 RID: 22918
		protected float _inFadeSecond;

		// Token: 0x04005987 RID: 22919
		protected bool _outEnableFade;

		// Token: 0x04005988 RID: 22920
		protected float _outFadeSecond;
	}
}
