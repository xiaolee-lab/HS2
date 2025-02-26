using System;
using System.Collections.Generic;
using AIProject.Definitions;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E02 RID: 3586
	public class Lie : PlayerStateBase
	{
		// Token: 0x06006EF5 RID: 28405 RVA: 0x002F76E4 File Offset: 0x002F5AE4
		protected override void OnAwake(PlayerActor player)
		{
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Sprite actionIcon;
			instance.itemIconTables.ActionIconTable.TryGetValue(instance.PlayerProfile.CommonActionIconID, out actionIcon);
			this.ChangeModeRelatedSleep(player);
			MapUIContainer.CommandList.OnCompletedStopAsObservable().Take(1).Subscribe(delegate(Unit _)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.CancelAcception);
				MapUIContainer.CommandLabel.CancelCommand = new CommandLabel.CommandInfo
				{
					Text = "もどる",
					Icon = actionIcon,
					TargetSpriteInfo = null,
					Transform = null,
					Event = delegate
					{
						MapUIContainer.CommandLabel.CancelCommand = null;
						player.Controller.ChangeState("Sleep");
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
					}
				};
			});
		}

		// Token: 0x06006EF6 RID: 28406 RVA: 0x002F7778 File Offset: 0x002F5B78
		private void ChangeModeRelatedSleep(PlayerActor player)
		{
			PoseKeyPair sleepTogetherRight = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SleepTogetherRight;
			PoseKeyPair sleepTogetherLeft = Singleton<Manager.Resources>.Instance.AgentProfile.PoseIDTable.SleepTogetherLeft;
			List<ActionPoint> groupActionPoints = player.CurrentPoint.GroupActionPoints;
			ActionPoint actionPoint = null;
			foreach (ActionPoint actionPoint2 in groupActionPoints)
			{
				if (actionPoint2.IsNeutralCommand)
				{
					actionPoint = actionPoint2;
					break;
				}
			}
			bool flag = false;
			ActionPointInfo actionPointInfo = default(ActionPointInfo);
			if (actionPoint != null)
			{
				flag = (actionPoint.FindAgentActionPointInfo(EventType.Sleep, sleepTogetherRight.poseID, out actionPointInfo) || actionPoint.FindAgentActionPointInfo(EventType.Sleep, sleepTogetherLeft.poseID, out actionPointInfo));
			}
			List<AgentActor> list = ListPool<AgentActor>.Get();
			List<AgentActor> list2 = ListPool<AgentActor>.Get();
			foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Manager.Map>.Instance.AgentTable)
			{
				if (keyValuePair.Value.Mode != Desire.ActionType.Cold2A && keyValuePair.Value.Mode != Desire.ActionType.Cold2B && keyValuePair.Value.Mode != Desire.ActionType.Cold3A && keyValuePair.Value.Mode != Desire.ActionType.Cold3B && keyValuePair.Value.Mode != Desire.ActionType.OverworkA && keyValuePair.Value.Mode != Desire.ActionType.OverworkB && keyValuePair.Value.Mode != Desire.ActionType.Cold2BMedicated && keyValuePair.Value.Mode != Desire.ActionType.Cold3BMedicated && keyValuePair.Value.Mode != Desire.ActionType.WeaknessA && keyValuePair.Value.Mode != Desire.ActionType.WeaknessB && keyValuePair.Value.Mode != Desire.ActionType.FoundPeeping)
				{
					if (keyValuePair.Value.EventKey != EventType.Sleep && keyValuePair.Value.EventKey != EventType.Toilet && keyValuePair.Value.EventKey != EventType.Bath && keyValuePair.Value.EventKey != EventType.Move && keyValuePair.Value.EventKey != EventType.Masturbation && keyValuePair.Value.EventKey != EventType.Lesbian)
					{
						MapArea mapArea = keyValuePair.Value.MapArea;
						int? num = (mapArea != null) ? new int?(mapArea.ChunkID) : null;
						int num2 = (num == null) ? keyValuePair.Value.AgentData.ChunkID : num.Value;
						if (player.ChunkID == num2)
						{
							if (Game.isAdd01 && keyValuePair.Value.ChaControl.fileGameInfo.hSkill.ContainsValue(13))
							{
								list.Add(keyValuePair.Value);
							}
							else if (flag && keyValuePair.Value.ChaControl.fileGameInfo.flavorState[1] >= Singleton<Manager.Resources>.Instance.StatusProfile.SoineReliabilityBorder && (keyValuePair.Value.Mode == Desire.ActionType.Normal || keyValuePair.Value.Mode == Desire.ActionType.SearchSleep || keyValuePair.Value.Mode == Desire.ActionType.Encounter))
							{
								list2.Add(keyValuePair.Value);
							}
						}
					}
				}
			}
			AgentActor element = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			ListPool<AgentActor>.Release(list);
			AgentActor element2 = list2.GetElement(UnityEngine.Random.Range(0, list2.Count));
			ListPool<AgentActor>.Release(list2);
			if (element != null)
			{
				element.Animation.ResetDefaultAnimatorController();
				element.TargetInSightActionPoint = null;
				element.TargetInSightActor = player;
				this.ChangeForceBehavior(element, Desire.ActionType.ChaseYobai);
				return;
			}
			if (element2 != null)
			{
				element2.Animation.ResetDefaultAnimatorController();
				actionPoint.Reserver = element2;
				element2.TargetInSightActionPoint = actionPoint;
				element2.TargetInSightActor = null;
				this.ChangeForceBehavior(element2, Desire.ActionType.ComeSleepTogether);
				return;
			}
		}

		// Token: 0x06006EF7 RID: 28407 RVA: 0x002F7BF0 File Offset: 0x002F5FF0
		private void ChangeForceBehavior(AgentActor agent, Desire.ActionType actionType)
		{
			int num = -1;
			agent.PoseID = num;
			agent.ActionID = num;
			agent.AgentData.CarryingItem = null;
			agent.StateType = State.Type.Normal;
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
			}
			if (agent.CommandPartner != null)
			{
				Actor commandPartner = agent.CommandPartner;
				if (commandPartner is AgentActor)
				{
					AgentActor agentActor = commandPartner as AgentActor;
					agentActor.CommandPartner = null;
					agentActor.ChangeBehavior(Desire.ActionType.Normal);
				}
				else if (commandPartner is MerchantActor)
				{
					MerchantActor merchantActor = commandPartner as MerchantActor;
					merchantActor.CommandPartner = null;
					merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
				}
				agent.CommandPartner = null;
			}
			agent.EventKey = (EventType)0;
			agent.CommandPartner = null;
			agent.ResetActionFlag();
			if (agent.Schedule.enabled)
			{
				Actor.BehaviorSchedule schedule = agent.Schedule;
				schedule.enabled = false;
				agent.Schedule = schedule;
			}
			agent.ClearItems();
			agent.ClearParticles();
			agent.ActivateNavMeshAgent();
			agent.ActivateTransfer(true);
			agent.Animation.ResetDefaultAnimatorController();
			agent.ChangeBehavior(actionType);
		}

		// Token: 0x06006EF8 RID: 28408 RVA: 0x002F7D24 File Offset: 0x002F6124
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x002F7D32 File Offset: 0x002F6132
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006EFA RID: 28410 RVA: 0x002F7D34 File Offset: 0x002F6134
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006EFB RID: 28411 RVA: 0x002F7D5A File Offset: 0x002F615A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}
	}
}
