using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000D9A RID: 3482
	public abstract class AgentStateAction : AgentAction
	{
		// Token: 0x06006CA6 RID: 27814 RVA: 0x002CACF4 File Offset: 0x002C90F4
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.ElectNextPoint();
			if (agent.CurrentPoint == null)
			{
				return;
			}
			agent.CurrentPoint.SetActiveMapItemObjs(false);
			ActionPointInfo actionPointInfo = agent.TargetInSightActionPoint.GetActionPointInfo(agent);
			agent.Animation.ActionPointInfo = actionPointInfo;
			ActionPointInfo actionPointInfo2 = actionPointInfo;
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo2.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			int num = actionPointInfo2.eventID;
			agent.ActionID = num;
			int num2 = num;
			num = actionPointInfo2.poseID;
			agent.PoseID = num;
			int num3 = num;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num3];
			agent.CurrentPoint.DestroyByproduct(num2, num3);
			UnityEx.ValueTuple<int, string> valueTuple;
			if (AIProject.Definitions.Action.NameTable.TryGetValue(agent.EventKey, out valueTuple))
			{
				int item = valueTuple.Item1;
				if (item != agent.ActionID)
				{
					return;
				}
			}
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(num2, num3, playState);
			agent.LoadActionFlag(num2, num3);
			agent.DeactivateNavMeshAgent();
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
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			if (animInfo.isLoop)
			{
				agent.SetCurrentSchedule(animInfo.isLoop, actionPointInfo2.actionName, animInfo.loopMinTime, animInfo.loopMaxTime, animInfo.hasAction, false);
			}
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x002CB084 File Offset: 0x002C9484
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			UnityEx.ValueTuple<int, string> valueTuple;
			if (AIProject.Definitions.Action.NameTable.TryGetValue(agent.EventKey, out valueTuple))
			{
				int item = valueTuple.Item1;
				if (item != agent.ActionID)
				{
					agent.ChangeBehavior(Desire.ActionType.Normal);
					return TaskStatus.Failure;
				}
			}
			if (agent.CurrentPoint == null)
			{
				return TaskStatus.Success;
			}
			return agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x002CB0EC File Offset: 0x002C94EC
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
			agent.SetActiveOnEquipedItem(true);
			agent.ClearItems();
			agent.ClearParticles();
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x002CB164 File Offset: 0x002C9564
		protected virtual void Complete()
		{
			AgentActor agent = base.Agent;
			if (!this._unchangeParamState)
			{
				agent.UpdateStatus(agent.ActionID, agent.PoseID);
				agent.CauseSick();
			}
			this.OnCompletedStateTask();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.CreateByproduct(agent.ActionID, agent.PoseID);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
			agent.ActivateNavMeshAgent();
			agent.Animation.EndStates();
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x002CB22B File Offset: 0x002C962B
		protected virtual void OnCompletedStateTask()
		{
		}

		// Token: 0x04005AF2 RID: 23282
		[SerializeField]
		private State.Type _stateType;

		// Token: 0x04005AF3 RID: 23283
		[SerializeField]
		protected bool _unchangeParamState;

		// Token: 0x04005AF4 RID: 23284
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005AF5 RID: 23285
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005AF6 RID: 23286
		private IDisposable _onCompleteActionDisposable;
	}
}
