using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DDE RID: 3550
	public class DateEat : PlayerStateBase
	{
		// Token: 0x06006DCB RID: 28107 RVA: 0x002EC854 File Offset: 0x002EAC54
		protected override void OnAwake(PlayerActor player)
		{
			DateEat.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey = new DateEat.<OnAwake>c__AnonStorey1();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.$this = this;
			<OnAwake>c__AnonStorey.player.EventKey = EventType.Eat;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			Observable.Timer(TimeSpan.FromSeconds(1.0)).Subscribe(delegate(long _)
			{
				MapUIContainer.RefreshCommands(0, <OnAwake>c__AnonStorey.player.DateEatCommandInfos);
				MapUIContainer.SetActiveCommandList(true, "食事");
				MapUIContainer.CommandList.CancelEvent = null;
			});
			<OnAwake>c__AnonStorey.type = AIProject.Definitions.Action.NameTable[EventType.Eat].Item1;
			DateActionPointInfo dateActionPointInfo;
			<OnAwake>c__AnonStorey.player.CurrentPoint.TryGetPlayerDateActionPointInfo(<OnAwake>c__AnonStorey.player.ChaControl.sex, EventType.Eat, out dateActionPointInfo);
			<OnAwake>c__AnonStorey.partner = (<OnAwake>c__AnonStorey.player.Partner as AgentActor);
			int num = dateActionPointInfo.poseIDA;
			<OnAwake>c__AnonStorey.player.PoseID = num;
			int key = num;
			DateEat.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
			num = dateActionPointInfo.poseIDB;
			<OnAwake>c__AnonStorey.partner.PoseID = num;
			<OnAwake>c__AnonStorey2.poseIDB = num;
			GameObject gameObject = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
			GameObject gameObject2 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameB);
			Transform t2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
			GameObject gameObject3 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameA);
			<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameB);
			<OnAwake>c__AnonStorey.player.Partner.Animation.RecoveryPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][<OnAwake>c__AnonStorey.type][key];
			<OnAwake>c__AnonStorey.player.Animation.LoadEventKeyTable(<OnAwake>c__AnonStorey.type, dateActionPointInfo.poseIDA);
			<OnAwake>c__AnonStorey.player.LoadEventItems(playState);
			<OnAwake>c__AnonStorey.player.LoadEventParticles(<OnAwake>c__AnonStorey.type, dateActionPointInfo.poseIDA);
			<OnAwake>c__AnonStorey.player.Animation.InitializeStates(playState);
			<OnAwake>c__AnonStorey.partner.Animation.LoadEventKeyTable(<OnAwake>c__AnonStorey.type, dateActionPointInfo.poseIDB);
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[<OnAwake>c__AnonStorey.type][dateActionPointInfo.poseIDB];
			<OnAwake>c__AnonStorey.partner.LoadEventItems(playState2);
			<OnAwake>c__AnonStorey.partner.LoadEventParticles(<OnAwake>c__AnonStorey.type, dateActionPointInfo.poseIDB);
			<OnAwake>c__AnonStorey.partner.Animation.InitializeStates(playState2);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate
			};
			<OnAwake>c__AnonStorey.player.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			ActorAnimInfo actorAnimInfo3 = new ActorAnimInfo
			{
				layer = playState2.Layer,
				inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState2.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState2.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate,
				loopMinTime = playState2.MainStateInfo.LoopMin,
				loopMaxTime = playState2.MainStateInfo.LoopMax,
				hasAction = playState2.ActionInfo.hasAction
			};
			<OnAwake>c__AnonStorey.partner.Animation.AnimInfo = actorAnimInfo3;
			ActorAnimInfo actorAnimInfo4 = actorAnimInfo3;
			List<int> list = ListPool<int>.Get();
			foreach (KeyValuePair<int, Dictionary<int, int>> keyValuePair in Singleton<Manager.Resources>.Instance.Map.FoodDateEventItemList)
			{
				foreach (KeyValuePair<int, int> keyValuePair2 in keyValuePair.Value)
				{
					if (keyValuePair2.Value != -1)
					{
						list.Add(keyValuePair2.Value);
					}
				}
			}
			int num2 = -1;
			if (!list.IsNullOrEmpty<int>())
			{
				num2 = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			}
			ListPool<int>.Release(list);
			ActionItemInfo eventItemInfo;
			if (Singleton<Manager.Resources>.Instance.Map.EventItemList.TryGetValue(num2, out eventItemInfo))
			{
				LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
				string rootParentName = locomotionProfile.RootParentName;
				GameObject gameObject5 = <OnAwake>c__AnonStorey.player.LoadEventItem(num2, rootParentName, false, eventItemInfo);
				if (gameObject5 != null)
				{
					Renderer[] componentsInChildren = gameObject5.GetComponentsInChildren<Renderer>(true);
					foreach (Renderer renderer in componentsInChildren)
					{
						renderer.enabled = true;
					}
				}
				GameObject gameObject6 = <OnAwake>c__AnonStorey.partner.LoadEventItem(num2, rootParentName, false, eventItemInfo);
				if (gameObject6 != null)
				{
					Renderer[] componentsInChildren2 = gameObject6.GetComponentsInChildren<Renderer>(true);
					foreach (Renderer renderer2 in componentsInChildren2)
					{
						renderer2.enabled = true;
					}
				}
			}
			<OnAwake>c__AnonStorey.player.DeactivateNavMeshAgent();
			<OnAwake>c__AnonStorey.player.IsKinematic = true;
			<OnAwake>c__AnonStorey.partner.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.partner.ChaControl.setAllLayerWeight(0f);
			<OnAwake>c__AnonStorey.partner.DeactivateNavMeshAgent();
			<OnAwake>c__AnonStorey.partner.IsKinematic = true;
			this._hasAction = playState.ActionInfo.hasAction;
			if (this._hasAction)
			{
				this._loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				this._randomCount = playState.ActionInfo.randomCount;
				this._oldNormalizedTime = 0f;
			}
			<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			<OnAwake>c__AnonStorey.player.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			<OnAwake>c__AnonStorey.partner.Animation.PlayInAnimation(actorAnimInfo4.inEnableBlend, actorAnimInfo4.inBlendSec, playState2.MainStateInfo.FadeOutTime, actorAnimInfo4.layer);
			<OnAwake>c__AnonStorey.partner.SetStand(t2, playState2.MainStateInfo.InStateInfo.EnableFade, playState2.MainStateInfo.InStateInfo.FadeSecond, actorAnimInfo4.layer);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				<OnAwake>c__AnonStorey.$this.ChangeCamera(<OnAwake>c__AnonStorey.type, <OnAwake>c__AnonStorey.poseIDB, <OnAwake>c__AnonStorey.partner);
			});
			<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand = <OnAwake>c__AnonStorey.player.HandsHolder.enabled;
			if (<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand)
			{
				<OnAwake>c__AnonStorey.player.HandsHolder.enabled = false;
				if (<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding)
				{
					<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding = false;
				}
			}
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x002ED14C File Offset: 0x002EB54C
		protected override void OnRelease(PlayerActor player)
		{
			Vector3 locatedPosition = player.CurrentPoint.LocatedPosition;
			locatedPosition.y = player.Locomotor.transform.position.y;
			RaycastHit raycastHit;
			Physics.Raycast(locatedPosition, Vector3.down * 10f, out raycastHit);
			locatedPosition.y = raycastHit.point.y;
			player.Locomotor.transform.position = locatedPosition;
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			player.Animation.RefsActAnimInfo = true;
			player.ClearItems();
			player.ClearParticles();
			Actor partner = player.Partner;
			if (partner != null)
			{
				partner.ClearItems();
			}
			Actor partner2 = player.Partner;
			if (partner2 != null)
			{
				partner2.ClearParticles();
			}
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x002ED238 File Offset: 0x002EB638
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x002ED25E File Offset: 0x002EB65E
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x002ED270 File Offset: 0x002EB670
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x002ED284 File Offset: 0x002EB684
		private void ChangeCamera(int actionID, int poseID, Actor actor)
		{
			Dictionary<int, ActAnimFlagData> dictionary;
			ActAnimFlagData actAnimFlagData;
			if (Singleton<Manager.Resources>.Instance.Action.AgentActionFlagTable.TryGetValue(actionID, out dictionary) && dictionary.TryGetValue(poseID, out actAnimFlagData))
			{
				int attitudeID = actAnimFlagData.attitudeID;
				if (attitudeID != 0)
				{
					if (attitudeID != 1)
					{
						ADV.ChangeADVFixedAngleCamera(actor, actAnimFlagData.attitudeID);
					}
					else
					{
						ADV.ChangeADVCameraDiagonal(actor);
					}
				}
				else
				{
					ADV.ChangeADVCamera(actor);
				}
			}
		}
	}
}
