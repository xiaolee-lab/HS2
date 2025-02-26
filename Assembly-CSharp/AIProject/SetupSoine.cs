using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D1E RID: 3358
	[TaskCategory("")]
	public class SetupSoine : AgentAction
	{
		// Token: 0x06006B69 RID: 27497 RVA: 0x002E052C File Offset: 0x002DE92C
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			ActionPoint targetInSightActionPoint = agent.TargetInSightActionPoint;
			agent.CurrentPoint = targetInSightActionPoint;
			ActionPoint actionPoint = targetInSightActionPoint;
			actionPoint.SetSlot(agent);
			PoseKeyPair sleepTogetherRight = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SleepTogetherRight;
			PoseKeyPair sleepTogetherLeft = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SleepTogetherLeft;
			bool flag = false;
			ActionPointInfo actionPointInfo = default(ActionPointInfo);
			if (actionPoint != null)
			{
				flag = (actionPoint.FindAgentActionPointInfo(EventType.Sleep, sleepTogetherRight.poseID, out actionPointInfo) || actionPoint.FindAgentActionPointInfo(EventType.Sleep, sleepTogetherLeft.poseID, out actionPointInfo));
			}
			if (!flag)
			{
				agent.ChangeBehavior(Desire.ActionType.Normal);
				return TaskStatus.Failure;
			}
			GameObject gameObject = actionPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? actionPoint.transform;
			GameObject gameObject2 = actionPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			agent.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[actionPointInfo.eventID][actionPointInfo.poseID];
			ActorAnimInfo actorAnimInfo = agent.Animation.LoadActionState(actionPointInfo.eventID, actionPointInfo.poseID, playState);
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			agent.DisableActionFlag();
			agent.DeactivateNavMeshAgent();
			agent.IsKinematic = true;
			agent.Animation.PlayInAnimation(actorAnimInfo.inEnableBlend, actorAnimInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo.layer);
			agent.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			agent.SetCurrentSchedule(actorAnimInfo.isLoop, "添い寝", actorAnimInfo.loopMinTime, actorAnimInfo.loopMaxTime, actorAnimInfo.hasAction, true);
			agent.ChangeBehavior(Desire.ActionType.EndTaskSleepAfterDate);
			return TaskStatus.Success;
		}
	}
}
