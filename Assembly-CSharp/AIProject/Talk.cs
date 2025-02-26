using System;
using System.Collections.Generic;
using AIChara;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CE8 RID: 3304
	[TaskCategory("")]
	public class Talk : AgentAction
	{
		// Token: 0x06006A93 RID: 27283 RVA: 0x002D6CDC File Offset: 0x002D50DC
		public override void OnStart()
		{
			base.OnStart();
			AgentActor agent = base.Agent;
			if (agent.CommandPartner != null)
			{
				if (agent.CommandPartner is AgentActor)
				{
					AgentActor agentActor = agent.CommandPartner as AgentActor;
					if (agentActor.CommandPartner != null && agentActor.CommandPartner != agent)
					{
						return;
					}
				}
				else if (agent.CommandPartner is MerchantActor)
				{
					MerchantActor merchantActor = agent.CommandPartner as MerchantActor;
					if (merchantActor.CommandPartner != null && merchantActor.CommandPartner != agent)
					{
						return;
					}
				}
			}
			Actor actor = this._partner = agent.CommandPartner;
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
			agent.RuntimeDesire = Desire.Type.Lonely;
			int listenID = 0;
			Dictionary<int, int> dictionary;
			if (actor is AgentActor)
			{
				AgentActor agentActor2 = this._partner as AgentActor;
				int id = base.Agent.ID;
				if (!agentActor2.AgentData.FriendlyRelationShipTable.TryGetValue(id, out this._relationShip))
				{
					int num = 50;
					agentActor2.AgentData.FriendlyRelationShipTable[id] = num;
					this._relationShip = num;
				}
				dictionary = Singleton<Manager.Resources>.Instance.Animation.TalkListenerRelationTable;
				agentActor2.CommandPartner = agent;
			}
			else
			{
				int id2 = actor.ID;
				if (!agent.AgentData.FriendlyRelationShipTable.TryGetValue(id2, out this._relationShip))
				{
					int num = 50;
					agent.AgentData.FriendlyRelationShipTable[id2] = num;
					this._relationShip = num;
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

		// Token: 0x06006A94 RID: 27284 RVA: 0x002D6FC8 File Offset: 0x002D53C8
		public override TaskStatus OnUpdate()
		{
			if (this._partner == null)
			{
				base.Agent.AbortTalkSeq(Desire.Type.Lonely);
				return TaskStatus.Failure;
			}
			if (base.Agent.LivesTalkSequence)
			{
				return TaskStatus.Running;
			}
			this.OnComplete();
			return TaskStatus.Success;
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x002D7006 File Offset: 0x002D5406
		public override void OnEnd()
		{
			base.Agent.StopTalkSequence();
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
			if (this._partner != null)
			{
				this._partner.ChangeDynamicNavMeshAgentAvoidance();
			}
			this._partner = null;
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x002D7044 File Offset: 0x002D5444
		private void OnComplete()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(agent.RuntimeDesire);
			if (desireKey != -1)
			{
				agent.SetDesire(desireKey, 0f);
			}
			agent.RuntimeDesire = Desire.Type.None;
			if (agent.CommandPartner is AgentActor)
			{
				int value = this._relationShip + UnityEngine.Random.Range(-10, 10);
				agent.AgentData.FriendlyRelationShipTable[agent.CommandPartner.ID] = Mathf.Clamp(value, 0, 100);
				agent.ApplySituationResultParameter(23);
				AgentActor agentActor = agent.CommandPartner as AgentActor;
				ChaFileGameInfo fileGameInfo = agent.ChaControl.fileGameInfo;
				if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(39))
				{
					agentActor.AddStatus(3, -20f);
				}
				int num = fileGameInfo.flavorState[4];
				int num2 = fileGameInfo.flavorState[7];
				int desireKey2 = Desire.GetDesireKey(Desire.Type.H);
				float? desire = agent.GetDesire(desireKey2);
				StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
				bool flag = num >= statusProfile.LesbianBorder && num2 >= statusProfile.GirlsActionBorder && desire >= statusProfile.LesbianBorderDesire;
				int lesbianBorder = Singleton<Manager.Resources>.Instance.StatusProfile.LesbianBorder;
				if (flag)
				{
					agent.Partner = this._partner;
					agentActor.Partner = agent;
					agent.ChangeBehavior(Desire.ActionType.GotoLesbianSpot);
					agentActor.BehaviorResources.ChangeMode(Desire.ActionType.GotoLesbianSpotFollow);
				}
				else
				{
					agent.ChangeBehavior(Desire.ActionType.Normal);
					agentActor.ChangeBehavior(Desire.ActionType.Normal);
				}
				agentActor.CommandPartner = null;
				agent.AgentData.SetAppendEventFlagCheck(7, true);
				agentActor.AgentData.SetAppendEventFlagCheck(7, true);
			}
			else if (agent.CommandPartner is MerchantActor)
			{
				Dictionary<Merchant.ActionType, int> resultAddFriendlyRelationShipTable = Singleton<Manager.Resources>.Instance.MerchantProfile.ResultAddFriendlyRelationShipTable;
				int num3;
				if (!resultAddFriendlyRelationShipTable.TryGetValue(Merchant.ActionType.TalkWithAgent, out num3))
				{
					num3 = 0;
				}
				int value2 = this._relationShip + num3;
				agent.AgentData.FriendlyRelationShipTable[agent.CommandPartner.ID] = Mathf.Clamp(value2, 0, 100);
				agent.ApplySituationResultParameter(25);
				MerchantActor merchantActor = agent.CommandPartner as MerchantActor;
				StatusProfile statusProfile2 = Singleton<Manager.Resources>.Instance.StatusProfile;
				int desireKey3 = Desire.GetDesireKey(Desire.Type.H);
				float? desire2 = agent.GetDesire(desireKey3);
				ChaFileGameInfo fileGameInfo2 = agent.ChaControl.fileGameInfo;
				int num4 = fileGameInfo2.flavorState[4];
				int num5 = fileGameInfo2.flavorState[7];
				bool flag2 = statusProfile2.LesbianBorder <= num4 && statusProfile2.GirlsActionBorder <= num5 && statusProfile2.LesbianBorderDesire <= desire2;
				if (flag2)
				{
					agent.Partner = this._partner;
					this._partner.Partner = agent;
					agent.ChangeBehavior(Desire.ActionType.GotoLesbianSpot);
					merchantActor.ChangeBehavior(Merchant.ActionType.GotoLesbianSpotFollow);
				}
				else
				{
					agent.ChangeBehavior(Desire.ActionType.Normal);
					merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				}
				merchantActor.CommandPartner = null;
			}
			int desireKey4 = Desire.GetDesireKey(Desire.Type.Lonely);
			agent.SetDesire(desireKey4, 0f);
			agent.TargetInSightActor = null;
			agent.CommandPartner = null;
		}

		// Token: 0x04005A15 RID: 23061
		private int _relationShip = 50;

		// Token: 0x04005A16 RID: 23062
		private Actor _partner;
	}
}
