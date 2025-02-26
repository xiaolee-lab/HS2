using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Player
{
	// Token: 0x02000DE0 RID: 3552
	public class DateSearch : PlayerStateBase
	{
		// Token: 0x06006DD8 RID: 28120 RVA: 0x002EDB3C File Offset: 0x002EBF3C
		protected override void OnAwake(PlayerActor player)
		{
			DateSearch.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey = new DateSearch.<OnAwake>c__AnonStorey1();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.player.EventKey = EventType.Search;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			UnityEx.ValueTuple<int, string> valueTuple;
			AIProject.Definitions.Action.NameTable.TryGetValue(EventType.Search, out valueTuple);
			int item = valueTuple.Item1;
			DateActionPointInfo dateActionPointInfo;
			<OnAwake>c__AnonStorey.player.CurrentPoint.TryGetPlayerDateActionPointInfo(<OnAwake>c__AnonStorey.player.ChaControl.sex, EventType.Search, out dateActionPointInfo);
			int poseIDA = dateActionPointInfo.poseIDA;
			<OnAwake>c__AnonStorey.player.PoseID = poseIDA;
			int num = poseIDA;
			GameObject gameObject = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
			GameObject gameObject2 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameA);
			<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][item][num];
			<OnAwake>c__AnonStorey.player.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDA);
			<OnAwake>c__AnonStorey.player.LoadEventItems(playState);
			<OnAwake>c__AnonStorey.player.LoadEventParticles(item, dateActionPointInfo.poseIDA);
			<OnAwake>c__AnonStorey.player.Animation.InitializeStates(playState);
			DateSearch.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
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
			<OnAwake>c__AnonStorey.player.Partner.Animation.AnimInfo = actorAnimInfo;
			actorAnimInfo = actorAnimInfo;
			<OnAwake>c__AnonStorey.player.Animation.AnimInfo = actorAnimInfo;
			<OnAwake>c__AnonStorey2.animInfo = actorAnimInfo;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			<OnAwake>c__AnonStorey.player.DeactivateNavMeshAgent();
			<OnAwake>c__AnonStorey.player.IsKinematic = true;
			<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
			<OnAwake>c__AnonStorey.player.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnAwake>c__AnonStorey.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey.animInfo.outEnableBlend, <OnAwake>c__AnonStorey.animInfo.outBlendSec, <OnAwake>c__AnonStorey.animInfo.layer);
			});
			<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand = <OnAwake>c__AnonStorey.player.HandsHolder.enabled;
			if (<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand)
			{
				<OnAwake>c__AnonStorey.player.HandsHolder.enabled = false;
				<OnAwake>c__AnonStorey.player.HandsHolder.Weight = 0f;
				if (<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding)
				{
					<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding = false;
				}
			}
			<OnAwake>c__AnonStorey.player.CameraControl.Mode = CameraMode.ActionFreeLook;
			<OnAwake>c__AnonStorey.player.CameraControl.SetShotTypeForce(ShotType.Near);
			<OnAwake>c__AnonStorey.player.CameraControl.LoadActionCameraFile(item, num, null);
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x002EDF4C File Offset: 0x002EC34C
		protected override void OnRelease(PlayerActor player)
		{
			player.ClearItems();
			player.ClearParticles();
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			player.Animation.RefsActAnimInfo = true;
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x002EDFA4 File Offset: 0x002EC3A4
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (actor.Animation.PlayingInAnimation)
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
			if (actor.Animation.PlayingOutAnimation)
			{
				return;
			}
			this.Elapsed(actor);
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x002EE010 File Offset: 0x002EC410
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x002EE020 File Offset: 0x002EC420
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x06006DDD RID: 28125 RVA: 0x002EE034 File Offset: 0x002EC434
		private void Elapsed(PlayerActor player)
		{
			ActionPoint currentPoint = player.CurrentPoint;
			Type type = currentPoint.GetType();
			if (Singleton<Game>.IsInstance() && player.CurrentPoint.GetType() == typeof(SearchActionPoint))
			{
				Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
				AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
				if (!searchActionLockTable.TryGetValue(player.CurrentPoint.RegisterID, out searchActionInfo))
				{
					searchActionInfo = new AIProject.SaveData.Environment.SearchActionInfo();
				}
				searchActionInfo.Count++;
				searchActionLockTable[player.CurrentPoint.RegisterID] = searchActionInfo;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			int pointID;
			if (!currentPoint.IDList.IsNullOrEmpty<int>())
			{
				pointID = currentPoint.IDList.GetElement(0);
			}
			else
			{
				pointID = currentPoint.ID;
			}
			Dictionary<int, ItemTableElement> itemTableInArea = instance.GameInfo.GetItemTableInArea(pointID);
			if (itemTableInArea == null)
			{
			}
			Actor.SearchInfo searchInfo = player.RandomAddItem(itemTableInArea, true);
			if (type == typeof(OnceSearchActionPoint))
			{
				OnceSearchActionPoint onceSearchActionPoint = currentPoint as OnceSearchActionPoint;
				if (onceSearchActionPoint.HaveMapItems)
				{
					Manager.Map.FadeStart(-1f);
				}
				onceSearchActionPoint.SetAvailable(false);
			}
			else if (type == typeof(DropSearchActionPoint))
			{
				DropSearchActionPoint dropSearchActionPoint = currentPoint as DropSearchActionPoint;
				if (dropSearchActionPoint.HaveMapItems)
				{
					Manager.Map.FadeStart(-1f);
				}
				dropSearchActionPoint.SetCoolTime();
			}
			if (searchInfo.IsSuccess)
			{
				foreach (Actor.ItemSearchInfo itemSearchInfo in searchInfo.ItemList)
				{
					StuffItem item = new StuffItem(itemSearchInfo.categoryID, itemSearchInfo.id, itemSearchInfo.count);
					player.PlayerData.ItemList.AddItem(item);
					StuffItemInfo item2 = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(itemSearchInfo.categoryID, itemSearchInfo.id);
					MapUIContainer.AddSystemItemLog(item2, itemSearchInfo.count, true);
				}
				player.Controller.ChangeState("Normal");
			}
			else
			{
				MapUIContainer.AddNotify(MapUIContainer.ItemGetEmptyText);
				player.Controller.ChangeState("Normal");
			}
			AgentActor agentPartner = player.AgentPartner;
			if (agentPartner.CurrentPoint == null)
			{
				agentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Ovation);
			}
			else if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = true;
				player.OldEnabledHoldingHand = false;
			}
		}

		// Token: 0x04005B5A RID: 23386
		private Subject<Unit> _onEndAction = new Subject<Unit>();
	}
}
