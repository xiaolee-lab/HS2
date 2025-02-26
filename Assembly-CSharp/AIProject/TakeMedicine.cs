using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D25 RID: 3365
	[TaskCategory("")]
	public class TakeMedicine : AgentAction
	{
		// Token: 0x06006B8A RID: 27530 RVA: 0x002E1C6C File Offset: 0x002E006C
		public override void OnStart()
		{
			base.OnStart();
			PoseKeyPair medicID = Singleton<Resources>.Instance.AgentProfile.PoseIDTable.MedicID;
			PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[medicID.postureID][medicID.poseID];
			AgentActor agent = base.Agent;
			agent.Animation.LoadEventKeyTable(medicID.postureID, medicID.poseID);
			this._layer = playState.Layer;
			this._inEnableFade = playState.MainStateInfo.InStateInfo.EnableFade;
			this._inFadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
			base.Agent.Animation.InitializeStates(playState);
			base.Agent.Animation.PlayInAnimation(this._inEnableFade, this._inFadeSecond, playState.MainStateInfo.FadeOutTime, this._layer);
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x002E1D52 File Offset: 0x002E0152
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005A8F RID: 23183
		private int _layer = -1;

		// Token: 0x04005A90 RID: 23184
		private bool _inEnableFade;

		// Token: 0x04005A91 RID: 23185
		private float _inFadeSecond;
	}
}
