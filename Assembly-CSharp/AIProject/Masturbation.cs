using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000CA1 RID: 3233
	[TaskCategory("")]
	public class Masturbation : AgentAction
	{
		// Token: 0x06006938 RID: 26936 RVA: 0x002CBEE0 File Offset: 0x002CA2E0
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Masturbation;
			base.OnStart();
			agent.CurrentPoint = base.Agent.TargetInSightActionPoint;
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
			agent.LoadActionFlag(num2, num3);
			agent.DeactivateNavMeshAgent();
			agent.Animation.StopAllAnimCoroutine();
			HScene.AnimationListInfo info = Singleton<Manager.Resources>.Instance.HSceneTable.lstAnimInfo[3][actionPointInfo2.poseID];
			agent.Animation.BeginIgnoreEvent();
			AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
			RuntimeAnimatorController animatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo.assetbundle, assetBundleInfo.asset, string.Empty);
			agent.Animation.SetAnimatorController(animatorController);
			agent.StartMasturbationSequence(info);
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(t, false, 0f, 0);
			agent.SurprisePoseID = new PoseKeyPair?(Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SurpriseMasturbationID);
			agent.UpdateMotivation = true;
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x002CC0EA File Offset: 0x002CA4EA
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.PlayingMasturbationSequence)
			{
				return TaskStatus.Running;
			}
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x0600693A RID: 26938 RVA: 0x002CC108 File Offset: 0x002CA508
		public override void OnEnd()
		{
			base.OnEnd();
			AgentActor agent = base.Agent;
			agent.StopMasturbationSequence();
			agent.Animation.EndIgnoreEvent();
			agent.UpdateMotivation = false;
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x002CC13C File Offset: 0x002CA53C
		private void Complete()
		{
			AgentActor agent = base.Agent;
			agent.Animation.CrossFadeScreen(-1f);
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			agent.SetStand(agent.Animation.RecoveryPoint, false, 0f, 0);
			agent.Animation.RecoveryPoint = null;
			int desireKey = Desire.GetDesireKey(Desire.Type.H);
			agent.SetDesire(desireKey, 0f);
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			agent.ApplySituationResultParameter(31);
			agent.ClearItems();
			agent.ClearParticles();
			agent.ResetActionFlag();
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
				agent.PrevActionPoint = agent.TargetInSightActionPoint;
			}
			agent.TargetInSightActionPoint = null;
			agent.SurprisePoseID = null;
			agent.Animation.EndIgnoreEvent();
			agent.Animation.ResetDefaultAnimatorController();
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x002CC240 File Offset: 0x002CA640
		public override void OnPause(bool paused)
		{
			AgentActor agent = base.Agent;
			if (paused)
			{
				this._updatedMotivation = paused;
				agent.UpdateMotivation = false;
			}
			else
			{
				agent.UpdateMotivation = this._updatedMotivation;
			}
		}

		// Token: 0x04005994 RID: 22932
		private bool _updatedMotivation;
	}
}
