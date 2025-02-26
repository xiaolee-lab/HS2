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
	// Token: 0x02000E0E RID: 3598
	public class Search : PlayerStateBase
	{
		// Token: 0x06006F59 RID: 28505 RVA: 0x002FC82C File Offset: 0x002FAC2C
		protected override void OnAwake(PlayerActor player)
		{
			Search.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey = new Search.<OnAwake>c__AnonStorey1();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.player.EventKey = EventType.Search;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			UnityEx.ValueTuple<int, string> valueTuple;
			AIProject.Definitions.Action.NameTable.TryGetValue(EventType.Search, out valueTuple);
			int item = valueTuple.Item1;
			ActionPointInfo playerActionPointInfo;
			if (<OnAwake>c__AnonStorey.player.CurrentPoint is TutorialSearchActionPoint)
			{
				playerActionPointInfo = (<OnAwake>c__AnonStorey.player.CurrentPoint as TutorialSearchActionPoint).GetPlayerActionPointInfo();
			}
			else
			{
				<OnAwake>c__AnonStorey.player.CurrentPoint.TryGetPlayerActionPointInfo(EventType.Search, out playerActionPointInfo);
			}
			int poseID = playerActionPointInfo.poseID;
			<OnAwake>c__AnonStorey.player.PoseID = poseID;
			int num = poseID;
			GameObject gameObject = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(playerActionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
			GameObject gameObject2 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(playerActionPointInfo.recoveryNullName);
			<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][item][num];
			<OnAwake>c__AnonStorey.player.Animation.LoadEventKeyTable(item, playerActionPointInfo.poseID);
			<OnAwake>c__AnonStorey.player.LoadEventItems(playState);
			<OnAwake>c__AnonStorey.player.LoadEventParticles(item, playerActionPointInfo.poseID);
			<OnAwake>c__AnonStorey.player.Animation.InitializeStates(playState.MainStateInfo.InStateInfo.StateInfos, playState.MainStateInfo.OutStateInfo.StateInfos, playState.MainStateInfo.AssetBundleInfo);
			Search.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
			ActorAnimInfo animInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				inFadeOutTime = playState.MainStateInfo.FadeOutTime,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate
			};
			<OnAwake>c__AnonStorey.player.Animation.AnimInfo = animInfo;
			<OnAwake>c__AnonStorey2.animInfo = animInfo;
			<OnAwake>c__AnonStorey.player.DeactivateNavMeshAgent();
			<OnAwake>c__AnonStorey.player.IsKinematic = true;
			<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, <OnAwake>c__AnonStorey.animInfo.inFadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnAwake>c__AnonStorey.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey.animInfo.outEnableBlend, <OnAwake>c__AnonStorey.animInfo.outBlendSec, <OnAwake>c__AnonStorey.animInfo.layer);
			});
			<OnAwake>c__AnonStorey.player.SetStand(t, <OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, <OnAwake>c__AnonStorey.animInfo.directionType);
			<OnAwake>c__AnonStorey.player.CameraControl.Mode = CameraMode.ActionFreeLook;
			<OnAwake>c__AnonStorey.player.CameraControl.SetShotTypeForce(ShotType.Near);
			<OnAwake>c__AnonStorey.player.CameraControl.LoadActionCameraFile(item, num, null);
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x002FCBC4 File Offset: 0x002FAFC4
		private void Elapsed(PlayerActor player)
		{
			ActionPoint currentPoint = player.CurrentPoint;
			Type type = currentPoint.GetType();
			if (Singleton<Game>.IsInstance() && type == typeof(SearchActionPoint))
			{
				Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
				AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
				if (!searchActionLockTable.TryGetValue(currentPoint.RegisterID, out searchActionInfo))
				{
					searchActionInfo = new AIProject.SaveData.Environment.SearchActionInfo();
				}
				searchActionInfo.Count++;
				searchActionLockTable[currentPoint.RegisterID] = searchActionInfo;
			}
			Actor.SearchInfo searchInfo = new Actor.SearchInfo
			{
				IsSuccess = false
			};
			if (currentPoint is SearchActionPoint)
			{
				SearchActionPoint searchActionPoint = currentPoint as SearchActionPoint;
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				int pointID;
				if (!searchActionPoint.IDList.IsNullOrEmpty<int>())
				{
					pointID = searchActionPoint.IDList.GetElement(0);
				}
				else
				{
					pointID = searchActionPoint.ID;
				}
				Dictionary<int, ItemTableElement> itemTableInArea = instance.GameInfo.GetItemTableInArea(pointID);
				searchInfo = player.RandomAddItem(itemTableInArea, true);
			}
			else if (currentPoint is TutorialSearchActionPoint)
			{
				TutorialSearchActionPoint tutorialSearchActionPoint = currentPoint as TutorialSearchActionPoint;
				searchInfo = tutorialSearchActionPoint.GetSearchInfo();
			}
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
					StuffItem stuffItem = new StuffItem(itemSearchInfo.categoryID, itemSearchInfo.id, itemSearchInfo.count);
					if (player.PlayerData.ItemList.Count < player.PlayerData.InventorySlotMax)
					{
						player.PlayerData.ItemList.AddItem(stuffItem, stuffItem.Count, player.PlayerData.InventorySlotMax);
					}
					StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(itemSearchInfo.categoryID, itemSearchInfo.id);
					MapUIContainer.AddSystemItemLog(item, itemSearchInfo.count, true);
				}
				player.Controller.ChangeState("Normal");
			}
			else
			{
				MapUIContainer.AddNotify(MapUIContainer.ItemGetEmptyText);
				player.Controller.ChangeState("Normal");
			}
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x002FCE6C File Offset: 0x002FB26C
		protected override void OnRelease(PlayerActor player)
		{
			player.ClearItems();
			player.ClearParticles();
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			player.Animation.RefsActAnimInfo = true;
		}

		// Token: 0x06006F5C RID: 28508 RVA: 0x002FCEC4 File Offset: 0x002FB2C4
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

		// Token: 0x06006F5D RID: 28509 RVA: 0x002FCF30 File Offset: 0x002FB330
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x002FCF3F File Offset: 0x002FB33F
		protected override void OnAnimatorStateEnterInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006F5F RID: 28511 RVA: 0x002FCF41 File Offset: 0x002FB341
		protected override void OnAnimatorStateExitInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006F60 RID: 28512 RVA: 0x002FCF44 File Offset: 0x002FB344
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x04005C0D RID: 23565
		private Subject<Unit> _onEndAction = new Subject<Unit>();
	}
}
