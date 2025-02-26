using System;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CB7 RID: 3255
	[TaskCategory("")]
	public class ComaWeakness : AgentAction
	{
		// Token: 0x060069A0 RID: 27040 RVA: 0x002CFA44 File Offset: 0x002CDE44
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			if (agent.CurrentPoint == null)
			{
				agent.CurrentPoint = agent.TargetInSightActionPoint;
			}
			agent.DeactivateNavMeshAgent();
			PoseKeyPair comaID = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.ComaID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[comaID.postureID][comaID.poseID];
			agent.Animation.LoadEventKeyTable(comaID.postureID, comaID.poseID);
			agent.Animation.InitializeStates(playState);
			agent.LoadActionFlag(comaID.postureID, comaID.poseID);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate,
				isLoop = playState.MainStateInfo.IsLoop,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction,
				loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName,
				randomCount = playState.ActionInfo.randomCount,
				oldNormalizedTime = 0f
			};
			agent.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			agent.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.inFadeOutTime, actorAnimInfo2.layer);
			ActionPointInfo actionPointInfo;
			agent.CurrentPoint.TryGetAgentActionPointInfo(EventType.Sleep, out actionPointInfo);
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			agent.SetCurrentSchedule(true, actionPointInfo.actionName, 175, 185, false, false);
			if (agent.AgentData.ScheduleEnabled)
			{
				Actor.BehaviorSchedule schedule = agent.Schedule;
				schedule.enabled = agent.AgentData.ScheduleEnabled;
				schedule.elapsedTime = agent.AgentData.ScheduleElapsedTime;
				schedule.duration = agent.AgentData.ScheduleDuration;
				agent.Schedule = schedule;
				agent.AgentData.ScheduleEnabled = false;
			}
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x002CFDAC File Offset: 0x002CE1AC
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			if (agent.Schedule.enabled)
			{
				return TaskStatus.Running;
			}
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x002CFDF0 File Offset: 0x002CE1F0
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.ApplySituationResultParameter(36);
			agent.ClearItems();
			agent.ClearParticles();
			agent.Animation.EndStates();
			agent.ResetActionFlag();
			agent.SetDefaultStateHousingItem();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.EventKey = (EventType)0;
			agent.PrevActionPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = null;
		}
	}
}
