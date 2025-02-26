using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000CD6 RID: 3286
	[TaskCategory("")]
	public class Lesbian : AgentAction
	{
		// Token: 0x06006A41 RID: 27201 RVA: 0x002D30E0 File Offset: 0x002D14E0
		public override void OnStart()
		{
			base.OnStart();
			this._isReleased = true;
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Lesbian;
			agent.CurrentPoint = agent.TargetInSightActionPoint;
			agent.SetActiveOnEquipedItem(false);
			agent.ChaControl.setAllLayerWeight(0f);
			Actor partner = agent.Partner;
			if (partner == null)
			{
				return;
			}
			agent.DeactivateNavMeshAgent();
			agent.IsKinematic = true;
			partner.SetActiveOnEquipedItem(false);
			partner.ChaControl.setAllLayerWeight(0f);
			partner.DeactivateNavMeshAgent();
			partner.IsKinematic = true;
			DateActionPointInfo dateActionPointInfo = agent.TargetInSightActionPoint.GetDateActionPointInfo(agent);
			agent.Animation.DateActionPointInfo = dateActionPointInfo;
			DateActionPointInfo dateActionPointInfo2 = dateActionPointInfo;
			GameObject gameObject = agent.CurrentPoint.transform.FindLoop(dateActionPointInfo2.baseNullNameA);
			Transform basePointA = ((gameObject != null) ? gameObject.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject2 = agent.CurrentPoint.transform.FindLoop(dateActionPointInfo2.recoveryNullNameA);
			agent.Animation.RecoveryPoint = (((gameObject2 != null) ? gameObject2.transform : null) ?? agent.CurrentPoint.transform);
			GameObject gameObject3 = agent.CurrentPoint.transform.FindLoop(dateActionPointInfo2.baseNullNameB);
			Transform basePointB = ((gameObject3 != null) ? gameObject3.transform : null) ?? agent.CurrentPoint.transform;
			GameObject gameObject4 = agent.CurrentPoint.transform.FindLoop(dateActionPointInfo2.recoveryNullNameB);
			partner.Animation.RecoveryPoint = (((gameObject4 != null) ? gameObject4.transform : null) ?? agent.CurrentPoint.transform);
			int num = dateActionPointInfo2.eventID;
			agent.ActionID = num;
			int num2 = num;
			num = dateActionPointInfo2.poseIDA;
			agent.PoseID = num;
			int num3 = num;
			num = dateActionPointInfo2.poseIDB;
			partner.PoseID = num;
			int num4 = num;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num3];
			agent.LoadActionFlag(num2, num3);
			if (partner is AgentActor)
			{
				AgentActor agentActor = partner as AgentActor;
				agentActor.LoadActionFlag(num2, num4);
			}
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[num2][num4];
			HScene.AnimationListInfo info = Singleton<Manager.Resources>.Instance.HSceneTable.lstAnimInfo[4][dateActionPointInfo2.poseIDA];
			agent.Animation.BeginIgnoreEvent();
			partner.Animation.BeginIgnoreEvent();
			AssetBundleInfo assetBundleInfo = playState.MainStateInfo.AssetBundleInfo;
			RuntimeAnimatorController animatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo.assetbundle, assetBundleInfo.asset, string.Empty);
			agent.Animation.SetAnimatorController(animatorController);
			AssetBundleInfo assetBundleInfo2 = playState2.MainStateInfo.AssetBundleInfo;
			RuntimeAnimatorController animatorController2 = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo2.assetbundle, assetBundleInfo2.asset, string.Empty);
			partner.Animation.SetAnimatorController(animatorController2);
			agent.StartLesbianSequence(partner, info);
			agent.CurrentPoint.SetSlot(agent);
			agent.SetStand(basePointA, false, 0f, 0);
			partner.SetStand(basePointB, false, 0f, 0);
			Observable.EveryLateUpdate().Skip(1).Take(5).Subscribe(delegate(long _)
			{
				agent.SetStand(basePointA, false, 0f, 0);
				partner.SetStand(basePointB, false, 0f, 0);
			});
			agent.UpdateMotivation = true;
			this._isReleased = false;
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x002D3538 File Offset: 0x002D1938
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.Partner == null)
			{
				return TaskStatus.Failure;
			}
			if (base.Agent.LivesLesbianHSequence)
			{
				return TaskStatus.Running;
			}
			this.OnComplete2();
			return TaskStatus.Success;
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x002D3578 File Offset: 0x002D1978
		private void OnComplete()
		{
			AgentActor agent = base.Agent;
			Actor partner = agent.Partner;
			int desireKey = Desire.GetDesireKey(Desire.Type.H);
			agent.SetDesire(desireKey, 0f);
			if (partner is AgentActor)
			{
				(partner as AgentActor).SetDesire(desireKey, 0f);
			}
			agent.Animation.CrossFadeScreen(-1f);
			agent.SetStand(agent.Animation.RecoveryPoint, false, 0f, 0);
			agent.Animation.RecoveryPoint = null;
			partner.SetStand(partner.Animation.RecoveryPoint, false, 0f, 0);
			partner.Animation.RecoveryPoint = null;
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			if (partner is AgentActor)
			{
				(partner as AgentActor).UpdateStatus(partner.ActionID, partner.PoseID);
			}
			agent.Partner = null;
			partner.Partner = null;
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			partner.ActivateNavMeshAgent();
			partner.SetActiveOnEquipedItem(true);
			if (partner is AgentActor)
			{
				Dictionary<int, int> friendlyRelationShipTable = base.Agent.AgentData.FriendlyRelationShipTable;
				int num;
				if (!friendlyRelationShipTable.TryGetValue(partner.ID, out num))
				{
					num = 50;
				}
				friendlyRelationShipTable[partner.ID] = Mathf.Clamp(num + 2, 0, 100);
				agent.ApplySituationResultParameter(24);
				(partner as AgentActor).ChangeBehavior(Desire.ActionType.Normal);
			}
			else if (partner is MerchantActor)
			{
				int num2;
				if (!base.Agent.AgentData.FriendlyRelationShipTable.TryGetValue(partner.ID, out num2))
				{
					num2 = 50;
				}
				Dictionary<Merchant.ActionType, int> resultAddFriendlyRelationShipTable = Singleton<Manager.Resources>.Instance.MerchantProfile.ResultAddFriendlyRelationShipTable;
				int num3;
				if (!resultAddFriendlyRelationShipTable.TryGetValue(Merchant.ActionType.HWithAgent, out num3))
				{
					num3 = 0;
				}
				base.Agent.AgentData.FriendlyRelationShipTable[partner.ID] = Mathf.Clamp(num2 + num3, 0, 100);
				base.Agent.ApplySituationResultParameter(26);
				MerchantActor merchantActor = partner as MerchantActor;
				merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				if (agent == merchantActor.CommandPartner)
				{
					merchantActor.CommandPartner = null;
				}
			}
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.TargetInSightActor = null;
			agent.EventKey = (EventType)0;
			agent.Animation.EndIgnoreEvent();
			partner.Animation.EndIgnoreEvent();
			agent.Animation.ResetDefaultAnimatorController();
			partner.Animation.ResetDefaultAnimatorController();
			agent.ResetActionFlag();
			if (partner is AgentActor)
			{
				AgentActor agentActor = partner as AgentActor;
				agentActor.ResetActionFlag();
			}
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x002D3828 File Offset: 0x002D1C28
		private void OnComplete2()
		{
			AgentActor agent = base.Agent;
			Actor partner = agent.Partner;
			int desireKey = Desire.GetDesireKey(Desire.Type.H);
			agent.SetDesire(desireKey, 0f);
			if (partner is AgentActor)
			{
				(partner as AgentActor).SetDesire(desireKey, 0f);
			}
			agent.UpdateStatus(agent.ActionID, agent.PoseID);
			if (partner is AgentActor)
			{
				(partner as AgentActor).UpdateStatus(partner.ActionID, partner.PoseID);
			}
			if (partner is AgentActor)
			{
				Dictionary<int, int> friendlyRelationShipTable = base.Agent.AgentData.FriendlyRelationShipTable;
				int num;
				if (!friendlyRelationShipTable.TryGetValue(partner.ID, out num))
				{
					num = 50;
				}
				friendlyRelationShipTable[partner.ID] = Mathf.Clamp(num + 2, 0, 100);
				agent.ApplySituationResultParameter(24);
				(partner as AgentActor).ChangeBehavior(Desire.ActionType.Normal);
			}
			else if (partner is MerchantActor)
			{
				int num2;
				if (!base.Agent.AgentData.FriendlyRelationShipTable.TryGetValue(partner.ID, out num2))
				{
					num2 = 50;
				}
				Dictionary<Merchant.ActionType, int> resultAddFriendlyRelationShipTable = Singleton<Manager.Resources>.Instance.MerchantProfile.ResultAddFriendlyRelationShipTable;
				int num3;
				if (!resultAddFriendlyRelationShipTable.TryGetValue(Merchant.ActionType.HWithAgent, out num3))
				{
					num3 = 0;
				}
				base.Agent.AgentData.FriendlyRelationShipTable[partner.ID] = Mathf.Clamp(num2 + num3, 0, 100);
				base.Agent.ApplySituationResultParameter(26);
				MerchantActor merchantActor = partner as MerchantActor;
				merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				if (agent == merchantActor.CommandPartner)
				{
					merchantActor.CommandPartner = null;
				}
			}
			this.OnReleaseProcessing();
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x002D39D4 File Offset: 0x002D1DD4
		private void OnReleaseProcessing()
		{
			if (this._isReleased)
			{
				return;
			}
			this._isReleased = true;
			AgentActor agent = base.Agent;
			Actor partner = agent.Partner;
			agent.Animation.CrossFadeScreen(-1f);
			agent.SetStand(agent.Animation.RecoveryPoint, false, 0f, 0);
			agent.Animation.RecoveryPoint = null;
			if (partner != null)
			{
				partner.SetStand(partner.Animation.RecoveryPoint, false, 0f, 0);
				partner.Animation.RecoveryPoint = null;
			}
			agent.Partner = null;
			if (partner != null)
			{
				partner.Partner = null;
			}
			agent.ActivateNavMeshAgent();
			agent.SetActiveOnEquipedItem(true);
			if (partner != null)
			{
				partner.ActivateNavMeshAgent();
				partner.SetActiveOnEquipedItem(true);
			}
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			agent.TargetInSightActor = null;
			agent.EventKey = (EventType)0;
			agent.Animation.EndIgnoreEvent();
			if (partner != null)
			{
				partner.Animation.EndIgnoreEvent();
			}
			agent.Animation.ResetDefaultAnimatorController();
			partner.Animation.ResetDefaultAnimatorController();
			agent.ResetActionFlag();
			if (partner is AgentActor)
			{
				AgentActor agentActor = partner as AgentActor;
				agentActor.ResetActionFlag();
			}
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x002D3B3C File Offset: 0x002D1F3C
		public override void OnEnd()
		{
			AgentActor agent = base.Agent;
			agent.ClearItems();
			agent.ClearParticles();
			agent.StopLesbianSequence();
			agent.UpdateMotivation = false;
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x002D3B6C File Offset: 0x002D1F6C
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

		// Token: 0x040059E3 RID: 23011
		private bool _isReleased = true;

		// Token: 0x040059E4 RID: 23012
		private bool _updatedMotivation;
	}
}
