using System;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000CAB RID: 3243
	[TaskCategory("")]
	public class StorageIn : AgentAction
	{
		// Token: 0x06006969 RID: 26985 RVA: 0x002CDE68 File Offset: 0x002CC268
		public override void OnStart()
		{
			StorageIn.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey = new StorageIn.<OnStart>c__AnonStorey0();
			<OnStart>c__AnonStorey.$this = this;
			<OnStart>c__AnonStorey.agent = base.Agent;
			<OnStart>c__AnonStorey.agent.EventKey = EventType.StorageIn;
			<OnStart>c__AnonStorey.agent.CurrentPoint = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint;
			<OnStart>c__AnonStorey.agent.SetActiveOnEquipedItem(false);
			<OnStart>c__AnonStorey.agent.ChaControl.setAllLayerWeight(0f);
			<OnStart>c__AnonStorey.agent.ElectNextPoint();
			UnityEx.ValueTuple<int, string> valueTuple;
			AIProject.Definitions.Action.NameTable.TryGetValue(<OnStart>c__AnonStorey.agent.EventKey, out valueTuple);
			<OnStart>c__AnonStorey.agent.ActionID = valueTuple.Item1;
			ActionPointInfo actionPointInfo = <OnStart>c__AnonStorey.agent.TargetInSightActionPoint.GetActionPointInfo(<OnStart>c__AnonStorey.agent);
			<OnStart>c__AnonStorey.agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnStart>c__AnonStorey.agent.CurrentPoint.transform;
			GameObject gameObject2 = <OnStart>c__AnonStorey.agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			<OnStart>c__AnonStorey.agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			<OnStart>c__AnonStorey.agent.PoseID = actionPointInfo2.poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[<OnStart>c__AnonStorey.agent.ActionID][actionPointInfo2.poseID];
			<OnStart>c__AnonStorey.agent.Animation.LoadEventKeyTable(<OnStart>c__AnonStorey.agent.ActionID, actionPointInfo2.poseID);
			<OnStart>c__AnonStorey.agent.LoadEventItems(playState);
			<OnStart>c__AnonStorey.agent.LoadEventParticles(<OnStart>c__AnonStorey.agent.ActionID, actionPointInfo2.poseID);
			<OnStart>c__AnonStorey.agent.Animation.InitializeStates(playState);
			StorageIn.<OnStart>c__AnonStorey0 <OnStart>c__AnonStorey2 = <OnStart>c__AnonStorey;
			ActorAnimInfo animInfo = new ActorAnimInfo
			{
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				inFadeOutTime = playState.MainStateInfo.FadeOutTime,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate,
				isLoop = playState.MainStateInfo.IsLoop,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction,
				layer = playState.MainStateInfo.InStateInfo.StateInfos[0].layer
			};
			<OnStart>c__AnonStorey.agent.Animation.AnimInfo = animInfo;
			<OnStart>c__AnonStorey2.animInfo = animInfo;
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			<OnStart>c__AnonStorey.agent.LoadActionFlag(actionPointInfo2.eventID, actionPointInfo2.poseID);
			<OnStart>c__AnonStorey.agent.DeactivateNavMeshAgent();
			this._onStartChestAction.Take(1).Subscribe(delegate(Unit _)
			{
				if (<OnStart>c__AnonStorey.$this._chestAnimation != null)
				{
					<OnStart>c__AnonStorey.$this._chestAnimation.PlayInAnimation();
				}
			});
			this._onStartAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnStart>c__AnonStorey.agent.Animation.PlayInAnimation(<OnStart>c__AnonStorey.animInfo.inEnableBlend, <OnStart>c__AnonStorey.animInfo.inBlendSec, <OnStart>c__AnonStorey.animInfo.inFadeOutTime, <OnStart>c__AnonStorey.animInfo.layer);
			});
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnStart>c__AnonStorey.agent.Animation.StopAllAnimCoroutine();
				<OnStart>c__AnonStorey.agent.Animation.PlayOutAnimation(<OnStart>c__AnonStorey.animInfo.outEnableBlend, <OnStart>c__AnonStorey.animInfo.outBlendSec, <OnStart>c__AnonStorey.animInfo.layer);
			});
			if (<OnStart>c__AnonStorey.animInfo.hasAction)
			{
				this._onActionPlay.Take(1).Subscribe(delegate(Unit _)
				{
					<OnStart>c__AnonStorey.agent.Animation.PlayActionAnimation(<OnStart>c__AnonStorey.animInfo.layer);
				});
			}
			<OnStart>c__AnonStorey.agent.CurrentPoint.SetSlot(<OnStart>c__AnonStorey.agent);
			<OnStart>c__AnonStorey.agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			if (<OnStart>c__AnonStorey.animInfo.isLoop)
			{
				base.Agent.SetCurrentSchedule(<OnStart>c__AnonStorey.animInfo.isLoop, valueTuple.Item2, <OnStart>c__AnonStorey.animInfo.loopMinTime, <OnStart>c__AnonStorey.animInfo.loopMaxTime, <OnStart>c__AnonStorey.animInfo.hasAction, false);
			}
			if (base.Agent.CurrentPoint != null)
			{
				this._chestAnimation = base.Agent.CurrentPoint.GetComponent<ChestAnimation>();
			}
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x002CE30C File Offset: 0x002CC70C
		public override TaskStatus OnUpdate()
		{
			if (this._chestAnimation != null && this._chestAnimation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			if (this._onStartChestAction != null)
			{
				this._onStartChestAction.OnNext(Unit.Default);
			}
			if (this._chestAnimation != null && this._chestAnimation.PlayingInAniamtion)
			{
				return TaskStatus.Running;
			}
			if (this._onStartAction != null)
			{
				this._onStartAction.OnNext(Unit.Default);
			}
			if (base.Agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			if (base.Agent.Animation.AnimInfo.isLoop)
			{
				if (base.Agent.Animation.PlayingActAnimation)
				{
					return TaskStatus.Running;
				}
				if (base.Agent.Schedule.enabled)
				{
					return TaskStatus.Running;
				}
				if (this._onEndAction != null)
				{
					this._onEndAction.OnNext(Unit.Default);
				}
				if (base.Agent.Animation.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				this.Complete();
				return TaskStatus.Success;
			}
			else
			{
				if (this._onEndAction != null)
				{
					this._onEndAction.OnNext(Unit.Default);
				}
				if (base.Agent.Animation.PlayingOutAnimation)
				{
					return TaskStatus.Running;
				}
				this.Complete();
				return TaskStatus.Success;
			}
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x002CE47C File Offset: 0x002CC87C
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.CauseSick();
			this.OnCompletedStateTask();
			base.Agent.ActivateNavMeshAgent();
			base.Agent.SetActiveOnEquipedItem(true);
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x002CE528 File Offset: 0x002CC928
		private void OnCompletedStateTask()
		{
			if (this._chestAnimation != null)
			{
				this._chestAnimation.PlayOutAnimation();
			}
			AgentActor agent = base.Agent;
			foreach (StuffItem item in agent.AgentData.ItemList)
			{
				Singleton<Game>.Instance.WorldData.Environment.ItemListInStorage.AddItem(item);
			}
			agent.AgentData.ItemList.Clear();
			agent.AgentData.SetAppendEventFlagCheck(0, true);
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x002CE5E0 File Offset: 0x002CC9E0
		public override void OnEnd()
		{
			base.OnEnd();
		}

		// Token: 0x040059A7 RID: 22951
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x040059A8 RID: 22952
		private Subject<Unit> _onStartChestAction = new Subject<Unit>();

		// Token: 0x040059A9 RID: 22953
		private Subject<Unit> _onStartAction = new Subject<Unit>();

		// Token: 0x040059AA RID: 22954
		protected Subject<Unit> _onActionPlay = new Subject<Unit>();

		// Token: 0x040059AB RID: 22955
		protected Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x040059AC RID: 22956
		private Subject<Unit> _onEndChestAction = new Subject<Unit>();

		// Token: 0x040059AD RID: 22957
		private ChestAnimation _chestAnimation;
	}
}
