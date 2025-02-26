using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CED RID: 3309
	[TaskCategory("")]
	public class PlayGameThere : AgentAction
	{
		// Token: 0x06006AAA RID: 27306 RVA: 0x002D8038 File Offset: 0x002D6438
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Play;
			base.OnStart();
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			List<PoseKeyPair> list = ListPool<PoseKeyPair>.Get();
			AgentProfile.PoseIDCollection poseIDTable = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable;
			list.AddRange(poseIDTable.PlayGameStandIDList);
			Weather weather = Singleton<Manager.Map>.Instance.Simulator.Weather;
			if (agent.AreaType == MapArea.AreaType.Normal)
			{
				list.AddRange(Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.PlayGameStandOutdoorIDList);
				if (weather == Weather.Cloud1 || weather == Weather.Cloud2)
				{
					list.Add(Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.ClearPoseID);
				}
			}
			PoseKeyPair element = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			ListPool<PoseKeyPair>.Release(list);
			agent.ActionID = element.postureID;
			agent.ActionID = element.poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[element.postureID][element.poseID];
			agent.Animation.RecoveryPoint = null;
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(element.postureID, element.poseID, playState);
			agent.LoadActionFlag(element.postureID, element.poseID);
			agent.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			agent.Animation.StopAllAnimCoroutine();
			agent.Animation.PlayInAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
			this._onEndActionDisposable = agent.AnimationAgent.OnEndActionAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				agent.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			});
			if (animInfo.hasAction)
			{
				this._onActionPlayDisposable = agent.AnimationAgent.OnActionPlayAsObservable().Subscribe(delegate(Unit _)
				{
					agent.Animation.PlayActionAnimation(animInfo.layer);
				});
			}
			this._onCompleteActionDisposable = agent.AnimationAgent.OnCompleteActionAsObservable().Subscribe(delegate(Unit _)
			{
				this.Complete();
			});
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, "その場で遊ぶ", animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x002D8314 File Offset: 0x002D6714
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x002D8328 File Offset: 0x002D6728
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
			AgentActor agent = base.Agent;
			agent.ChangeDynamicNavMeshAgentAvoidance();
			agent.SetActiveOnEquipedItem(true);
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x002D83A4 File Offset: 0x002D67A4
		private void Complete()
		{
			AgentActor agent = base.Agent;
			if (!agent.Animation.AnimInfo.outEnableBlend)
			{
				agent.Animation.CrossFadeScreen(-1f);
			}
			agent.Animation.RefsActAnimInfo = true;
			if (!this._unchangeParamState)
			{
				agent.UpdateStatus(agent.ActionID, agent.PoseID);
				int desireKey = Desire.GetDesireKey(Desire.Type.Game);
				agent.SetDesire(desireKey, 0f);
				agent.CauseSick();
				agent.AgentData.AddAppendEventFlagParam(3, 1);
			}
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.EventKey = (EventType)0;
			agent.Animation.EndStates();
		}

		// Token: 0x04005A25 RID: 23077
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005A26 RID: 23078
		[SerializeField]
		private bool _unchangeParamState;

		// Token: 0x04005A27 RID: 23079
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A28 RID: 23080
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A29 RID: 23081
		private IDisposable _onCompleteActionDisposable;
	}
}
