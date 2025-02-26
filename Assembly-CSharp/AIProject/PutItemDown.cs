using System;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CDC RID: 3292
	[TaskCategory("")]
	public class PutItemDown : AgentAction
	{
		// Token: 0x06006A6C RID: 27244 RVA: 0x002D5744 File Offset: 0x002D3B44
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			agent.SetActiveOnEquipedItem(false);
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.ChaControl.setAllLayerWeight(0f);
			agent.EventKey = EventType.PutItem;
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
			ActorAnimInfo animInfo = agent.Animation.LoadActionState(num2, num3, playState);
			agent.LoadActionFlag(num2, num3);
			agent.DeactivateNavMeshAgent();
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

		// Token: 0x06006A6D RID: 27245 RVA: 0x002D5A64 File Offset: 0x002D3E64
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
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x002D5ABF File Offset: 0x002D3EBF
		public override TaskStatus OnUpdate()
		{
			return base.Agent.AnimationAgent.OnUpdateActionState();
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x002D5AD4 File Offset: 0x002D3ED4
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.ActivateNavMeshAgent();
			agent.Animation.EndStates();
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			agent.CurrentPoint.SetActiveMapItemObjs(true);
			agent.ReleaseEquipedEventItem();
			agent.EventKey = (EventType)0;
		}

		// Token: 0x040059FF RID: 23039
		private IDisposable _onActionPlayDisposable;

		// Token: 0x04005A00 RID: 23040
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005A01 RID: 23041
		private IDisposable _onCompleteActionDisposable;
	}
}
