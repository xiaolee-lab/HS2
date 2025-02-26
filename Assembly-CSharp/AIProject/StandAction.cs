using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CE5 RID: 3301
	[TaskCategory("")]
	public class StandAction : AgentAction
	{
		// Token: 0x06006A86 RID: 27270 RVA: 0x002D63A8 File Offset: 0x002D47A8
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.ElectNextPoint();
			this._poseInfo = null;
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				return;
			}
			if (agent.PrevMode == Desire.ActionType.Encounter)
			{
				return;
			}
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			AgentProfile agentProfile = Singleton<Manager.Resources>.Instance.AgentProfile;
			float num = agent.AgentData.StatsTable[0];
			float num2 = agent.AgentData.StatsTable[2];
			int desireKey = Desire.GetDesireKey(Desire.Type.Bath);
			float? desire = agent.GetDesire(desireKey);
			int desireKey2 = Desire.GetDesireKey(Desire.Type.Sleep);
			float? desire2 = agent.GetDesire(desireKey2);
			if (agent.AgentData.SickState.ID == 0)
			{
				this._poseInfo = new PoseKeyPair?(agentProfile.PoseIDTable.CoughID);
			}
			else if (desire2 >= 70f)
			{
				this._poseInfo = new PoseKeyPair?(agentProfile.PoseIDTable.YawnID);
			}
			else if (desire >= 70f)
			{
				this._poseInfo = new PoseKeyPair?(agentProfile.PoseIDTable.GrossID);
			}
			else if (num <= agentProfile.ColdTempBorder)
			{
				this._poseInfo = new PoseKeyPair?(agentProfile.PoseIDTable.ColdPoseID);
			}
			else if (num >= agentProfile.HotTempBorder)
			{
				this._poseInfo = new PoseKeyPair?(agentProfile.PoseIDTable.HotPoseID);
			}
			else if (num2 <= 0f)
			{
				this._poseInfo = new PoseKeyPair?(agentProfile.PoseIDTable.HungryID);
			}
			if (this._poseInfo != null)
			{
				PoseKeyPair value = this._poseInfo.Value;
				agent.ActionID = value.postureID;
				agent.PoseID = value.poseID;
				PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[value.postureID][value.poseID];
				ActorAnimInfo animInfo = agent.Animation.LoadActionState(value.postureID, value.poseID, playState);
				agent.LoadActionFlag(value.postureID, value.poseID);
				agent.DeactivateNavMeshAgent();
				agent.Animation.RecoveryPoint = null;
				agent.Animation.StopAllAnimCoroutine();
				agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
				this._onEndActionDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
				{
					agent.Animation.StopAllAnimCoroutine();
					agent.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
				});
				if (animInfo.hasAction)
				{
					this._onActionPlayDisposable = agent.AnimationAgent.OnActionPlayAsObservable().Subscribe(delegate(Unit _)
					{
						this.Agent.Animation.PlayActionAnimation(animInfo.layer);
					});
				}
				this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Take(1).Subscribe(delegate(Unit _)
				{
					this.Complete();
				});
				if (animInfo.isLoop)
				{
					agent.SetCurrentSchedule(animInfo.isLoop, "立ちアクション", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
				}
			}
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x002D67C9 File Offset: 0x002D4BC9
		public override TaskStatus OnUpdate()
		{
			if (this._poseInfo == null)
			{
				return TaskStatus.Success;
			}
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x002D67F0 File Offset: 0x002D4BF0
		public override void OnEnd()
		{
			base.OnEnd();
			if (this._onEndActionDisposable != null)
			{
				this._onEndActionDisposable.Dispose();
			}
			if (this._onActionPlayDisposable != null)
			{
				this._onActionPlayDisposable.Dispose();
			}
			if (this._onCompleteActionDisposable != null)
			{
				this._onCompleteActionDisposable.Dispose();
			}
			base.Agent.ActivateNavMeshAgent();
			base.Agent.SetActiveOnEquipedItem(true);
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x002D6864 File Offset: 0x002D4C64
		private void Complete()
		{
			AgentActor agent = base.Agent;
			Actor actor = agent;
			int num = -1;
			agent.PoseID = num;
			actor.ActionID = num;
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.EventKey = (EventType)0;
		}

		// Token: 0x04005A10 RID: 23056
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A11 RID: 23057
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A12 RID: 23058
		private IDisposable _onCompleteActionDisposable;

		// Token: 0x04005A13 RID: 23059
		private PoseKeyPair? _poseInfo;
	}
}
