using System;
using System.Collections.Generic;
using AIChara;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D0A RID: 3338
	[TaskCategory("")]
	public class RequestLesbianH : AgentAction
	{
		// Token: 0x06006B15 RID: 27413 RVA: 0x002DC03C File Offset: 0x002DA43C
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			if (agent.TargetInSightActor != null)
			{
				if (agent.TargetInSightActor is AgentActor)
				{
					AgentActor agentActor = agent.TargetInSightActor as AgentActor;
					if (agentActor.CommandPartner != null && agentActor.CommandPartner != agent)
					{
						return;
					}
				}
				else if (agent.TargetInSightActor is MerchantActor)
				{
					MerchantActor merchantActor = agent.TargetInSightActor as MerchantActor;
					if (merchantActor.CommandPartner != null && merchantActor.CommandPartner != agent)
					{
						return;
					}
				}
			}
			Actor targetInSightActor = agent.TargetInSightActor;
			agent.CommandPartner = targetInSightActor;
			Actor actor = this._partner = targetInSightActor;
			if (actor == null)
			{
				return;
			}
			agent.SetActiveOnEquipedItem(false);
			actor.SetActiveOnEquipedItem(false);
			agent.StopNavMeshAgent();
			actor.StopNavMeshAgent();
			agent.ChangeStaticNavMeshAgentAvoidance();
			actor.ChangeStaticNavMeshAgentAvoidance();
			agent.DisableActionFlag();
			if (actor is AgentActor)
			{
				(actor as AgentActor).DisableActionFlag();
			}
			agent.RuntimeDesire = Desire.Type.H;
			int listenID = 0;
			Dictionary<int, int> dictionary;
			if (actor is AgentActor)
			{
				AgentActor agentActor2 = actor as AgentActor;
				int id = agent.ID;
				if (!agentActor2.AgentData.FriendlyRelationShipTable.TryGetValue(id, out this._relationShip))
				{
					int defaultRelationShip = Singleton<Manager.Resources>.Instance.AgentProfile.DefaultRelationShip;
					agentActor2.AgentData.FriendlyRelationShipTable[id] = defaultRelationShip;
					this._relationShip = defaultRelationShip;
				}
				dictionary = Singleton<Manager.Resources>.Instance.Animation.TalkListenerRelationTable;
				agentActor2.CommandPartner = agent;
			}
			else
			{
				int id2 = actor.ID;
				if (!agent.AgentData.FriendlyRelationShipTable.TryGetValue(id2, out this._relationShip))
				{
					int defaultRelationShip = Singleton<Manager.Resources>.Instance.AgentProfile.DefaultRelationShip;
					agent.AgentData.FriendlyRelationShipTable[id2] = defaultRelationShip;
					this._relationShip = defaultRelationShip;
				}
				dictionary = Singleton<Manager.Resources>.Instance.Animation.MerchantListenerRelationTable;
				(this._partner as MerchantActor).CommandPartner = agent;
			}
			List<KeyValuePair<int, int>> list = ListPool<KeyValuePair<int, int>>.Get();
			foreach (KeyValuePair<int, int> item in dictionary)
			{
				list.Add(item);
			}
			list.Sort((KeyValuePair<int, int> v1, KeyValuePair<int, int> v2) => v1.Value - v2.Value);
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<int, int> keyValuePair = list[i];
				if (this._relationShip <= keyValuePair.Value)
				{
					listenID = keyValuePair.Key;
					break;
				}
			}
			ListPool<KeyValuePair<int, int>>.Release(list);
			agent.StartTalkSequence(actor, agent.ChaControl.fileParam.personality, listenID);
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x002DC344 File Offset: 0x002DA744
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (this._partner == null)
			{
				agent.AbortTalkSeq(Desire.Type.H);
				return TaskStatus.Failure;
			}
			if (agent.LivesTalkSequence)
			{
				return TaskStatus.Running;
			}
			ChaFileGameInfo fileGameInfo = agent.ChaControl.fileGameInfo;
			int num = fileGameInfo.flavorState[4];
			int num2 = fileGameInfo.flavorState[7];
			int desireKey = Desire.GetDesireKey(Desire.Type.H);
			float? desire = agent.GetDesire(desireKey);
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			bool flag = num >= statusProfile.LesbianBorder && num2 >= statusProfile.GirlsActionBorder && desire >= statusProfile.LesbianBorderDesire;
			Actor partner = this._partner;
			if (flag)
			{
				agent.Partner = partner;
				partner.Partner = agent;
				agent.ChangeBehavior(Desire.ActionType.GotoLesbianSpot);
				if (partner is AgentActor)
				{
					AgentActor agentActor = partner as AgentActor;
					Desire.ActionType mode = Desire.ActionType.GotoLesbianSpotFollow;
					agentActor.Mode = mode;
					agentActor.BehaviorResources.ChangeMode(mode);
				}
				else if (partner is MerchantActor)
				{
					(partner as MerchantActor).ChangeBehavior(Merchant.ActionType.GotoLesbianSpotFollow);
				}
			}
			else
			{
				float value = UnityEngine.Random.value;
				if (value < 0.5f)
				{
					agent.ChangeBehavior(Desire.ActionType.SearchMasturbation);
				}
				else
				{
					agent.SetDesire(desireKey, 0f);
					agent.ChangeBehavior(Desire.ActionType.Normal);
				}
				if (partner is AgentActor)
				{
					(partner as AgentActor).ChangeBehavior(Desire.ActionType.Normal);
				}
				else if (partner is MerchantActor)
				{
					MerchantActor merchantActor = partner as MerchantActor;
					merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				}
			}
			agent.CommandPartner = null;
			agent.TargetInSightActor = null;
			if (partner is AgentActor)
			{
				(partner as AgentActor).CommandPartner = null;
				(partner as AgentActor).AgentData.SetAppendEventFlagCheck(7, true);
			}
			else if (partner is MerchantActor)
			{
				(partner as MerchantActor).CommandPartner = null;
			}
			agent.AgentData.SetAppendEventFlagCheck(7, true);
			return TaskStatus.Success;
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x002DC563 File Offset: 0x002DA963
		public override void OnEnd()
		{
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
			if (this._partner != null)
			{
				this._partner.ChangeDynamicNavMeshAgentAvoidance();
			}
			base.Agent.StopTalkSequence();
			this._partner = null;
		}

		// Token: 0x04005A56 RID: 23126
		private Actor _partner;

		// Token: 0x04005A57 RID: 23127
		private int _relationShip = 50;
	}
}
